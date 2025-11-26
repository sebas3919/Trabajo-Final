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
    public partial class Docente : Form
    {
        private Usuario docenteActual;
        private Usuario usuario = new Usuario();
        private List<Usuario> usuarios;
        public Docente()
        {
            InitializeComponent();
            

        }

        public Docente(Usuario docente)
        {
            InitializeComponent();
            docenteActual = docente;  // <-- AQUÍ SE ASIGNA
            ConfigurarDataGridUsuarios();
            CargarUsuarios();
        }

        private void ConfigurarDataGridUsuarios()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add("Nombre", "Nombre");
            dataGridView1.Columns["Nombre"].DataPropertyName = "Nombre";

            dataGridView1.Columns.Add("Gmail", "Gmail");
            dataGridView1.Columns["Gmail"].DataPropertyName = "Gmail";

            dataGridView1.Columns.Add("Notas", "Notas");
            dataGridView1.Columns["Notas"].DataPropertyName = "Notas";

            // NOTA 2
            dataGridView1.Columns.Add("Nota2", "Nota 2");
            dataGridView1.Columns["Nota2"].DataPropertyName = "Notas2";

            // NOTA 3
            dataGridView1.Columns.Add("Nota3", "Nota 3");
            dataGridView1.Columns["Nota3"].DataPropertyName = "Notas3";

            // NOTA 4
            dataGridView1.Columns.Add("Nota4", "Nota 4");
            dataGridView1.Columns["Nota4"].DataPropertyName = "Notas4";

            // NOTA 5
            dataGridView1.Columns.Add("Nota5", "Nota 5");
            dataGridView1.Columns["Nota5"].DataPropertyName = "Notas5";

            // NOTA 6
            dataGridView1.Columns.Add("Nota6", "Nota 6");
            dataGridView1.Columns["Nota6"].DataPropertyName = "Notas6";


            // Botón Editar
            DataGridViewButtonColumn btnEditar = new DataGridViewButtonColumn();
            btnEditar.Name = "Editar";
            btnEditar.HeaderText = "Editar";
            btnEditar.Text = "Editar";
            btnEditar.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(btnEditar);

            // Botón Eliminar
            DataGridViewButtonColumn btnEliminar = new DataGridViewButtonColumn();
            btnEliminar.Name = "Eliminar";
            btnEliminar.HeaderText = "Eliminar";
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(btnEliminar);

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }

        private void CargarUsuarios()
        {
            var lista = usuario.Readjason();

            // Filtrar solo estudiantes
            usuarios = lista
                .Where(x => x.TipoUsuario == "Estudiante")
                .ToList();

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = usuarios;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            AgregarEstudiante form = new AgregarEstudiante();

            if (form.ShowDialog() == DialogResult.OK)
            {
                CargarUsuarios();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            EditarPerfilDocente editardocente = new EditarPerfilDocente(docenteActual);

            if (editardocente.ShowDialog() == DialogResult.OK)
            {
                // Recargar por si cambió nombre o contraseña del docente
                var lista = usuario.Readjason();
                docenteActual = lista.FirstOrDefault(x => x.Gmail == docenteActual.Gmail);

            }
        }

        private void OnCellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // EDITAR
            if (e.ColumnIndex == dataGridView1.Columns["Editar"].Index)
            {
                var usuarioSeleccionado = usuarios[e.RowIndex];

                FormEditarNotas formEditar = new FormEditarNotas(usuarioSeleccionado);
                formEditar.ShowDialog();

                CargarUsuarios();
            }

            // ELIMINAR
            if (e.ColumnIndex == dataGridView1.Columns["Eliminar"].Index)
            {

                var usuarioSeleccionado = usuarios[e.RowIndex];

                // 1. Cargar TODOS los usuarios
                var listaCompleta = usuario.Readjason();

                // 2. Buscar el usuario por su Gmail o Código (mejor Gmail)
                var userInJson = listaCompleta.FirstOrDefault(u => u.Gmail == usuarioSeleccionado.Gmail);

                if (userInJson != null)
                    listaCompleta.Remove(userInJson);

                // 3. Guardar la lista completa
                usuario.SavetoJson(listaCompleta);

                // 4. Recargar solo los estudiantes al DataGridView
                CargarUsuarios();
                /*var usuarioSeleccionado = usuarios[e.RowIndex];

                usuarios.Remove(usuarioSeleccionado);
                usuario.SavetoJson(usuarios);

                CargarUsuarios();*/
            }

        }
    }
}
