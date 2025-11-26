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
    public partial class AgregarEstudiante : Form
    {
        private Usuario gestorUsuarios = new Usuario();
        private List<Usuario> usuarios;
        public AgregarEstudiante()
        {
            InitializeComponent();
            button1.Click += button1_Click;
            button2.Click += button2_Click;

        }
        private string GenerarCodigo()
        {
            return "COD" + DateTime.Now.Ticks.ToString().Substring(8);
        }
        private void CrearLabel(TextBox textbox, string mensaje)
        {
            string labelName = "Error_" + textbox.Name;

            Label lblError = this.Controls[labelName] as Label;

            if (lblError == null)
            {
                lblError = new Label();
                lblError.Name = labelName;
                lblError.AutoSize = true;
                lblError.ForeColor = Color.Red;
                Controls.Add(lblError);
            }

            lblError.Text = mensaje;
            lblError.Location = new Point(textbox.Location.X, textbox.Location.Y + textbox.Height + 2);
            lblError.Visible = true;
        }
        private void QuitarLabel(TextBox textbox)
        {
            string labelName = "Error_" + textbox.Name;

            Label lblError = this.Controls[labelName] as Label;

            if (lblError != null)
                lblError.Visible = false;
        }
        private bool ValidateForm()
        {
            bool valido = true;

            foreach (Control control in Controls)
            {
                if (control is TextBox tb)
                {
                    if (string.IsNullOrWhiteSpace(tb.Text))
                    {
                        CrearLabel(tb, $"El campo {tb.Tag} es obligatorio");
                        valido = false;
                    }
                    else
                    {
                        QuitarLabel(tb);
                    }
                }
            }

            return valido;
        }
        private bool ExisteDuplicado(string gmail)
        {
            usuarios = gestorUsuarios.Readjason();

            return usuarios.Any(u =>
                u.Gmail.Equals(gmail, StringComparison.OrdinalIgnoreCase)
            );
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
                return;

            usuarios = gestorUsuarios.Readjason();

            if (ExisteDuplicado(textBox2.Text.Trim()))
            {
                MessageBox.Show(
                    "Ya existe un estudiante registrado con ese Gmail.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            Usuario nuevo = new Usuario()
            {
                Nombre = textBox1.Text.Trim(),
                Gmail = textBox2.Text.Trim(),
                CodigoEstudiantil = GenerarCodigo(),
                Notas = "Sin notas",
                Notas2 = "Sin notas",
                Notas3 = "Sin notas",
                Notas4 = "Sin notas",
                Notas5 = "Sin notas",
                Notas6 = "Sin notas",
                TipoUsuario = "Estudiante"
            };

            usuarios.Add(nuevo);
            gestorUsuarios.SavetoJson(usuarios);

            MessageBox.Show("Estudiante agregado correctamente.");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
