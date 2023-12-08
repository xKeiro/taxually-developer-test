namespace Taxually.TechnicalTest.Interfaces.Services.Clients;

public interface ITaxuallyQueueClient
{
    public Task EnqueueAsync<TPayload>(string queueName, TPayload payload);
}
