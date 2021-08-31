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
using System.Configuration;


namespace Farmacia
{
    class AgregarNR
    {
        public SqlConnection cn;
        public MySqlConnection cn2;
        public string myConnectionString;
        public string myConnectionSQL;
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
        public double AliIVA;
        public double M_perIB;
        public double M_sb;
        public double M_exe;
        public double M_iva;
        public int sucursal;
        public string IDComp;
        public int contNR;
        public string tipoOpe;
        public string feHoy;
        public string CodEquipo;
        public string date1;
        public string date2;

        /***
         * Conexión a la BDD SQL
         */
        public void abrirconexion()
        {

            try
            {
                var sqlCone = ConfigurationManager.ConnectionStrings["conexionSQL"].ConnectionString;
                myConnectionSQL = sqlCone;
                cn = new SqlConnection(myConnectionSQL);
                //   Console.WriteLine("Conexión exitosa");
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
                // Console.WriteLine("Conexión Terminada");
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
                var mysqlCone = ConfigurationManager.ConnectionStrings["coneMYSQL"].ConnectionString;
                myConnectionString = mysqlCone;
                cn2 = new MySqlConnection(myConnectionString);
                //  Console.WriteLine("Conexión exitosa");

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
                //  Console.WriteLine("Conexión Terminada");
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
        public void buscarDatosNR(string fe1, string fe2)
        {
            try
            {
                cn.Open();
                cn2.Open();


                date1 = fe1;
                date2 = fe2;

                string date = Convert.ToDateTime((date1)).ToString("yyyy/MM/dd");
                string dateDos = Convert.ToDateTime((date2)).ToString("yyyy/MM/dd");

                string mysql = "Select plex.comprascabecera.IDComprobante, plex.comprascabecera.tipo, plex.comprascabecera.IDProveedor, plex.comprascabecera.Letra, plex.comprascabecera.PuntoVta, plex.comprascabecera.Numero, EXTRACT(MONTH FROM plex.comprascabecera.FechaEmision),EXTRACT(YEAR FROM plex.comprascabecera.FechaEmision),plex.comprascabecera.FechaEmision, plex.comprascabecera.FechaImputacion, plex.comprascabecera.CUIT, plex.comprascabecera.Estado,plex.comprascabecera.Total, plex.comprascabecera.sucursal FROM  plex.comprascabecera WHERE plex.comprascabecera.fechaEmision between '" + date + "' and '" + dateDos + "' AND CUIT<> '' and PuntoVta between 0 and 30000 Having Tipo='NR'";

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
                        this.M_sb = reader.GetDouble(12);
                        this.total = reader.GetDouble(12);
                        this.sucursal = reader.GetInt32(13);

                        string id = "";
                        string id2 = "";
                        string ti = tipo;
                        string mod = modulo;
                        string CodComp = Cod_comp;
                        int socio = idproveedor;
                        char le = letra;
                        int pvta = ptovta;
                        long nro_comp = nro;
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

                        System.Guid miGUID2 = System.Guid.NewGuid();
                        id2 = miGUID2.ToString();
                        if (M_sb > 0)
                        {
                            AliIVA = 21;
                        }else if (M_sb==0)
                        {
                            AliIVA = 0;
                        }

                        double aliVa = this.AliIVA;
                        double Mex = this.M_exe;
                        int tipG = this.tipoGasto;
                        double sb = this.M_sb;
                        double per = this.M_perIB;
                        double iv = this.M_iva;
                        string cod_comp2= "   20";

                    

                        insertarIMmovimientos(id, ti, mod, CodComp, socio, le, pvta, nro_comp, nroDoc, suc, fecha, feContable, total, 0, this.M_sb, idcomprobante);
                        insertarIMmovimientos2(id2, ti, mod, cod_comp2, socio, le, pvta, nro_comp, nroDoc, suc, fecha, feContable, total, 0, this.M_sb, idcomprobante);

                    }
                    reader.Close();
                }
                else
                {
                    //MessageBox.Show("No se encontraron Notas de Recupero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                Console.WriteLine("\nComprobantes agregados: " + this.contNR);
            }
            catch (MySqlException ex)
            {
                string numeroLinea = ex.StackTrace.Substring(ex.StackTrace.Length - 4, 4);

                MessageBox.Show("Error al buscar " + ex.Message + " Linea: " + numeroLinea, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
            }
        }
        /***
         * METODO PARA AGREGAR REGISTROS A LA TABLA IM_movimientos
         */
        public void insertarIMmovimientos(string id, string ti, string mod, string cod_comp, int socio, char le, int pvta, long nro_comp, string nroDoc, int suc, string fecha, string feContable, double tot, int Estado, double sb, long idCOMprobante)
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

                while (rea.Read())
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
                query2.CommandText = string.Format("SELECT COUNT(*) FROM IM_movimientos WHERE nro_comp = " + nro_comp + " AND letra='" + le + "' AND cod_comp= '   18' AND pto_vta =" + pvta);//STRING PARA COMPROBAR SI EXISTE EL COMPROBANTE             
                int exis = int.Parse(query2.ExecuteScalar().ToString());

                //SI NO EXISTE:
                while (exis == 0)
                {
                    string sucu = "";
                    if (suc == 1)
                    {
                        sucu = "UNICA";
                    }
                    query.CommandText = string.Format("INSERT INTO IM_movimientos(ID_comp,modulo,cod_comp,letra,pto_vta,nro_comp,cta_cant,nro_socio,cod_doc,nro_doc,cod_sede,cod_condi,fec_comp,fec_conta,cod_costo,t_total,estado,tipo) VALUES('" + id + "','" + mod + "' , '   18' , '" + le + "' , " + pvta + " , " + nro_comp + " ," + 1 + ", " + socio + " , 'CUIT' , " + nroDoc.ToString().Replace("-", "") + " , '" + sucu + "' , " + 1 + " , '" + fecha + "' , '" + feContable + "' , '7' , " + tot.ToString().Replace(",", ".") + " , " + Estado + ",'D') ");

                    int fil = query.ExecuteNonQuery();

                    if (fil > 0)
                    {
                        this.contNR++;
                        nroItem = 1;
                        string detalle = Convert.ToString(cod_comp + "-" + le + "-" + pvta + "-" + nro_comp + "");
                        insertarIMitems(idCOMprobante, id, nro_comp, 1, 0, AliIVA, sb, 0, 0, 0, mes, anio, total);
                        insertarIMauditoria(id, tipoOpe, detalle, feHoy, "AUTOMATICO", CodEquipo);
                        //insertarIMCaja(id, idCOMprobante, 1, 1, total, cod_comp, "1", nro_comp, fecha, feContable);
                        this.IDComp = id;
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


            }
            catch (SqlException exc)
            {
                string numeroLinea = exc.StackTrace.Substring(exc.StackTrace.Length - 4, 4);
                //MessageBox.Show("Error al agregar datos a IM_movimientos: " + exc.Message + " Linea: " + numeroLinea, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public void insertarIMmovimientos2(string id2, string ti, string mod, string cod_comp, int socio, char le, int pvta, long nro_comp, string nroDoc, int suc, string fecha, string feContable, double tot, int Estado, double sb, long idCOMprobante)
        {
            try
            {
                /*
                 * OBTENGO DATOS DE LA TABLA IM_COMPROBANTES Y LOS AGREGO EN IM_MOVIMIENTOS
                 */
                string Sql = "Select modulo FROM IM_comprobantes WHERE cod_tercero= '" + ti + "'";
                SqlDataReader rea = null;
                SqlCommand comando = new SqlCommand(Sql, cn);
                rea = comando.ExecuteReader();

                while (rea.Read())
                {
                    mod = rea["modulo"].ToString();
                }
                rea.Close();
                SqlCommand query4= cn.CreateCommand();
                SqlCommand query3 = cn.CreateCommand();
                query3.CommandText = string.Format("SELECT COUNT(*) FROM IM_movimientos WHERE nro_comp = " + nro_comp + " AND letra='" + le + "' AND cod_comp= '" + cod_comp + "' AND pto_vta =" + pvta);//STRING PARA COMPROBAR SI EXISTE EL COMPROBANTE             
                int exis1 = int.Parse(query3.ExecuteScalar().ToString());

                while (exis1 == 0)
                {
                    string sucu = "";
                    if (suc == 1)
                    {
                        sucu = "UNICA";
                    }
                    query4.CommandText = string.Format("INSERT INTO IM_movimientos(ID_comp,modulo,cod_comp,letra,pto_vta,nro_comp,cta_cant,nro_socio,cod_doc,nro_doc,cod_sede,cod_condi,fec_comp,fec_conta,cod_costo,t_total,estado,tipo) VALUES('" + id2 + "','" + mod + "' , '" + cod_comp + "' , '" + le + "' , " + pvta + " , " + nro_comp + " ," + 1 + ", " + socio + " , 'CUIT' , " + nroDoc.ToString().Replace("-", "") + " , '" + sucu + "' , " + 1 + " , '" + fecha + "' , '" + feContable + "' , '7' , " + tot.ToString().Replace(",", ".") + " , " + Estado + ",'D') ");

                    int fil = query4.ExecuteNonQuery();

                    if (fil > 0)
                    {
                        this.contNR++;
                        nroItem = 1;
                        string detalle = Convert.ToString(cod_comp + "-" + le + "-" + pvta + "-" + nro_comp + "");
                        insertarIMitems(idCOMprobante, id2, nro_comp, 1, 0, AliIVA, sb, 0, 0, 0, mes, anio, total);
                        insertarIMauditoria(id2, tipoOpe, detalle, feHoy, "AUTOMATICO", CodEquipo);
                        //insertarIMCaja(id, idCOMprobante, 1, 1, total, cod_comp, "1", nro_comp, fecha, feContable);

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
            }
            catch (SqlException exc)
            {
                string numeroLinea = exc.StackTrace.Substring(exc.StackTrace.Length - 4, 4);
                //MessageBox.Show("Error al agregar datos a IM_movimientos: " + exc.Message + " Linea: " + numeroLinea, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /***
         * METODO PARA AGREGAR REGISTROS A LA TABLA IM_ITEMS
         */
        public void insertarIMitems(long idCom, string Id_comp, long nro, int Item, int tipoCodigo, double ali_iva, double mSB, double mEXE, double mPIB, double mIVA, int me, int a, double total)
        {
            try
            {

                //OBTENGO VALORES DE LA TABLA IM_CODIGOS PARA INSERTARLOS EN LA TABLA IM_ITEMS

                /*  string tipCo = "";
                  switch (tipoCodigo)
                  {
                      case 1:
                          tipCo = "1";
                          break;
                      case 2:
                          tipCo = "2";
                          break;
                      case 3:
                          tipCo = "3";
                          break;
                      case 4:
                          tipCo = "4";
                          break;
                  }
                  //cn.Open();
                  string Sql = "Select cod_alquimia, tipo FROM IM_codigos WHERE cod_tercero= '" + tipCo + "'";
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
                  rea1.Close();*/

                /*
                 * AGREGO DATOS A LA TABLA IM_ITEMS
                 */
                SqlCommand query = cn.CreateCommand();
                query.CommandText = string.Format("INSERT INTO IM_items(ID_comp,tipo,nro_item,codigo,debe_haber,cod_iva,ali_iva,m_sb,m_int,m_bon,m_iva,m_exe,per_ib,m_tot,per_mes,per_anio,cta_nro,per_var,per_gan,per_iva,ret_iva,ret_var,ret_ib,ret_gan) VALUES('" + Id_comp + "','LQ'," + Item + ",'" + 26 + "','D','    1'," + ali_iva + "," + mSB.ToString().Replace(",", ".") + "," + 0 + "," + 0 + "," + mIVA.ToString().Replace(",", ".") + "," + mEXE.ToString().Replace(",", ".") + "," + mPIB.ToString().Replace(",", ".") + "," + total.ToString().Replace(",", ".") + "," + me + "," + a + "," + 1 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + ")");

                int fil = query.ExecuteNonQuery();

                if (fil > 0)
                {
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
                MessageBox.Show("Error al agregar datos a IM_ITEMS: " + exc.Message + " Linea: " + numeroLinea + " CODIGO COMP: " + idCom + " NUMERO: " + this.nro, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        * Metodo para ingresar un registro en la tabla IM_caja
       
        public void insertarIMCaja(string id, long idComp, int Item, double pago, double monto, string cod_comp, string IDbanco, long numeroCheque, string fechaCheqEmi, string fechaCobroCheque)
        {
            try
            {
             //  cn.Open();
                SqlCommand query2 = cn.CreateCommand();

                ////VERIFICO SI LOS COMPROBANTES EXISTEN  
                query2.CommandText = string.Format("SELECT COUNT(*) FROM IM_caja WHERE ID_comp = '" + id + "' AND nro_mov=" + pago + " AND monto= " + monto.ToString().Replace(",", ".") + " AND cod_medio ='" + cod_comp + "' and codigo1= '" + IDbanco + "' and codigo2=" + numeroCheque + " and fec_emite='" + fechaCheqEmi + "' and fec_cobro='" + fechaCobroCheque + "'");//STRING PARA COMPROBAR SI EXISTE EL COMPROBANTE             
                int exis = int.Parse(query2.ExecuteScalar().ToString());

                while (exis == 0)
                {
                    SqlCommand query = cn.CreateCommand();
                    query.CommandText = string.Format("INSERT INTO IM_caja(ID_comp,nro_mov,nro_item,monto,cod_medio,codigo1,codigo2,fec_emite, fec_cobro) VALUES('" + id + "'," + pago + "," + Item + "," + monto.ToString().Replace(",", ".") + ",'" + cod_comp + "','" + IDbanco + "'," + numeroCheque + ",'" + fechaCheqEmi + "','" + fechaCobroCheque + "')");

                    int fil = query.ExecuteNonQuery();

                    if (fil > 0)
                    {
                        this.contarcaja++;
                    }
                    else
                    {
                        MessageBox.Show("No se agregaron registros", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("NO SE PUDO AGREGAR A IM_CAJA: " + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
            }
        } */
    }
}

