using ScienceArchive.Infrastructure.Connectivity.Options;
using ScienceArchive.Infrastructure.Connectivity.RabbitMq.Options;
using ScienceArchive.Infrastructure.Persistence.Options;
using ScienceArchive.Infrastructure.Persistence.PostgreSql.Options;

namespace ScienceArchive.Web.Api.Configuration;

public static class ConfigurationManager
{
	/// <summary>
	/// Get connection options
	/// </summary> 
	/// <param name="builder">Instance of <see cref="WebApplicationBuilder"/></param>
	/// <returns>Connection options</returns>
	public static PersistenceOptions GetPersistenceOptions(WebApplicationBuilder builder)
	{
		string dbConnectionString;

		if (builder.Environment.IsDevelopment())
		{
			dbConnectionString =
				builder.Configuration.GetConnectionString("PostgreSQL") ??
				throw new NullReferenceException("Cannot get DB connection string from config file!");
		}
		else
		{
			dbConnectionString =
				Environment.GetEnvironmentVariable("POSTGRESQL_CONNECTION_STRING") ??
				throw new NullReferenceException("Cannot get DB connection string from environment!");
		}

		if (dbConnectionString is null)
		{
			throw new NullReferenceException("Cannot get connection string!");
		}

		return new PersistenceOptions
		{
			PostgresConnectionOptions = new PostgresConnectionOptions
			{
				PostgresConnectionString = dbConnectionString 
			}
		};
	}

	/// <summary>
	/// Get connectivity options
	/// </summary>
	/// <param name="builder">Instance of <see cref="WebApplicationBuilder"/></param>
	/// <returns>Connectivity options</returns>
	public static ConnectivityOptions GetConnectivityOptions(WebApplicationBuilder builder)
	{
		return new ConnectivityOptions
		{
			RabbitMqConnectionOptions = new RabbitMqConnectionOptions
			{
				Host = "localhost",
				NotificationsQueueName = "notifications",
				RequestLogsQueueName = "request_logs"
			}
		};
	}
}