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
    public partial class FormAgregarProductos : Form
    {
        public Control control { get; set; }

        public FormAgregarProductos(Control control)
        {
            InitializeComponent();
            this.control = control;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (txtProducto.Text == "" || txtCodigo.Text == "" || txtPrecio.Text == "")
            {
                MessageBox.Show("No puedes dejar campos en blanco", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            else {

                Producto p = new Producto()
                {
                    Codigo = txtCodigo.Text,
                    NombreProducto = txtProducto.Text,
                    Precio = Convert.ToDouble(txtPrecio.Text)

                };

                Resultado r = ProductoBLL.AgregarProducto(p);

                if (r.Codigo == 1)
                {
                    MessageBox.Show("Producto agregado correctamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(r.Mensaje, "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                control.AbrirFormProductos();

            }


            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            control.AbrirFormProductos();
            this.Visible = false;

            limpiarTxt();
        }

        private void FormAgregarProductos_Load(object sender, EventArgs e)
        {

        }


        public void AparecerBotonAgregar() {
            btnAgregar.Visible = true;
            btnModificar.Visible = false;

        }




        public void cargarParaModificar(int idProducto) {
            btnAgregar.Visible = false;

            btnModificar.Visible = true;

            Producto p = ProductoBLL.BuscarProductoIdentificacion(idProducto);

            if (p != null)
            {
                txtId.Text = p.IdProducto.ToString();
                txtCodigo.Text = p.Codigo;
                txtProducto.Text = p.NombreProducto;
                txtPrecio.Text = p.Precio.ToString();
            }

        }


        public void limpiarTxt() {

            txtCodigo.Text = "";
            txtProducto.Text = "";

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (txtProducto.Text == "" || txtCodigo.Text == "" || txtPrecio.Text == "")
            {
                MessageBox.Show("No puedes dejar campos en blanco", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            else
            {

                Producto p = new Producto()
                {
                    IdProducto = Convert.ToInt32(txtId.Text),
                    Codigo = txtCodigo.Text,
                    NombreProducto = txtProducto.Text,
                    Precio = Convert.ToDouble(txtPrecio.Text)
                };

                Resultado r = ProductoBLL.ModificarProducto(p);

                if (r.Codigo == 1)
                {
                    MessageBox.Show("Producto Modificado correctamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(r.Mensaje, "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                control.AbrirFormProductos();

            }
        }
    }
}
