using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using WebERP.Utils.Attributes;
using WebERP.Utils.Identity;

namespace WebERP.ViewModels.AccountViewModels
{
    public class RegisterViewModel
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "O campo nome é obrigatório.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo nome deve ter no mínimo 3 e no máximo 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo sobrenome é obrigatório.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo nome deve ter no mínimo 3 e no máximo 100 caracteres.")]
        public string Sobrenome { get; set; }
        
        [Required(ErrorMessage = "O campo e-mail é obrigatório.")]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "O campo senha é obrigatório")]
        [StringLength(100, ErrorMessage = "A {0} deve ter ao menos {2} e no máximo {1} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmação de senha")]
        [Compare(nameof(Password), ErrorMessage = "A senha e a confirmação de senha não conferem.")]
        public string ConfirmPassword { get; set; }

        [EnsureMinimumElements(1, ErrorMessage = "O usuário deve ter ao menos 1 role.")]
        public List<string> UserRoles { get; set; }

        public bool IsNewUser => string.IsNullOrWhiteSpace(Id);
    }
}
