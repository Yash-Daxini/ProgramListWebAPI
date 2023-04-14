using ProgramListWebAPI.Models;
using MongoDB.Driver;

namespace ProgramListWebAPI.Services
{
    public class ProgramListService : IProgramListService
    {
        private readonly IMongoCollection<MST_Program> _program;
        private readonly IMongoCollection<MST_ProgramTopic> _topic;

        public ProgramListService(IProgramListDatabaseSettings settings,IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _program = database.GetCollection<MST_Program>("MST_Program");
            _topic = database.GetCollection<MST_ProgramTopic>("MST_ProgramTopic");

        }
        public void DELETE(string id)
        {
            string topic = _program.Find(program => program.ID == id).FirstOrDefault().Program_Topic;
            _program.DeleteOne(program => program.ID == id);
            if( _program.Find(program => program.Program_Topic.Equals(topic)).CountDocuments() == 0 ){
                _topic.DeleteOne(Topic => Topic.Topic_Name.Equals(topic));
            }
        }

        public List<MST_Program> GET()
        {
            return _program.Find(program => true).ToList();
        }

        public MST_Program GET(string id)
        {
            return _program.Find(program => program.ID == id).FirstOrDefault();
        }

        public MST_Program POST(MST_Program program)
        {
            string topic = program.Program_Topic;
            if (_program.Find(program => program.Program_Topic.Equals(topic)).CountDocuments() == 0)
            {
                MST_ProgramTopic newTopic = new MST_ProgramTopic();
                newTopic.Topic_Name = topic;
                _topic.InsertOne( newTopic );
            }
            _program.InsertOne(program);
            return program;
        }

        public void PUT(string id, MST_Program program)
        {
            string oldtopic = _program.Find(Program => Program.ID == id).FirstOrDefault().Program_Topic;
            string topic = program.Program_Topic;
            _program.ReplaceOne(program => program.ID == id , program);
            if (_program.Find(Program => Program.Program_Topic.Equals(oldtopic)).CountDocuments() == 0)
            {
                _topic.DeleteOne(Topic => Topic.Topic_Name.Equals(oldtopic));
            }
            else
            {
                if( _topic.Find( Topic => Topic.Topic_Name.Equals(topic) ).CountDocuments() == 0)
                {
                    MST_ProgramTopic newTopic = new MST_ProgramTopic();
                    newTopic.Topic_Name = topic;
                    _topic.InsertOne(newTopic);
                }
            }
        }
    }
}
