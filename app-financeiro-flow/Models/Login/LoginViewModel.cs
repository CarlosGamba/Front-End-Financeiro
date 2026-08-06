using System.ComponentModel.DataAnnotations;

namespace app_financeiro_flow.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O campo Login é obrigatório.")]
        public string Login { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        public string Senha { get; set; } = string.Empty;
    }
}
