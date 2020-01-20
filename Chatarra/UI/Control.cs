using BLL.Configuracion;
using BLL.Personas;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.Impresora;
using UI.Reportes;

namespace UI
{
    public class Control
    {
        FormPrincipal principal;
        FormUsuario usuario;
        FormReportes formMenuReportes;
        FormReporteFecha reportesRangoFechas;
        FormImpresora formImpresora;
        FormAgregarHistoria formAgregarHistoria;
        FormAgregarProductos formAgregarProductos;
        FormProductos formProductos;
        FormConfig formConfig;
        ListaPersonas listaPersonas;
        ListaProductos listaProductos;
        Tickets ticket;


        public string NombreImpresora { get; set; }
        public string NombreInstitucion { get; set; }

        public Control() {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            usuario = new FormUsuario(this);
            formMenuReportes = new FormReportes(this);
            reportesRangoFechas = new FormReporteFecha(this);
            listaPersonas = new ListaPersonas(this);
            formImpresora = new FormImpresora(this);
            formConfig = new FormConfig(this);
            formAgregarProductos = new FormAgregarProductos(this);
            formAgregarHistoria = new FormAgregarHistoria(this);
            formProductos = new FormProductos(this);
            listaProductos = new ListaProductos(this);
            ticket = new Tickets();


            // Llamada al metodo para obtener el nombre de la impresora
            NombresImpresora();

            Application.Run(principal = new FormPrincipal(this));

        }


        //Control de Ventanas

        public void AbrirFormUsuarioAgregar() {
            usuario.Show();
            usuario.ocultarBotonesAlAgregar();
            usuario.LimpiarTextos();
        }

        public void AbrirFormUsuario()
        {
            usuario.Show();
        }

        public void AbrirFormPrincipal()
        {
            principal.Show();
        }

        public void AbrirFormMenuReportes() {
            formMenuReportes.Show();

        }




        public void AbrirFormReporteFechas() {
            reportesRangoFechas.Visible = true;
        }

        public void AbrirFormListaUsuarios() {
            listaPersonas.Show();
        }

        public void AbrirFormImpresora()
        {
            formImpresora.Show();
        }

        public void AbrirFormAgregarHistoria() {
            formAgregarHistoria.Show();
        }

        public void AbrirFormAgregarProductos() {
            formAgregarProductos.Show();
            formAgregarProductos.AparecerBotonAgregar();
        }

        public void AbrirFormProductos() {
            formProductos.Show();
            formProductos.llenarGrid();
        }

        public void AbrirFormListaProductos() {
            listaProductos.Show();
        }

        public void AbrirFormConfig()
        {
            formConfig.Show();
        }

        public void CerrarFormReportesPrincipal() {
            formMenuReportes.Visible = false;
        }


        public Persona buscarUsuarioAlInsertarHistorial(string cedula)
        {
            Persona persona = PersonasBLL.BuscarUnaPersona(cedula);


            if (persona != null)
            {
                persona.Codigo = 1;
                persona.Mensaje = "Buscado correctamente";
                return persona;
            }
            else
            {
                Persona p = new Persona();

                p.Codigo = 99;
                p.Mensaje = "No encontrado";

                return p;
            }

        }


        public void buscarProducto(string dato) {

            formAgregarHistoria.asiganarValorBuscar(dato);

            formAgregarHistoria.BuscarProducto();


        }

        public Resultado buscarUsuario(string cedula) {

            Persona persona = PersonasBLL.BuscarUnaPersona(cedula);


            if (persona != null)
            {
                AbrirFormUsuario();
                usuario.CargarDatosUsuarioBuscado(persona);
                usuario.ocultarBotonesAlModificar();

                return new Resultado
                {
                    Codigo = 1,
                    Mensaje = "Buscado correctamente"
                };

            }
            else {
                return new Resultado
                {
                    Codigo = 99,
                    Mensaje = "No encontrado"
                };

            }
        }

        public void CargarDatosUsuarioHistorial(Persona persona)
        {



            formAgregarHistoria.CargarDatosUsuario(persona);
        }

        public void LlenarFormAgregarProducto(int id) {

            formAgregarProductos.cargarParaModificar(id);
        }

        public Resultado ModificarUsuario(Persona persona)
        {

            Resultado resultado = PersonasBLL.ActualizarPersonas(persona);

            return resultado;
        }

        public void CargarGridUsuarios() {

            listaPersonas.CargarGrid();
        }


        public void CrearTickete(Persona persona, List<ProductosLineaDetalle> lista, Factura factura) {



            //ticket.AbreCajon();

            ticket.TextoDerecha("Comprobante No. " + factura.NumeroFactura.ToString());
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");
            ticket.TextoCentro("COMPROBANTE DE COMPRA");
            ticket.TextoCentro(NombreInstitucion);
            ticket.TextoCentro("Ced. Jur 310166639");
            ticket.TextoCentro("Tel. 4082-4397");
            ticket.TextoCentro("Fecha: " + DateTime.Now);
            ticket.TextoCentro("Cliente " + persona.Nombre);
            ticket.TextoCentro("Cedula " + persona.Identificacion);

            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");

            ticket.TextoIzquierda("Producto      Cantidad    Total");



            for (int i = 0; i < lista.Count(); i++)
            {


                //public void AgregaArticulo(string articulo, int cant, decimal precio, decimal importe)

                ticket.AgregaArticulo(lista[i].NombreProducto, Convert.ToInt32(lista[i].Cantidad), Convert.ToDecimal(lista[i].PrecioProducto));
            }

        

                ticket.TextoIzquierda("");
            //ticket.TextoIzquierda("");

                ticket.TextoDerecha("SubTotal     " +String.Format("{0:n}", factura.SubTotal));
                ticket.TextoDerecha("IVA:         "+ String.Format("{0:n}", factura.IVA));
                ticket.TextoDerecha("Descuento:   "+ String.Format("{0:n}", factura.Descuento));
                ticket.TextoDerecha("Total:       "+ String.Format("{0:n}", factura.TotalVenta));
                //ticket.TextoCentro("₡" + factura.TotalVenta.ToString());
                ticket.TextoIzquierda("");

                ticket.TextoCentro("Declaro que este material vendido");
                ticket.TextoCentro("es de mi propiedad y eximo de toda");
                ticket.TextoCentro("responsabilidad a la compannia");
                ticket.TextoIzquierda("");
                ticket.TextoIzquierda("");
                ticket.TextoIzquierda("");
                ticket.TextoIzquierda("FIRMA");
                ticket.TextoIzquierda("");
                ticket.TextoIzquierda("");

                ticket.TextoCentro("");
                ticket.TextoIzquierda("_____________________________________________");
                ticket.TextoIzquierda("");
                ticket.TextoIzquierda("");
                ticket.TextoIzquierda("");
                ticket.TextoIzquierda("");
                ticket.TextoIzquierda("");
                ticket.TextoIzquierda("");
                ticket.TextoIzquierda("");
                ticket.TextoIzquierda("");
                ticket.TextoIzquierda("");
                ticket.TextoIzquierda("");
                ticket.TextoIzquierda("");
                ticket.TextoIzquierda("");

    
            //ticket.CortarTicket();

            ticket.ImprimirTicket(NombreImpresora); // NOMBRE DE LA IMPRESORA      



        }




















        public void NombresImpresora() {
            //traer el nombre de la institucion y impresora que esta en la BD

            Configuracion c = ConfiguracionBLL.BuscarConfiguracion();

            if (c != null)
            {
                NombreImpresora = c.NombreImpresora;
                NombreInstitucion = c.NombreInstitucion;
            }
            else {
                NombreImpresora = "";
                NombreInstitucion = "";
            }


        }



        public void ObtenerNombreImprespra(string nombre) {

            //Este metodo obtiene el nombre de la impresora del FormImpresora y despues lo coloca en el FormConfig
            //Seteo el nombre de la impresora
            formConfig.NombreImpresora = nombre;

            //Este metodo toma la variable y la coloca en el txt
            formConfig.cargarNombreImpresora(); 

        }




    }
}
