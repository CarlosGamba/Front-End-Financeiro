using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using app_financeiro_flow.Models;

namespace app_financeiro_flow.Controllers
{
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public AuthController(ILogger<AuthController> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var client = _httpClientFactory.CreateClient();
            var BaseUrl = _configuration["ApiSettings:BaseUrl"];

            var usernameEscaped = Uri.EscapeDataString(model.Login);
            var passwordEscaped = Uri.EscapeDataString(model.Senha);
            var apiUrl = $"{BaseUrl}/api/Auth/Login?username={usernameEscaped}&password={passwordEscaped}";

            var content = new StringContent(
                JsonSerializer.Serialize(model),
                Encoding.UTF8,
                "application/json"
            );

            try
            {
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    using var jsonDoc = System.Text.Json.JsonDocument.Parse(responseBody);
                    string? token = jsonDoc.RootElement.GetProperty("accessToken").GetString();
                                        
                    if (!string.IsNullOrEmpty(token))
                    {
                        HttpContext.Session.SetString("AuthToken", token);
                    }
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Usuário ou senha inválidos.");
            }
            catch (HttpRequestException)
            {
                ModelState.AddModelError(string.Empty, "Erro ao se comunicar com o serviço de autenticação.");
            }

            return View(model);
            
        }
    }
}
