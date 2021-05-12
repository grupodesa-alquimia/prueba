using SpreadsheetLight;
using System;
using System.Windows.Forms;
using System.Xml;

namespace CargarExcel
{
    class CargarEx
    {

        string rutaXml;
        XmlDocument doc;
        string A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z, AA, AB, AC; //objetos para obtener los registros del excel

        public void crearXml(string ruta, string nodoRaiz)
        {
            try
            {
                this.rutaXml = ruta;
                this.doc = new XmlDocument();

                XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "utf-8", null);

                XmlNode root = doc.DocumentElement;
                doc.InsertBefore(xmlDeclaration, root);

                XmlNode element1 = doc.CreateElement(nodoRaiz);
                doc.AppendChild(element1);

                doc.Save(ruta);
            }
            catch (Exception)
            {

            }

        }
        public void generarExc(string ruta)
        {
            try
            {
                SLDocument sl = new SLDocument(ruta);
                SLWorksheetStatistics propiedades = sl.GetWorksheetStatistics();

                string a = sl.GetCellValueAsString("A" + 2);
                string b = sl.GetCellValueAsString("B" + 2);
                string c = sl.GetCellValueAsString("C" + 2);
                string d = sl.GetCellValueAsString("D" + 2);
                string e = sl.GetCellValueAsString("E" + 2);
                string f = sl.GetCellValueAsString("F" + 2);
                string g = sl.GetCellValueAsString("G" + 2);
                string h = sl.GetCellValueAsString("H" + 2);
                string i = sl.GetCellValueAsString("I" + 2);
                string j = sl.GetCellValueAsString("J" + 2);
                string k = sl.GetCellValueAsString("K" + 2);
                string l = sl.GetCellValueAsString("L" + 2);
                string m = sl.GetCellValueAsString("M" + 2);
                string n = sl.GetCellValueAsString("N" + 2);
                string o = sl.GetCellValueAsString("O" + 2);

                this.A = a;
                this.B = b;
                this.C = c;
                this.D = d;
                this.E = e;
                this.F = f;
                this.H = h;
                this.I = i;
                this.J = j;
                this.K = k;
                this.L = l;
                this.M = m;
                this.N = n;
                this.O = o;

                crearElementosEncabezado(ruta);
                AgregarCamion(ruta);


                int ultimafila = propiedades.EndRowIndex;

                for (int xa = 2; xa <= ultimafila; xa++)
                {
                    string p = sl.GetCellValueAsString("P" + xa);
                    string q = sl.GetCellValueAsString("Q" + xa);
                    string r = sl.GetCellValueAsString("R" + xa);
                    string s = sl.GetCellValueAsString("S" + xa);
                    string t = sl.GetCellValueAsString("T" + xa);
                    string u = sl.GetCellValueAsString("U" + xa);
                    string v = sl.GetCellValueAsString("V" + xa);
                    string w = sl.GetCellValueAsString("W" + xa);
                    string x = sl.GetCellValueAsString("X" + xa);
                    string y = sl.GetCellValueAsString("Y" + xa);
                    string z = sl.GetCellValueAsString("Z" + xa);
                    string aa = sl.GetCellValueAsString("AA" + xa);
                    string ab = sl.GetCellValueAsString("AB" + xa);
                    string ac = sl.GetCellValueAsString("AC" + xa);
      
                    this.P = p;
                    this.Q = q;
                    this.R = r;
                    this.S = s;
                    this.T = t;
                    this.U = u;
                    this.V = v;
                    this.W = w;
                    this.X = x;
                    this.Y = y;
                    this.Z = z;
                    this.AA = aa;
                    this.AB = ab;
                    this.AC = ac;

                    agregarRegistro(ruta);
                }
                MessageBox.Show("XML GUARDADO EN: " + rutaXml, "Guardado");
                Application.Exit();
            }
            catch (Exception)
            {
                MessageBox.Show("ERROR AL GENERAR EL XML", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /*
         * Nodos para el encabezado
         */
        public XmlNode crearElementosEncabezado(string ruta)
        {
            SLDocument sl2 = new SLDocument(ruta);
            SLWorksheetStatistics propiedades = sl2.GetWorksheetStatistics();

            doc.Load(rutaXml);
            XmlNode nodoRaiz1 = doc.DocumentElement;


            XmlElement xversion = doc.CreateElement("version");
            nodoRaiz1.InsertAfter(xversion, nodoRaiz1.LastChild);
            xversion.InnerText = "1.0.1";

            XmlElement xtropas = doc.CreateElement("tropasPorProducto");
            nodoRaiz1.InsertAfter(xtropas, nodoRaiz1.LastChild);
            xtropas.InnerText = "true";


            XmlElement xA1 = doc.CreateElement(sl2.GetCellValueAsString("A1"));
            nodoRaiz1.InsertAfter(xA1, nodoRaiz1.LastChild);
            xA1.InnerText = A;

            XmlElement xB1 = doc.CreateElement(sl2.GetCellValueAsString("B1"));
            nodoRaiz1.InsertAfter(xB1, nodoRaiz1.LastChild);
            xB1.InnerText = B;

            XmlElement xC1 = doc.CreateElement(sl2.GetCellValueAsString("C1"));
            nodoRaiz1.InsertAfter(xC1, nodoRaiz1.LastChild);
            xC1.InnerText = C;

            XmlElement xD1 = doc.CreateElement(sl2.GetCellValueAsString("D1"));
            nodoRaiz1.InsertAfter(xD1, nodoRaiz1.LastChild);
            xD1.InnerText = D;

            XmlElement xE1 = doc.CreateElement(sl2.GetCellValueAsString("E1"));
            nodoRaiz1.InsertAfter(xE1, nodoRaiz1.LastChild);
            xE1.InnerText = E;

            XmlElement xF1 = doc.CreateElement(sl2.GetCellValueAsString("F1"));
            nodoRaiz1.InsertAfter(xF1, nodoRaiz1.LastChild);
            xF1.InnerText = F;

            XmlElement xG1 = doc.CreateElement(sl2.GetCellValueAsString("G1"));
            nodoRaiz1.InsertAfter(xG1, nodoRaiz1.LastChild);
            xG1.InnerText = G;

            XmlElement xH1 = doc.CreateElement(sl2.GetCellValueAsString("H1"));
            nodoRaiz1.InsertAfter(xH1, nodoRaiz1.LastChild);
            xH1.InnerText = H;

            XmlElement xI1 = doc.CreateElement(sl2.GetCellValueAsString("I1"));
            nodoRaiz1.InsertAfter(xI1, nodoRaiz1.LastChild);
            xI1.InnerText = I;

            XmlElement xJ1 = doc.CreateElement(sl2.GetCellValueAsString("J1"));
            nodoRaiz1.InsertAfter(xJ1, nodoRaiz1.LastChild);
            xJ1.InnerText = J;

            XmlElement xK1 = doc.CreateElement(sl2.GetCellValueAsString("K1"));
            nodoRaiz1.InsertAfter(xK1, nodoRaiz1.LastChild);
            xK1.InnerText = K;

            XmlElement xL1 = doc.CreateElement(sl2.GetCellValueAsString("L1"));
            nodoRaiz1.InsertAfter(xL1, nodoRaiz1.LastChild);
            xL1.InnerText = L;

            doc.Save(rutaXml);

            return xversion;
        }

        /*
         *Agregar nodos del camion
         */

        public XmlNode AgregarCamion(string ruta)
        {
            SLDocument sl2 = new SLDocument(ruta);
            SLWorksheetStatistics propiedades = sl2.GetWorksheetStatistics();

            doc.Load(rutaXml);
            XmlNode nodoRaiz1 = doc.DocumentElement;
            doc.Save(rutaXml);

            XmlNode camion = doc.CreateElement("camion");//Nodo padre

            nodoRaiz1.InsertAfter(camion, nodoRaiz1.LastChild);

            XmlElement xM1 = doc.CreateElement(sl2.GetCellValueAsString("M1"));
            xM1.InnerText = M;
            camion.AppendChild(xM1);

            XmlElement xN1 = doc.CreateElement(sl2.GetCellValueAsString("N1"));
            xN1.InnerText = N;
            camion.AppendChild(xN1);

            XmlElement xO1 = doc.CreateElement(sl2.GetCellValueAsString("O1"));
            xO1.InnerText = O;
            camion.AppendChild(xO1);

            doc.Save(rutaXml);

            return camion;
        }
        /*
         * agregar registros al nodo detalles
         */
        public XmlNode agregarRegistro(string ruta)
        {
            SLDocument sl2 = new SLDocument(ruta);
            SLWorksheetStatistics propiedades = sl2.GetWorksheetStatistics();

            doc.Load(rutaXml);
            XmlNode nodoRaiz = doc.DocumentElement;

            XmlNode detalles = doc.CreateElement("detalles");//Nodo padre
            nodoRaiz.InsertAfter(detalles, nodoRaiz.LastChild);

            XmlNode detalle = doc.CreateElement("detalle");//Nodo hijo de detalles
            detalles.AppendChild(detalle);



            XmlElement xP1 = doc.CreateElement(sl2.GetCellValueAsString("P1"));
            xP1.InnerText = P;
            detalle.AppendChild(xP1);

            XmlElement xQ1 = doc.CreateElement(sl2.GetCellValueAsString("Q1"));
            xQ1.InnerText = Q;
            detalle.AppendChild(xQ1);

            XmlElement xR1 = doc.CreateElement(sl2.GetCellValueAsString("R1"));
            xR1.InnerText = R;
            detalle.AppendChild(xR1);

            XmlElement xS1 = doc.CreateElement(sl2.GetCellValueAsString("S1"));
            xS1.InnerText = S;
            detalle.AppendChild(xS1);

            XmlElement xT1 = doc.CreateElement(sl2.GetCellValueAsString("X1"));
            xT1.InnerText = T;
            detalle.AppendChild(xT1);

            XmlElement xV1 = doc.CreateElement(sl2.GetCellValueAsString("V1"));
            xV1.InnerText = T;
            detalle.AppendChild(xV1);

            XmlElement xW1 = doc.CreateElement(sl2.GetCellValueAsString("W1"));
            xW1.InnerText = W;
            detalle.AppendChild(xW1);

            XmlNode producto = doc.CreateElement("producto");//Nodo hijo de detalles
            detalle.AppendChild(producto);

            XmlElement xX1 = doc.CreateElement(sl2.GetCellValueAsString("X1"));
            xX1.InnerText = X;
            producto.AppendChild(xX1);

            XmlElement xY1 = doc.CreateElement(sl2.GetCellValueAsString("Y1"));
            xY1.InnerText = Y;
            producto.AppendChild(xY1);

            XmlNode tropa = doc.CreateElement("tropa");//Nodo hijo de detalles
            detalle.AppendChild(tropa);

            XmlElement xZ1 = doc.CreateElement(sl2.GetCellValueAsString("Z1"));
            xZ1.InnerText = Z;
            tropa.AppendChild(xZ1);

            XmlElement xAA1 = doc.CreateElement(sl2.GetCellValueAsString("AA1"));
            xAA1.InnerText = AA;
            tropa.AppendChild(xAA1);

            XmlElement xAB1 = doc.CreateElement(sl2.GetCellValueAsString("AB1"));
            xAB1.InnerText = AB;
            tropa.AppendChild(xAB1);

            XmlElement xAC1 = doc.CreateElement(sl2.GetCellValueAsString("AC1"));
            xAC1.InnerText = AC;
            tropa.AppendChild(xAC1);

            doc.Save(rutaXml);

            return detalles;
        }

    }
}
