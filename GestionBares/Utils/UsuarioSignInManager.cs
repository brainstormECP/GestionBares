using System.Threading.Tasks;
using GestionBares.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GestionBares.Utils
{
    public class UsuarioSignInManager : SignInManager<Usuario>
    {
        public UsuarioSignInManager(UserManager<Usuario> userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<Usuario> claimsFactory, IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager<Usuario>> logger, IAuthenticationSchemeProvider schemes) : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes)
        {
        }

        public override Task<bool> CanSignInAsync(Usuario user)
        {
            if (!user.Activo)
            {
                return Task.FromResult(false);
            }
            return base.CanSignInAsync(user);
        }
    }
}