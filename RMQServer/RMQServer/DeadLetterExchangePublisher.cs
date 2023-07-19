using RabbitMQ.Client;
using System.Text.Json;
using System.Text;

namespace RMQServer;

public static class DeadLetterExchangePublisher
{
    public static void Publish(IModel channel)
    {
        channel.ExchangeDeclare("DEFAULT-EXCHANGE", ExchangeType.Direct);

        int counter = 0;
        while (counter <= 400)
        {
            var message = new { Name = "Ahad", Count = counter };

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

            channel.BasicPublish("DEFAULT-EXCHANGE",
                                routingKey: "DEFAULT-ROUTING-KEY",
                                basicProperties: null,
                                body: body);

            Console.WriteLine($" [x] Sent ");
            counter++;
        }


    }
}
