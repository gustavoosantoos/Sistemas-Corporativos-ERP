using System.ComponentModel.DataAnnotations;

namespace WebERP.ViewModels.ManageViewModels
{
    public class IndexViewModel
    {
        public string Username { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Telefone")]
        public string PhoneNumber { get; set; }

        public string StatusMessage { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [StringLength(100)]
        public string Sobrenome { get; set; }
    }
}
