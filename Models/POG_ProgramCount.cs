using MongoDB.Bson.Serialization.Attributes;

namespace ProgramListWebAPI.Models
{
    public class POG_ProgramCount
    {
        public string Topic_Name { get; set; } = String.Empty;
        public int Program_Count { get; set; }
    }
}
