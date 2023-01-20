using MongoDB.Driver;
using Ski_ServiceNoSQL.Models;

namespace Ski_ServiceNoSQL.Services
{
    public class OrdersService : IOrdersService
    {

        private readonly IMongoCollection<Orders> _orders;

        public OrdersService(ISkiDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _orders = database.GetCollection<Orders>(settings.OrdersCollectionName);
        }

        public List<Orders> Get() =>
            _orders.Find(order => true).ToList();

        public Orders Get(string id) =>
            _orders.Find<Orders>(order => order.Id == id).FirstOrDefault();

        public Orders Create(Orders order)
        {
            _orders.InsertOne(order);
            return order;
        }

        public void Update(string id, Orders orderIn) =>
            _orders.ReplaceOne(order => order.Id.ToString() == id, orderIn);

        public void Remove(Orders orderIn) =>
            _orders.DeleteOne(order => order.Id == orderIn.Id);

        public void Remove(string id) =>
            _orders.DeleteOne(order => order.Id.ToString() == id);
    }
}
