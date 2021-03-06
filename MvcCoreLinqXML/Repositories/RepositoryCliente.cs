using MvcCoreLinqXML.Models;
using MvcCoreLinqXML.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MvcCoreLinqXML.Repositories
{
    public class RepositoryCliente
    {
        private XDocument document;
        private string path;

        public RepositoryCliente()
        {
            this.path = PathProvider.MapPath("ClientesID.xml", Folders.Documents);
            this.document = XDocument.Load(path);
        }

        public List<Cliente> GetClientes()
        {
            var consulta = from datos in this.document.Descendants("CLIENTE")
                           select datos;
            List<Cliente> clientes = new List<Cliente>();

            foreach (var element in consulta)
            {
                Cliente cliente = new Cliente();
                //PARA ACCEDER A UNA ETIQUETA UTILIZAMOS Element
                //PARA ACCEDER A UN ATRIBUTO UTILIZAMOS Attribute
                cliente.IdCliente = int.Parse(element.Attribute("IDCLIENTE").Value);
                cliente.Nombre = element.Element("NOMBRE").Value;
                cliente.Direccion = element.Element("DIRECCION").Value;
                cliente.Email = element.Element("EMAIL").Value;
                cliente.Imagen = element.Element("IMAGENCLIENTE").Value;

                clientes.Add(cliente);
            }

            return clientes;
        }

        public Cliente FindCliente(int idcliente)
        {
            var consulta = from datos in this.document.Descendants("CLIENTE")
                           where datos.Element("IDCLIENTE").Value == idcliente.ToString()
                           select datos;

            XElement dato = consulta.FirstOrDefault();
            Cliente cliente = new Cliente
            {
                IdCliente = int.Parse(dato.Element("IDCLIENTE").Value),
                Nombre = dato.Element("NOMBRE").Value,
                Direccion = dato.Element("DIRECCION").Value,
                Email = dato.Element("EMAIL").Value,
                Imagen = dato.Element("IMAGENCLIENTE").Value
            };

            return cliente;
        }

        private XElement FindXElementCliente(string idcliente)
        {
            var consulta = from datos in this.document.Descendants("CLIENTE")
                           where datos.Element("IDCLIENTE").Value == idcliente.ToString()
                           select datos;

            return consulta.FirstOrDefault();
        }

        public void DeleteCliente(int id)
        {
            XElement xElement = this.FindXElementCliente(id.ToString());
            xElement.Remove();
            this.document.Save(this.path);
        }

        public void UpdateCliente(int id, string nombre, string direccion, string email, string imagen)
        {
            XElement xElement = this.FindXElementCliente(id.ToString());
            xElement.Element("NOMBRE").Value = nombre;
            xElement.Element("DIRECCION").Value = direccion;
            xElement.Element("EMAIL").Value = email;
            xElement.Element("IMAGENCLIENTE").Value = imagen;
            
            this.document.Save(this.path);
        }

        public void AddCliente(int id, string nombre, string direccion, string email, string imagen)
        {
            XElement rootCliente = new XElement("CLIENTE");
            //DEBEMOS SEGUIR EL ORDEN DE ESTRUCTURA DE ETIQUETAS DEL XML
            rootCliente.Add(new XElement("IDCLIENTE", id));
            rootCliente.Add(new XElement("NOMBRE", nombre));
            rootCliente.Add(new XElement("DIRECCION", direccion));
            rootCliente.Add(new XElement("EMAIL", email));
            rootCliente.Add(new XElement("IMAGENCLIENTE", imagen));

            //INSERTAMOS DENTRO DEL DOCUMENT, EN EL NIVEL QUE CORRESPONDA
            //EN ESTE EJEMPLO, DIRECTAMENTE EN LA RAIZ
            this.document.Element("CLIENTES").Add(rootCliente);
            this.document.Save(this.path);
        }
    }
}
