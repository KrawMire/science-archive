using System.Text.Json;
using RabbitMQ.Client;
using ScienceArchive.Application.Abstractions.Logging.Gateways;
using ScienceArchive.Application.Abstractions.Logging.Models;
using ScienceArchive.Infrastructure.Connectivity.RabbitMq.Options;

namespace ScienceArchive.Infrastructure.Connectivity.RabbitMq.Gateways;

internal class RabbitMqLogGateway : ILogGateway
{
    private readonly string _queueName;
    private readonly string _host;

    public RabbitMqLogGateway(RabbitMqConnectionOptions options)
    {
        _host = options.Host;
        _queueName = options.RequestLogsQueueName;
    }
    
    public async Task LogRequest(RequestLog log)
    {
        var factory = new ConnectionFactory
        {
            HostName = _host,
        };

        using var connection = await factory.CreateConnectionAsync();
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