using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using CommentService.Domain.Entities;

namespace CommentService.Infrastructure.Services;

public class RabbitMQProducer
{
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitMQProducer(IConfiguration configuration)
    {
        var factory = new ConnectionFactory
        {
            HostName = configuration["RabbitMQ:HostName"],
            UserName = configuration["RabbitMQ:UserName"],
            Password = configuration["RabbitMQ:Password"]
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: "comments", durable: false, exclusive: false, autoDelete: false, arguments: null);
    }

    public void Publish(Comment comment)
    {
        var message = JsonSerializer.Serialize(comment);
        var body = Encoding.UTF8.GetBytes(message);

        _channel.BasicPublish(exchange: "", routingKey: "comments", basicProperties: null, body: body);
    }
}