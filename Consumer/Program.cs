using System.Text;
using RabbitMQ.Client;
// using Consumer;
using RabbitMQ.Client.Events;

DotNetEnv.Env.Load();
var factory = new ConnectionFactory() { HostName = "localhost", UserName = "supereasy-socket", Password = "BSYYXf7UXk2A2fQ4RzsqJu" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();
channel.QueueDeclare(queue: "hello", durable: true, exclusive: false, autoDelete: false, arguments: null);
var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, e) =>
{
    var body = e.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    System.Console.WriteLine($"[x] Received from C# console {message}");
};
channel.BasicConsume(queue: "hello", autoAck: true, consumer: consumer);

System.Console.WriteLine("Press any key to exit...");
System.Console.ReadKey();
