using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Net;
using System.Configuration;

namespace Farmacia
{
    public class test
    {
        static void Main(string[] args)
       {
            try
            {
                agrFCyND cone = new agrFCyND();
                AgregarNC cone2 = new AgregarNC();
                agregarOP con3 = new agregarOP();
                AgregarNR con4 = new AgregarNR();

                /***
                 * LEER EL ARCHIVO TXT DONDE ESTAN LAS FECHAS
                 */
                List<string> fechas = new List<string>();
                var txt = ConfigurationManager.ConnectionStrings["fechatxt"].ConnectionString; ; 
                StreamReader leer = new StreamReader(txt);
                string lectura="";
                while ((lectura = leer.ReadLine()) != null)
                {
                    fechas.Add(lectura);
                    fechas.Sort();
                }
                string f1 = fechas[0];
                string f2 = fechas[1];

                cone.abrirconexion();
                cone.conexion2();
                cone.buscarDatos(f1, f2);
                cone2.abrirconexion();
                cone2.conexion2();
                cone2.buscarDatosNC(f1, f2);
                con3.abrirconexion();
                con3.conexion2();
                con3.buscarop(f1, f2);
                con3.itemImimputados();
                con3.obtenerChequeTercero();
                con3.obtenerChequePropio();
                con3.obtenerCajaTranferencia();
                con3.obtenerCajaEfectivo();
                con4.abrirconexion();
                con4.conexion2();
                con4.buscarDatosNR(f1, f2);

                MessageBox.Show("\nSe agregaron " + cone.contFCND + " Fácturas y Notas de Debito.\nSe agregaron " + cone2.contNC + " Notas de Crédito.\nSe agregaron " + con3.cont + " Ordenes de Pago. \nSe agregaron " + con4.contNR + " Notas de Recupero", "PROCESO COMPLETADO", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception e)
            {
                MessageBox.Show("Error al importar comprobantes: " + e, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
