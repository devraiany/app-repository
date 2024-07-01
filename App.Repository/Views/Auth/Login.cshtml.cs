using App.Repository.Models.Auth;

namespace App.Repository.Views.Auth
{
    public class LoginViewModel
    {
        public AuthModel AuthModel { get; set; } = new AuthModel();
    }
}
