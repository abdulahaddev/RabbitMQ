using RabbitMQ.Client;
using System.Text.Json;
using System.Text;

namespace RMQServer
{
    public static class QueueProducer
    {
        public static void Publish(IModel channel)
        {
            channel.QueueDeclare(queue: "hello",
            durable: true,    // will Survive after broker restart
            exclusive: false,  // false - define multiple connection to access this queue
            autoDelete: false, // will not be automatically deleted by the broker
            arguments: null);  // additional arguments such as configuration

            int counter = 0;

            while (true)
            {
                var message = new { Name = "Ahad", Count = counter };

                var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

                channel.BasicPublish(string.Empty,
                                    routingKey: "hello",
                                    basicProperties: null,
                                    body: body);

                Console.WriteLine($" [x] Sent ");
                counter++;
                Thread.Sleep(2000);
            }
        }
    }
}
