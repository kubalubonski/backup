namespace FilesBackupAPI;

public class FilesBackupDatabaseSettings
{
    public string ConnectionString {get; set;} = null!;
    public string DatabaseName {get; set;} = null!;
    public string FilesBackupCollectionName {get; set;} = null!;
}