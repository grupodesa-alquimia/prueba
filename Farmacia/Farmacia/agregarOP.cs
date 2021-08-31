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
    class agregarOP
    {
        SqlConnection cn;
        MySqlConnection cn2;
        public string myConnectionString;
        public string myConnectionSQL;
        public long idcomprobante;
        public string modulo;
        public string Cod_comp;
        public string Cod_compcaja;
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
        public double total2;
        public string tipo;
        public int nroItem;
        public int tipoGasto;
        public int AliIVA;
        public double M_perIB;
        public double M_sb;
        public double M_exe;
        public double M_iva;
        public int sucursal;
        public string IDComp;
        public int cont;
        public int cont2;
        public int cont3;
        public string tipoOpe;
        public string feHoy;
        public string CodEquipo;
        public string date1;
        public string date2;
        public string date;
        public string dateDos;
        public char letracan;
        public int ptvtacan;
        public long numcan;
        public string tipocan;
        public int pago;
        public int medioDePago;
        public string cod_comprobante;
        public double montoTotal;
        public string idBanco;
        public long nroCheque;
        public string fechaEmiChe;
        public string fechaCobChe;
        public long cancelado;
        public string tipocomcan;
        public char letracaja;
        public int ptvtacaja;
        public long numcaja;
        public string tipocaja;
        public double montocaja;
        public long contarimputados;
        public long contarcaja;
        public string fechaTransferencia;
        public long nroTransferencia;
        public char letraefec;
        public int ptvtaefec;
        public long nromedioefec;
        public string tipoefec;
        public string fechaemisionefec;
        public string fechacobrofec;
        public string Cod_compefec;
        public double montocajaefec;
        public int idCuentaCheque;
        public string codigo1CajaChequeP;
        public int idCuentaTransferencia;
        public string codBanco;
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
        public void buscarop(string fe1, string fe2)
        {
            try
            {
                cn.Open();
                cn2.Open();
             
                date1 = fe1;                
                date2 = fe2;
                this.date = Convert.ToDateTime((date1)).ToString("yyyy/MM/dd");
                this.dateDos = Convert.ToDateTime((date2)).ToString("yyyy/MM/dd");

                string buscaOP = "select plex.comprascabecera.IDComprobante, plex.comprascabecera.tipo, plex.comprascabecera.IDProveedor, plex.comprascabecera.Letra, plex.comprascabecera.PuntoVta, plex.comprascabecera.Numero, EXTRACT(MONTH FROM plex.comprascabecera.FechaEmision),EXTRACT(YEAR FROM plex.comprascabecera.FechaEmision),plex.comprascabecera.FechaEmision, plex.comprascabecera.FechaImputacion, plex.proveedores.CUIT, plex.comprascabecera.Estado,  plex.comprascabecera.sucursal,comprascabecera.TotNogravado, comprascabecera.Totexento, comprascabecera.Total, comprascabecera.TotPercepciones from comprascabecera inner join proveedores on proveedores.Idproveedor = comprascabecera.idproveedor where comprascabecera.fechaEmision between '" + date + "' and '" + dateDos + "' and PuntoVta between 0 and 30000 HAVING comprascabecera.Tipo='OP'";
                MySqlDataReader reader = null;


                MySqlCommand comand = new MySqlCommand(buscaOP, cn2);
                reader = comand.ExecuteReader();
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
                        this.M_sb = reader.GetDouble(13);
                        this.M_exe = reader.GetDouble(14);
                        this.total = reader.GetDouble(15);
                        this.M_perIB = reader.GetDouble(16);

                        string id = "";
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
                        long IDComprobante = idcomprobante;
                        System.Guid miGUID = System.Guid.NewGuid();
                        id = miGUID.ToString();

                        if (M_sb > 0)
                        {
                            AliIVA = 21;
                        }
                        else if (M_sb == 0)
                        {
                            AliIVA = 0;
                        }

                        double aliVa = this.AliIVA;
                        double Mex = this.M_exe;
                        int tipG = this.tipoGasto;
                        double sb = this.M_sb;
                        double per = this.M_perIB;
                        double iv = this.M_iva;

                        total2 = Mex + sb + per + iv;
                        insertarIMmovimientos(id, ti, mod, CodComp, socio, le, pvta, nro_comp, nroDoc, suc, fecha, feContable, total, 0, 1, aliVa, Mex, tipG, sb, per, iv);
                    }
                }
                reader.Close();          
                //MessageBox.Show("\nSe agregaron " + fcnd.contFCND + " Fácturas y Notas de Debito.\nSe agregaron " + nc.contNC + " Notas de Crédito.\nSe agregaron " + this.cont+" Ordenes de Pago.","PROCESO COMPLETADO");
            }
            catch (Exception ex)
            {
                //MessageBox.Show("No se encontraron Ordenes de Pago: " + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        public void insertarIMmovimientos(string id, string ti, string mod, string cod_comp, int socio, char le, int pvta, long nro_comp, string nroDoc, int suc, string fecha, string feContable, double tot, int Estado, int tipotipoG, double aliVa, double Mex, int tipG, double sb, double per, double iv)
        {
            try
            {
                //OBTENGO DATOS DE LA TABLA IM_COMPROBANTES Y LOS AGREGO EN IM_MOVIMIENTOS
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

                nroItem = 1;
                SqlCommand query = cn.CreateCommand();
                SqlCommand query2 = cn.CreateCommand();
                
                //VERIFICO SI LOS COMPROBANTES EXISTEN  
                query2.CommandText = string.Format("SELECT COUNT(*) FROM IM_movimientos WHERE nro_comp = " + nro_comp + " AND letra='" + le + "' AND cod_comp= '" + cod_comp + "' AND pto_vta =" + pvta);//STRING PARA COMPROBAR SI EXISTE EL COMPROBANTE             
                int exis = int.Parse(query2.ExecuteScalar().ToString());
              
                //SI NO EXISTE:
                while (exis == 0)
                {
                    string sucu = "";
                    if (suc == 1)
                    {
                        sucu = "UNICA";
                    }
                    query.CommandText = string.Format("INSERT INTO IM_movimientos(ID_comp,modulo,cod_comp,letra,pto_vta,nro_comp,cta_cant,nro_socio,cod_doc,nro_doc,cod_sede,cod_condi,fec_comp,fec_conta,cod_costo,t_total,estado,tipo) VALUES('" + id + "','" + mod + "' , '" + cod_comp + "' , '" + le + "' , " + pvta + " , " + nro_comp + " ," + 1 + ", " + socio + " , 'CUIT' , " + nroDoc.ToString().Replace("-", "") + " , '" + sucu + "' , " + 0 + " , '" + fecha + "' , '" + feContable + "' , '7' , " + tot.ToString().Replace(",", ".") + " , " + Estado + ",'D') ");
                 
                    int fil = query.ExecuteNonQuery();
                    if (fil > 0)
                    {
                        this.cont++;
                        nroItem = 1;
                        string detalle = Convert.ToString(cod_comp + "-" + le + "-" + pvta + "-" + nro_comp + "");
                        insertarIMitems(idcomprobante, id, nro_comp, nroItem, tipG, aliVa, sb, Mex, per, iv, mes, anio, total);
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
                Console.Write("Ya existe el comprobante: ID_COMP:"+id+"- Codigo: " + cod_comp + " -Letra: " + le + " -Punto de venta: " + pvta + " - Numero: " + nro_comp);

            }
            catch (SqlException exc)
            {
                string numeroLinea = exc.StackTrace.Substring(exc.StackTrace.Length - 4, 4);

                MessageBox.Show("Error al agregar las OP a IM_movimientos: " + exc.Message + "\nLinea: " + numeroLinea, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        
        }

        /***
        * METODO PARA AGREGAR REGISTROS A LA TABLA IM_ITEMS
        */
        public void insertarIMitems(long idCom, string Id_comp, long nro, int Item, int tipoCodigo, double ali_iva, double mSB, double mEXE, double mPIB, double mIVA, int me, int a, double total)
        {
            try
            {
                /**
                    * OBTENGO VALORES DE LA TABLA IM_CODIGOS PARA INSERTARLOS EN LA TABLA IM_ITEMS
                **/

                string tipCo = "";
                if (tipoCodigo == null || tipoCodigo==0)
                {
                    tipCo = "1         ";
                }
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
                    this.tipo = tipo;
                }
                rea1.Close();



                /*
                * AGREGO DATOS A LA TABLA IM_ITEMS
                */
                if (mEXE == 0)
                {
                    mEXE = total;
                }
                SqlCommand query = cn.CreateCommand();
                query.CommandText = string.Format("INSERT INTO IM_items(ID_comp,tipo,nro_item,codigo,debe_haber,cod_iva,ali_iva,m_sb,m_int,m_bon,m_iva,m_exe,per_ib,m_tot,per_mes,per_anio,cta_nro,per_var,per_gan,per_iva,ret_iva,ret_var,ret_ib,ret_gan) VALUES('" + Id_comp + "','" + this.tipo + "'," + Item + ",'" + cod+ "','D','    1'," + ali_iva + "," + mSB.ToString().Replace(",", ".") + "," + 0 + "," + 0 + "," + mIVA.ToString().Replace(",", ".") + "," + mEXE.ToString().Replace(",", ".") + "," + mPIB.ToString().Replace(",", ".") + "," + total.ToString().Replace(",", ".") + "," + me + "," + a + "," + 1 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + ")");

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
         * METODO PARA OBTENER LOS VALORES QUE IRAN A IM_IMPUTADOS E IM_CAJA
         */
        public void itemImimputados()
        {
            try
            {
                char letracan;
                int ptvtacan;
                long numcan;
                long idCancelado = 0;
                string tipocan = "";
                double monTotal = 0;
                string countMysql = "SELECT plex.pagosproveedores.idcomprobantecancelatorio, plex.pagosproveedores.idcomprobantecancelado, plex.pagosproveedores.monto, plex.comprascabecera.letra, plex.comprascabecera.puntovta,plex.comprascabecera.numero, plex.comprascabecera.tipo from plex.pagosproveedores inner join plex.comprascabecera on plex.comprascabecera.idcomprobante = plex.pagosproveedores.idcomprobantecancelatorio where comprascabecera.tipo = 'op' and comprascabecera.fechaEmision between '" + date + "' and '" + dateDos + "'";

                using (MySqlConnection cone = new MySqlConnection(myConnectionString))
                {
                    cone.Open();
                    MySqlCommand command = new MySqlCommand(countMysql, cone);
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            idcomprobante = reader.GetInt64(0);
                            idCancelado = reader.GetInt64(1);
                            monTotal = reader.GetDouble(2);
                            this.letra = reader.GetChar(3);
                            this.ptovta = reader.GetInt32(4);
                            this.nro = reader.GetInt32(5);
                            this.tipo = reader.GetString(6);
                            this.cancelado = idCancelado;
                            this.montoTotal = monTotal;

                            using (SqlConnection conesql = new SqlConnection(myConnectionSQL))
                            {
                                conesql.Open();
                                string cod_comp = "";
                                string Sql = "Select cod_alquimia FROM IM_comprobantes WHERE cod_tercero= '" + this.tipo + "'";
                                SqlDataReader rea = null;
                                SqlCommand comandodsa = new SqlCommand(Sql, conesql);
                                rea = comandodsa.ExecuteReader();

                                if (rea.Read())
                                {
                                    cod_comp = rea["cod_alquimia"].ToString();
                                    this.Cod_comp = cod_comp;
                                }
                                rea.Close();

                                string IDCOMPROBANTECANCELATORIO = "Select ID_comp FROM IM_movimientos WHERE cod_comp= '" + this.Cod_comp + "' and letra = '" + this.letra + "' and pto_vta=" + this.ptovta + " and nro_comp=" + this.nro + "";
                                SqlDataReader Rea = null;
                                SqlCommand CMAND = new SqlCommand(IDCOMPROBANTECANCELATORIO, conesql);
                                Rea = CMAND.ExecuteReader();

                                while (Rea.Read())
                                {
                                    this.IDComp = Rea["ID_comp"].ToString();
                                }
                                Rea.Close();
                            }

                            using (MySqlConnection cone2 = new MySqlConnection(myConnectionString))
                            {
                                cone2.Open();
                                //OBTENGO LA LETRA, PUNTO DE VENTA, NUMERO Y CODIGO DEL COMPROBANTE CANCELADO Y VERIFICO SI EXISTEN EN IM_MOVIMIENTOS
                                string busca = "select letra,puntovta,numero,tipo from comprascabecera where idcomprobante= " + this.cancelado;

                                MySqlCommand command2 = new MySqlCommand(busca, cone2);
                                MySqlDataReader reader2 = command2.ExecuteReader();

                                while (reader2.Read())
                                {
                                    letracan = reader2.GetChar(0);
                                    ptvtacan = reader2.GetInt32(1);
                                    numcan = reader2.GetInt32(2);
                                    tipocan = reader2.GetString(3);
                                    this.letracan = letracan;
                                    this.ptvtacan = ptvtacan;
                                    this.numcan = numcan;
                                    this.tipocan = tipocan;
                                }
                                reader2.Close();
                            }

                            using (SqlConnection ccc = new SqlConnection(myConnectionSQL))
                            {
                                ccc.Open();
                                string Sql = "Select cod_alquimia FROM IM_comprobantes WHERE cod_tercero= '" + this.tipocan + "'";
                                SqlDataReader rea = null;
                                SqlCommand comandodsa = new SqlCommand(Sql, ccc);
                                rea = comandodsa.ExecuteReader();
                                string tipoCan = "";
                                if (rea.Read())
                                {
                                    tipoCan = rea["cod_alquimia"].ToString();
                                    this.tipocomcan = tipoCan;
                                }
                                rea.Close();
                                ccc.Close();
                            }
                            insertarImimputados(this.IDComp, this.nroItem, this.tipocomcan, this.letracan, this.ptvtacan, this.numcan, this.montoTotal);                            
                        }
                        Console.WriteLine("\nSe agregaron "+contarimputados+" a IM_imputados");
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("NO SE PUDO AGREGAR A IM_IMPUTADOS: " + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
                cn2.Close();
            }
        }
        /***
         * Insertar datos a la tabla Im_Imputados
         */
        public void insertarImimputados(string id_comp, int nroitem, string codComp, char letra, int pvta, long nro, double tot)
        {
            try
            {
                cn.Open();
                SqlCommand query = cn.CreateCommand();
                SqlCommand query2 = cn.CreateCommand();

                //VERIFICO SI LOS COMPROBANTES EXISTEN  
                query2.CommandText = string.Format("SELECT COUNT(*) FROM IM_Imputados WHERE ID_comp = '" + id_comp + "' AND DB_comp='" + codComp + "' AND DB_letra= '" + letra + "' AND DB_pvta = "+ pvta+" and DB_nro= "+nro+" and DB_item="+nroitem+" and monto="+tot.ToString().Replace(",", "."));//STRING PARA COMPROBAR SI EXISTE EL COMPROBANTE             
                int exis = int.Parse(query2.ExecuteScalar().ToString());

                while (exis == 0)
                {  
                    query.CommandText = string.Format("INSERT INTO IM_imputados(ID_comp,ID_item,DB_comp,DB_letra,DB_pvta,DB_nro,DB_item,monto) VALUES('" + id_comp + "'," + nroitem + ",'" + codComp + "','" + letra + "'," + pvta + "," + nro + "," + 1 + "," + tot.ToString().Replace(",", ".") + ")");
                    int fil = query.ExecuteNonQuery();

                    if (fil > 0)
                    {
                        this.contarimputados++;
                    }
                    else
                    {
                        MessageBox.Show("No se agregaron registros", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return;
                }
                Console.WriteLine("Ya existe este comprobante en im_imputados");
            }
            catch (Exception exc)
            {
                string numeroLinea = exc.StackTrace.Substring(exc.StackTrace.Length - 4, 4);

                Console.WriteLine("\n");
                //MessageBox.Show("Error al agregar datos a IM_imputados: " + exc.Message + " Linea: " + numeroLinea, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cn.Close();
            }
        }


        public void obtenerChequeTercero()
        {

            double monto = 0;
            int pago = 0;
            int medioDePago = 0;
            string countMysql = "select plex.pagosproveedoresmedios.idcomprobante,plex.pagosproveedoresmedios.pago,plex.pagosproveedoresmedios.importe,plex.pagosproveedoresmedios.idmediodepago, plex.pagosproveedoresmedios.idcheque, plex.cheques.idBanco,plex.cheques.numero, plex.cheques.FechaEmision, plex.cheques.fechacobro,plex.comprascabecera.letra, plex.comprascabecera.puntovta,plex.comprascabecera.numero, plex.comprascabecera.tipo from plex.pagosproveedoresmedios inner join plex.cheques on pagosproveedoresmedios.idcheque = cheques.idcheque INNER JOIN plex.comprascabecera on pagosproveedoresmedios.idcomprobante = comprascabecera.idcomprobante where comprascabecera.tipo = 'OP' and cheques.tipo='T' and comprascabecera.fechaEmision between '" + date + "' and '" + dateDos + "'";

            using (MySqlConnection cone = new MySqlConnection(myConnectionString))
            {
                cone.Open();
                MySqlCommand command = new MySqlCommand(countMysql, cone);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        idcomprobante = reader.GetInt64(0);
                        pago = reader.GetInt32(1);
                        monto = reader.GetDouble(2);
                        medioDePago = reader.GetInt32(3);
                        this.idBanco = reader.GetString(5);
                        this.nroCheque = reader.GetInt64(6);
                        this.fechaEmiChe = reader.GetString(7);
                        this.fechaCobChe = reader.GetString(8);
                        this.letracaja = reader.GetChar(9);
                        this.ptvtacaja = reader.GetInt32(10);
                        this.numcaja= reader.GetInt32(11);
                        this.tipocaja = reader.GetString(12);
                        this.montocaja = monto;
                        this.pago = pago;
                        this.medioDePago = medioDePago;
                        
                        using (SqlConnection ccc = new SqlConnection(myConnectionSQL))
                        {
                            ccc.Open();
                            string codcomp = "";
                            // string tipo = "";
                            string tipCo = "";
                            switch (this.medioDePago)
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
                                case 5:
                                    tipCo = "PIB       ";
                                    break;
                                case 6:
                                    tipCo = "6";
                                    break;
                                case 7:
                                    tipCo = "7";
                                    break;
                            }
                            
                            string Sqla = "Select cod_alquimia,cod_alquimia1, tipo FROM IM_Codigos WHERE cod_tercero= '" + tipCo + "' and tipo='ME'";
                            SqlDataReader reada = null;
                            SqlCommand comandoa = new SqlCommand(Sqla, ccc);
                            reada = comandoa.ExecuteReader();

                            if (reada.Read())
                            {
                                codcomp = reada["cod_alquimia"].ToString();
                                this.cod_comprobante = codcomp;
                            }
                            reada.Close();

                            string codBanco = "";
                            string Sqla1 = "Select cod_alquimia, tipo FROM IM_Codigos WHERE cod_tercero= '" + idBanco + "' and tipo='BA'";
                            SqlDataReader reada1 = null;
                            SqlCommand comandoa1 = new SqlCommand(Sqla1, ccc);
                            reada1 = comandoa1.ExecuteReader();

                            if (reada1.Read())
                            {
                                codBanco = reada1["cod_alquimia"].ToString();
                                this.codBanco = codBanco;
                            }
                            reada1.Close();


                            string cod_comp = "";
                            string Sql = "Select cod_alquimia FROM IM_comprobantes WHERE cod_tercero= '" + this.tipocaja + "'";
                            SqlDataReader rea = null;
                            SqlCommand comandodsa = new SqlCommand(Sql, ccc);
                            rea = comandodsa.ExecuteReader();

                            if (rea.Read())
                            {
                                cod_comp = rea["cod_alquimia"].ToString();
                                this.Cod_compcaja = cod_comp;
                            }
                            rea.Close();

                            string IDCOMPROBANTECANCELATORIO = "Select ID_comp FROM IM_movimientos WHERE cod_comp= '" + this.Cod_compcaja + "' and letra = '" + this.letracaja + "' and pto_vta=" + this.ptvtacaja + " and nro_comp=" + this.numcaja + "";
                            SqlDataReader Rea = null;
                            SqlCommand CMAND = new SqlCommand(IDCOMPROBANTECANCELATORIO, ccc);
                            Rea = CMAND.ExecuteReader();

                            while (Rea.Read())
                            {
                                this.IDComp = Rea["ID_comp"].ToString();
                            }
                            Rea.Close();

                        }

                        insertarIMCaja(this.IDComp, idcomprobante, nroItem, this.pago, this.montocaja, this.cod_comprobante, this.codBanco, this.nroCheque, this.fechaEmiChe, this.fechaCobChe, codigo1CajaChequeP);

                    }
                    Console.WriteLine("Se agregaron "+ this.contarcaja +" comprobantes nuevos a im_caja");
                }

            }
        }


        public void obtenerChequePropio()
        {

            double monto = 0;
            int pago = 0;
            int medioDePago = 0;
            string countMysql = "select plex.pagosproveedoresmedios.idcomprobante,plex.pagosproveedoresmedios.pago,plex.pagosproveedoresmedios.importe,plex.pagosproveedoresmedios.idmediodepago, plex.pagosproveedoresmedios.idcheque, plex.cheques.idBanco,plex.cheques.numero, plex.cheques.FechaEmision, plex.cheques.fechacobro,plex.comprascabecera.letra, plex.comprascabecera.puntovta,plex.comprascabecera.numero, plex.comprascabecera.tipo,plex.cheques.idCuenta from plex.pagosproveedoresmedios inner join plex.cheques on pagosproveedoresmedios.idcheque = cheques.idcheque INNER JOIN plex.comprascabecera on pagosproveedoresmedios.idcomprobante = comprascabecera.idcomprobante where comprascabecera.tipo = 'OP' and cheques.tipo='P' and comprascabecera.fechaEmision between '" + date + "' and '" + dateDos + "'";

            using (MySqlConnection cone = new MySqlConnection(myConnectionString))
            {
                cone.Open();
                MySqlCommand command = new MySqlCommand(countMysql, cone);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        idcomprobante = reader.GetInt64(0);
                        pago = reader.GetInt32(1);
                        monto = reader.GetDouble(2);
                        medioDePago = reader.GetInt32(3);
                        this.idBanco = reader.GetString(5);
                        this.nroCheque = reader.GetInt64(6);
                        this.fechaEmiChe = reader.GetString(7);
                        this.fechaCobChe = reader.GetString(8);
                        this.letracaja = reader.GetChar(9);
                        this.ptvtacaja = reader.GetInt32(10);
                        this.numcaja = reader.GetInt32(11);
                        this.tipocaja = reader.GetString(12);
                        this.idCuentaCheque = reader.GetInt32(13);
                        this.montocaja = monto;
                        this.pago = pago;
                        this.medioDePago = medioDePago;

                        using (SqlConnection ccc = new SqlConnection(myConnectionSQL))
                        {
                            ccc.Open();
                            string codcomp = "";
                            // string tipo = "";
                            string tipCo = "";
                            switch (this.medioDePago)
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
                                case 5:
                                    tipCo = "PIB       ";
                                    break;
                                case 6:
                                    tipCo = "6";
                                    break;
                                case 7:
                                    tipCo = "7";
                                    break;
                            }
                            string idCodTercero1 = "";
                            switch (this.idCuentaCheque)
                            {
                                case 1:
                                    idCodTercero1 = "1         ";
                                    break;
                                case 2:
                                    idCodTercero1 = "2         ";
                                    break;

                            }

                            string Sqla = "Select cod_alquimia,cod_alquimia1, tipo FROM IM_Codigos WHERE cod_tercero= '" + tipCo + "' and cod_tercero1 = '" + idCodTercero1 + "' and tipo='ME'";
                            SqlDataReader reada = null;
                            SqlCommand comandoa = new SqlCommand(Sqla, ccc);
                            reada = comandoa.ExecuteReader();

                            if (reada.Read())
                            {
                                codcomp = reada["cod_alquimia"].ToString();
                                this.cod_comprobante = codcomp;
                                this.codigo1CajaChequeP = reada["cod_alquimia1"].ToString();
                            }
                            reada.Close();

                            string codBanco = "";
                            string Sqla1 = "Select cod_alquimia, tipo FROM IM_Codigos WHERE cod_tercero= '" + idBanco + "' and tipo='BA'";
                            SqlDataReader reada1 = null;
                            SqlCommand comandoa1 = new SqlCommand(Sqla1, ccc);
                            reada1 = comandoa1.ExecuteReader();

                            if (reada1.Read())
                            {
                                codBanco = reada1["cod_alquimia"].ToString();
                                this.codBanco = codBanco;
                            }
                            reada1.Close();

                            string cod_comp = "";
                            string Sql = "Select cod_alquimia FROM IM_comprobantes WHERE cod_tercero= '" + this.tipocaja + "'";
                            SqlDataReader rea = null;
                            SqlCommand comandodsa = new SqlCommand(Sql, ccc);
                            rea = comandodsa.ExecuteReader();

                            if (rea.Read())
                            {
                                cod_comp = rea["cod_alquimia"].ToString();
                                this.Cod_compcaja = cod_comp;
                            }
                            rea.Close();

                            string IDCOMPROBANTECANCELATORIO = "Select ID_comp FROM IM_movimientos WHERE cod_comp= '" + this.Cod_compcaja + "' and letra = '" + this.letracaja + "' and pto_vta=" + this.ptvtacaja + " and nro_comp=" + this.numcaja + "";
                            SqlDataReader Rea = null;
                            SqlCommand CMAND = new SqlCommand(IDCOMPROBANTECANCELATORIO, ccc);
                            Rea = CMAND.ExecuteReader();

                            while (Rea.Read())
                            {
                                this.IDComp = Rea["ID_comp"].ToString();
                            }
                            Rea.Close();

                        }

                        insertarIMCaja(this.IDComp, idcomprobante, nroItem, this.pago, this.montocaja, this.cod_comprobante, this.codBanco, this.nroCheque, this.fechaEmiChe, this.fechaCobChe, codigo1CajaChequeP);

                    }
                   // Console.WriteLine("Se agregaron " + this.contarcaja + " comprobantes nuevos a im_caja");
                }

            }
        }


        /***
         * Obtener datos de transferencias para insertar en la tabla IM_caja
         */
        public void obtenerCajaTranferencia()
        {

            double monto = 0;
            int pago = 0;
            int medioDePago = 0;
            string countMysql = "select plex.pagosproveedoresmedios.idcomprobante,plex.pagosproveedoresmedios.pago, plex.pagosproveedoresmedios.importe,plex.pagosproveedoresmedios.idmediodepago, plex.comprascabecera.letra, plex.comprascabecera.puntovta,plex.comprascabecera.numero, plex.comprascabecera.tipo, plex.pagosproveedoresmedios.idTransferencia ,plex.transferencias.nroOperacion,plex.transferencias.fecha,plex.transferencias.idcuenta from plex.pagosproveedoresmedios INNER JOIN plex.comprascabecera on pagosproveedoresmedios.idcomprobante = comprascabecera.idcomprobante inner join plex.transferencias on transferencias.idtransferencia = pagosproveedoresmedios.idtransferencia where comprascabecera.tipo = 'OP' and comprascabecera.fechaEmision between '" + date + "' and '" + dateDos + "'";

            using (MySqlConnection cone = new MySqlConnection(myConnectionString))
            {
                cone.Open();
                MySqlCommand command = new MySqlCommand(countMysql, cone);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        idcomprobante = reader.GetInt64(0);
                        pago = reader.GetInt32(1);
                        monto = reader.GetDouble(2);
                        medioDePago = reader.GetInt32(3);
                        this.letracaja = reader.GetChar(4);
                        this.ptvtacaja = reader.GetInt32(5);
                        this.numcaja = reader.GetInt32(6);
                        this.tipocaja = reader.GetString(7);
                        this.nroTransferencia = reader.GetInt32(9);
                        this.fechaTransferencia = reader.GetString(10);
                        this.idCuentaTransferencia = reader.GetInt32(11);
                        this.montocaja = monto;
                        this.pago = pago;
                        this.medioDePago = medioDePago;

                        using (SqlConnection ccc = new SqlConnection(myConnectionSQL))
                        {
                            ccc.Open();
                            string codcomp = "";
                            // string tipo = "";
                            string tipCo = "";
                            switch (this.medioDePago)
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
                                case 5:
                                    tipCo = "PIB       ";
                                    break;
                                case 6:
                                    tipCo = "6";
                                    break;
                                case 7:
                                    tipCo = "7";
                                    break;
                            }
                            string idCodTercero1 = "";
                            switch (this.idCuentaTransferencia)
                            {
                                case 1:
                                    idCodTercero1 = "1         ";
                                    break;
                                case 2:
                                    idCodTercero1 = "2         ";
                                    break;

                            }
                            string Sqla = "Select cod_alquimia, tipo FROM IM_Codigos WHERE cod_tercero= '" + tipCo + "' and cod_tercero1='"+idCodTercero1+"' and tipo='ME'";
                            SqlDataReader reada = null;
                            SqlCommand comandoa = new SqlCommand(Sqla, ccc);
                            reada = comandoa.ExecuteReader();

                            if (reada.Read())
                            {
                                codcomp = reada["cod_alquimia"].ToString();
                                this.cod_comprobante = codcomp;
                                //this.codigo1CajaTransferencia = reada["cod_alquimia1"].ToString();
                            }
                            reada.Close();



                            string cod_comp = "";
                            string Sql = "Select cod_alquimia FROM IM_comprobantes WHERE cod_tercero= '" + this.tipocaja + "'";
                            SqlDataReader rea = null;
                            SqlCommand comandodsa = new SqlCommand(Sql, ccc);
                            rea = comandodsa.ExecuteReader();

                            if (rea.Read())
                            {
                                cod_comp = rea["cod_alquimia"].ToString();
                                this.Cod_compcaja = cod_comp;
                            }
                            rea.Close();

                            string IDCOMPROBANTECANCELATORIO = "Select ID_comp FROM IM_movimientos WHERE cod_comp= '" + this.Cod_compcaja + "' and letra = '" + this.letracaja + "' and pto_vta=" + this.ptvtacaja + " and nro_comp=" + this.numcaja + "";
                            SqlDataReader Rea = null;
                            SqlCommand CMAND = new SqlCommand(IDCOMPROBANTECANCELATORIO, ccc);
                            Rea = CMAND.ExecuteReader();

                            while (Rea.Read())
                            {
                                this.IDComp = Rea["ID_comp"].ToString();
                            }
                            Rea.Close();

                        }
                        insertarIMCaja(this.IDComp, idcomprobante, nroItem, this.pago, this.montocaja, this.cod_comprobante, "", this.nroTransferencia, this.fechaTransferencia, this.fechaTransferencia, "");

                    }
                    //Console.WriteLine("Se agregaron " + this.contarcaja + " comprobantes nuevos a im_caja");
                }

            }
        }


        public void obtenerCajaEfectivo()
        {

            double monto = 0;
            int pago = 0;
            int medioDePago = 0;
            string countMysql = "select plex.pagosproveedoresmedios.idcomprobante,plex.pagosproveedoresmedios.pago, plex.pagosproveedoresmedios.importe,plex.pagosproveedoresmedios.idmediodepago, plex.comprascabecera.letra, plex.comprascabecera.puntovta,plex.comprascabecera.numero, plex.comprascabecera.tipo,comprascabecera.fechaEmision, comprascabecera.fechaimputacion from plex.pagosproveedoresmedios INNER JOIN plex.comprascabecera on pagosproveedoresmedios.idcomprobante = comprascabecera.idcomprobante where comprascabecera.tipo = 'OP' and comprascabecera.fechaEmision between '" + date + "' and '" + dateDos + "' and concepto='EFECTIVO'";

            using (MySqlConnection cone = new MySqlConnection(myConnectionString))
            {
                cone.Open();
                MySqlCommand command = new MySqlCommand(countMysql, cone);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        idcomprobante = reader.GetInt64(0);
                        pago = reader.GetInt32(1);
                        monto = reader.GetDouble(2);
                        medioDePago = reader.GetInt32(3);
                        this.letraefec = reader.GetChar(4);
                        this.ptvtaefec = reader.GetInt32(5);
                        this.nromedioefec = reader.GetInt32(6);
                        this.tipoefec = reader.GetString(7);
                        this.fechaemisionefec = reader.GetString(8);
                        this.fechacobrofec = reader.GetString(9);
                        this.montocajaefec = monto;
                        this.pago = pago;
                        this.medioDePago = medioDePago;

                        using (SqlConnection ccc = new SqlConnection(myConnectionSQL))
                        {
                            ccc.Open();
                            string codcomp = "";
                            // string tipo = "";
                            string tipCo = "";
                            switch (this.medioDePago)
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
                                case 5:
                                    tipCo = "PIB       ";
                                    break;
                                case 6:
                                    tipCo = "6";
                                    break;
                                case 7:
                                    tipCo = "7";
                                    break;
                            }

                            string Sqla = "Select cod_alquimia, tipo FROM IM_Codigos WHERE cod_tercero= '" + tipCo + "' and tipo='ME'";
                            SqlDataReader reada = null;
                            SqlCommand comandoa = new SqlCommand(Sqla, ccc);
                            reada = comandoa.ExecuteReader();

                            if (reada.Read())
                            {
                                codcomp = reada["cod_alquimia"].ToString();
                                this.cod_comprobante = codcomp;
                            }
                            reada.Close();

                            string cod_comp = "";
                            string Sql = "Select cod_alquimia FROM IM_comprobantes WHERE cod_tercero= '" + this.tipoefec + "'";
                            SqlDataReader rea = null;
                            SqlCommand comandodsa = new SqlCommand(Sql, ccc);
                            rea = comandodsa.ExecuteReader();

                            if (rea.Read())
                            {
                                cod_comp = rea["cod_alquimia"].ToString();
                                this.Cod_compefec = cod_comp;
                            }
                            rea.Close();

                            string IDCOMPROBANTECANCELATORIO = "Select ID_comp FROM IM_movimientos WHERE cod_comp= '" + this.Cod_compefec + "' and letra = '" + this.letraefec + "' and pto_vta=" + this.ptvtaefec + " and nro_comp=" + this.nromedioefec + "";
                            SqlDataReader Rea = null;
                            SqlCommand CMAND = new SqlCommand(IDCOMPROBANTECANCELATORIO, ccc);
                            Rea = CMAND.ExecuteReader();

                            while (Rea.Read())
                            {
                                this.IDComp = Rea["ID_comp"].ToString();
                            }
                            Rea.Close();

                        }
                        insertarIMCaja(this.IDComp, idcomprobante, nroItem, this.pago, this.montocajaefec, this.cod_comprobante, "", this.nromedioefec, this.fechaemisionefec, this.fechacobrofec,"");

                    }
                   // Console.WriteLine("Se agregaron " + this.contarcaja + " comprobantes nuevos a im_caja");
                }

            }
        }
        /***
         * INSERTAR OP A LA TABLA IM_CAJA
         */
        public void insertarIMCaja(string id,long idComp, int Item, int pago, double monto, string cod_comp, string IDbanco, long numeroCheque, string fechaCheqEmi, string fechaCobroCheque, string cod3)
        {
            try
            {
                cn.Open();
                SqlCommand query2 = cn.CreateCommand();

                ////VERIFICO SI LOS COMPROBANTES EXISTEN   and codigo1= '" + IDbanco + "' 
                query2.CommandText = string.Format("SELECT COUNT(*) FROM IM_caja WHERE ID_comp = '" + id + "' AND nro_mov=" + pago + " AND monto= '" + monto.ToString().Replace(",", ".") + "' AND cod_medio ='" + cod_comp + "'and codigo2=" + numeroCheque + " and fec_emite='" + fechaCheqEmi + "' and fec_cobro='" + fechaCobroCheque + "'");//STRING PARA COMPROBAR SI EXISTE EL COMPROBANTE             
                int exis = int.Parse(query2.ExecuteScalar().ToString());

                while (exis == 0)
                {
                    SqlCommand query = cn.CreateCommand();
                    query.CommandText = string.Format("INSERT INTO IM_caja(ID_comp,nro_mov,nro_item,monto,cod_medio,codigo1,codigo2,codigo3,fec_emite, fec_cobro) VALUES('" + id + "'," + pago + "," + Item + "," + monto.ToString().Replace(",", ".") + ",'" + cod_comp + "','" + IDbanco + "'," + numeroCheque + ",'" + cod3 + "','" + fechaCheqEmi + "','" + fechaCobroCheque + "')");

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
                cn.Close();
            }
        }
    }
}
