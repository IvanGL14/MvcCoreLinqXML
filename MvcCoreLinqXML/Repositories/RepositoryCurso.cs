using MvcCoreLinqXML.Models;
using MvcCoreLinqXML.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MvcCoreLinqXML.Repositories
{
    public class RepositoryCurso
    {
        private XDocument docuCursos;
        private string path = "";

        public RepositoryCurso()
        {
            this.path = PathProvider.MapPath("Cursos.xml", Folders.Documents);
            this.docuCursos = XDocument.Load(path);
        }

        public List<Usuario> GetUsuarios(int idcurso)
        {
            var consulta = from datos in this.docuCursos.Descendants("curso")
                           where datos.Parent.Attribute("idcurso").Value == idcurso.ToString()
                           select datos;

            List<Usuario> usuarios = new List<Usuario>();

            foreach (var dato in consulta)
            {
                Usuario usu = new Usuario();
                //PARA ACCEDER A UNA ETIQUETA UTILIZAMOS Element
                //PARA ACCEDER A UN ATRIBUTO UTILIZAMOS Attribute
                usu.IdUsuario = int.Parse(dato.Attribute("idusuario").Value);
                usu.Nombre = dato.Element("nombre").Value;
                usu.Apellidos = dato.Element("apellidos").Value;
                usu.Nota = int.Parse(dato.Element("nota").Value);
                usu.Password = int.Parse(dato.Element("password").Value);
                usu.Username = dato.Element("username").Value;
                usu.Perfil = dato.Element("perfil").Value;

                usuarios.Add(usu);
            }
            return usuarios;
        }

        public List<Curso> GetCursos()
        {
            var consulta = from datos in this.docuCursos.Descendants("curso")
                           select datos;
            List<Curso> cursos = new List<Curso>();

            foreach (var dato in consulta)
            {
                Curso curso = new Curso();
                //PARA ACCEDER A UNA ETIQUETA UTILIZAMOS Element
                //PARA ACCEDER A UN ATRIBUTO UTILIZAMOS Attribute
                curso.IdCurso = int.Parse(dato.Attribute("idcurso").Value);
                curso.Titulo = dato.Element("titulo").Value;
                curso.Edicion = dato.Element("poster").Value;
                curso.Turno = dato.Element("descripcion").Value;
                curso.Contenidos = dato.Element("titulooriginal").Value;

                cursos.Add(curso);
            }
            return cursos;
        }
    }
}
