using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Windows.Forms;



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
         
        }

        
    }
}
