using Microsoft.AspNetCore.Mvc;
using MvcCoreLinqXML.Models;
using MvcCoreLinqXML.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreLinqXML.Controllers
{
    public class PeliculasController : Controller
    {
        private RepositoryPeliculas repo;

        public PeliculasController(RepositoryPeliculas repo)
        {
            this.repo = repo;
        }
        public IActionResult ListaPeliculas()
        {
            List<Pelicula> peliculas = this.repo.GetPeliculas();
            return View(peliculas);
        }

        public IActionResult EscenasPelicula(int idpelicula)
        {
            Escenas escena = this.repo.GetEscenasPelicula(idpelicula);
            return View(escena);
        }
    }
}
