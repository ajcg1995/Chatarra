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
    public partial class FormImpresora : Form
    {
        public Control control { get; set; }
        public FormImpresora(Control control)
        {
            InitializeComponent();

            this.control = control;

            listarImpresoras();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            control.ObtenerNombreImprespra(CmbImpresora.SelectedItem.ToString());

            
            this.Visible = false;

            control.AbrirFormConfig();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            control.AbrirFormConfig();
            this.Visible = false;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void FormImpresora_Load(object sender, EventArgs e)
        {

        }


        

        public void listarImpresoras() {

            List<string> listadoImpresora = new List<string>();
            foreach (string item in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                listadoImpresora.Add(item);
            }

            CmbImpresora.DataSource = listadoImpresora;

        }
    }
}
