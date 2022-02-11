using Microsoft.AspNetCore.Mvc;
using MvcCoreLinqXML.Models;
using MvcCoreLinqXML.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreLinqXML.Controllers
{
    public class ClientesController : Controller
    {
        private RepositoryCliente repo;

        public ClientesController(RepositoryCliente repo)
        {
            this.repo = repo;
        }
        public IActionResult ListaClientes()
        {
            List<Cliente> clientes = this.repo.GetClientes();
            return View(clientes);
        }

        public IActionResult Details(int idcliente)
        {
            Cliente cliente = this.repo.FindCliente(idcliente);
            return View(cliente);
        }

        public IActionResult Editar(int idcliente)
        {
            Cliente cliente = this.repo.FindCliente(idcliente);
            return View(cliente);
        }

        [HttpPost]
        public IActionResult Editar(Cliente cliente)
        {
            this.repo.UpdateCliente(cliente.IdCliente, cliente.Nombre, cliente.Direccion, cliente.Email, cliente.Imagen);
            return RedirectToAction("ListaClientes");
        }

        public IActionResult Delete(int idcliente)
        {
            this.repo.DeleteCliente(idcliente);
            return RedirectToAction("ListaClientes");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Cliente cliente)
        {
            this.repo.AddCliente(cliente.IdCliente, cliente.Nombre, cliente.Direccion, cliente.Email, cliente.Imagen);
            return RedirectToAction("ListaClientes");
        }
    }
}
