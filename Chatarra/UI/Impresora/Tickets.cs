using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Librerias
using System.Drawing;
using System.Runtime.InteropServices;


namespace UI.Impresora
{
    public class Tickets
    {
        StringBuilder linea = new StringBuilder();


        //Numero maximo de caracteres de la impresora
        int maxCar = 48;


        //metodo para dibujar guiones

        public string dibujarGuiones() {

            string lineasGuion = "";

            for (int i = 0; i < maxCar; i++)
            {
                lineasGuion += "-";
            }

            return linea.AppendLine(lineasGuion).ToString();
        }



        public string dibujarAsteriscos()
        {
            string lineasAstericos = "";

            for (int i = 0; i < maxCar; i++)
            {
                lineasAstericos += "*";
            }

            return linea.AppendLine(lineasAstericos).ToString();

        }


        public void encabezado()
        {
            linea.AppendLine("Registro del uso de comedor");


        }

        public void TextoIzquierda(string texto) {

            if (texto.Length > maxCar)
            {
                int caracterActual = 0;

                for (int longitudTexto = texto.Length; longitudTexto > maxCar; longitudTexto -= maxCar) {
                    //Agrgarmos fragmenos que salgan del texto
                    linea.AppendLine(texto.Substring(caracterActual, maxCar));
                    caracterActual += maxCar;
                }
                //Agregar el fragmento restante
                linea.AppendLine(texto.Substring(caracterActual, texto.Length - caracterActual)); 



            }
            else
            {
                linea.AppendLine(texto);
            }
        }



        public void TextoDerecha(string texto) {

            if (texto.Length > maxCar)
            {
                int caracterActual = 0;

                for (int longitudTexto = texto.Length; longitudTexto > maxCar; longitudTexto -= maxCar)
                {
                    //Agrgarmos fragmenos que salgan del texto
                    linea.AppendLine(texto.Substring(caracterActual, maxCar));
                    caracterActual += maxCar;
                }

                string espacios = "";

                for (int i = 0; i < (maxCar - texto.Substring(caracterActual, texto.Length - caracterActual).Length); i++) { 
                    espacios += " "; // Agregar espacios para alinear a la derecha
                }
                                             
                //Agregar el fragmento restante
                linea.AppendLine(texto.Substring(caracterActual, texto.Length - caracterActual));



            }
            else
            {

                string espacios = "";

                for (int i = 0; i < (maxCar - texto.Length); i++)
                {
                    espacios += " "; // Agregar espacios para alinear a la derecha
                }



                linea.AppendLine(espacios + texto);
            }

        }


         


        public void TextoCentro(string texto) {

            int caracterActual = 0;

            for (int logitudTexto = texto.Length; logitudTexto > maxCar; logitudTexto -= maxCar)
            {
                //Agregar el texto
                linea.AppendLine(texto.Substring(caracterActual, maxCar));
                caracterActual += maxCar;
            }

            //Variable para poner espacios restantes
            string espacios = "";
            //Sacar la cantidad de espacios libres
            int centrar = (maxCar - texto.Length) / 2;

            for (int i = 0; i < centrar; i++)
            {
                espacios += " "; //Agregar espacios en blanco para centrar
            }

            linea.AppendLine(espacios + texto);
           


        }



        // METODOS DE ADMINISTRACION DE TIQUETES,  ESTOS CODIGOS VARIAN SEGUN LA IMPRESORA

        public void CortarTicket() {

            linea.AppendLine("\x1B" + "\x64" + "\x00");


            /*
            linea.AppendLine("\x18" + "m"); //Caracteres de corte
            linea.AppendLine("\x18" + "d" + "\x09"); // Avanza 9 reglones */

        }

        public void AbreCajon() {
            //linea.AppendLine("\x1b" + "p" + "\x00" + "\x0F" + "\x96");

            linea.Append("\x07");

        }

        public void ImprimirTicket(string impresora) {

            RawPrinterHelper.SendStringToPrinter(impresora, linea.ToString());
            linea.Clear();
        }

        public void AgregaArticulo(string articulo, int cant, decimal precio)

        //public void AgregaArticulo(string articulo, int cant, decimal precio, decimal importe)
        {
            //Valida que cant precio e importe esten dentro del rango.
            if (cant.ToString().Length <= 5 && precio.ToString().Length <= 7)
            {
                string elemento = "", espacios = "";
                bool bandera = false;//Indicara si es la primera linea que se escribe cuando bajemos a la segunda si el nombre del articulo no entra en la primera linea
                int nroEspacios = 0;

                //Si el nombre o descripcion del articulo es mayor a 20, bajar a la siguiente linea
                if (articulo.Length > 20)
                {
                    //Colocar la cantidad a la derecha.
                    nroEspacios = (5 - cant.ToString().Length);
                    espacios = "";
                    for (int i = 0; i < nroEspacios; i++)
                    {
                        espacios += " ";//Generamos los espacios necesarios para alinear a la derecha
                    }
                    elemento += espacios + cant.ToString();//agregamos la cantidad con los espacios

                    //Colocar el precio a la derecha.
                    nroEspacios = (7 - precio.ToString().Length);
                    espacios = "";
                    for (int i = 0; i < nroEspacios; i++)
                    {
                        espacios += " ";//Genera los espacios
                    }
                    //el operador += indica que agregar mas cadenas a lo que ya existe.
                    elemento += espacios + precio.ToString();//Agregamos el precio a la variable elemento

                    //Colocar el importe a la derecha.
                    //nroEspacios = (8 - importe.ToString().Length);
                    espacios = "";
                    for (int i = 0; i < nroEspacios; i++)
                    {
                        espacios += " ";
                    }
                    //elemento += espacios + importe.ToString();//Agregamos el importe alineado a la derecha

                    int caracterActual = 0;//Indicara en que caracter se quedo al bajae a la siguiente linea

                    //Por cada 20 caracteres se agregara una linea siguiente
                    for (int longitudTexto = articulo.Length; longitudTexto > 20; longitudTexto -= 20)
                    {
                        if (bandera == false)//si es false o la primera linea en recorrerer, continuar...
                        {
                            //agregamos los primeros 20 caracteres del nombre del articulos, mas lo que ya tiene la variable elemento
                            linea.AppendLine(articulo.Substring(caracterActual, 20) + elemento);
                            bandera = true;//cambiamos su valor a verdadero
                        }
                        else
                            linea.AppendLine(articulo.Substring(caracterActual, 20));//Solo agrega el nombre del articulo

                        caracterActual += 20;//incrementa en 20 el valor de la variable caracterActual
                    }
                    //Agrega el resto del fragmento del  nombre del articulo
                    linea.AppendLine(articulo.Substring(caracterActual, articulo.Length - caracterActual));

                }
                else //Si no es mayor solo agregarlo, sin dar saltos de lineas
                {
                    for (int i = 0; i < (20 - articulo.Length); i++)
                    {
                        espacios += " "; //Agrega espacios para completar los 20 caracteres
                    }
                    elemento = articulo + espacios;

                    //Colocar la cantidad a la derecha.
                    nroEspacios = (5 - cant.ToString().Length);// +(20 - elemento.Length);
                    espacios = "";
                    for (int i = 0; i < nroEspacios; i++)
                    {
                        espacios += " ";
                    }
                    elemento += espacios + cant.ToString();

                    //Colocar el precio a la derecha.
                    nroEspacios = (7 - precio.ToString().Length);
                    espacios = "";
                    for (int i = 0; i < nroEspacios; i++)
                    {
                        espacios += " ";
                    }
                    elemento += espacios + String.Format("{0:n}", precio);

                    //Colocar el importe a la derecha.
                    //nroEspacios = (8 - importe.ToString().Length);
                    espacios = "";
                    for (int i = 0; i < nroEspacios; i++)
                    {
                        espacios += " ";
                    }
                    //elemento += espacios + importe.ToString();

                    //String.Format("{0:n}", 1234);

                    linea.AppendLine(elemento);//Agregamos todo el elemento: nombre del articulo, cant, precio, importe.
                }
            }
            else
            {
                linea.AppendLine("Los valores ingresados para esta fila");
                linea.AppendLine("superan las columnas soportdas por éste.");
                throw new Exception("Los valores ingresados para algunas filas del ticket\nsuperan las columnas soportdas por éste.");
            }
        }






















    }


    //Clase para mandara a imprimir texto plano a la impresora
    public class RawPrinterHelper
    {
        // Structure and API declarions:
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public class DOCINFOA
        {
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDocName;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pOutputFile;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDataType;
        }
        [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, out IntPtr hPrinter, IntPtr pd);

        [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartDocPrinter(IntPtr hPrinter, Int32 level, [In, MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);

        [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndDocPrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "WritePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, Int32 dwCount, out Int32 dwWritten);

        // SendBytesToPrinter()
        // When the function is given a printer name and an unmanaged array
        // of bytes, the function sends those bytes to the print queue.
        // Returns true on success, false on failure.
        public static bool SendBytesToPrinter(string szPrinterName, IntPtr pBytes, Int32 dwCount)
        {
            Int32 dwError = 0, dwWritten = 0;
            IntPtr hPrinter = new IntPtr(0);
            DOCINFOA di = new DOCINFOA();
            bool bSuccess = false; // Assume failure unless you specifically succeed.

            di.pDocName = "Ticket de Venta";//Este es el nombre con el que guarda el archivo en caso de no imprimir a la impresora fisica.
            di.pDataType = "RAW";//de tipo texto plano

            // Open the printer.
            if (OpenPrinter(szPrinterName.Normalize(), out hPrinter, IntPtr.Zero))
            {
                // Start a document.
                if (StartDocPrinter(hPrinter, 1, di))
                {
                    // Start a page.
                    if (StartPagePrinter(hPrinter))
                    {
                        // Write your bytes.
                        bSuccess = WritePrinter(hPrinter, pBytes, dwCount, out dwWritten);
                        EndPagePrinter(hPrinter);
                    }
                    EndDocPrinter(hPrinter);
                }
                ClosePrinter(hPrinter);
            }
            // If you did not succeed, GetLastError may give more information
            // about why not.
            if (bSuccess == false)
            {
                dwError = Marshal.GetLastWin32Error();
            }
            return bSuccess;
        }

        public static bool SendStringToPrinter(string szPrinterName, string szString)
        {
            IntPtr pBytes;
            Int32 dwCount;
            // How many characters are in the string?
            dwCount = szString.Length;
            // Assume that the printer is expecting ANSI text, and then convert
            // the string to ANSI text.
            pBytes = Marshal.StringToCoTaskMemAnsi(szString);
            // Send the converted ANSI string to the printer.
            SendBytesToPrinter(szPrinterName, pBytes, dwCount);
            Marshal.FreeCoTaskMem(pBytes);
            return true;
        }
    }


}



