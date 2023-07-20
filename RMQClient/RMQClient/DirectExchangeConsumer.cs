using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RMQClient
{
    public static class DirectExchangeConsumer
    {
        public static void Consume(IModel channel)
        {
            //channel.ExchangeDeclare("hello-direct-exchange", ExchangeType.Direct, arguments: null);

            channel.QueueDeclare(queue: "hello-direct-queue",
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

            channel.QueueBind("hello-direct-queue", "hello-direct-exchange", "key-direct-route");  

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                //channel.BasicAck(e.DeliveryTag, multiple: false);

                Console.WriteLine($"C-1 -> Message = {message}");                
            };

            channel.BasicConsume(queue: "hello-direct-queue",
                                autoAck: true,
                                consumer: consumer);

            Console.ReadLine();
        }
    }
}
