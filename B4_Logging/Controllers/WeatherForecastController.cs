using Microsoft.AspNetCore.Mvc;

namespace B4_Logging.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            _logger.LogInformation("WeatherForecast GET endpoint wurde aufgerufen.");

            var forecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

            _logger.LogDebug("Erzeugte Wettervorhersagen: {@Forecasts}", forecasts);

            return forecasts;
        }

        [HttpGet("details/{id}")]
        public ActionResult<string> GetDetails(int id)
        {
            if (id < 0 || id >= Summaries.Length)
            {
                _logger.LogWarning("Ungültige ID {Id} wurde abgefragt.", id);
                return BadRequest("Ungültige ID.");
            }

            _logger.LogInformation("Detailabfrage für ID {Id}: {Summary}", id, Summaries[id]);
            return Ok(Summaries[id]);
        }
    }
}
