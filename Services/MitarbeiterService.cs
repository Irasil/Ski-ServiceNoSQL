using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Ski_ServiceNoSQL.Models;

namespace Ski_ServiceNoSQL.Services
{
    public class MitarbeiterService : IMitarbeiterService
    {
        private readonly IMongoCollection<Mitarbeiter> _orders;
        public ITokenService _tokenService;

        public MitarbeiterService(ISkiDatabaseSettings settings, ITokenService tokenService)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _orders = database.GetCollection<Mitarbeiter>(settings.MitarbeiterCollectionName);
            _tokenService = tokenService;
        }

        public List<Mitarbeiter> AllMitarbeiter() =>
            _orders.Find(order => true).ToList();

        /// <summary>
        /// Mitarbeiter Autorisation,
        /// </summary>
        /// <param name="mitarbeiter"></param>
        /// <returns></returns>
        /// 
        public int counter;
        public List<Mitarbeiter> mitarbeiters;
        public JsonResult? ProveUser(Mitarbeiter mitarbeiter)
        {
            //Hier ist es ein "Gebastel" ich wusste nicht wie ich es mit dem JsonResult besser machen könnte.

            mitarbeiters = _orders.Find(order => true).ToList();
            foreach (var m in mitarbeiters)
            {
                if (m.name == mitarbeiter.name && m.password == mitarbeiter.password)
                {
                    return new JsonResult(new { userName = mitarbeiter.name, token = _tokenService.CreateToken(mitarbeiter.name) });
                }
                else if (m.name == mitarbeiter.name && m.password != mitarbeiter.password)
                {
                    m.counter += 1;
                    _orders.Find(mitarbeiter => mitarbeiter.counter == m.counter);
                    if (m.counter >= 3)
                    {
                        return new JsonResult(new { gespert = m.counter });
                    }
                }
            }
            return new JsonResult(new { falsch = "Falsch" });
        }
    }
}
