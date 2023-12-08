namespace Taxually.TechnicalTest.Interfaces.Services.Clients;

public interface ITaxuallyHttpClient
{
    public Task PostAsync<TRequest>(string url, TRequest request);
}
