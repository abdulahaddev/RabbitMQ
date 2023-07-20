using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RMQClient2;

public static class TopicExchangeConsumer
{
    public static void Consume(IModel channel)
    {
        channel.ExchangeDeclare("hello-topic-exchange", ExchangeType.Topic);

        channel.QueueDeclare(queue: "hello-topic-queue2",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        channel.QueueBind("hello-topic-queue2", "hello-topic-exchange", "key.#");

        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += (sender, e) =>
        {
            var body = e.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            Console.WriteLine($"C-2 -> Message = {message}");
        };

        channel.BasicConsume(queue: "hello-topic-queue2",
                            autoAck: true,
                            consumer: consumer);

        Console.ReadLine();
    }
}
