using RabbitMQ.Client;

namespace RMQServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            DeadLetterExchangePublisher.Publish(channel);

            Console.WriteLine("Finised!");
            Console.ReadKey();
        }
    }
}