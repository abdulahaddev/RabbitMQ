using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RMQClient2;

public static class TopicExchangeConsumer
{
    public static void Consume(IModel channel)
    {
        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += (sender, e) =>
        {
            var body = e.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            Console.WriteLine($"C-1 -> Message = {message}");
        };

        channel.BasicConsume(queue: "hello-topic-queue2",
                            autoAck: true,
                            consumer: consumer);

        Console.ReadLine();
    }
}
