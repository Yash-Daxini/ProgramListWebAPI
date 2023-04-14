using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProgramListWebAPI.Models
{
    [BsonIgnoreExtraElements]
    public class MST_ProgramTopic
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; set; } = String.Empty;
        [BsonElement("topic_name")]
        public string Topic_Name { get; set; } = String.Empty;
    }
}
