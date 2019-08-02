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
                FileStream fileStream = new FileStream("" + folderPath + "\\file1.txt", FileMode.Create);
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
                Process.Start(folderPath + "\\file1.txt", "notepad.exe");

                query = "select geb_id,geschoss_id,cad_id from kl_raum";
                dt = DataConnection.GetData(query);
                int fileRowId = 1;
                if(dt!=null && dt.Rows.Count > 0)
                {
                    txt = string.Empty;
                    fileStream = new FileStream("" + folderPath + "\\file2.txt", FileMode.Create);
                    sw = new StreamWriter(fileStream);
                    string[] columns = new string[24];
                    columns[0] = "Laufende Nr";
                    columns[1] = "Gebäude";
                    columns[2] = "Geschoss";
                    columns[3] = "CAD-ID";
                    columns[4] = "Ausstattungstyp";
                    columns[5] = "Att1";
                    columns[6] = "Att_Typ2";
                    columns[7] = "Att2";
                    columns[8] = "Att_Typ3";
                    columns[9] = "Att3";
                    columns[10] = "Att_Typ4";
                    columns[11] = "Att4";
                    columns[12] = "Att_Typ5";
                    columns[13] = "Att5";
                    columns[14] = "Att_Typ6";
                    columns[15] = "Att6";
                    columns[16] = "Att_Typ7";
                    columns[17] = "Att7";
                    columns[18] = "Att_Typ8";
                    columns[19] = "Att8";
                    columns[20] = "Att_Typ9";
                    columns[21] = "Att9";
                    columns[22] = "Att_Typ10";
                    columns[23] = "Att10";
                    for (int i = 0; i < columns.Length; i++)
                    {
                        //Add the Header row for Text file.
                        txt += columns[i] + "\t\t";
                    }

                    //Add new line.
                    txt += "\r\n";
                    
                    //}
                    sw.Write(txt);
                    txt = "";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //as_bodenbelag
                        query = "select * from as_bodenbelag where gebaeude_id=" + dt.Rows[i][0].ToString() + " and geschoss_id='" + dt.Rows[i][1].ToString() + "'";
                        DataTable dtBodenbelag = DataConnection.GetData(query);
                        if(dtBodenbelag!=null && dtBodenbelag.Rows.Count > 0)
                        {
                            for (int j = 0; j < dtBodenbelag.Rows.Count; j++)
                            {
                                txt += fileRowId + "\t\t" + dt.Rows[i][0].ToString() + "\t\t" + dt.Rows[i][1].ToString() + "\t\t" + dt.Rows[i][2].ToString() + "\t\t" + "bodenbelag"+ "\t\t" + dtBodenbelag.Rows[j][5].ToString() + "\t\t" + "flaeche" + "\t\t" + dtBodenbelag.Rows[j][5].ToString();
                                txt += "\r\n";
                                sw.Write(txt);
                                txt = "";
                            }
                            
                        }
                        //as_fenster
                        query = "select * from as_fenster where gebaeude_id=" + dt.Rows[i][0].ToString() + " and geschoss_id='" + dt.Rows[i][1].ToString() + "'";
                        DataTable dtfenster = DataConnection.GetData(query);
                        if (dtfenster != null && dtfenster.Rows.Count > 0)
                        {
                            for (int j = 0; j < dtfenster.Rows.Count; j++)
                            {
                                txt += fileRowId + "\t\t" + dt.Rows[i][0].ToString() + "\t\t" + dt.Rows[i][1].ToString() + "\t\t" + dt.Rows[i][2].ToString() + "\t\t" + "fenster"+ "\t\t"  + dtfenster.Rows[j][5].ToString() + "\t\t" + "fensterrahmenmaterial" + "\t\t" + dtfenster.Rows[j][6].ToString() + "verglasung" + "\t\t" + dtfenster.Rows[j][7].ToString() + "breite" + "\t\t" + dtfenster.Rows[j][8].ToString() + "hoehe" + "\t\t" + dtfenster.Rows[j][9].ToString() + "feststellanlage" + "\t\t" + dtfenster.Rows[j][10].ToString() + "tuernummer" + "\t\t" + dtfenster.Rows[j][11].ToString();
                                txt += "\r\n";
                                sw.Write(txt);
                                txt = "";
                            }

                        }
                        //as_feuerloescher
                        query = "select * from as_feuerloescher where gebaeude_id=" + dt.Rows[i][0].ToString() + " and geschoss_id='" + dt.Rows[i][1].ToString() + "'";
                        DataTable as_feuerloescher = DataConnection.GetData(query);
                        if (as_feuerloescher != null && as_feuerloescher.Rows.Count > 0)
                        {
                            for (int j = 0; j < as_feuerloescher.Rows.Count; j++)
                            {
                                txt += fileRowId + "\t\t" + dt.Rows[i][0].ToString() + "\t\t" + dt.Rows[i][1].ToString() + "\t\t" + dt.Rows[i][2].ToString() + "\t\t" + "feuerloescher" +"\t\t"+ as_feuerloescher.Rows[j][5].ToString();
                                txt += "\r\n";
                                sw.Write(txt);
                                txt = "";
                            }

                        }
                        //as_glasbau-element
                        query = "select * from [as_glasbau-element] where gebaeude_id=" + dt.Rows[i][0].ToString() + " and geschoss_id='" + dt.Rows[i][1].ToString() + "'";
                        DataTable dtglasbauelement = DataConnection.GetData(query);
                        if (dtglasbauelement != null && dtglasbauelement.Rows.Count > 0)
                        {
                            for (int j = 0; j < dtglasbauelement.Rows.Count; j++)
                            {
                                txt += fileRowId + "\t\t" + dt.Rows[i][0].ToString() + "\t\t" + dt.Rows[i][1].ToString() + "\t\t" + dt.Rows[i][2].ToString() + "\t\t" + "glasbau-element"+ "\t\t" + dtglasbauelement.Rows[j][5].ToString() + "\t\t" + "glasflaeche einseitig" + "\t\t" + dtglasbauelement.Rows[j][6].ToString() + "\t\t" + "reinigungsflaechen" + "\t\t" + dtglasbauelement.Rows[j][7].ToString();
                                txt += "\r\n";
                                sw.Write(txt);
                                txt = "";
                            }

                        }
                        //as_leuchte
                        query = "select * from as_leuchte where gebaeude_id=" + dt.Rows[i][0].ToString() + " and geschoss_id='" + dt.Rows[i][1].ToString() + "'";
                        DataTable dtleuchte = DataConnection.GetData(query);
                        if (dtleuchte != null && dtleuchte.Rows.Count > 0)
                        {
                            for (int j = 0; j < dtleuchte.Rows.Count; j++)
                            {
                                txt += fileRowId + "\t\t" + dt.Rows[i][0].ToString() + "\t\t" + dt.Rows[i][1].ToString() + "\t\t" + dt.Rows[i][2].ToString() + "\t\t" + "leuchte" + dtleuchte.Rows[j][5].ToString() + "\t\t" + "leuchtmitteltyp" + "\t\t" + dtleuchte.Rows[j][6].ToString() + "\t\t" + "anzahl leuchtmittel" + "\t\t" + dtleuchte.Rows[j][7].ToString() + "montage" + "\t\t" + dtleuchte.Rows[j][8].ToString();
                                txt += "\r\n";
                                sw.Write(txt);
                                txt = "";
                            }

                        }
                        //as_oberlicht
                        query = "select * from as_oberlicht where gebaeude_id=" + dt.Rows[i][0].ToString() + " and geschoss_id='" + dt.Rows[i][1].ToString() + "'";
                        DataTable dtoberlicht = DataConnection.GetData(query);
                        if (dtoberlicht != null && dtoberlicht.Rows.Count > 0)
                        {
                            for (int j = 0; j < dtoberlicht.Rows.Count; j++)
                            {
                                txt += fileRowId + "\t\t" + dt.Rows[i][0].ToString() + "\t\t" + dt.Rows[i][1].ToString() + "\t\t" + dt.Rows[i][2].ToString() + "\t\t" + "breite" + dtoberlicht.Rows[j][5].ToString() + "\t\t" + "hoehe" + "\t\t" + dtoberlicht.Rows[j][6].ToString();
                                txt += "\r\n";
                                sw.Write(txt);
                                txt = "";
                            }

                        }
                        //as_sonnenschutz
                        query = "select * from as_sonnenschutz where gebaeude_id=" + dt.Rows[i][0].ToString() + " and geschoss_id='" + dt.Rows[i][1].ToString() + "'";
                        DataTable dtsonnenschutz = DataConnection.GetData(query);
                        if (dtsonnenschutz != null && dtsonnenschutz.Rows.Count > 0)
                        {
                            for (int j = 0; j < dtsonnenschutz.Rows.Count; j++)
                            {
                                txt += fileRowId + "\t\t" + dt.Rows[i][0].ToString() + "\t\t" + dt.Rows[i][1].ToString() + "\t\t" + dt.Rows[i][2].ToString() + "\t\t" + "sonnenschutz" + dtsonnenschutz.Rows[j][5].ToString() + "\t\t" + "lage" + "\t\t" + dtsonnenschutz.Rows[j][6].ToString();
                                txt += "\r\n";
                                sw.Write(txt);
                                txt = "";
                            }

                        }
                        //as_tor
                        query = "select * from as_tor where gebaeude_id=" + dt.Rows[i][0].ToString() + " and geschoss_id='" + dt.Rows[i][1].ToString() + "'";
                        DataTable dttor = DataConnection.GetData(query);
                        if (dttor != null && dttor.Rows.Count > 0)
                        {
                            for (int j = 0; j < dttor.Rows.Count; j++)
                            {
                                txt += fileRowId + "\t\t" + dt.Rows[i][0].ToString() + "\t\t" + dt.Rows[i][1].ToString() + "\t\t" + dt.Rows[i][2].ToString() + "\t\t" + "tor" + dttor.Rows[j][5].ToString() + "\t\t" + "breite" + "\t\t" + dttor.Rows[j][6].ToString() + "\t\t" + "hoehe" + "\t\t" + dttor.Rows[j][7].ToString() + "\t\t" + "antrieb" + "\t\t" + dttor.Rows[j][8].ToString();
                                txt += "\r\n";
                                sw.Write(txt);
                                txt = "";
                            }

                        }
                        //as_bodenbelag
                        query = "select * from as_tuer where gebaeude_id=" + dt.Rows[i][0].ToString() + " and geschoss_id='" + dt.Rows[i][1].ToString() + "'";
                        DataTable dttuer = DataConnection.GetData(query);
                        if (dttuer != null && dttuer.Rows.Count > 0)
                        {
                            for (int j = 0; j < dttuer.Rows.Count; j++)
                            {
                                txt += fileRowId + "\t\t" + dt.Rows[i][0].ToString() + "\t\t" + dt.Rows[i][1].ToString() + "\t\t" + dt.Rows[i][2].ToString() + "\t\t" + "tuer" + "\t\t" + dttuer.Rows[j][5].ToString() + "\t\t" + "türblattmaterial" + "\t\t" + dttuer.Rows[j][6].ToString() + "hoehe" + "\t\t" + dttuer.Rows[j][7].ToString() + "breite" + "\t\t" + dttuer.Rows[j][8].ToString() + "glasflaeche einseitig" + "\t\t" + dttuer.Rows[j][9].ToString() + "antrieb" + "\t\t" + dttuer.Rows[j][10].ToString();
                                txt += "\r\n";
                                sw.Write(txt);
                                txt = "";
                            }

                        }
                    }
                    sw.Close();
                    fileStream.Close();
                    Process.Start(folderPath + "\\file2.txt", "notepad.exe");
                }
                //File.Copy(AppDomain.CurrentDomain.BaseDirectory + "\\file2.txt", "" + folderPath + "\\file2.txt");
                //Process.Start(folderPath + "\\file1.txt", "notepad.exe");
                //Process.Start(folderPath + "\\file2.txt", "notepad.exe");

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
