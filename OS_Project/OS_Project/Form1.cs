using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Runtime.InteropServices;
using System.IO;
using System.Reflection;





namespace OS_Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            timer1.Start();
            panel1.Hide();
        
        }
        bool flag = false;
        private void button1_Click(object sender, EventArgs e)
        {
            string keyPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
            string status_path = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\StartupApproved\Run";
            RegistryKey key = Registry.CurrentUser.OpenSubKey(keyPath, true);
            RegistryKey status = Registry.CurrentUser.OpenSubKey(status_path, true);

            {
                string[] programNames = key.GetValueNames();

                
                if (programNames.Length > 0 && flag == false)
                {
                    Console.WriteLine("List of startup programs:");
                    foreach (string programName in programNames)
                    {
                        byte[] value = (byte[])status.GetValue(programName);

                        string programPath = key.GetValue(programName).ToString();
                        checkedListBox1.Items.Add(programName, value[0] == 2);

                    }
                    flag = true;
                }


            }


            panel1.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            System.IO.DirectoryInfo di = new DirectoryInfo("C:\\Windows\\Temp");

            foreach (FileInfo file in di.GetFiles())
            {
                try
                {
                    file.Delete();

                }

                catch { }

            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                try
                {
                    dir.Delete(true);
                }

                catch { }

            }
            MessageBox.Show("Cleaned");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            PerformanceCounter memoryCounter = new PerformanceCounter("Memory", "Available MBytes");

            float availableMemoryMB = memoryCounter.NextValue();

            label2.Text = ((int)((16000 - availableMemoryMB) / 16000 * 100)).ToString() + "%";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Hide();
        }

        private void checkedListBox1_SelectedIndexChanged_1(object sender, ItemCheckEventArgs e)
        {
            string status_path = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\StartupApproved\Run";
            RegistryKey status = Registry.CurrentUser.OpenSubKey(status_path, true);

            byte[] value = (byte[])status.GetValue(checkedListBox1.Items[e.Index].ToString());

            if (!checkedListBox1.GetItemChecked(e.Index))
            {
                checkedListBox1.Items[e.Index].ToString();
                value[0] = 2;
                status.SetValue(checkedListBox1.Items[e.Index].ToString(), value);

            }
            else
            {

                value[0] = 3;
                status.SetValue(checkedListBox1.Items[e.Index].ToString(), value);

            }


        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
