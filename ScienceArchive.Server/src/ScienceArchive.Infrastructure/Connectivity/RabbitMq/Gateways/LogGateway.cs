using System.Text.Json;
using RabbitMQ.Client;
using ScienceArchive.Shared.Abstractions.Logging.Gateways;
using ScienceArchive.Shared.Abstractions.Logging.Models;
using ScienceArchive.Infrastructure.Connectivity.RabbitMq.Options;

namespace ScienceArchive.Infrastructure.Connectivity.RabbitMq.Gateways;

internal class RabbitMqLogGateway : ILogGateway
{
    private readonly string _queueName;
    private readonly IConnectionFactory _connectionFactory;

    public RabbitMqLogGateway(RabbitMqConnectionOptions options)
    {
        if (options.Host is null && options.ConnectionString is null)
        {
            throw new ArgumentNullException(nameof(options));
        }

        if (options.Host is not null)
        {
            _connectionFactory = new ConnectionFactory()
            {
                HostName = options.Host,
            };
        }
        else if (options.ConnectionString is not null)
        {
            _connectionFactory = new ConnectionFactory()
            {
                Uri = new Uri(options.ConnectionString),
            };
        }
        
        _queueName = options.RequestLogsQueueName;
    }
    
    public async Task LogRequest(RequestLog log)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(
            queue: _queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        log.Timestamp = log.Timestamp.ToUniversalTime();
        var body = JsonSerializer.SerializeToUtf8Bytes(log);
        
        await channel.BasicPublishAsync(
            exchange: string.Empty,
            routingKey: _queueName,
            body: body);
    }
}