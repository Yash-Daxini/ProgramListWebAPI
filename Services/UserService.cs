using MongoDB.Driver;
using ProgramListWebAPI.Models;

namespace ProgramListWebAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<SEC_User> _user;

        public UserService(IProgramListDatabaseSettings settings, IMongoClient mongoClient)
        {
            var databse = mongoClient.GetDatabase(settings.DatabaseName);
            _user = databse.GetCollection<SEC_User>("SEC_User");

        }
        public void DELETE(string id)
        {
            _user.DeleteOne(user => user.ID == id);
        }

        public List<SEC_User> GET()
        {
            return _user.Find(user => true).ToList();
        }

        public SEC_User GET(string id)
        {
            return _user.Find(user => user.ID == id).FirstOrDefault();
        }

        public SEC_User POST(SEC_User user)
        {
            _user.InsertOne(user);
            return user;
        }

        public void PUT(string id, SEC_User user)
        {
            _user.ReplaceOne(user => user.ID == id, user);
        }
    }
}
