using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PUC.LDSI.Identity.Entities;

namespace PUC.LDSI.MVC.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly UserManager<Usuario> _userManager;

        private Usuario _user;
        protected Usuario UsuarioLogado
        {
            get
            {
                if (_user == null)
                    _user = _userManager.GetUserAsync(User).Result;
                return _user;
            }
        }

        private int _integrationUserId;
        protected int IntegrationUserId
        {
            get
            {
                if (_integrationUserId == 0)
                    _integrationUserId = UsuarioLogado.IntegrationId;

                return _integrationUserId;
            }
        }

        public BaseController(UserManager<Usuario> user)
        {
            _userManager = user;
        }
    }
}