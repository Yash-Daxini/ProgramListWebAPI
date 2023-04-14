namespace ProgramListWebAPI.Models
{
    public interface IProgramListDatabaseSettings
    {
        String DatabaseName { get; set; }
        String ConnectionString { get; set; }
    }
}
