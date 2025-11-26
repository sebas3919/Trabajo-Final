using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trabajo_Final
{
    public partial class FormLogin : Form
    {
        private Usuario usuario = new Usuario();
        private List<Usuario> usuarios;
        public FormLogin()
        {
            InitializeComponent();
        }

        private void Crearlabel(TextBox textbox, string mensaje)
        {
            label1 = new Label();
            label1.Text = mensaje;

            label1.Location = new Point(textbox.Location.X, textbox.Location.Y + textbox.Height);
            label1.ForeColor = Color.Blue;
            label1.AutoSize = true;
            Controls.Add(label1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form formularioRegistro = new Form1();
            formularioRegistro.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string usuarioIngresado = txtCódigo.Text.Trim();   // Puede ser Código o Gmail
            string contrasena = txtContrasena.Text.Trim();

            var usuarios = usuario.Readjason();

            // 1️ Buscar estudiante por código
            var estudiante = usuarios.FirstOrDefault(u =>
                u.TipoUsuario == "Estudiante" &&
                string.Equals(u.CodigoEstudiantil, usuarioIngresado, StringComparison.OrdinalIgnoreCase) &&
                u.Password == contrasena);

            // 2️ Buscar docente por correo
            var docente = usuarios.FirstOrDefault(u =>
                u.TipoUsuario == "Profesor" &&
                string.Equals(u.Gmail, usuarioIngresado, StringComparison.OrdinalIgnoreCase) &&
                u.Password == contrasena);

            if (estudiante != null)
            {
                MessageBox.Show($"Bienvenido {estudiante.Nombre}");

                PanelUsuario panel = new PanelUsuario(estudiante);
                panel.Show();
                this.Hide();
                return;
            }

            if (docente != null)
            {
                MessageBox.Show($"Bienvenido profesor {docente.Nombre}");

                Docente formDocente = new Docente(docente);   // ⬅ AHORA docActual NO ES NULL
                formDocente.Show();
                this.Hide();
                return;
            }

            // Si no coincide nada
            MessageBox.Show("Usuario o contraseña incorrectos");
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtCódigo_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void txtContrasena_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
