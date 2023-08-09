namespace FilesBackupAPI;
using Confluent.Kafka;
using Newtonsoft.Json;
using NLog;

public class SendMessageKafka
{
    private readonly string _kafkaTopic = "backupEvents";
    private readonly ILogger<SendMessageKafka> _logger;
    

     //Kafka
    public async Task SendRentalEventCreateToKafka()
    {
       
        var config = new ProducerConfig {BootstrapServers = "localhost:9092"};
        
        using (var producer = new ProducerBuilder<Null, string>(config).Build())
        {
            
                
                var msg = 
                
                  "  {\n    \"userHash\": \"qwerty\",\n    \"accountId\": 1234,\n    \"sid\": \"S-1-5-21-81367644-1902293147-1658268802\",\n    \"event\": \"backupMajorUpdate\",\n    \"content\": {\n      \"backupTaskId\": \"05417148-e440-41a5-8f49-7e6bb232de37\",\n      \"backupId\": \"027c03f8-94e1-472a-8298-e9e73c888988\",\n      \"state\": {\n        \"lastBackupTime\": \"2023-08-08T10:57:48.4817402\",\n        \"nextBackupTime\": \"2023-08-09T08:18:00\",\n        \"status\": {\n          \"type\": \"active\"\n        }\n      },\n      \"actions\": [\n        {\n          \"itemId\": \"34e5fc25-f06a-4bdb-9974-7a0ea31e586a\",\n          \"index\": 1,\n          \"type\": {\n            \"name\": \"analysis\",\n            \"params\": {\n              \"name\": \"test3\"\n            }\n          },\n          \"started\": \"2023-08-08T10:57:49.045249+02:00\",\n          \"state\": {\n            \"status\": {\n              \"type\": \"active\"\n            },\n            \"durationSeconds\": 0\n          }\n        }\n      ],\n      \"items\": [\n        {\n          \"id\": \"34e5fc25-f06a-4bdb-9974-7a0ea31e586a\",\n          \"name\": \"test3\",\n          \"type\": \"database\",\n          \"index\": 1,\n          \"state\": {\n            \"status\": {\n              \"type\": \"active\"\n            }\n          },\n          \"remoteId\": \"1607157\"\n        }\n      ],\n      \"version\": 1\n    }";
                
                //var book = await _client.GetBookById(rental.Bookid);
                
                
               
                
                //var backupJson = JsonConvert.SerializeObject(msg);
                var message = new Message<Null, string> {Value = msg};

                var report = await producer.ProduceAsync(_kafkaTopic, message);


                //Logging with X-Request-ID
                //var requestId = _httpContextAccessor.HttpContext.Items["X-Request-ID"]?.ToString();
                //_logger.LogInformation($"Kafka message(create rent) sent (topic: {report.Topic}, partition: {report.Partition}, offset: {report.Offset}), , X-Request-ID: {requestId}");

           
          
           
           
        
        }
    }
}