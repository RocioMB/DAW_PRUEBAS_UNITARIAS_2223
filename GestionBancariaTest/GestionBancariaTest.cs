using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using GestionBancariaAppNS;

namespace GestionBancariaTest
{
    [TestClass]
    public class GestionBancariaTest
    {
        [TestMethod]
        //!? RMB2324 Se añaden directivas DataRow
        [DataRow (1000, 250, 750)]
        [DataRow (1000, 1000, 0)]
        [DataRow (1000, 1, 999)]
        [DataRow (1000, 500, 500)]
        public void ValidarReintegro(double saldoInicial, double reintegro,
                                     double saldoEsperado)
        {
            GestionBancariaApp miApp = new GestionBancariaApp(saldoInicial);

            // Método a probar
            miApp.RealizarReintegro(reintegro);

            Assert.AreEqual(saldoEsperado, miApp.ObtenerSaldo(), 0.001,
            "Se produjo un error al realizar el reintegro, saldo" +
            "incorrecto.");
        }

        [TestMethod]
        //!? RMB2324 Se añaden directivas DataRow
        [DataRow (1000, 250, 1250)]
        [DataRow(1000, 1, 1001)]
        [DataRow(1000, 500, 1500)]
        [DataRow(1000, 1000, 2000)]
        public void ValidarIngreso(double saldoInicial, double ingreso,
                                   double saldoEsperado)
        {
            GestionBancariaApp miApp = new GestionBancariaApp(saldoInicial);

            //!? RMB2324 Método a probar
            miApp.RealizarIngreso(ingreso);
            Assert.AreEqual(saldoEsperado, miApp.ObtenerSaldo(), 0.001,
            "RMB2324 Se produjo un error al realizar el ingreso, saldo" +
            "incorrecto.");
        }

        [TestMethod]
        //!? RMB2324 Se añaden directivas DataRow
        [DataRow (1000, -1)]
        [DataRow(1000, 0)]
        [DataRow(1000, -1000)]
        [DataRow(1000, -2000)]
        public void ValidarIngresoCantidadNoValida(double saldoInicial, double ingreso)
        {
            GestionBancariaApp miApp = new GestionBancariaApp(saldoInicial);

            try
            {
                miApp.RealizarIngreso(ingreso);
            }
            catch (ArgumentOutOfRangeException exception)
            {
                StringAssert.Contains(exception.Message, GestionBancariaApp.ERR_CANTIDAD_NO_VALIDA);
            }
        }

        [TestMethod]
        //!? RMB2324 Se añaden directivas DataRow
        [DataRow(1000, -1)]
        [DataRow(1000, 0)]
        [DataRow(1000, -1000)]
        [DataRow(1000, -2000)]
        public void ValidarReintegroCantidadNoValida(double saldoInicial, double reintegro)
        {
            GestionBancariaApp miApp = new GestionBancariaApp(saldoInicial);

            //!? RMB2324 Se incluye try catch
            try
            {
                miApp.RealizarReintegro(reintegro);
            }
            catch (ArgumentOutOfRangeException exception)
            {
                StringAssert.Contains(exception.Message, GestionBancariaApp.ERR_CANTIDAD_NO_VALIDA);
            }
        }

        [TestMethod]
        //!? RMB2324 Se añaden directivas DataRow
        [DataRow(1000, 1001)]
        [DataRow(1000, 1002)]
        [DataRow(1000, 3000)]
        [DataRow(1000, 5000)]
        public void ValidarReintegroSaldoInsuficiente(double saldoInicial, double reintegro)
        {
            GestionBancariaApp miApp = new GestionBancariaApp(saldoInicial);

            //!? RMB2324 Se incluye try catch
            try
            {
                miApp.RealizarReintegro(reintegro);
            }
            catch (ArgumentOutOfRangeException exception)
            {
                StringAssert.Contains(exception.Message, GestionBancariaApp.ERR_SALDO_INSUFICIENTE);
            }
        }
    }
}
