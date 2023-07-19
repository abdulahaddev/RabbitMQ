using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace RMQServer;

public static class HeaderExchangePublisher
{
    public static void Publish(IModel channel)
    {
        channel.ExchangeDeclare("hello-headers-exchange", ExchangeType.Headers);

        var properties = channel.CreateBasicProperties();
        properties.Headers = new Dictionary<string, object>() { { "priority", "high" } };

        int counter = 0;
        while (counter <= 10)
        {
            var message = new { Name = "Ahad", Count = counter };

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

            channel.BasicPublish("hello-headers-exchange",
                                routingKey: "key.hello",
                                basicProperties: properties,
                                body: body);

            Console.WriteLine($" [x] Sent ");
            counter++;
        }
    }
}
