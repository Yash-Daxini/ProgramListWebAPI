using ProgramListWebAPI.Models;

namespace ProgramListWebAPI.Services
{
    public interface IProgramListService
    {
        List<MST_Program> GET();
        MST_Program GET(string id);
        MST_Program POST(MST_Program program);
        void PUT(string id, MST_Program program);
        void DELETE(string id);
    }
}
