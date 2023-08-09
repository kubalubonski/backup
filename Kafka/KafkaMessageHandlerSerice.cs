
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Newtonsoft.Json;

using FilesBackupAPI.Models;



namespace FilesBackupAPI;

public class KafkaMessageHandlerService : BackgroundService
{
    private readonly IMongoCollection<MongoEntity> _filesBackupCollection;
    private readonly string _kafkaTopic = "backupEvents";
    private readonly ConsumerConfig _kafkaConfig;
    private readonly ILogger<KafkaMessageHandlerService> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    

    public KafkaMessageHandlerService(IOptions<FilesBackupDatabaseSettings> databaseSettings, ConsumerConfig kafkaConfig, ILogger<KafkaMessageHandlerService> logger, IHttpContextAccessor httpContextAccessor)
    {
       
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;

        var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
        var mongoBase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
        _filesBackupCollection = mongoBase.GetCollection<MongoEntity>(databaseSettings.Value.FilesBackupCollectionName); 

       

        _kafkaConfig  = new ConsumerConfig
        {
            BootstrapServers = "localhost:9092",
            GroupId = "lalalala1",
            //AutoOffsetReset = AutoOffsetReset.Latest
        };
    }
    public async Task ConsumeKafka (CancellationToken stoppingToken)
    {
        using (var consumer = new ConsumerBuilder<Ignore, string>(_kafkaConfig).Build())
        {
            
            consumer.Subscribe(_kafkaTopic);
            try 
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                
                    var message = consumer.Consume(stoppingToken);
                    var mongoEntity = JsonConvert.DeserializeObject<MongoEntity>((message.Value));
                   

                    
                        
                        
                    await _filesBackupCollection.InsertOneAsync(mongoEntity, cancellationToken: stoppingToken);
                    //     _logger.LogInformation($"Save communicate from kafka to MongoDB action INSERT executed");
                         
                     
                    //  else
                    //  {
                    //     //  _logger.LogInformation($"Save communicate from kafka to MongoDB action UPDATE invoked");
                    //     //  var filter = Builders<RentalData>.Filter.Where(r => r.Rentid == rentalData.Rentid);
                    //     //  var update = Builders<RentalData>.Update.Set(r => r.ReturnDate, rentalData.ReturnDate);
                    //     //  await _rentalHistoryCollection.FindOneAndUpdateAsync(filter, update, new FindOneAndUpdateOptions<RentalData>(), stoppingToken);
                    //     //  _logger.LogInformation($"Save communicate from kafka to MongoDB action UPDATE executed");
                    //  }
                      
                }   
            }
            catch (OperationCanceledException)
            {
                consumer.Close();
            }

        }
    }
    protected override  Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Task.Run(() => ConsumeKafka(stoppingToken));
        return Task.CompletedTask;
    
        
    }
}