using app_financeiro_flow.Models;
using app_financeiro_flow.Models.Fornecedor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace app_financeiro_flow.Controllers
{
    public class FornecedorController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public FornecedorController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Criar()
        {
            CarregarCombos();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Criar(FornecedorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                CarregarCombos();
                return View(model);
            }

            var client = _httpClientFactory.CreateClient();
                        
            var token = HttpContext.Session.GetString("AuthToken");
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            var BaseUrl = _configuration["ApiSettings:BaseUrl"];
            var apiUrl = $"{BaseUrl}/api/Fornecedor";

            try
            {
                
                var response = await client.PostAsJsonAsync(apiUrl, new
                {
                    nome = model.Nome,
                    cnpj = model.Cnpj,
                    idTipoFornecedor = model.IdTipoFornecedor,
                    idAtivo = model.IdAtivo
                });

                if (response.IsSuccessStatusCode)
                {                    
                    return RedirectToAction("ConsultaTodos", "Fornecedor");
                }

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Login", "Auth");
                }

                ModelState.AddModelError(string.Empty, "Falha ao cadastrar o fornecedor.");
            }
            catch (HttpRequestException)
            {
                ModelState.AddModelError(string.Empty, "Erro de conexão com o servidor da API.");
            }

            CarregarCombos();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ConsultaTodos()
        {
            var client = _httpClientFactory.CreateClient();

            var token = HttpContext.Session.GetString("AuthToken");
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            var baseUrl = _configuration["ApiSettings:BaseUrl"];
            var apiUrl = $"{baseUrl}/api/Fornecedor";

            try
            {
                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var fornecedores = await response.Content.ReadFromJsonAsync<IEnumerable<FornecedorViewModel>>();
                    return View(fornecedores);
                }

                ModelState.AddModelError(string.Empty, "Erro ao carregar os fornecedores da API.");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Não foi possível conectar à API.");
            }

            return View(Enumerable.Empty<FornecedorViewModel>());
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var client = _httpClientFactory.CreateClient();

            var token = HttpContext.Session.GetString("AuthToken");
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            var baseUrl = _configuration["ApiSettings:BaseUrl"];
            var apiUrl = $"{baseUrl}/api/Fornecedor/{id}"; 

            try
            {
                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var fornecedor = await response.Content.ReadFromJsonAsync<FornecedorViewModel>();
                    return View(fornecedor);
                }

                ModelState.AddModelError(string.Empty, "Não foi possível encontrar o fornecedor.");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Erro de conexão com a API.");
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, FornecedorViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var client = _httpClientFactory.CreateClient();

            var token = HttpContext.Session.GetString("AuthToken");
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            var baseUrl = _configuration["ApiSettings:BaseUrl"];
            var apiUrl = $"{baseUrl}/api/Fornecedor/{id}";

            try
            {
                var response = await client.PutAsJsonAsync(apiUrl, model);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Sucesso"] = "Fornecedor atualizado com sucesso!";
                    return RedirectToAction("ConsultaTodos", "Fornecedor");
                }

                ModelState.AddModelError(string.Empty, "Erro ao atualizar o fornecedor na API.");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Não foi possível conectar à API.");
            }

            return View(model);
        }

        private void CarregarCombos()
        {
            ViewBag.TiposFornecedor = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Hotel" },
                new SelectListItem { Value = "2", Text = "Fornecedor" }
            };

            ViewBag.StatusAtivo = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Ativo" },
                new SelectListItem { Value = "0", Text = "Inativo" }
            };
        }
    }
}

