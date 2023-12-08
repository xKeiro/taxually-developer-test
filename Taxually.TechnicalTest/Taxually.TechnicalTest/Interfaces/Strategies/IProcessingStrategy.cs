namespace Taxually.TechnicalTest.Interfaces.Strategies;

public interface IProcessingStrategy<T> where T : class
{
    void Process(T request);
}
