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


namespace Farmacia
{
    class conexionbd
    {
        SqlConnection cn;
        MySqlConnection cn2;

        public int idcomprobante;
        public string modulo;
        public string Cod_comp;
        public int idproveedor;
        public char letra;
        public int ptovta;
        public long nro;
        public int anio;
        public int mes;
        public string fechaemi;
        public string fechaimpu;
        public string cuit;
        public string estado;
        public double total;
        public int cont;
        public int cont2;

        /**
         * Conexión a la BDD SQL
         */

        public void abrirconexion()
        {
            try
            {
                cn = new SqlConnection("Data Source=SERVIDOR\\SQLEXPRESS2019;Initial Catalog=Amssf;Persist Security Info=True;User ID=grupodesa;Password=0");
                Console.WriteLine("Conexión exitosa");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al conectarse a la BDD " + e.Message);
            }
        }

        public void cerrarConexion()
        {
            try
            {
                Console.WriteLine("Conexión Terminada");
                cn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al finalizar la conexion a la BDD " + e.Message);
            }
        }

        /**
         * Conexión a la BDD de la Farmacia
         */
        public void conexion2()
        {
            try
            {
                string myConnectionString = "Data Source=192.168.100.8;Database=plex;Port=3307;User ID=alquimia; password=3387;";
                cn2 = new MySqlConnection(myConnectionString);
                cn2.Open();
                Console.WriteLine("Conexión exitosa");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al conectarse a la BDD Mysql " + e.Message);
            }
        }

        public void cerrarConexion2()
        {
            try
            {
                Console.WriteLine("Conexión Terminada");
                cn2.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al finalizar la conexion a la BDD " + e.Message);
            }
        }

        /**
         * Agregar Registros a la tabla IM_movimientos
         */
        public void insertarIMmovimientos(string modulo, string cod_comp, char letra, int ptovta, int nro_comp, int cuotas, int permes, int peranio, int socio, string codDoc, long nroDoc, int sede, int codCondi, string fecha, string feContable, string cod_costo, double total, int estado)
        {
            try
            {
                SqlCommand query = cn.CreateCommand();
                SqlCommand query2 = cn.CreateCommand();

                query2.CommandText = string.Format("SELECT COUNT(*) FROM IM_movimientos WHERE nro_comp = " + nro_comp + " AND letra='" + letra + "' AND cod_comp= '" + cod_comp + "'and pto_vta =" + ptovta);//STRING PARA COMPROBAR SI EXISTE EL COMPROBANTE

                //VERIFICA SI EL COMPROBANTE YA EXISTE
                int exis = int.Parse(query2.ExecuteScalar().ToString());
                /*  if (exis != 0)
                  {
                      MessageBox.Show("Ya existe este comprobante", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                  }*/
                //SI NO EXISTE
                while (exis == 0)
                {
                    //   else if (exis == 0) {
                    query.CommandText = string.Format("INSERT INTO IM_movimientos(modulo,cod_comp,letra,pto_vta,nro_comp,cta_cant,per_mes,per_anio,nro_socio,cod_doc,nro_doc,cod_sede,cod_condi,fec_comp,fec_conta,cod_costo,t_total,estado) VALUES('" + modulo + "','" + cod_comp + "','" + letra + "'," + ptovta + "," + nro_comp + "," + cuotas + "," + permes + "," + peranio + "," + socio + ",'" + codDoc + "'," + nroDoc + "," + sede + "," + codCondi + ",'" + fecha + "','" + feContable + "','" + cod_costo + "'," + total + "," + estado + ")");
                    int fil = query.ExecuteNonQuery();

                    if (fil > 0)
                    {
                        MessageBox.Show("Datos agregados correctamente");

                    }
                    else
                    {
                        MessageBox.Show("No se agregaron registros", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    cn.Close();

                }
                MessageBox.Show("Ya existe este comprobante", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error al agregar registros en IMmovimientos: " + exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


        }

        /**
         * Metodo para ingresar un registro en la tabla IM_auditoria
         */
        public void insertarIMauditoria(int idc, int tip, string op, string det, string fecha, string usu, string eq)
        {
            try
            {
                SqlCommand query = cn.CreateCommand();

                //COMANDO INSERT
                query.CommandText = string.Format("INSERT INTO IM_auditoria(tipo,operacion,detalle,fecha,cod_usr,cod_equipo) VALUES(" + tip + ",'" + op + "','" + det + "','" + fecha + "','" + usu + "','" + eq + "')");

                int fil = query.ExecuteNonQuery();

                if (fil > 0)
                {
                    MessageBox.Show("Datos agregados correctamente");
                }
                else
                {
                    MessageBox.Show("No se agregaron registros", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }



            }
            catch (Exception exc)
            {
                MessageBox.Show("Error al agregar datos: " + exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public void insertarIMitems(int Id_comp, string tipo, int nro_item, int cod, char debe_haber, string cod_iva, double ali_iva, double sb, double interes, double bonificacion, double iva, double total)
        {
            try
            {
                SqlCommand query = cn.CreateCommand();
                int n = 0;
                int tn = n + 1;

                query.CommandText = string.Format("INSERT INTO IM_items(ID,tipo,nro_item,codigo,debe_haber,cod_iva,ali_iva,m_sb,m_int,m_bon,m_iva,m_tot) VALUES(" + tn + ",'" + tipo + "'," + nro_item + "," + cod + ",'" + debe_haber + "','" + cod_iva + "'," + ali_iva + "," + sb + "," + interes + "," + bonificacion + "," + iva + "," + total + ")");

                int fil = query.ExecuteNonQuery();

                if (fil > 0)
                {
                    MessageBox.Show("Datos agregados correctamente");
                }
                else
                {
                    MessageBox.Show("No se agregaron registros", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                cn.Close(); //CERRAR BDD

            }
            catch (Exception exc)
            {
                MessageBox.Show("Error al agregar datos: " + exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
        }

        public void buscarDatos()
        {
            try
            {
                cn.Open();
                MySqlDataReader reader = null;

                string sql = "Select IDComprobante, Tipo, IDProveedor, Letra, PuntoVta, Numero, EXTRACT(MONTH FROM FechaEmision), EXTRACT(YEAR FROM FechaEmision),FechaEmision, FechaImputación, CUIT, Total, Estado FROM comprascabecera WHERE fechaEmision between  '2017-10-02' and '2018-04-01' AND  CUIT <> '' and PuntoVta between 0 and 30000";

                MySqlCommand comando = new MySqlCommand(sql, cn2);
                reader = comando.ExecuteReader();
                List<string> lista = new List<string>();
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        //GUARDO LOS RESULTADOS DE LA CONSULTA A LOS ATRIBUTOS
                        this.idcomprobante = reader.GetInt32(0);
                        this.Cod_comp = reader.GetString(1);
                        this.idproveedor = reader.GetInt32(2);
                        this.letra = reader.GetChar(3);
                        this.ptovta = reader.GetInt32(4);
                        this.nro = reader.GetInt64(5);
                        this.mes = reader.GetInt32(6);
                        this.anio = reader.GetInt32(7);
                        this.fechaemi = reader.GetString(8);
                        this.fechaimpu = reader.GetString(9);
                        this.cuit = reader.GetString(10);
                        this.total = reader.GetDouble(11);
                        this.estado = reader.GetString(12);

                        foreach (var row in lista)
                        {
                           // AGREGO LOS REGISTROS A LA LISTA
                            lista.Add(Convert.ToString(idcomprobante));
                            lista.Add(modulo);
                            lista.Add(Convert.ToString(idproveedor));
                            lista.Add(Convert.ToString(letra));
                            lista.Add(Convert.ToString(ptovta));
                            lista.Add(Convert.ToString(nro));
                            lista.Add(Convert.ToString(mes));
                            lista.Add(Convert.ToString(anio));
                            lista.Add(fechaemi);
                            lista.Add(fechaimpu);
                            lista.Add(cuit);
                            lista.Add(estado);
                            lista.Add(Convert.ToString(total));


                            }
                            int est = 0;
                            if (estado == "C")
                            {
                                est = 2;
                            }
                            else if (estado == "A")
                            {
                                est = 1;
                            }

                            int id = idcomprobante;
                            string mod = modulo;
                            string CodComp = Cod_comp;
                            int socio = idproveedor;
                            char le = letra;
                            int pvta = ptovta;
                            long nro_comp = nro;
                            int m = mes;
                            int a = anio;
                            string fecha = fechaemi;
                            string feContable = fechaimpu;
                            string nroDoc = this.cuit;
                            double tot = this.total;

                            // string Estado = this.estado;
                            insertarIMmovimientos2(id, mod, CodComp, socio, le, pvta, nro_comp, nroDoc, m, a, fecha, feContable, tot, est);
                            // insertarIMitems(id,it);
                    }
                   
                }
                else
                {
                    MessageBox.Show("No se encontraron registros", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Console.Write("\n");
                Console.WriteLine("Comprobantes agregados: " + this.cont);
                Console.WriteLine("Comprobantes NO agregados: " + this.cont2);
                return;


            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al buscar " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void insertarIMmovimientos2(int id, string mod, string cod_comp, int socio, char le, int pvta, long nro_comp, string nroDoc, int me, int a, string fecha, string feContable,  double tot, int Estado)
        {
       
            try
            {
                SqlCommand query = cn.CreateCommand();
                SqlCommand query2 = cn.CreateCommand();

                query2.CommandText = string.Format("SELECT COUNT(*) FROM IM_movimientos WHERE nro_comp = " + nro_comp + " AND letra='" + le + "' AND cod_comp= '" + cod_comp + "' AND pto_vta =" + pvta);//STRING PARA COMPROBAR SI EXISTE EL COMPROBANTE
             
                //VERIFICA SI EL COMPROBANTE YA EXISTE
                int exis = int.Parse(query2.ExecuteScalar().ToString());
              
                while (exis == 0)
                {

                    query.CommandText = string.Format("INSERT INTO IM_movimientos(modulo,cod_comp,letra,pto_vta,nro_comp,cta_cant,per_mes,per_anio,nro_socio,cod_doc,nro_doc,cod_sede,cod_condi,fec_comp,fec_conta,cod_costo,t_total,estado) VALUES('PV' , '" + cod_comp + "' , '" + le + "' , " + pvta + " , " + nro_comp + " , " + 12 + " , " + me + " , " + a + " , " + socio + " , 'CUIT' , " + nroDoc.ToString().Replace("-", "") + " , " + 1 + " , " + 1 + " , '" + fecha + "' , '" + feContable + "' , 'A' , " + tot.ToString().Replace(",", ".") + " , " + Estado + " ) " );

                    int fil = query.ExecuteNonQuery();

                    if (fil > 0)
                    {
                        this.cont++;
                    }
                    else
                    {
                        Console.WriteLine("No se agregaron registros");
                    }

                    return;

                }
                Console.WriteLine("\n");
                Console.Write("Ya existe el comprobante: Codigo: " + cod_comp + " -Letra: " + le + " -Punto de venta: " + pvta + " - Numero: " + nro_comp);
                this.cont2++;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

          
        }
    }
}


