using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
//using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Trabajo_Final
{
    public partial class Form1 : Form
    {
        private Usuario usuario = new Usuario();
        private List<Usuario> usuarios;

        private string tipoUsuario;
        Label label1;

        enum TipoUser
        {
            Seleccionar,
            Estudiante,
            Profesor

        }

        public Form1(string tipo)
        {
            InitializeComponent();
            // label1.Text = "hola sebas";
            //tipouser.DataSource = Enum.GetValues(typeof(TipoUser));
            tipoUsuario = tipo;
            usuarios = usuario.Readjason();
            password.UseSystemPasswordChar = true;
            confirm.UseSystemPasswordChar = true;

            if (tipo == "Estudiante")
            {
                code.Enabled = true;
            }
            else // Profesor
            {
                label8.Visible = false;
                code.Visible = false;
            }
        }


        private void Crearlabel(TextBox textbox, string mensaje)
        {
            label1 = new Label();
            label1.Text = mensaje;

            //label1.Text = "El campo " + textbox.Tag + " es requerido";
            label1.Location = new Point(textbox.Location.X, textbox.Location.Y + textbox.Height);
            label1.ForeColor = Color.Blue;
            label1.AutoSize = true;
            Controls.Add(label1);
        }



        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



        private void CrearUsuario()
        {

            var isValid = ValidateForm();
            if (isValid)
            {
                bool correoExiste = usuarios.Any(u => u.Gmail.Equals(email.Text, StringComparison.OrdinalIgnoreCase));

                if (correoExiste)
                {
                    MessageBox.Show("El correo ingresado ya está registrado. Por favor, use otro o inicie sesión.",
                                    "Correo duplicado",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    return;
                }
                Usuario usuario = new Usuario();
                usuario.Nombre = name.Text;
                usuario.Gmail = email.Text;
                usuario.Password = password.Text;
                //usuario.TipoUsuario = tipouser.SelectedItem.ToString();
                usuario.TipoUsuario = tipoUsuario;  // <-- aquí se asigna automáticamente

                if (tipoUsuario == "Estudiante")
                {
                    usuario.CodigoEstudiantil = code.Text;
                    usuario.Edad = int.TryParse(confirm.Text, out int edad) ? edad : 0;
                }
                else
                {
                    usuario.CodigoEstudiantil = null;
                    usuario.Edad = 0;
                }
                usuarios.Add(usuario);

                usuario.SavetoJson(usuarios);
                DialogResult = DialogResult.OK;
                Close();

            }
        }

        private bool ValidateForm()
        {
            bool isValid = true;

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
                            isValid = false;
                        }
                        else if (textBox.Text.Length < 6)
                        {
                            Crearlabel(textBox, "El campo " + textBox.Tag + " debe tener al menos 6 caracteres");
                            isValid = false;
                        }
                    }
                    else if (textBox.Name == "confirm")
                    {
                        if (string.IsNullOrWhiteSpace(textBox.Text))
                        {
                            Crearlabel(textBox, "Debe confirmar la contraseña");
                            isValid = false;
                        }
                        else if (password.Text.Length >= 6 && password.Text != confirm.Text)
                        {
                            Crearlabel(textBox, "Las contraseñas no coinciden");
                            isValid = false;
                        }
                    }
                    else if (textBox.Name == "email")
                    {
                        if (!Regex.IsMatch(textBox.Text, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
                        {
                            Crearlabel(textBox, "Debe ser un correo válido");
                            isValid = false;
                        }
                    }
                    else if (string.IsNullOrWhiteSpace(textBox.Text))
                    {
                        // Si es DOCENTE y el campo es CODE, NO validar
                        if (tipoUsuario == "Profesor" && textBox.Name == "code")
                            continue;

                        Crearlabel(textBox, "El campo " + textBox.Tag + " es requerido");
                        isValid = false;
                    }
                }

            }

            return isValid;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CrearUsuario();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pictureboxClick(object sender, EventArgs e)
        {
            
        }

        private void mouseClick1(object sender, MouseEventArgs e)
        {
            password.UseSystemPasswordChar = false;
            confirm.UseSystemPasswordChar = false;

           
        }

        private void mouseLeave1(object sender, EventArgs e)
        {
            password.UseSystemPasswordChar = true;
            confirm.UseSystemPasswordChar = true;
        }
    }
}
