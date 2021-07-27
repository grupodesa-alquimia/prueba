using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Farmacia
{
    class conexionbd
    {
        SqlConnection cn;
        MySqlConnection cn2;
        public string myConnectionString;
        public long idcomprobante;
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
        public string tipo;
        public int nroItem;
        public int tipoGasto;
        public char debeHaber;
        public string codIVA;
        public int AliIVA;
        public double M_perIB;
        public double M_sb;
        public double M_exe;
        public double M_iva;
        public double M_tot;
        public int sucursal;
        public long IDComp;
        public int cont;
        public int cont2;
        public int cont3;
        public string tipoOpe;
        public string feHoy;
        public string CodEquipo;
        public int contador2;
        public string date1;
        public string date2;


        /***
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

        /***
         * Conexión a la BDD de la Farmacia
         */
        public void conexion2()
        {
            try
            {
                myConnectionString = "Data Source=192.168.100.8;Database=plex;Port=3307;User ID=alquimia; password=3387;";
                cn2 = new MySqlConnection(myConnectionString);
                Console.WriteLine("Conexión exitosa");

            }
            catch (MySqlException e)
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

        /***
         * Metodo para buscar los datos a traves de la fecha
         */
        public void buscarDatos()
        {
            try
            {
                cn.Open();
                cn2.Open();

                Console.WriteLine("DESDE: ");
                date1 = "27/07/2021";
                Console.WriteLine("HASTA: ");
                date2 = "01/12/2021";
                
                string date = Convert.ToDateTime((date1)).ToString("yyyy/MM/dd");
                string dateDos = Convert.ToDateTime((date2)).ToString("yyyy/MM/dd");

                string mysql = "Select comprascabecera.IDComprobante, comprascabecera.tipo, comprascabecera.IDProveedor, comprascabecera.Letra, comprascabecera.PuntoVta, comprascabecera.Numero, EXTRACT(MONTH FROM comprascabecera.FechaEmision),EXTRACT(YEAR FROM comprascabecera.FechaEmision),comprascabecera.FechaEmision, comprascabecera.FechaImputación, comprascabecera.CUIT, comprascabecera.Estado, comprasdetalle.sucursal,comprasdetalle.IVAAlicuota,comprasdetalle.NetoExento, comprascabecera.IDTipoGasto, comprasdetalle.NetoNoGravado, comprasdetalle.PercepDGR,comprasdetalle.IVAImporte FROM comprascabecera INNER JOIN comprasdetalle ON comprascabecera.IDComprobante = comprasdetalle.IDComprobante WHERE comprascabecera.fechaEmision between '" + date + "' and '" + dateDos + "' AND CUIT<> '' and PuntoVta between 0 and 30000";

                MySqlDataReader reader = null;
                MySqlCommand comando = new MySqlCommand(mysql, cn2);
                reader = comando.ExecuteReader();

                List<string> lista = new List<string>();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //GUARDO LOS RESULTADOS DE LA CONSULTA A LOS ATRIBUTOS
                        this.idcomprobante = reader.GetInt64(0);
                        this.tipo = reader.GetString(1);
                        this.idproveedor = reader.GetInt32(2);
                        this.letra = reader.GetChar(3);
                        this.ptovta = reader.GetInt32(4);
                        this.nro = reader.GetInt64(5);
                        this.mes = reader.GetInt32(6);
                        this.anio = reader.GetInt32(7);
                        this.fechaemi = reader.GetString(8);
                        this.fechaimpu = reader.GetString(9);
                        this.cuit = reader.GetString(10);
                        this.estado = reader.GetString(11);
                        this.sucursal = reader.GetInt32(12);
                        this.AliIVA = reader.GetInt32(13);
                        this.M_exe = reader.GetDouble(14);
                        this.tipoGasto = reader.GetInt32(15);
                        this.M_sb = reader.GetDouble(16);
                        this.M_perIB = reader.GetDouble(17);
                        this.M_iva = reader.GetDouble(18);


                        foreach (var row in lista)
                        {
                            // AGREGO LOS REGISTROS A LA LISTA
                            lista.Add(Convert.ToString(idcomprobante));
                            lista.Add(tipo);
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
                            lista.Add(Convert.ToString(sucursal));
                            lista.Add(Convert.ToString(AliIVA));
                            lista.Add(Convert.ToString(tipoGasto));
                        }

                        string id = "";
                        string ti = tipo;
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
                        int suc = this.sucursal;
                        int tipoG = this.tipoGasto;
                        string tipOperacion = "ALTA";
                        string fechaHoy = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                        string Cod_equipo = Dns.GetHostName();
                        feHoy = fechaHoy;
                        CodEquipo = Cod_equipo;
                        tipoOpe = tipOperacion;
                        System.Guid miGUID = System.Guid.NewGuid();
                        id = miGUID.ToString();

                        calculo(idcomprobante);
                        calculoItems(idcomprobante);
                        insertarIMmovimientos(id, ti, mod, CodComp, socio, le, pvta, nro_comp, nroDoc, m, a, suc, fecha, feContable, total, 0, tipoG);

                    }
                    reader.Close();
                }
                else
                {
                    MessageBox.Show("No se encontraron registros", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                Console.WriteLine("\nComprobantes agregados: " + this.cont);
                Console.WriteLine("Comprobantes NO agregados: " + this.cont2);
                Console.WriteLine("Comprobantes Agregados a IM_items: " + this.cont3);
            }
            catch (MySqlException ex)
            {
                string numeroLinea = ex.StackTrace.Substring(ex.StackTrace.Length - 4, 4);

                MessageBox.Show("Error al buscar " + ex.Message + " Linea: " + numeroLinea, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                cn.Close();
                cn2.Close();
            }
        }
        /***
         * METODO PARA AGREGAR REGISTROS A LA TABLA IM_movimientos
         */
        public void insertarIMmovimientos(string id, string ti, string mod, string cod_comp, int socio, char le, int pvta, long nro_comp, string nroDoc, int me, int a, int suc, string fecha, string feContable, double tot, int Estado, int tipo)
        {

            try
            {
                /*
                 * OBTENGO DATOS DE LA TABLA IM_COMPROBANTES Y LOS AGREGO EN IM_MOVIMIENTOS
                 */
                string Sql = "Select cod_alquimia, modulo FROM IM_comprobantes WHERE cod_tercero= '" + ti + "'";
                SqlDataReader rea = null;
                SqlCommand comando = new SqlCommand(Sql, cn);
                rea = comando.ExecuteReader();

                if (rea.Read())
                {
                    cod_comp = rea["cod_alquimia"].ToString();
                    mod = rea["modulo"].ToString();
                }
                rea.Close();


                SqlCommand query = cn.CreateCommand();
                SqlCommand query2 = cn.CreateCommand();
                /*
                 * VERIFICO SI LOS COMPROBANTES EXISTEN
                 */
                query2.CommandText = string.Format("SELECT COUNT(*) FROM IM_movimientos WHERE nro_comp = " + nro_comp + " AND letra='" + le + "' AND cod_comp= '" + cod_comp + "' AND pto_vta =" + pvta);//STRING PARA COMPROBAR SI EXISTE EL COMPROBANTE             
                int exis = int.Parse(query2.ExecuteScalar().ToString());

                //SI NO EXISTE:
                while (exis == 0)
                {

                    query.CommandText = string.Format("INSERT INTO IM_movimientos(ID_comp,modulo,cod_comp,letra,pto_vta,nro_comp,cta_cant,per_mes,per_anio,nro_socio,cod_doc,nro_doc,cod_sede,cod_condi,fec_comp,fec_conta,cod_costo,t_total,estado) VALUES('" + id + "','" + mod + "' , '" + cod_comp + "' , '" + le + "' , " + pvta + " , " + nro_comp + " , " + 1 + " , " + me + " , " + a + " , " + socio + " , 'CUIT' , " + nroDoc.ToString().Replace("-", "") + " , " + suc + " , " + 1 + " , '" + fecha + "' , '" + feContable + "' , '' , " + tot.ToString().Replace(",", ".") + " , " + Estado + " ) ");

                    int fil = query.ExecuteNonQuery();

                    if (fil > 0)
                    {
                        this.cont++;
                        nroItem = 1;
                        string detalle = Convert.ToString(cod_comp + "-" + nro_comp + "-" + le + "-" + pvta + "");
                        insertarIMitems(idcomprobante, id, nro_comp, nroItem, tipoGasto, AliIVA, M_sb, M_exe, M_perIB, M_iva, total);
                        insertarIMauditoria(id, tipoOpe, detalle, feHoy, "AUTOMATICO", CodEquipo);
                    }
                    else
                    {
                        Console.WriteLine("No se agregaron registros");
                    }

                    return;

                }
                //SI EXISTE
                Console.WriteLine("\n");
                Console.Write("Ya existe el comprobante: Codigo: " + cod_comp + " -Letra: " + le + " -Punto de venta: " + pvta + " - Numero: " + nro_comp);
                this.cont2++;


                //VERIFICAR SI EL ITEM DEL COMPROBANTE YA EXISTE O NO
                SqlCommand query3 = cn.CreateCommand();
                query3.CommandText = string.Format("SELECT COUNT(*) FROM IM_items WHERE ID = " + idcomprobante + "");//contamos cuantos items hay en la tabla im_items
                int count = int.Parse(query3.ExecuteScalar().ToString());

                if (count == 1 && contador2 > 1)
                {
                    nroItem = 2;
                    insertarIMitems(idcomprobante, id, nro_comp, nroItem, tipoGasto, AliIVA, M_sb, M_exe, M_perIB, M_iva, total);
                }
                else if (count == 0 && contador2 >= 1)
                {
                    nroItem = 1;
                    insertarIMitems(idcomprobante, id, nro_comp, nroItem, tipoGasto, AliIVA, M_sb, M_exe, M_perIB, M_iva, total);
                }
                else if (count == 1 && contador2 == 1)
                {
                    Console.WriteLine("\nYA EXISTE ESTE COMPROBANTE EN IM_ITEMS");
                }
                else if (count > 1 && contador2 >= 1)
                {
                    Console.WriteLine("\nYA EXISTE ESTE COMPROBANTE EN IM_ITEMS");
                }

            }
            catch (SqlException exc)
            {
                string numeroLinea = exc.StackTrace.Substring(exc.StackTrace.Length - 4, 4);

                MessageBox.Show("Error al agregar datos a IM_movimientos: " + exc.Message + " Linea: " + numeroLinea, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        /***
         * METODO PARA AGREGAR REGISTROS A LA TABLA IM_ITEMS
         */
        public void insertarIMitems(long idCom, string Id_comp, long nro, int Item, int tipoCodigo, double ali_iva, double mSB, double mEXE, double mPIB, double mIVA, double total)
        {
            try
            {
                /**
                 * OBTENGO VALORES DE LA TABLA IM_CODIGOS PARA INSERTARLOS EN LA TABLA IM_ITEMS
                * */
                string Sql = "Select cod_alquimia, tipo FROM IM_codigos WHERE cod_tercero= " + tipoCodigo + "";

                SqlDataReader rea1 = null;
                SqlCommand comando = new SqlCommand(Sql, cn);
                rea1 = comando.ExecuteReader();

                string tipo = "";
                string cod = "";
                if (rea1.Read())
                {
                    tipo = rea1["tipo"].ToString();
                    cod = rea1["cod_alquimia"].ToString();
                }
                rea1.Close();



                /*
                 * AGREGO DATOS A LA TABLA IM_ITEMS
                 */

                SqlCommand query = cn.CreateCommand();
                query.CommandText = string.Format("INSERT INTO IM_items(ID,ID_comp,tipo,nro_item,codigo,debe_haber,cod_iva,ali_iva,m_sb,m_int,m_bon,m_iva,m_tot,m_exe,per_ib) VALUES(" + idCom + ",'" + Id_comp + "','" + tipo + "'," + Item + ",'" + cod + "','D',''," + ali_iva + "," + mSB.ToString().Replace(",", ".") + "," + 0 + "," + 0 + "," + mIVA.ToString().Replace(",", ".") + "," + total.ToString().Replace(",", ".") + "," + mEXE.ToString().Replace(",", ".") + "," + mPIB.ToString().Replace(",", ".") + ")");

                int fil = query.ExecuteNonQuery();

                if (fil > 0)
                {
                    this.cont3++;
                }
                else
                {
                    MessageBox.Show("No se agregaron registros", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;

            }
            catch (Exception exc)
            {
                string numeroLinea = exc.StackTrace.Substring(exc.StackTrace.Length - 4, 4);

                Console.WriteLine("\n");
                MessageBox.Show("Error al agregar datos a IM_ITEMS: " + exc.Message + " Linea: " + numeroLinea, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


        }

        /***
        * Metodo para ingresar un registro en la tabla IM_auditoria
        */
        public void insertarIMauditoria(string idc, string op, string det, string fecha, string usu, string eq)
        {
            try
            {
                SqlCommand query = cn.CreateCommand();

                //COMANDO INSERT
                query.CommandText = string.Format("INSERT INTO IM_auditoria(ID_comp,tipo,operacion,detalle,fecha,cod_usr,cod_equipo) VALUES('" + idc + "'," + 0 + ",'" + op + "','" + det + "','" + fecha + "','" + usu + "','" + eq + "')");

                int fil = query.ExecuteNonQuery();

                if (fil > 0)
                {
                }
                else
                {
                    MessageBox.Show("No se agregaron registros", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception exc)
            {
                // Get stack trace for the exception with source file information

                string numeroLinea = exc.StackTrace.Substring(exc.StackTrace.Length - 4, 4);

                MessageBox.Show("Error al agregar datos a IM_AUDITORIA: " + exc.Message + " Linea: " + numeroLinea, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        /***
         * METODO PARA OBTENER LOS TOTALES DEL COMPROBANTE E ITEMS 
         */
        public void calculo(long idComp)
        {
            try
            {
                string query = "SELECT sum(NetoExento), sum(NetoNoGravado), sum(PercepDGR), sum(IVAImporte)  FROM comprasdetalle WHERE IDComprobante = " + idComp + "";
                using (MySqlConnection cone = new MySqlConnection(myConnectionString))
                {
                    MySqlCommand command = new MySqlCommand(query, cone);
                    cone.Open();

                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            this.M_sb = reader.GetDouble(1);
                            this.M_perIB = reader.GetDouble(2);
                            this.M_exe = reader.GetDouble(0);
                            this.M_iva = reader.GetDouble(3);
                            total = M_sb + M_perIB + M_exe + M_iva;
                        }

                    }

                    reader.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("NO SE PUDO OBTENER EL TOTAL " + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        /***
         * METODO PARA CONTAR LA CANTIDAD DE ITEMS QUE TIENE UN COMPROBANTE
         */
        public void calculoItems(long idComp)
        {
            try
            {
                string countMysql = "select count(*) from comprasdetalle where idcomprobante = " + idComp;
                using (MySqlConnection cone = new MySqlConnection(myConnectionString))
                {
                    MySqlCommand command = new MySqlCommand(countMysql, cone);
                    cone.Open();

                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int count2 = reader.GetInt32(0);
                            contador2 = count2;

                        }

                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("NO SE PUDO OBTENER EL total de ITEMS " + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}


