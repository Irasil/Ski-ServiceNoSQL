using Microsoft.AspNetCore.Mvc;
using Ski_ServiceNoSQL.Models;

namespace Ski_ServiceNoSQL.Services
{
    public interface IMitarbeiterService
    {
        public JsonResult? ProveUser(Mitarbeiter mitarbeiter);
        //public Mitarbeiter Deblocker(int id);
        public List<Mitarbeiter> AllMitarbeiter();
    }
}
