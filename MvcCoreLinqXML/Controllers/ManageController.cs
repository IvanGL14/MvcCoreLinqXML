using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MvcCoreLinqXML.Models;
using MvcCoreLinqXML.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MvcCoreLinqXML.Controllers
{
    public class ManageController : Controller
    {
        private RepositoryCurso repo;

        public ManageController(RepositoryCurso repo)
        {
            this.repo = repo;
        }

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            Usuario user = this.repo.FindUsuario(username, password);

            if (user == null)
            {
                ViewData["MENSAJE"] = "No se ha encontrado ningún usuario";
                return View();
            }
            else
            {
                ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);

                Claim claimName = new Claim(ClaimTypes.Name, user.Username);
                Claim claimId = new Claim(ClaimTypes.NameIdentifier, user.IdUsuario.ToString());
                Claim claimRol = new Claim(ClaimTypes.Role, user.Perfil);
                Claim claimCurso = new Claim("Curso", user.IdCurso);


                ClaimsPrincipal userPrincipal = new ClaimsPrincipal(identity);

                identity.AddClaim(claimName);
                identity.AddClaim(claimId);
                identity.AddClaim(claimRol);
                identity.AddClaim(claimCurso);


                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal);
                //var controller = HttpContext.Request.Cookies["controller"];
                //var action = HttpContext.Request.Cookies["action"];
                //var inscripcion = HttpContext.Request.Cookies["inscripcion"];

                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ErrorAcceso()
        {
            return View();
        }

    }
}
