using BLL.Personas;
using Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    public partial class ListaPersonas : Form
    {
        public Control control { get; set; }
        public ListaPersonas(Control control)
        {
            InitializeComponent();

            this.control = control;

        }

        private void ListaPersonas_Load(object sender, EventArgs e)
        {

        }

        public void CargarGrid() {

            TableUsuarios.Rows.Clear();

            List<Persona> listado = PersonasBLL.Listar();

            for (int i = 0; i < listado.Count(); i++)
            {
                TableUsuarios.Rows.Add();
                TableUsuarios.Rows[i].Cells["Nombre"].Value = listado[i].Nombre;
                TableUsuarios.Rows[i].Cells["Identificacion"].Value = listado[i].Identificacion;

            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            control.AbrirFormUsuario();

            this.Visible = false;
        }
    }
}
