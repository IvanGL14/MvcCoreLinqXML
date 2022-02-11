using MvcCoreLinqXML.Models;
using MvcCoreLinqXML.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MvcCoreLinqXML.Repositories
{
    public class RepositoryPeliculas
    {
        private XDocument docuPeliculas;
        private XDocument docuEscenas;
        private string path = "";
        private string pathEscenas = "";

        public RepositoryPeliculas(PathProvider pathProvider)
        {
            this.path = pathProvider.MapPath("peliculas.xml", Folders.Documents);
            this.pathEscenas = pathProvider.MapPath("escenaspeliculas.xml", Folders.Documents);
            this.docuPeliculas = XDocument.Load(path);
            this.docuEscenas = XDocument.Load(pathEscenas);
        }

        public List<Pelicula> GetPeliculas()
        {
            var consulta = from datos in this.docuPeliculas.Descendants("pelicula")
                           select datos;
            List<Pelicula> peliculas = new List<Pelicula>();

            foreach (var dato in consulta)
            {
                Pelicula peli = new Pelicula();
                //PARA ACCEDER A UNA ETIQUETA UTILIZAMOS Element
                //PARA ACCEDER A UN ATRIBUTO UTILIZAMOS Attribute
                peli.IdPelicula = int.Parse(dato.Attribute("idpelicula").Value);
                peli.Titulo = dato.Element("titulo").Value;
                peli.TituloOriginal = dato.Element("titulooriginal").Value;
                peli.Descripcion = dato.Element("descripcion").Value;
                peli.Poster = dato.Element("poster").Value;

                peliculas.Add(peli);
            }

            return peliculas;
        }

        public Escenas GetEscenasPelicula(int idpelicula)
        {
            var consulta = from datos in this.docuEscenas.Descendants("escena")
                           where datos.Attribute("idpelicula").Value == idpelicula.ToString()
                           select datos;

            XElement dato = consulta.FirstOrDefault();
            Escenas escena = new Escenas
            {
                IdPelicula = int.Parse(dato.Attribute("idpelicula").Value),
                Titulo = dato.Element("titulo").Value,
                Desripcion = dato.Element("descripcion").Value,
                Imagen = dato.Element("imagen").Value,
             };

            return escena;
        }
    }
}
