using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Text.Encodings.Web;
using GestionBares.Models;
using GestionBares.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace GestionBares.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {
        DbContext _db;
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly UrlEncoder _urlEncoder;
        public ILogger<Usuario> _logger { get; set; }

        public UsuariosController(DbContext context, UserManager<Usuario> userManager,
          SignInManager<Usuario> signInManager,
          UrlEncoder urlEncoder, ILogger<Usuario> logger)
        {
            _db = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _urlEncoder = urlEncoder;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var roles = _db.Set<IdentityRole>();
            var userRoles = roles.Join(_db.Set<IdentityUserRole<string>>(), u => u.Id, r => r.RoleId, (u, r) => new { u.Id, u.Name, r.UserId });
            var usuarios = _db.Set<Usuario>().Select(u => new UsuarioViewModel()
            {
                Id = u.Id,
                Nombre = u.UserName,
                Activo = u.Activo,
                Roles = userRoles.Where(r => r.UserId == u.Id).Select(r => new RoleViewModel()
                {
                    Id = r.Id,
                    Nombre = r.Name
                }).ToList(),
            }
            );
            return View(usuarios);
        }

        [HttpGet]
        public async Task<IActionResult> PasswordReset(string id)
        {
            var user = _db.Set<Usuario>().Find(id);
            if (user == null)
            {
                throw new ApplicationException($"No se encontro usuario con ID '{_userManager.GetUserId(User)}'.");
            }
            ViewBag.Usuario = user.UserName;
            var model = new ResetPasswordViewModel { Id = id };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PasswordReset(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _db.Set<Usuario>().Find(model.Id);
            if (user == null)
            {
                throw new ApplicationException($"No se encontro usuario con ID '{_userManager.GetUserId(User)}'.");
            }

            var addPasswordResult = _userManager.AddPasswordAsync(user, model.NewPassword).Result;
            if (!addPasswordResult.Succeeded)
            {
                TempData["error"] = "Error al resetear la contraseña";
                return View(model);
            }

            //await _signInManager.SignInAsync(user, isPersistent: false);
            TempData["exito"] = "Contraseña cambiada correctamente";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> ChangeActive(string id)
        {
            var user = _db.Set<Usuario>().Find(id);
            if (user == null)
            {
                throw new ApplicationException($"No se encontro usuario con ID '{_userManager.GetUserId(User)}'.");
            }
            user.Activo = !user.Activo;
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> CambiarRoles(string id)
        {
            var user = _db.Set<Usuario>().Find(id);
            if (user == null)
            {
                throw new ApplicationException($"No se encontro usuario con ID '{_userManager.GetUserId(User)}'.");
            }
            var roles = _db.Set<IdentityRole>();
            var userRoles = roles.Join(_db.Set<IdentityUserRole<string>>(), u => u.Id, r => r.RoleId, (u, r) => new { u.Id, u.Name, r.UserId });
            var data = new UsuarioViewModel()
            {
                Id = user.Id,
                Nombre = user.UserName,
                Activo = user.Activo,
                Roles = userRoles.Where(r => r.UserId == user.Id).Select(r => new RoleViewModel()
                {
                    Id = r.Id,
                    Nombre = r.Name
                }).ToList(),
            };
            ViewBag.RolesIds = new MultiSelectList(roles, "Id", "Name");
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> CambiarRoles(UsuarioViewModel usuarioVM)
        {
            var user = _userManager.FindByIdAsync(usuarioVM.Id);
            if (user == null)
            {
                throw new ApplicationException($"No se encontro usuario con ID '{_userManager.GetUserId(User)}'.");
            }
            var roles = _db.Set<IdentityRole>();
            var userRoles = roles.Join(_db.Set<IdentityUserRole<string>>(), u => u.Id, r => r.RoleId, (u, r) => new { u.Id, u.Name, r.UserId });
            foreach (var rol in userRoles)
            {
                await _userManager.RemoveFromRoleAsync(user.Result, rol.Name);
            }
            foreach (var rol in usuarioVM.RolesIds)
            {
                await _userManager.AddToRoleAsync(user.Result, roles.SingleOrDefault(r => r.Id == rol).Name);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Nuevo()
        {
            var roles = _db.Set<IdentityRole>();
            ViewBag.RolesIds = new MultiSelectList(roles, "Name", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Nuevo(NuevoUsuarioVM usuario)
        {
            var user = new Usuario
            {
                UserName = usuario.Nombre,
                Email = usuario.Nombre + "@patriarca.cu",
                Activo = usuario.Activo
            };
            var result = await _userManager.CreateAsync(user, usuario.Password);
            if (result.Succeeded)
            {
                _logger.LogInformation("Usuario creado correctamente.");
                foreach (var rol in usuario.RolesIds)
                {
                    await _userManager.AddToRoleAsync(user, rol);
                }
                _logger.LogInformation("Roles agregados correctamente.");
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction(nameof(Index));
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}