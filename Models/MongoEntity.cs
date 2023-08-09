using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
namespace FilesBackupAPI.Models;


public class Action
    {
        public string itemId { get; set; }
        public int index { get; set; }
        public Type type { get; set; }
        public DateTime started { get; set; }
        public State state { get; set; }
    }

    public class Content
    {
        public string backupTaskId { get; set; }
        public string backupId { get; set; }
        public State state { get; set; }
        public List<Action> actions { get; set; }
        public List<Item> items { get; set; }
        public int version { get; set; }
    }

    public class Item
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public int index { get; set; }
        public State state { get; set; }
        public string remoteId { get; set; }
    }

    public class Params
    {
        public string name { get; set; }
    }

    public class MongoEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id {get; set;}
        public string userHash { get; set; }
        public int accountId { get; set; }
        public string sid { get; set; }
        public string @event { get; set; }
        public Content content { get; set; }
    }

    public class State
    {
        public DateTime lastBackupTime { get; set; }
        public DateTime nextBackupTime { get; set; }
        public Status status { get; set; }
        public int durationSeconds { get; set; }
    }

    public class Status
    {
        public string type { get; set; }
    }

    public class Type
    {
        public string name { get; set; }
        public Params @params { get; set; }
    }
