using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace pingronnies
{
    public class Function1
    {
        private readonly ILogger _logger;

        public Function1(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<Function1>();
        }

        [Function("Function1")]
        public async Task Run([TimerTrigger("0 */10 * * * *")] TimerInfo myTimer)
        {
            _logger.LogInformation($"Ping function triggered at: {DateTime.Now}");

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("https://smsapi20250111164242.azurewebsites.net/api/Events");
                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Ping successful");
                }
                else
                {
                    _logger.LogError("Ping failed with status code: {response.StatusCode}");
                }

            }
        }
    }
}
