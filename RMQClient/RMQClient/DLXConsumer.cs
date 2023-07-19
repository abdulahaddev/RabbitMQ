using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RMQClient;

public static class DLXConsumer
{
    public static void Consume(IModel channel)
    {
        channel.ExchangeDeclare("DEFAULT-EXCHANGE", ExchangeType.Direct);
        channel.ExchangeDeclare("DLX-EXCHANGE", ExchangeType.Direct);

        var args = new Dictionary<string, object>()
        {
            { "x-message-ttl", 20000 },
            { "x-dead-letter-exchange", "DLX-EXCHANGE" },
            { "x-dead-letter-routing-key", "DLX-ROUTING-KEY" }
        };
        channel.QueueDeclare("DEFAULT-QUEUE", durable: true, exclusive: false, autoDelete: false, args);
        channel.QueueBind("DEFAULT-QUEUE", "DEFAULT-EXCHANGE", "DEFAULT-ROUTING-KEY");

        channel.QueueDeclare("DLX-QUEUE", durable: true, exclusive: false, autoDelete: false);
        channel.QueueBind("DLX-QUEUE", "DLX-EXCHANGE", "DLX-ROUTING-KEY");

        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += (sender, e) =>
        {
            var body = e.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            Console.WriteLine($"C-1 -> Message = {message}");
        };

        channel.BasicConsume(queue: "DEFAULT-QUEUE",
                            autoAck: true,
                            consumer: consumer);

        Console.ReadLine();
    }
}

