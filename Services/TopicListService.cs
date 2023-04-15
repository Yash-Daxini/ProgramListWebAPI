using ProgramListWebAPI.Models;
using MongoDB.Driver;

namespace ProgramListWebAPI.Services
{
    public class TopicListService : ITopicListService
    {
        private readonly IMongoCollection<MST_ProgramTopic> _topic;

        public TopicListService(IProgramListDatabaseSettings settings , IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _topic = database.GetCollection<MST_ProgramTopic>("MST_ProgramTopic");

        }

        public void DELETE(string id)
        {
            _topic.DeleteOne(Topic => Topic.ID == id);
        }

        public List<MST_ProgramTopic> GET()
        {
            return _topic.Find(Topic => true).ToList();
        }

        public MST_ProgramTopic GET(string id)
        {
            return _topic.Find(Topic => Topic.ID == id).FirstOrDefault();
        }

        public List<MST_ProgramTopic> GETByName(string id , string topic_name)
        {
            return _topic.Find(Topic => Topic.Topic_Name.Equals(topic_name)).ToList();
        }

        public int GETCOUNT(string topic_name)
        {
            return (int)_topic.Find(Topic => Topic.Topic_Name.Equals(topic_name)).CountDocuments();
        }

        public MST_ProgramTopic POST(MST_ProgramTopic topic)
        {
            _topic.InsertOne(topic);
            return topic;
        }

        public void PUT(string id, MST_ProgramTopic topic)
        {
            _topic.ReplaceOne( Topic => Topic.ID == id, topic);
        }
    }
}
