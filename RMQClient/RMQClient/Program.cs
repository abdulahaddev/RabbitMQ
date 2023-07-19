using RabbitMQ.Client;

namespace RMQClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConnectionFactory factory = new() { HostName = "localhost", UserName = "guest", Password = "guest" };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            DLXConsumer.Consume(channel);

            Console.ReadKey();
        }
    }
}