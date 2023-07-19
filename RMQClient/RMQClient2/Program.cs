using RabbitMQ.Client;

namespace RMQClient2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConnectionFactory factory = new() { HostName = "localhost", UserName = "guest", Password = "guest" };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            TopicExchangeConsumer.Consume(channel);

            Console.ReadKey();
        }
    }
}