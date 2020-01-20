using BLL.Configuracion;
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
    public partial class FormConfig : Form
    {
        public Control control { get; set; }

        public string NombreImpresora { get; set; }

        public FormConfig(Control control)
        {
            InitializeComponent();

            this.control = control;

            cargarDatosForm();

        }


        public void cargarDatosForm() {
            Configuracion c = ConfiguracionBLL.BuscarConfiguracion();

            if (c != null) {
                txtImpresora.Text = c.NombreImpresora;
                txtNombreInstitucion.Text = c.NombreInstitucion;
            }




        }

        public void cargarNombreImpresora() {
            txtImpresora.Text = NombreImpresora;
        }

        private void FormConfig_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (txtImpresora.Text == "" || txtNombreInstitucion.Text == "")
            {
                MessageBox.Show("No puese dejar campos en blanco", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else {

                Configuracion c = new Configuracion
                {
                    Id = 1, //El Id siempre es el mismo
                    NombreImpresora = txtImpresora.Text,
                    NombreInstitucion = txtNombreInstitucion.Text
                };

                Resultado result = ConfiguracionBLL.AgregarConfiguracion(c);

                if (result.Codigo == 1)
                {
                    MessageBox.Show(result.Mensaje, "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    control.NombresImpresora(); // llamada al metodo para cargar las variables con los datos nuevos
                }
                else
                {
                    MessageBox.Show(result.Mensaje);
                }
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            control.AbrirFormImpresora();
            this.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            control.AbrirFormPrincipal();
            this.Visible = false;
        }

    }
}
