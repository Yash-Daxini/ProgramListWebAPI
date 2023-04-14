using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProgramListWebAPI.Models
{
    [BsonIgnoreExtraElements]
    public class MST_Program
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; set; } = String.Empty;
        [BsonElement("program_name")]
        public string Program_Name { get; set; } = String.Empty;
        [BsonElement("program_topic")]
        public string Program_Topic { get; set; } = String.Empty;
        [BsonElement("program_link")]
        public string Program_Link { get; set; } = String.Empty;
        [BsonElement("solution_link")]
        public string Program_SolutionLink { get; set; } = String.Empty;
        [BsonElement("difficulty")]
        public string Program_Difficulty { get; set; } = String.Empty;

        [BsonElement("issolved")]
        public bool isSolved { get; set; }
    }
}
