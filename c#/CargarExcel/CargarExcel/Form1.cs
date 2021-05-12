using System;
using System.Windows.Forms;

namespace CargarExcel
{
    public partial class Form1 : Form
    {
        string RUTA;
        string rutaGuardar;
        public Form1()
        {
            InitializeComponent();
        }
        /*
         * Botón para seleccionar el archivo excel que se generara a xml
         */
        private void seleccionar_Click(object sender, EventArgs e)
        {
            string rutaExc = string.Empty;
            OpenFileDialog open = new OpenFileDialog();

            if (open.ShowDialog() == DialogResult.OK)
            {
                rutaExc = open.FileName;
            }
            txtRuta.Text = rutaExc;
            this.RUTA = rutaExc;

        }

        /*
        * Botón para seleccionar donde se guardara el archivo xml
        */

        private void button1_Click(object sender, EventArgs e)
        {
            string rutaDir = string.Empty;
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                rutaDir = fbd.SelectedPath;

            }
            this.rutaGuardar = rutaDir + "/xmlPrueba.xml";
            guardarEn.Text = rutaGuardar;

        }
        /*
        * boton para generar el excel a xml
        */
        private void btngene_Click(object sender, EventArgs e)
        {
            CargarEx ex = new CargarEx();
            ex.crearXml(rutaGuardar, "solicitud");
            ex.generarExc(RUTA);
            txtRuta.Text = "";
        }

    }
}
