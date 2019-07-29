using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace IMMO.BIM.TOOL
{
    /// <summary>
    /// Interaction logic for Export.xaml
    /// </summary>
    public partial class Export : Window
    {
        private static string folderPath = string.Empty;
        public Export()
        {
            InitializeComponent();
        }

        private void BtnSaveFileExport_Click(object sender, RoutedEventArgs e)
        {
            //using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            //{
            //System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            //MessageBox.Show(dialog.SelectedPath);
            if (folderPath != "")
            {
                string query = "select * from kl_raum";
                DataTable dt = DataConnection.GetData(query);
                string txt = string.Empty;
                FileStream fileStream = new FileStream("" + folderPath + "\\test.txt", FileMode.Create);
                TextWriter sw = new StreamWriter(fileStream);


                foreach (DataColumn column in dt.Columns)
                {
                    //Add the Header row for Text file.
                    txt += column.ColumnName + "\t\t";
                }

                //Add new line.
                txt += "\r\n";
                foreach (DataRow row in dt.Rows)
                {
                    foreach (DataColumn column in dt.Columns)
                    {
                        //Add the Data rows.
                        txt += row[column.ColumnName].ToString() + "\t\t";
                    }

                    //Add new line.
                    txt += "\r\n";
                }
                sw.Write(txt);

                sw.Close();
                fileStream.Close();
                Process.Start(folderPath + "\\test.txt", "notepad.exe");
            }
            else
                MessageBox.Show("Select folder to save file");
            //}
        }

        private void BtnExport_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                folderPath = dialog.SelectedPath;
                txtFolder.Text = folderPath;
            }
        }
    }
   
}
