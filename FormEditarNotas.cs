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
    public partial class FormEditarNotas : Form
    {
        private Usuario estudiante;
        private Usuario gestorUsuarios = new Usuario();
        private List<Usuario> usuarios;
        public FormEditarNotas(Usuario user)
        {
            InitializeComponent();
            estudiante = user;
            CargarDatos();
        }
        private void CargarDatos()
        {
            lblNombre.Text = estudiante.Nombre;
            lblCorreo.Text = estudiante.Gmail;
            lblCodigo.Text = estudiante.CodigoEstudiantil;
            txtNotas.Text = estudiante.Notas;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            estudiante.Notas = txtNotas.Text;

            usuarios = gestorUsuarios.Readjason();

            var original = usuarios.Find(u => u.CodigoEstudiantil == estudiante.CodigoEstudiantil);

            if (original != null)
            {
                original.Notas = estudiante.Notas;
                gestorUsuarios.SavetoJson(usuarios);
            }

            MessageBox.Show("Notas guardadas correctamente.");
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
