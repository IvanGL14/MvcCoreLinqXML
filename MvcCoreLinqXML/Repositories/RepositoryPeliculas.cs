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

        public RepositoryPeliculas()
        {
            this.path = PathProvider.MapPath("peliculas.xml", Folders.Documents);
            this.pathEscenas = PathProvider.MapPath("escenaspeliculas.xml", Folders.Documents);
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

        public Pelicula GetDetallesPelicula(int idpelicula)
        {

            var consulta = from datos in this.docuPeliculas.Descendants("pelicula")
                           where datos.Attribute("idpelicula").Value == idpelicula.ToString()
                           select datos;

            XElement dato = consulta.FirstOrDefault();
            Pelicula pelicula = new Pelicula
            {
                IdPelicula = int.Parse(dato.Attribute("idpelicula").Value),
                Titulo = dato.Element("titulo").Value,
                TituloOriginal = dato.Element("titulooriginal").Value,
                Descripcion = dato.Element("descripcion").Value,
                Poster = dato.Element("poster").Value
            };

            return pelicula;
        }

        public List<Escenas> GetEscenasPelicula(int idpelicula)
        {
            
            var consulta = from datos in this.docuEscenas.Descendants("escena")
                           where datos.Attribute("idpelicula").Value == idpelicula.ToString()
                           select datos;

           
            List<Escenas> escenas = new List<Escenas>();
            foreach(XElement dato in consulta)
            {
                Escenas escena = new Escenas
                {
                    IdPelicula = int.Parse(dato.Attribute("idpelicula").Value),
                    Titulo = dato.Element("tituloescena").Value,
                    Desripcion = dato.Element("descripcion").Value,
                    Imagen = dato.Element("imagen").Value,
                };
                escenas.Add(escena);
             };
            return escenas;
        }

        public Escenas GetEscenaPeliculaPaginacion(int idpelicula, int posicion, ref int numescenas)
        {


            var consulta = from datos in this.docuEscenas.Descendants("escena")
                            where datos.Attribute("idpelicula").Value == idpelicula.ToString()
                            select datos;


            List<Escenas> escenas = new List<Escenas>();
            foreach (XElement dato in consulta)
            {
                Escenas escena = new Escenas
                {
                    IdPelicula = int.Parse(dato.Attribute("idpelicula").Value),
                    Titulo = dato.Element("tituloescena").Value,
                    Desripcion = dato.Element("descripcion").Value,
                    Imagen = dato.Element("imagen").Value,
                };
                escenas.Add(escena);
            };

            numescenas = escenas.Count();
            Escenas escena1 = escenas.Skip(posicion).Take(1).SingleOrDefault();
            return escena1;
        }

        public int NumEscenas(int idpelicula)
        {
            int numregistros = (from datos in this.docuEscenas.Descendants("escena")
                                where datos.Attribute("idpelicula").Value == idpelicula.ToString()
                                select datos).Count();

            return numregistros;
        }
    }
}
