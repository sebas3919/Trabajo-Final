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
            string codigo = txtCódigo.Text.Trim();
            string contrasena = txtContrasena.Text.Trim();
            


            foreach (var control in Controls)
            {
                if (control is TextBox textbox)
                {
                    TextBox textBox = (TextBox)control;
                    if (textBox.Name == "password")
                    {
                        if (string.IsNullOrWhiteSpace(textBox.Text))
                        {
                            Crearlabel(textBox, "El campo " + textBox.Tag + " es requerido");

                        }
                        else if (textBox.Text.Length < 6)
                        {
                            Crearlabel(textBox, "El campo " + textBox.Tag + " debe tener al menos 6 caracteres");

                        }
                    }

                    /* else if (textBox.Name == "email")
                     {
                         if (!Regex.IsMatch(textBox.Text, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
                         {
                             Crearlabel(textBox, "Debe ser un correo válido");

                         }
                     }*/
                  

                        else if (string.IsNullOrWhiteSpace(textBox.Text))
                    {
                        Crearlabel(textBox, "El campo " + textBox.Tag + " es requerido");

                    }
                }

            }


            var usuarios = usuario.Readjason();
            var user = usuarios.FirstOrDefault(u =>
               string.Equals(u.CodigoEstudiantil, txtCódigo.Text, StringComparison.OrdinalIgnoreCase) &&
               u.Password == txtContrasena.Text);

            if (user != null)
            {
                MessageBox.Show($"Bienvenido {user.Nombre}!");
                var sesion = new GuardarSesion { Gmail = user.CodigoEstudiantil, MantenerSesion = true };
                sesion.SaveSesion(sesion);

                PanelUsuario panel = new PanelUsuario(user);
                panel.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Correo o contraseña incorrectos");
            }


       
            /*else
                new PanelProfesor(usuario).Show();*/

            this.Hide();
        }
    }
}
