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
    public class CursoController : Controller
    {
        private RepositoryCurso repo;

        public CursoController(RepositoryCurso repo)
        {
            this.repo = repo;
        }

        public IActionResult CompañerosCurso(string idcurso)
        {
            List<Usuario> compis = this.repo.GetCompañerosCurso(idcurso);
            return View(compis);
        }

        public IActionResult MiPerfil()
        {
            return View();
        }

        public IActionResult CrearUsuario()
        {
            return View();
        }

        public IActionResult EditarUsuario()
        {
            return View();
        }

        public IActionResult EliminarUsuario()
        {
            return View();
        }

    }
}
