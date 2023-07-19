using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace RMQServer;

public static class FanoutExchangePublisher
{
    public static void Publish(IModel channel)
    {
        channel.ExchangeDeclare("hello-fanout-exchange", ExchangeType.Fanout);

        channel.QueueDeclare(queue: "hello-fanout-queue",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        channel.QueueBind("hello-fanout-queue", "hello-fanout-exchange", string.Empty);
        int counter = 0;
        while (counter <= 500)
        {
            var message = new { Name = "Ahad", Count = counter };

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

            channel.BasicPublish("hello-fanout-exchange",
                                routingKey: string.Empty,
                                basicProperties: null,
                                body: body);

            Console.WriteLine($" [x] Sent ");
            counter++;
        }

        Console.WriteLine("Published!");
        Console.ReadKey();
    }
}
