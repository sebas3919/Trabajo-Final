using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Xml;

namespace Trabajo_Final
{
    public class Usuario
    {
        private string archivo = "usuario.json";

        public string Nombre { get; set; }
        public string Gmail { get; set; }
        public string Password { get; set; }

        public string TipoUsuario { get; set; } // "Estudiante" o "Profesor"

        // Solo estudiante
        public string CodigoEstudiantil { get; set; }
        public int Edad { get; set; }

        public void SavetoJson(List<Usuario> usuarios)
        {
            string json = JsonConvert.SerializeObject(usuarios, Formatting.Indented);
            File.WriteAllText(archivo, json);

        }

        public List<Usuario> Readjason()
        {
            if (File.Exists(archivo))
            {
                string json = File.ReadAllText(archivo);
                return JsonConvert.DeserializeObject<List<Usuario>>(json);
            }
            return new List<Usuario>();
        }
    }
}
