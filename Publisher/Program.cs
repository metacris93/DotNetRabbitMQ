using System.Text;
using System.Text.Json;
using Publisher;
using RabbitMQ.Client;

DotNetEnv.Env.Load();
var factory = new ConnectionFactory() { 
    HostName = "localhost", 
    UserName = DotNetEnv.Env.GetString("RABBITMQ_USERNAME"), 
    Password = DotNetEnv.Env.GetString("RABBITMQ_PASSWORD")
};
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();
channel.QueueDeclare(queue: "hello", durable: true, exclusive: false, autoDelete: false, arguments: null);
var obj = new Body { Name = "Producer", Message = "Hello!" };
var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(obj));
channel.BasicPublish(exchange: "", routingKey: "hello", basicProperties: null, body: body);
System.Console.WriteLine($"[x] Sent...");

System.Console.WriteLine("Press any key to exit...");
System.Console.ReadKey();
