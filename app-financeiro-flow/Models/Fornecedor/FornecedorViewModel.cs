using System.ComponentModel.DataAnnotations;

namespace app_financeiro_flow.Models.Fornecedor
{
    public class FornecedorViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [Display(Name = "Nome")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O CNPJ é obrigatório.")]
        [Display(Name = "CNPJ")]
        public string Cnpj { get; set; } = string.Empty;
                
        public int IdTipoFornecedor { get; set; }

        public int IdAtivo { get; set; }
    }
}
