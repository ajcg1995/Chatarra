using BLL.Historial;
using DAL.Historial;
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
    public partial class ListaProductos : Form
    {
        public Control control { get; set; }

        public ListaProductos(Control control)
        {
            InitializeComponent();
            this.control = control;
            cargarGrid();
        }

        public void cargarGrid() {
            tablaProductos.Rows.Clear();



            List<Producto> listado = ProductoBLL.ListadoProdutos();


            for (int i = 0; i < listado.Count(); i++)
            {
                tablaProductos.Rows.Add();
                tablaProductos.Rows[i].Cells["Codigo"].Value = listado[i].Codigo;
                tablaProductos.Rows[i].Cells["Producto"].Value = listado[i].NombreProducto;

            }


        }


        private void ListaProductos_Load(object sender, EventArgs e)
        {

        }

        private void lstProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            string valorSeleccionado = lstProductos.ValueMember.;
            if (valorSeleccionado != "")
            {


                //control.buscarProducto(valorSeleccionado);
            }*/
        }

        private void tablaProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string codigoSeleccionado = tablaProductos.Rows[tablaProductos.CurrentRow.Index].Cells[0].Value.ToString();

            control.buscarProducto(codigoSeleccionado);

            this.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }
    }
}

