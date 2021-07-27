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
            cone.abrirconexion();
            cone.conexion2();
            cone.buscarDatos();
        }


    }
}
