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
        public List<MST_Program> GETProgramByTopicName(string topic_name)
        {
            return _program.Find(program => program.Program_Topic.Equals(topic_name)).ToList();
        }

        public List<POG_ProgramCount> GETCountOfProgramByTopicName()
        {
            List<MST_Program> l = GET();
            List<POG_ProgramCount> programCount = new List<POG_ProgramCount>();
            foreach ( MST_Program program in l )
            {
                POG_ProgramCount programCountObj = new POG_ProgramCount();
                programCountObj.Topic_Name = program.Program_Topic;
                programCountObj.Program_Count = (int)_program.Find(p => p.Program_Topic.Equals(program.Program_Topic)).CountDocuments();
                bool flag = true;
                foreach( POG_ProgramCount progamCountObjOfList in programCount)
                {
                    if( progamCountObjOfList.Topic_Name.Equals(program.Program_Topic) )
                    {
                        flag = false;
                        break;
                    }
                }
                if( flag)
                {
                    programCount.Add(programCountObj);
                }
            }
            return programCount;
        }

        public int GETCountByTopicName(string topic_name)
        {
            return (int)_program.Find(program => program.Program_Topic.Equals(topic_name)).CountDocuments();
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

        public List<MST_Program> GETByFilter(string Program_Topic, string Program_Difficulty)
        {
            if (Program_Topic.Equals("all") && Program_Difficulty.Equals("all"))
            {
                return GET();
            }
            else if ( Program_Difficulty.Equals("all") )
            {
                return _program.Find(program => program.Program_Topic.Equals(Program_Topic)).ToList();
            }
            else if (Program_Topic.Equals("all"))
            {
                return _program.Find(program => program.Program_Difficulty.Equals(Program_Difficulty)).ToList();
            }
            return _program.Find(program => program.Program_Topic.Equals(Program_Topic) && program.Program_Difficulty.Equals(Program_Difficulty)).ToList();
        }
    }
}
