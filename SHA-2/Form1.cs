using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SHA_2
{
    public partial class Form1 : Form
    {

        private string fileName;

        private string OpenFile()
        {
            string filePath = "";
            using (var openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                }
            }

            return filePath;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fileName = OpenFile();
            textBox1.Text = fileName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox2.Text);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            SHA2 hash = new SHA2();
            using (FileStream fileStreamIn = File.OpenRead(fileName))
            {
                long sizeFile = fileStreamIn.Length;
                byte[] contentFile = new byte[sizeFile];
                int countOfReadBytes = 0;
                while (countOfReadBytes < sizeFile)
                {
                    byte[] buffer = new byte[1024];
                    fileStreamIn.Read(buffer, 0, 1024);
                    countOfReadBytes += 1024;
                    for(int i=0; i <1024 && (countOfReadBytes+i) < sizeFile; i++)
                    {              
                        contentFile[countOfReadBytes + i] = buffer[i];
                    }
                }
                hash.Start(contentFile, sizeFile);
            }
            textBox2.Text = TackHashHex(hash);
        }

        private string TackHashHex(SHA2 hash)
        {
            string hashHexString = String.Empty;

            hashHexString += hash.DigestResult.H0.ToString("X");
            hashHexString += hash.DigestResult.H1.ToString("X");
            hashHexString += hash.DigestResult.H2.ToString("X");
            hashHexString += hash.DigestResult.H3.ToString("X");
            hashHexString += hash.DigestResult.H4.ToString("X");
            hashHexString += hash.DigestResult.H5.ToString("X");
            hashHexString += hash.DigestResult.H6.ToString("X");
            hashHexString += hash.DigestResult.H7.ToString("X");
            return hashHexString;
        }
    }
}
