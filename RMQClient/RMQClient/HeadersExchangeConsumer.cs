using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RMQClient;

public static class HeadersExchangeConsumer
{
    public static void Consume(IModel channel)
    {
        channel.QueueDeclare(queue: "hello-headers-queue",
                            durable: true,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);

        var arguments = new Dictionary<string, object>
        {
            { "x-match", "any" },
            { "priority", "high" }
        };

        channel.QueueBind("hello-headers-queue", "hello-headers-exchange", string.Empty, arguments);

        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += (sender, e) =>
        {
            var body = e.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            Console.WriteLine($"C-1 -> Message = {message}");
        };

        channel.BasicConsume(queue: "hello-headers-queue",
                            autoAck: true,
                            consumer: consumer);

        Console.ReadLine();
    }
}
