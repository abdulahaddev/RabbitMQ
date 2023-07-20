using RabbitMQ.Client;
using System.Text.Json;
using System.Text;

namespace RMQServer;

//
// -> * can substitute for exactly one word. ex: *.orange.* => quick.orange.rabbit but not quick.orange.rabbit.something
// -> # can substitute for zero or more word. ex: lazy.# => lazy.brown.fox
//
public static class TopicExchangePublisher
{
    public static void Publish(IModel channel)
    {
        channel.ExchangeDeclare("hello-topic-exchange", ExchangeType.Topic);

        int counter = 0;
        while (counter <= 10)
        {
            var message = new { Name = "Ahad", Count = counter };

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

            channel.BasicPublish("hello-topic-exchange",
                                routingKey: "key.hello",
                                basicProperties: null,
                                body: body);

            Console.WriteLine($" [x] Sent ");
            counter++;
        }
    }
}
