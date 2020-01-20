using System;
using BLL.Personas;
using Entidades;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Persona persona = new Persona
            {
                Nombre = "Andrey",
                Identificacion = "116280687",
                Esbecado = true,
                TipoPersona = 1

            };

            PersonasBLL.InsertarPersonas(persona);

           
        }
    }
}
