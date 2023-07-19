using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RMQClient2
{
    public static class DirectExchangeConsumer
    {
        public static void Consume(IModel channel)
        {
            channel.QueueDeclare(queue: "hello-direct-queue",
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

            channel.QueueBind("hello-direct-queue", "hello-direct-exchange", "key-direct-route");

            channel.BasicQos(0, 10, false);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                channel.BasicAck(e.DeliveryTag, multiple: false);

                Console.WriteLine($"C-2 -> Message = {message}");                
            };

            channel.BasicConsume(queue: "hello-direct-queue",
                                autoAck: false,
                                consumer: consumer);

            Console.ReadLine();
        }
    }
}
