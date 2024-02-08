using AbpFramework.Poc.Core.Dto;
using AbpFramework.Poc.Core.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace AbpFramework.POC.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ICreateAccount _createAccount;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ICreateAccount createAccount)
        {
            _logger = logger;
            _createAccount = createAccount;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost(Name = "CreateAccount")]
        public async Task<IActionResult> CreateAccount(CreateAccountDto createAccountDto)
        {
            var resultDto = await _createAccount.CreateAsync(createAccountDto);

            return NotFound();
        }

    }
}
