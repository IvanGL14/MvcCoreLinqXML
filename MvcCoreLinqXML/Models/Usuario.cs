using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreLinqXML.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Perfil { get; set; }
        public int Nota { get; set; }
        public string Username { get; set; }
        public int Password { get; set; }
    }
}
