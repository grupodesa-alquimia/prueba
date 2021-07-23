using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Sql;
using System.Net;

namespace Farmacia
{
    public class test
    {
        static void Main(string[] args)
        {
            conexionbd cone = new conexionbd();
            //cone.insertarIMmovimientos("FC", "a", 'B', 16, 101412, 12, 12, 2017, 1, "DNI", 41860964, 1, 1, "2017/10/10 00:00:00", "2017/10/10 00:00:00", "a", 200.00, 1);
            cone.abrirconexion();
            cone.conexion2();
            cone.buscarDatos();
            
            //string detalle = string.Format("N° de comprobante: " + 4000 + ", Letra: " + 'A' + ", Punto de venta: " + 5 + ", Codigo: " + 6);
            //Console.WriteLine(detalle);

        }


    }
}
