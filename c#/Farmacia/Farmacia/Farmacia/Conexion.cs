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

        public string idcomprobante;
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
        public double SuBruto;
        public double M_int;
        public double M_bon;
        public double M_iva;
        public double M_tot;
        public int sucursal;
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

        public void buscarDatos()
        {
            try
            {
                cn.Open();
                MySqlDataReader reader = null;
               
                string mysql = "Select comprascabecera.IDComprobante, comprascabecera.tipo, comprascabecera.IDProveedor, comprascabecera.Letra, comprascabecera.PuntoVta, comprascabecera.Numero, EXTRACT(MONTH FROM comprascabecera.FechaEmision),EXTRACT(YEAR FROM comprascabecera.FechaEmision),comprascabecera.FechaEmision, comprascabecera.FechaImputación, comprascabecera.CUIT,comprascabecera.Total, comprascabecera.Estado, comprasdetalle.sucursal,comprasdetalle.IVAAlicuota,comprasdetalle.NetoExento, comprascabecera.IDTipoGasto FROM comprascabecera INNER JOIN comprasdetalle ON comprascabecera.IDComprobante = comprasdetalle.IDComprobante WHERE comprascabecera.fechaEmision between '2017-10-02' and '2017-11-01' AND CUIT<> '' and PuntoVta between 0 and 30000";
                
                MySqlCommand comando = new MySqlCommand(mysql, cn2);
                reader = comando.ExecuteReader();

               

                List<string> lista = new List<string>();

                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        //GUARDO LOS RESULTADOS DE LA CONSULTA A LOS ATRIBUTOS
                     
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
                        this.total = reader.GetDouble(11);
                        this.estado = reader.GetString(12);
                        this.sucursal = reader.GetInt32(13);
                        this.AliIVA = reader.GetInt32(14);
                        this.M_tot = reader.GetDouble(15);
                        this.tipoGasto = reader.GetInt32(16);
                        

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
                            lista.Add(Convert.ToString(M_tot));
                            lista.Add(Convert.ToString(tipoGasto));
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
                        string id = idcomprobante;
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
                        double tot = this.total;
                        int suc = this.sucursal;
                        int iva = this.AliIVA;
                        double mto = this.M_tot;
                        int tipoG = this.tipoGasto;

                        System.Guid miGUID = System.Guid.NewGuid();
                        id = miGUID.ToString();

                        insertarIMmovimientos(id,ti, mod, CodComp, socio, le, pvta, nro_comp, nroDoc, m, a, suc, fecha, feContable, tot, est, tipoG);
                        insertarIMitems(id, nro_comp, tipoG, iva, mto);
                    }
                }
                else
                {
                    MessageBox.Show("No se encontraron registros", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
                Console.WriteLine("\nComprobantes agregados: " + this.cont);
                Console.WriteLine("Comprobantes NO agregados: " + this.cont2);
                return;


            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al buscar " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("ERRROR: "+ex.Message);
            }
        }

                

       

        public void insertarIMmovimientos(string id, string ti,string mod, string cod_comp, int socio, char le, int pvta, long nro_comp, string nroDoc, int me, int a, int suc, string fecha, string feContable,  double tot, int Estado,int tipo)
        {
       
            try
            {
               
                /*
                 * OBTENGO DATOS DE LA TABLA IM_COMPROBANTES Y LOS AGREGO EN IM_MOVIMIENTOS
                 */
                string Sql = "Select cod_alquimia, modulo FROM IM_comprobantes WHERE cod_tercero= '" + ti+"'";
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

                    query.CommandText = string.Format("INSERT INTO IM_movimientos(ID_comp,modulo,cod_comp,letra,pto_vta,nro_comp,cta_cant,per_mes,per_anio,nro_socio,cod_doc,nro_doc,cod_sede,cod_condi,fec_comp,fec_conta,cod_costo,t_total,estado) VALUES('"+id+"','" + mod+"' , '" + cod_comp + "' , '" + le + "' , " + pvta + " , " + nro_comp + " , " + 12 + " , " + me + " , " + a + " , " + socio + " , 'CUIT' , " + nroDoc.ToString().Replace("-", "") + " , " + suc + " , " + 1 + " , '" + fecha + "' , '" + feContable + "' , 'A' , " + tot.ToString().Replace(",", ".") + " , " + Estado + " ) " );

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
                //SI EXISTE
                Console.WriteLine("\n");
                Console.Write("Ya existe el comprobante: Codigo: " + cod_comp + " -Letra: " + le + " -Punto de venta: " + pvta + " - Numero: " + nro_comp);
                this.cont2++;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error al agregar registros IM_Movimientos "+ex.Message);
            }
            cn.Close();
        }

        public void insertarIMitems(string Id_comp, long nro,int tipoCodigo,double ali_iva,double total)
        {
            //insertarIMitems(int Id_comp, string tipo, int nro_item, int cod, char debe_haber, string cod_iva, double ali_iva, double sb, double interes, double bonificacion, double iva, double total)
            try
            {

                cn.Open();
                MySqlCommand my = cn2.CreateCommand();
                my = new MySqlCommand("Select Count(comprasdetalle.IDComprobante) FROM comprasdetalle INNER JOIN comprascabecera ON comprasdetalle.IDComprobante = comprascabecera.IDComprobante where comprascabecera.numero = " + nro+ "");
                int item = int.Parse(my.ExecuteScalar().ToString()); ;


                /**
               * OBTENGO VALORES DE LA TABLA IM_CODIGOS PARA INSERTARLOS EN LA TABLA IM_ITEMS
               * */
                string Sql = "Select cod_alquimia, tipo FROM IM_codigos WHERE cod_tercero= " + tipoCodigo + "";

                SqlDataReader rea = null;
                SqlCommand comando = new SqlCommand(Sql, cn);
                rea = comando.ExecuteReader();

                string tipo = "";
                string cod = "";
                if (rea.Read())
                {
                    tipo = rea["tipo"].ToString();
                    cod = rea["cod_alquimia"].ToString();
                    
                }
                rea.Close();

              
                /*
                 * AGREGO DATOS A LA TABLA IM_ITEMS
                 */
                SqlCommand query = cn.CreateCommand();
                query.CommandText = string.Format("INSERT INTO IM_items(ID,ID_comp,tipo,nro_item,codigo,debe_haber,cod_iva,ali_iva,m_sb,m_int,m_bon,m_iva,m_tot) VALUES("+0+",'" + Id_comp + "','"+tipo+"'," +item+ ",'" + cod + "','D','asd'," + ali_iva + "," + 100 + "," + 300 + "," + 400 + "," + 21.0 + "," + total.ToString().Replace(",", ".") + ")");

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
                Console.WriteLine("Error al agregar datos a IM_ITEMS: " + exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}


