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
    public partial class FormUsuario : Form
    {

        public Control control { get; set; }

        public bool Esbecado { get; set; }

        public FormUsuario(Control control)
        {
            this.control = control;

            InitializeComponent();
            
        }

        private void FormUsuario_Load(object sender, EventArgs e)
        {

                       

        }

        public void ocultarBotonesAlAgregar() {
            //este metodo oculta los botones de modificar y elimar

            btnModificar.Visible = false;


            btnAceptar.Visible = true;

            //Desbloquear la cedula.
            txtCedula.Enabled = true;

        }

        public void ocultarBotonesAlModificar()
        {
            //este metodo oculta los botones de modificar y elimar

            btnModificar.Visible = true;


            btnAceptar.Visible = false;


            //Bloquear la cedula
            txtCedula.Enabled = false;
        }


        public void CargarDatosUsuarioBuscado(Persona persona)
        {
            txtCedula.Text = persona.Identificacion;
            txtNombre.Text = persona.Nombre;

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Visible = false;

            control.AbrirFormPrincipal();
        }

        private void button1_Click(object sender, EventArgs e)
        {



            if (txtCedula.Text != "" && txtNombre.Text != "")
            {
                
                Persona persona = new Persona
                {
                    Identificacion = txtCedula.Text,
                    Nombre = txtNombre.Text
                };

                Resultado resultado = PersonasBLL.ActualizarPersonas(persona);

                if (resultado.Codigo == 1)
                {
                    MessageBox.Show("Modificado correctamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    control.AbrirFormPrincipal();
                }
                else
                {
                    MessageBox.Show("Problemas al modificar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    control.AbrirFormPrincipal();
                }

            }
            else {
                MessageBox.Show("No puede dejar campos en blanco", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }



        }

        private void button2_Click(object sender, EventArgs e)
        {


            Persona persona = new Persona
            {
                Identificacion = txtCedula.Text,
                Nombre = txtNombre.Text
            };

            

            DialogResult result = MessageBox.Show("Seguro que desea eliminar el registro?", "Informacion", MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);


            switch (result)
            {
                case DialogResult.Yes:
                    Resultado resultado = PersonasBLL.EliminarPersonas(persona);
                    
                        if (resultado.Codigo == 1)
                        {
                            MessageBox.Show("Eliminado correctamente","Alerta",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                            LimpiarTextos();
                        }
                        else
                        {
                            MessageBox.Show("Problemas al eliminar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }

                    break;
                case DialogResult.No:
                   
                    break;
                case DialogResult.Cancel:
                   
                    break;
            }

        }

        public void LimpiarTextos() {
            txtCedula.Text = "";
            txtNombre.Text = "";



        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            //Verificar si existe registro 

            if (txtCedula.Text != "" && txtNombre.Text != "")
            {
                Persona p = PersonasBLL.BuscarUnaPersona(txtCedula.Text);

                if (p == null)
                {

                    Persona persona = new Persona
                    {
                        Identificacion = txtCedula.Text,
                        Nombre = txtNombre.Text
                    };

                    Resultado resultado = PersonasBLL.InsertarPersonas(persona);

                    if (resultado.Codigo == 1)
                    {
                        MessageBox.Show("Insertado correctamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        control.AbrirFormPrincipal();
                    }
                    else
                    {
                        MessageBox.Show("Problemas al insertar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                    LimpiarTextos();

                }
                else
                {
                    MessageBox.Show("Ya existe el registro", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            else {
                MessageBox.Show("No puede dejar campos en blanco", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
 




        }

        private void txtCedula_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            control.AbrirFormListaUsuarios();
        }

        private void btnListado_Click(object sender, EventArgs e)
        {
            control.AbrirFormListaUsuarios();
            control.CargarGridUsuarios();

            this.Visible = false;
        }

        private void txtCedula_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
