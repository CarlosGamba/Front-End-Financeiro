using app_financeiro_flow.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace app_financeiro_flow.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<AuthController> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            // 1. Recupera o token salvo na Session durante o Login
            var token = HttpContext.Session.GetString("AuthToken");

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }

            var client = _httpClientFactory.CreateClient();
            var BaseUrl = _configuration["ApiSettings:BaseUrl"];
            var apiUrl = $"{BaseUrl}/usuario";
            
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                        
            var response = await client.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var conteudo = await response.Content.ReadAsStringAsync();
                ViewBag.Mensagem = conteudo; 
                return View();
            }

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {                
                return RedirectToAction("Login", "Auth");
            }

            return View("Error");
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
}
