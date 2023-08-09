using Confluent.Kafka;
using FilesBackupAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var _kafkaConfig  = new ConsumerConfig
        {
            BootstrapServers = "localhost:9092",
            GroupId = "lalalala"
        };

builder.Services.AddSingleton<ConsumerConfig>(_kafkaConfig);
builder.Services.AddHostedService<KafkaMessageHandlerService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

builder.Services.Configure<FilesBackupDatabaseSettings>(

    builder.Configuration.GetSection("FilesBackupDatabase")
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
