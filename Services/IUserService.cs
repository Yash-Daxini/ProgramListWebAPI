using ProgramListWebAPI.Models;

namespace ProgramListWebAPI.Services
{
    public interface IUserService
    {
        List<SEC_User> GET();
        SEC_User GET(string id);
        SEC_User POST(SEC_User topic);
        void PUT(string id, SEC_User topic);
        void DELETE(string id);
    }
}
