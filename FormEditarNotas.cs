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
            button1.Click += button1_Click;
            button2.Click += button2_Click;

        }
        private void CargarDatos()
        {
            lblNombre.Text = estudiante.Nombre;
            lblCorreo.Text = estudiante.Gmail;
            lblCodigo.Text = estudiante.CodigoEstudiantil;
            txtNotas.Text = estudiante.Notas;
            txtNotas2.Text = estudiante.Notas2;
            txtNotas3.Text = estudiante.Notas3;
            txtNotas4.Text = estudiante.Notas4;
            txtNotas5.Text = estudiante.Notas5;
            txtNotas6.Text = estudiante.Notas6;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            estudiante.Notas = txtNotas.Text;
            estudiante.Notas2 = txtNotas2.Text;
            estudiante.Notas3 = txtNotas3.Text;
            estudiante.Notas4 = txtNotas4.Text;
            estudiante.Notas5 = txtNotas5.Text;
            estudiante.Notas6 = txtNotas6.Text;

            usuarios = gestorUsuarios.Readjason();

            var original = usuarios.Find(u => u.CodigoEstudiantil == estudiante.CodigoEstudiantil);

            if (original != null)
            {
                original.Notas = estudiante.Notas;
                original.Notas2 = estudiante.Notas2;
                original.Notas3 = estudiante.Notas3;
                original.Notas4 = estudiante.Notas4;
                original.Notas5 = estudiante.Notas5;
                original.Notas6 = estudiante.Notas6;
                gestorUsuarios.SavetoJson(usuarios);
            }

            MessageBox.Show("Notas guardadas correctamente.");
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormEditarNotas_Load(object sender, EventArgs e)
        {

        }
    }
}
