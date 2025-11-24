using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
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
        private const string CodigoDocente = "ABC123";

        enum TipoUser
        {
            Seleccionar,
            Estudiante,
            Profesor

        }
        public FormLogin()
        {
            InitializeComponent();
            tipouser.DataSource = Enum.GetValues(typeof(TipoUser));
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
            string tipo = tipouser.SelectedItem.ToString();

            if (tipo == "Seleccionar")
            {
                MessageBox.Show("Seleccione un tipo de usuario.");
                return;
            }

            if (tipo == "Estudiante")
            {
                // Registro normal
                Form formularioRegistro = new Form1();
                formularioRegistro.ShowDialog();
                return;
            }

            if (tipo == "Profesor")
            {
                // Validar código
                if (txtcoddoc.Text == CodigoDocente)
                {
                    Form formularioRegistro = new Form1();
                    formularioRegistro.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Código de acceso incorrecto. Solo personal autorizado puede registrarse como docente.");
                }
            }
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


            String input = txtCódigo.Text.Trim();  // Puede ser código o correo


            var usuarios = usuario.Readjason();

            var user = usuarios.FirstOrDefault(u =>
                (
                    string.Equals(u.CodigoEstudiantil, input, StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(u.Gmail, input, StringComparison.OrdinalIgnoreCase)
                )
                && u.Password == contrasena
            );

            if (user != null)
            {
                MessageBox.Show($"Bienvenido {user.Nombre}!");

                var sesion = new GuardarSesion
                {
                    Gmail = user.Gmail,
                    MantenerSesion = true
                };
                sesion.SaveSesion(sesion);

                if (user.TipoUsuario == "Profesor")
                {
                    new Docente(user).Show();
                }
                else
                {
                    new PanelUsuario(user).Show();
                }

                this.Hide();
                return;
            }
            else
            {
                MessageBox.Show("Código/Correo o contraseña incorrectos");
                return;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void tipouser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tipouser.SelectedItem.ToString() == "Profesor")
            {
                txtcoddoc.Visible = true;
                label2.Visible = true;
            }
            else
            {
                txtcoddoc.Visible = false;
                label2.Visible = false;
                txtcoddoc.Text = "";
            }
        }
    }
}
