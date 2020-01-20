using BLL.Historial;
using DAL.Historial;
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
    public partial class FormProductos : Form
    {
        public Control control { get; set; }

        public FormProductos(Control control)
        {
            InitializeComponent();

            this.control = control;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            control.AbrirFormAgregarProductos();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (this.tablaProductos.Columns[e.ColumnIndex].Name == "Modificar")
            {

                int IdBuscar = Convert.ToInt32(tablaProductos.Rows[tablaProductos.CurrentRow.Index].Cells[0].Value);
                this.Visible = false;
                control.AbrirFormAgregarProductos();
                control.LlenarFormAgregarProducto(IdBuscar);
            }


        }


        public void llenarGrid() {

            tablaProductos.Rows.Clear();

            List<Producto> listado = ProductoBLL.ListadoProdutos();

            for (int i = 0; i < listado.Count; i++)
            {
                tablaProductos.Rows.Add();
                tablaProductos.Rows[i].Cells["Id"].Value = listado[i].IdProducto;
                tablaProductos.Rows[i].Cells["Codigo"].Value = listado[i].Codigo;
                tablaProductos.Rows[i].Cells["NombreProducto"].Value = listado[i].NombreProducto;
                tablaProductos.Rows[i].Cells["Precio"].Value = listado[i].Precio;

            }

        }



        private void FormProductos_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            control.AbrirFormPrincipal();
            this.Visible = false;
        }
    }
}