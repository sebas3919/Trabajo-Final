using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trabajo_Final
{
    public partial class EditarPerfilDocente : Form
    {
        private Usuario docente;
        private Usuario gestor = new Usuario();
        private List<Usuario> usuarios;


        public EditarPerfilDocente(Usuario u)
        {
            InitializeComponent();
            docente = u;
            CargarDatos();
            
        }

        private void CargarDatos()
        {
            textBox1.Text = docente.Nombre;
            textBox2.Text = docente.Gmail;
            textBox3.Text = docente.Password;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            usuarios = gestor.Readjason();

            // Buscar docente por su Gmail ORIGINAL
            var original = usuarios.FirstOrDefault(x => x.Gmail == docente.Gmail);

            if (original != null)
            {
                original.Nombre = textBox1.Text.Trim();
                original.Password = textBox3.Text.Trim();

                gestor.SavetoJson(usuarios);

                // Actualizar los datos del objeto en memoria
                docente.Nombre = original.Nombre;
                docente.Password = original.Password;

                MessageBox.Show("Perfil actualizado correctamente.");
            }
            else
            {
                MessageBox.Show("Error: No se encontró el registro del docente.");
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
