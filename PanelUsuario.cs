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
    public partial class PanelUsuario : Form
    {
        private Usuario usuario;
        public PanelUsuario(Usuario user)
        {
            InitializeComponent();

            usuario = user;

            label1.Text = $"Hola {usuario.TipoUsuario} {usuario.Nombre}";
            label2.Text = usuario.Gmail;
            label3.Text = usuario.CodigoEstudiantil;
        }

        private void PanelUsuario_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            GuardarSesion.CerrarSesion();

            new FormLogin().Show();
            this.Close();
        }
    }
}
