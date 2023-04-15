using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ProgramListWebAPI.Models
{
    public class SEC_User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; set; } = String.Empty;
        [BsonElement("user_name")]
        public string User_Name { get; set; } = String.Empty;
        [BsonElement("user_password")]
        public string User_Password { get; set; } = String.Empty;
        [BsonElement("user_emailaddress")]
        public string User_EmailAddress { get; set; } = String.Empty;
        [BsonElement("user_mobilenumber")]
        public string User_MobileNumber { get; set; } = String.Empty;
    }
}
