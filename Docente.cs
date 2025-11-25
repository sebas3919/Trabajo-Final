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
            //docenteActual = docente;
            //ConfigurarDataGridUsuarios();
            //CargarUsuarios();
            
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
        /*private void eliminarEstudiante()
        {
            usuarios = usuario.Readjason();
            dataGridView1.DataSource = null; // Limpiar la fuente de datos
            dataGridView1.DataSource = usuarios; // Asignar la nueva lista como fuente de datos
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Ajustar el tamaño de las columnas
            dataGridView1.AutoGenerateColumns = false;
            // agregar boton editar
            if (!dataGridView1.Columns.Contains("Eliminar"))
            {
                DataGridViewButtonColumn btnEliminar = new DataGridViewButtonColumn();
                btnEliminar.HeaderText = "Eliminar";
                btnEliminar.Name = "Eliminar";
                btnEliminar.Text = "Eliminar";
                btnEliminar.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Add(btnEliminar);
            }
        }*/




        private void CargarUsuarios()
        {
            var lista = usuario.Readjason();

            // Filtrar solo estudiantes
            var soloEstudiantes = lista
                .Where(x => x.TipoUsuario == "Estudiante")
                .ToList();

            dataGridView1.DataSource = soloEstudiantes;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
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

                usuarios.Remove(usuarioSeleccionado);
                usuario.SavetoJson(usuarios);

                CargarUsuarios();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AgregarEstudiante form = new AgregarEstudiante();

            if (form.ShowDialog() == DialogResult.OK)
            {
                CargarUsuarios();
            }
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
    }
}
