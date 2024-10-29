public class StockMarketDataAnalysis
{
    public StockMarketDataAnalysis(string data)
    {
        
    }

    public string CalculateFMA()
    {        
        Thread.Sleep(7000);
        return $"{nameof(CalculateFMA)} - ThreadId: {Thread.CurrentThread.ManagedThreadId}";
    }

    public string CalculateSMA()
    {
        Thread.Sleep(6000);
        return $"{nameof(CalculateSMA)} - ThreadId: {Thread.CurrentThread.ManagedThreadId}";
    }

    public string CalculateUpperBoundBollingerBand()
    {
        Thread.Sleep(9000);
        return $"{nameof(CalculateUpperBoundBollingerBand)} - ThreadId: {Thread.CurrentThread.ManagedThreadId}";
    }

    public string CalculateLowerBoundBollingerBand()
    {
        Thread.Sleep(8000);
        return $"{nameof(CalculateLowerBoundBollingerBand)} - ThreadId: {Thread.CurrentThread.ManagedThreadId}";
    }

    public string CalculateStockastics()
    {
        Thread.Sleep(9000);
        return $"{nameof(CalculateStockastics)} - ThreadId: {Thread.CurrentThread.ManagedThreadId}";
    }
}