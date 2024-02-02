using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionBancariaAppNS
{
    public partial class GestionBancariaApp : Form
    {
        private double saldo;
        //!? RMB2324 Se redefine la constante de error en Form1
        public const string ERR_CANTIDAD_NO_VALIDA = "Cantidad no válida";
        //!? RMB2324 Se redefine la constante de error en Form1
        public const string ERR_SALDO_INSUFICIENTE = "Saldo insuficiente";

        public GestionBancariaApp(double saldo = 0)
        {
            InitializeComponent();
            if (saldo > 0)
                this.saldo = saldo;
            else
                this.saldo = 0;
            txtSaldo.Text = ObtenerSaldo().ToString();
            txtCantidad.Text = "0";
        }

        public double ObtenerSaldo() { return saldo; }

        public int RealizarReintegro(double cantidad) 
        {
            if (cantidad <= 0)
                //!? RMB2324 Se cambia la forma de generar la excepción
                throw new ArgumentOutOfRangeException(ERR_CANTIDAD_NO_VALIDA);
            if (saldo < cantidad)
                //!? RMB2324 Sustitución return
                throw new ArgumentOutOfRangeException(ERR_SALDO_INSUFICIENTE);
            //!? RMB2324 Corregimos el código
            saldo -= cantidad;
            return 0;
        }

        public int RealizarIngreso(double cantidad) {
            if (cantidad <= 0)
                //!? RMB2324 Se cambia la forma de generar la excepción
                throw new ArgumentOutOfRangeException(ERR_CANTIDAD_NO_VALIDA);
            //!? RMB2324 Corregimos el código
            //!? cambiando -= por +=
            saldo += cantidad;
            return 0;
        }

        private void btOperar_Click(object sender, EventArgs e)
        {
            double cantidad = Convert.ToDouble(txtCantidad.Text); // Cogemos la cantidad del TextBox y la pasamos a número
            if (rbReintegro.Checked)
            {
                try
                {
                    RealizarReintegro(cantidad);
                    MessageBox.Show("Transacción realizada.");
                }
                catch (Exception error)
                {
                    if (error.Message.Contains(ERR_SALDO_INSUFICIENTE))
                    {
                        MessageBox.Show("No se ha podido realizar la operación (¿Saldo insuficiente?)");
                    }
                    else
                    {
                        if (error.Message.Contains(ERR_CANTIDAD_NO_VALIDA))
                        {
                            MessageBox.Show("Cantidad no válida, sólo se admiten cantidades positivas.");
                        }
                    }
                }
            }
            else
            {
                try
                {
                    RealizarIngreso(cantidad);
                    MessageBox.Show("Transacción realizada.");
                }
                catch (Exception error)
                {
                    if (error.Message.Contains(ERR_CANTIDAD_NO_VALIDA))
                    {
                        MessageBox.Show("Cantidad no válida, sólo se admiten cantidades positivas.");
                    }
                }
            }
           txtSaldo.Text = ObtenerSaldo().ToString();
        }
    }
}
