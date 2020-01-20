using BLL.Historial;
using BLL.Personas;
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
    public partial class FormAgregarHistoria : Form
    {
        public Control control { get; set; }


        public FormAgregarHistoria(Control control)
        {
            InitializeComponent();

            this.control = control;



            //cargar comboBox

            List<IVA> ListadoIva = new List<IVA>();
            ListadoIva.Add(new IVA { detalle = "0%", porcentaje = 0 });
            ListadoIva.Add(new IVA { detalle = "4%", porcentaje = 4 });
            ListadoIva.Add(new IVA { detalle = "8%", porcentaje = 8 });
            ListadoIva.Add(new IVA { detalle = "13%", porcentaje = 13 });


            cmbIVA.DataSource = ListadoIva;

            cmbIVA.DisplayMember = "detalle";
            cmbIVA.ValueMember = "porcentaje";



            ObtenerNumeroFactura();


        }

        public void ObtenerNumeroFactura() {

            //Obtener numeroFactura
            Factura f = HistorialBLL.ultimoRegistro();

            if (f != null)
            {
                lblNumFactura.Text = (f.NumeroFactura + 1).ToString();

            }
            else
            {
                lblNumFactura.Text = "1";
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text == "" ||  txtCedula.Text == "" || txtVenta.Text =="" || txtDescuento.Text == "" || 
                txtSubTotal.Text == "" ||txtIVA.Text == "" || txtTotalVenta.Text == "" || tablaLinea.RowCount <= 0)
            {
                MessageBox.Show("No puedes dejar campos en blanco");
            }
            else
            {

                Factura historial = new Factura
                {
                    Identificacion = txtCedula.Text,
                    Fecha = DateTime.Now.ToString("yyyy-MM-dd"),
                    Hora = DateTime.Now.ToString("HH:mm:ss"),
                    TotalVenta = Convert.ToDouble(txtTotalVenta.Text),
                    Descuento = Convert.ToDouble(txtDescuento.Text),
                    SubTotal = Convert.ToDouble(txtSubTotal.Text),
                    IVA = Convert.ToDouble(txtIVA.Text),
                    PorcentajeIVA = cmbIVA.SelectedValue.ToString()+"%"
                };

                //Se inserta la factura
                Resultado r = HistorialBLL.InsertarHistorial(historial);

                if (r.Codigo == 1)
                {
                    // insetar linea factura
                    for (int i = 0; i < tablaLinea.RowCount; i++)
                    {
                        LineaDetalle l = new LineaDetalle
                        {
                            NumeroFactura = Convert.ToInt32(lblNumFactura.Text),
                            IdLinea = i + 1,
                            IdProducto = Convert.ToInt32(tablaLinea.Rows[i].Cells["Codigo"].Value),
                            Cantidad = Convert.ToInt32(tablaLinea.Rows[i].Cells["Cantidad"].Value),
                            PrecioProducto = Convert.ToDouble(tablaLinea.Rows[i].Cells["PrecioUnit"].Value)                            
                        };
                        ProductoBLL.InsertarLinea(l);
                    }
                                       

                    ObtenerNumeroFactura();

                    
                    //llenar objetos para crear el ticket

                    Persona persona = PersonasBLL.BuscarUnaPersona(txtCedula.Text);

                    Factura factura = HistorialBLL.ultimoRegistro();

                    List<ProductosLineaDetalle> ListadoProductosLineaDetalle = ProductoBLL.ListadoProductosLineaDetalle(factura.NumeroFactura);

                    control.CrearTickete(persona, ListadoProductosLineaDetalle, factura);

                    MessageBox.Show("Tomar el tiquete", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    limpiarTxt();

                    this.Visible = false;

                    control.AbrirFormPrincipal();


                }
                else {
                    MessageBox.Show(r.Mensaje, "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);


                }

            }

        }

        private void FormAgregarHistoria_Load(object sender, EventArgs e)
        {

        }

        public void CargarDatosUsuario(Persona persona) {

            txtNombre.Text = persona.Nombre;
            txtCedula.Text = persona.Identificacion;
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {

        }

        public void limpiarTxt() {

            txtCodigo.Text = "";
            txtNombre.Text = "";
            txtCedula.Text = "";
            txtVenta.Text = "";
            txtDescuento.Text = "";
            txtSubTotal.Text = "";
            txtIVA.Text = "";
            txtTotalVenta.Text = "";


            tablaLinea.Rows.Clear();
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
                 if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso
            {
                e.Handled = false;
            }
            else
            {
                //el resto de teclas pulsadas se desactivan
                e.Handled = true;
            }
        }

        private void txtCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            //Evento al tocar enter
            if (e.KeyCode == Keys.Enter)
            {
                BuscarProducto();
            }
        }

        public void asiganarValorBuscar(string valor) {

            txtCodigo.Text = valor;

        }

        public void BuscarProducto() {

            Producto p = ProductoBLL.BuscarProductoId(txtCodigo.Text);

            if (p != null)
            {
                this.tablaLinea.Rows.Add(p.IdProducto, p.NombreProducto, p.Precio);
            }
            else
            {
                MessageBox.Show("Producto no encontrado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }


        public void CalcularTotal() {

            double total = 0;

            foreach (DataGridViewRow item in tablaLinea.Rows)
            {
                total += Convert.ToDouble(item.Cells[4].Value);
            }

            txtVenta.Text = total.ToString();
        }


        public void calcularTotalesPorFilas() {

            double resultado;

            foreach (DataGridViewRow item in tablaLinea.Rows)
            {
                resultado = Convert.ToDouble(item.Cells[2].Value) * Convert.ToInt16(item.Cells[3].Value);

                item.Cells[4].Value = resultado;
            }


        }



        private void button4_Click(object sender, EventArgs e)
        {
            control.AbrirFormPrincipal();
            this.Visible = false;
        }

        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {

        }

        private void tablaLinea_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        // validar que solo numeros
        public void dText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }



        private void tablaLinea_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            calcularTotalesPorFilas();


            CalcularTotal();
        }

        private void txtDescuento_TextChanged(object sender, EventArgs e)
        {

            cmbIVA.SelectedIndex = 0;
            txtIVA.Text = "";

            if (txtDescuento.Text != "" && txtVenta.Text != "") {
                txtSubTotal.Text = (Convert.ToDouble(txtVenta.Text) - Convert.ToDouble(txtDescuento.Text)).ToString();
            }

        }

        private void txtVenta_TextChanged(object sender, EventArgs e)
        {
            txtDescuento.Text = "";
            txtSubTotal.Text = "";
        }

        private void cmbIVA_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string valor = cmbIVA.SelectedValue.ToString();

            if (valor == "13")
            {
                if (txtSubTotal.Text != "") {
                    txtIVA.Text = (Convert.ToDouble(txtSubTotal.Text) * 0.13).ToString();
                }
                
            }
            else if (valor == "8")
            {
                if (txtSubTotal.Text != "")
                {
                    txtIVA.Text = (Convert.ToDouble(txtSubTotal.Text) * 0.8).ToString();
                }
 
            }
            else if (valor == "4")
            {
                if (txtSubTotal.Text != "")
                {
                    txtIVA.Text = (Convert.ToDouble(txtSubTotal.Text) * 0.4).ToString();
                }

            }
            else {
                txtIVA.Text = "0.0";

            }
        }

        private void cmbIVA_SelectedIndexChanged(object sender, EventArgs e)
        {
 

        }

        public void calcularTotalFinal() {

            double subtotal = Convert.ToDouble(txtSubTotal.Text);

            if (txtIVA.Text != "") {
                double tarifaIVA = Convert.ToDouble(txtIVA.Text);

                txtTotalVenta.Text = (subtotal + tarifaIVA).ToString();
            }


            

        }

        private void txtIVA_TextChanged(object sender, EventArgs e)
        {
            calcularTotalFinal();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            control.AbrirFormListaProductos();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnEliminarLinea_Click(object sender, EventArgs e)
        {
            if (tablaLinea.CurrentRow == null)
            {
                MessageBox.Show("Debes seleccionar una fila", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);


            }
            //if (tablaLinea.SelectedRows[0].Cells["Codigo"].Value != null)

            else {
                tablaLinea.Rows.Remove(tablaLinea.CurrentRow);

                calcularTotalesPorFilas();


                CalcularTotal();

            }

        }

        private void tablaLinea_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridViewTextBoxEditingControl dText = (DataGridViewTextBoxEditingControl)e.Control;

            dText.KeyPress -= new KeyPressEventHandler(dText_KeyPress);
            dText.KeyPress += new KeyPressEventHandler(dText_KeyPress);
        }
    }
}
