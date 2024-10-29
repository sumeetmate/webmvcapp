using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ClientMvcApp.Models;
using System.Text;
using System.Threading.Tasks;

namespace ClientMvcApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    CancellationTokenSource tokenSource = new CancellationTokenSource();

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> CallWebApi()
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                string apiUrl = "http://localhost:5137/api/LongOperation";

                var response = await client.GetAsync(apiUrl, tokenSource.Token);
                var content = await response.Content.ReadAsStringAsync(tokenSource.Token);

                return Json(content);
            }
            catch (HttpRequestException ex)
            {
                return Json(new { error = ex.Message});
            }
        }
    }

    [HttpGet]
    public async Task<IActionResult> CPUBoundOperations()
    {
        StockMarketDataAnalysis analysis = new StockMarketDataAnalysis("data");

        List<Task<string>> tasks = new List<Task<string>>();

        tasks.Add(Task.Run(() => analysis.CalculateFMA()));
        tasks.Add(Task.Run(() => analysis.CalculateSMA()));
        tasks.Add(Task.Run(() => analysis.CalculateStockastics()));
        tasks.Add(Task.Run(() => analysis.CalculateLowerBoundBollingerBand()));
        tasks.Add(Task.Run(() => analysis.CalculateUpperBoundBollingerBand()));

        //this should block UI thread as it does not return task object
        //Task.WaitAll(tasks.ToArray());

        //Task.WhenAll returns the task object hence it does not block UI thread
        await Task.WhenAll(tasks.ToArray());

        //Use ConfigureWait(false) in case dont want to return to UI thread instead 
        //want to do operation like saving return result to file or db
        //await Task.WhenAll(tasks.ToArray()).ConfigureAwait(false);

        StringBuilder builder = new StringBuilder();

        foreach (var task in tasks)
            builder.AppendLine(task.Result);

        return Json(builder.ToString());
    }

    [HttpGet]
    public IActionResult CancelTask()
    {
        tokenSource.Cancel();
        return Json("Operation Cancelled");
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }    
}
