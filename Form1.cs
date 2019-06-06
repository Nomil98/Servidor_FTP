using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace Servidor_FTP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void CargarArchivos(string DirecFTP, string Ruta, string usuario, string contra)
        {
           
            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(DirecFTP + "/" + Path.GetFileName(Ruta));

            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential(usuario, contra);
            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = false;


            
            FileStream Stream = File.OpenRead(Ruta);
            byte[] Buffer = new byte[Stream.Length];

            Stream.Read(Buffer, 0, Buffer.Length);
            Stream.Close();

            
            Stream reqStream = request.GetRequestStream();
            reqStream.Write(Buffer, 0, Buffer.Length);
            reqStream.Close();

            MessageBox.Show("Archivo cargado con éxito.");


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnsubir_Click(object sender, EventArgs e)
        {
            btnsubir.Enabled = false;
            Application.DoEvents();

            CargarArchivos(txtdireccion.Text, txtarchivo.Text, txtusuario.Text, txtcontra.Text);
            btnsubir.Enabled = true;


        }

        private void txtdireccion_TextChanged(object sender, EventArgs e)
        {
            if (txtdireccion.Text.StartsWith("ftp://"))
                txtdireccion.Text = "ftp://" + txtdireccion.Text;
        }

        private void btnRuta_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                txtarchivo.Text = openFileDialog1.FileName;
        }
    }
}
