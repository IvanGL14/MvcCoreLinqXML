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

        public IActionResult EscenasPelicula(int idpelicula, int? numescena)
        {
            List<Escenas> escenas = this.repo.GetEscenasPelicula(idpelicula);
            return View(escenas);
        }

        public IActionResult EscenasPeliculaPaginacion(int idpelicula, int? posicion)
        {
            if(posicion == null)
            {
                posicion = 0;
            }

            int numregistros = 0;
            Escenas escena = this.repo.GetEscenaPeliculaPaginacion(idpelicula, posicion.Value, ref numregistros);
            ViewData["NUMREGISTROS"] = numregistros;

            int siguiente = posicion.Value + 1;
            int anterior = posicion.Value - 1;

            if(siguiente > numregistros)
            {
                siguiente = 0;
            }

            if (anterior < 0)
            {
                anterior = numregistros - 1;
            }

            ViewBag.Siguiente = siguiente;
            ViewBag.Anterior = anterior;

            return View(escena);
        }

        public IActionResult _EscenasPeliculaPaginacionPartial(int idpelicula, int? posicion)
        {
            if (posicion == null)
            {
                posicion = 0;
            }

            int numregistros = 0;
            Escenas escena = this.repo.GetEscenaPeliculaPaginacion(idpelicula, posicion.Value, ref numregistros);
            ViewData["NUMREGISTROS"] = numregistros;

            int siguiente = posicion.Value + 1;
            int anterior = posicion.Value - 1;

            if (siguiente > numregistros)
            {
                siguiente = 0;
            }

            if (anterior < 0)
            {
                anterior = numregistros - 1;
            }

            ViewBag.Siguiente = siguiente;
            ViewBag.Anterior = anterior;

            return PartialView("_EscenasPeliculaPaginacionPartial", escena);
        }


        public IActionResult DetallesPelicula(int idpelicula)
        {
            Pelicula peli = this.repo.GetDetallesPelicula(idpelicula);
            return View(peli);
        }
    }
}
