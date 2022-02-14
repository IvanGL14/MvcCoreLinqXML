using MvcCoreLinqXML.Models;
using MvcCoreLinqXML.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MvcCoreLinqXML.Repositories
{
    public class RepositoryJoyerias
    {
        private XDocument document;

        public RepositoryJoyerias()
        {
            string filename = "joyerias.xml";
            string path = PathProvider.MapPath(filename, Folders.Documents);
            this.document = XDocument.Load(path);
        }

        public List<Joyeria> GetJoyerias()
        {
            var consulta = from datos in this.document.Descendants("joyeria")
                           select datos;
            List<Joyeria> joyerias = new List<Joyeria>();

            foreach(var element in consulta)
            {
                Joyeria joye = new Joyeria();
                //PARA ACCEDER A UNA ETIQUETA UTILIZAMOS Element
                //PARA ACCEDER A UN ATRIBUTO UTILIZAMOS Attribute
                joye.Nombre = element.Element("nombrejoyeria").Value;
                joye.CIF = element.Attribute("cif").Value;
                joye.Telefono = element.Element("telf").Value;
                joye.Direccion = element.Element("direccion").Value;
                joyerias.Add(joye);
            }

            return joyerias;
        }
    }
}
