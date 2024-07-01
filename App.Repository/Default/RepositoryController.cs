using App.Repository.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace App.Repository.Default
{
    public class RepositoryController : Controller
    {
        protected bool IsAuthorized()
        {
            // --== Obtendo usuário
            var uid = HttpContext.Session.GetString(RepositoryKeys.UID.ToString());
            return !string.IsNullOrEmpty(uid);
        }

    }
}
