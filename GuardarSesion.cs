using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabajo_Final
{
    internal class GuardarSesion
    {
        public string Gmail { get; set; }
        public bool MantenerSesion { get; set; }

        private static string archivo = "Sesion.json";

        public void SaveSesion(GuardarSesion sesion)
        {
            string json = JsonConvert.SerializeObject(sesion, Formatting.Indented);
            File.WriteAllText(archivo, json);


        }
        public static GuardarSesion LeerSesion()
        {
            if (File.Exists(archivo))
            {
                string json = File.ReadAllText(archivo);
                return JsonConvert.DeserializeObject<GuardarSesion>(json);
            }
            return null;
        }
        public static void CerrarSesion()
        {
            if (File.Exists(archivo))
            {
                File.Delete(archivo);
            }
        }
    }
}
