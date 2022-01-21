using Microsoft.Extensions.Logging;


namespace MediatMyR;

public interface IPublish
{
	Task PublishAsync<T>(string key, string topic, T datush);
}

public class MockPublisher : IPublish
{
	private ILogger<MockPublisher> _logger;

	public MockPublisher(ILogger<MockPublisher> logger)
	{
		_logger = logger;
	}

	public async Task PublishAsync<T>(string key, string topic, T datush)
	{
		_logger.LogInformation("publishing message with key {key} to topic {topic}", key, topic);
		await Task.Delay(TimeSpan.FromMilliseconds(50));
	}
}
