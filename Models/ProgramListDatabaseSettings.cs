namespace ProgramListWebAPI.Models
{
    public class ProgramListDatabaseSettings : IProgramListDatabaseSettings
    {
        public String DatabaseName { get; set; } = String.Empty;
        public String ConnectionString { get; set; } = String.Empty;
    }
}
