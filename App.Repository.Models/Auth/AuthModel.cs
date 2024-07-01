using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace App.Repository.Models.Auth
{
    public class AuthModel
    {
        [BindProperty]
        [Required(ErrorMessage = "O email é um campoobrigatório")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "O email inserido é invalido")]
        public string? Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "A senha é um campo obrigatório")]
        public string? Senha { get; set; }
    }
}
