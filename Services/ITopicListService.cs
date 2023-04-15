using ProgramListWebAPI.Models;

namespace ProgramListWebAPI.Services
{
    public interface ITopicListService
    {
        List<MST_ProgramTopic> GET();
        MST_ProgramTopic GET(string id);
        MST_ProgramTopic POST(MST_ProgramTopic topic);
        void PUT(string id, MST_ProgramTopic topic);
        void DELETE(string id);
        int GETCOUNT(string topic_name);
        List<MST_ProgramTopic> GETByName(string id, string topic_name);
    }
}
