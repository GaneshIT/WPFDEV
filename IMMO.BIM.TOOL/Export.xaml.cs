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
                FileStream fileStream = new FileStream("" + folderPath + "\\raumbuch.txt", FileMode.Create);
                TextWriter sw = new StreamWriter(fileStream);


                foreach (DataColumn column in dt.Columns)
                {
                    //Add the Header row for Text file.
                    txt += column.ColumnName + "\t";
                }

                //Add new line.
                txt += "\r\n";
                foreach (DataRow row in dt.Rows)
                {
                    foreach (DataColumn column in dt.Columns)
                    {
                        //Add the Data rows.
                        txt += row[column.ColumnName].ToString() + "\t";
                    }

                    //Add new line.
                    txt += "\r\n";
                }
                sw.Write(txt);

                sw.Close();
                fileStream.Close();
                Process.Start(folderPath + "\\raumbuch.txt", "notepad.exe");

                query = "select geb_id,geschoss_id,cad_id from kl_raum";
                dt = DataConnection.GetData(query);
                int fileRowId = 1;
                if(dt!=null && dt.Rows.Count > 0)
                {
                    txt = string.Empty;
                    fileStream = new FileStream("" + folderPath + "\\ausstattungu.txt", FileMode.Create);
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
                        txt += columns[i] + "\t";
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
                                txt += fileRowId + "\t" + dt.Rows[i][0].ToString() + "\t" + dt.Rows[i][1].ToString() + "\t" + dt.Rows[i][2].ToString() + "\t" + "bodenbelag"+ "\t" + dtBodenbelag.Rows[j][5].ToString() + "\t" + "flaeche" + "\t" + dtBodenbelag.Rows[j][6].ToString();
                                txt += "\r\n";
                                sw.Write(txt);
                                fileRowId = fileRowId + 1;
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
                                txt += fileRowId + "\t" + dt.Rows[i][0].ToString() + "\t" + dt.Rows[i][1].ToString() + "\t" + dt.Rows[i][2].ToString() + "\t" + "fenster"+ "\t"  + dtfenster.Rows[j][5].ToString() + "\t" + "fensterrahmenmaterial" + "\t" + dtfenster.Rows[j][6].ToString() +"\t" + "verglasung" + "\t" + dtfenster.Rows[j][7].ToString() + "\t" + "breite" + "\t" + dtfenster.Rows[j][8].ToString() + "\t" + "hoehe" + "\t" + dtfenster.Rows[j][9].ToString() + "\t" + "feststellanlage" + "\t" + dtfenster.Rows[j][10].ToString() + "\t" + "tuernummer" + "\t" + dtfenster.Rows[j][11].ToString();
                                txt += "\r\n";
                                sw.Write(txt);
                                fileRowId = fileRowId + 1;
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
                                txt += fileRowId + "\t" + dt.Rows[i][0].ToString() + "\t" + dt.Rows[i][1].ToString() + "\t" + dt.Rows[i][2].ToString() + "\t" + "feuerloescher" +"\t"+ as_feuerloescher.Rows[j][5].ToString();
                                txt += "\r\n";
                                sw.Write(txt);
                                fileRowId = fileRowId + 1;
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
                                txt += fileRowId + "\t" + dt.Rows[i][0].ToString() + "\t" + dt.Rows[i][1].ToString() + "\t" + dt.Rows[i][2].ToString() + "\t" + "glasbau-element"+ "\t" + dtglasbauelement.Rows[j][5].ToString() + "\t" + "glasflaeche einseitig" + "\t" + dtglasbauelement.Rows[j][6].ToString() + "\t" + "reinigungsflaechen" + "\t" + dtglasbauelement.Rows[j][7].ToString();
                                txt += "\r\n";
                                sw.Write(txt);
                                fileRowId = fileRowId + 1;
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
                                txt += fileRowId + "\t" + dt.Rows[i][0].ToString() + "\t" + dt.Rows[i][1].ToString() + "\t" + dt.Rows[i][2].ToString() + "\t" + "leuchte" +"\t"+ dtleuchte.Rows[j][5].ToString() + "\t" + "leuchtmitteltyp" + "\t" + dtleuchte.Rows[j][6].ToString() + "\t" + "anzahl leuchtmittel" + "\t" + dtleuchte.Rows[j][7].ToString() +"\t"+ "montage" + "\t" + dtleuchte.Rows[j][8].ToString();
                                txt += "\r\n";
                                sw.Write(txt);
                                fileRowId = fileRowId + 1;
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
                                txt += fileRowId + "\t" + dt.Rows[i][0].ToString() + "\t" + dt.Rows[i][1].ToString() + "\t" + dt.Rows[i][2].ToString() + "\t" + "breite" + "\t" + dtoberlicht.Rows[j][5].ToString() + "\t" + "hoehe" + "\t" + dtoberlicht.Rows[j][6].ToString();
                                txt += "\r\n";
                                sw.Write(txt);
                                fileRowId = fileRowId + 1;
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
                                txt += fileRowId + "\t" + dt.Rows[i][0].ToString() + "\t" + dt.Rows[i][1].ToString() + "\t" + dt.Rows[i][2].ToString() + "\t" + "sonnenschutz" + "\t" + dtsonnenschutz.Rows[j][5].ToString() + "\t" + "lage" + "\t" + dtsonnenschutz.Rows[j][6].ToString();
                                txt += "\r\n";
                                sw.Write(txt);
                                fileRowId = fileRowId + 1;
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
                                txt += fileRowId + "\t" + dt.Rows[i][0].ToString() + "\t" + dt.Rows[i][1].ToString() + "\t" + dt.Rows[i][2].ToString() + "\t" + "tor" + "\t" + dttor.Rows[j][5].ToString() + "\t" + "breite" + "\t" + dttor.Rows[j][6].ToString() + "\t" + "hoehe" + "\t" + dttor.Rows[j][7].ToString() + "\t" + "antrieb" + "\t" + dttor.Rows[j][8].ToString();
                                txt += "\r\n";
                                sw.Write(txt);
                                fileRowId = fileRowId + 1;
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
                                txt += fileRowId + "\t" + dt.Rows[i][0].ToString() + "\t" + dt.Rows[i][1].ToString() + "\t" + dt.Rows[i][2].ToString() + "\t" + "tuer" + "\t" + dttuer.Rows[j][5].ToString() + "\t" + "türblattmaterial" + "\t" + dttuer.Rows[j][6].ToString() + "\t" + "hoehe" + "\t" + dttuer.Rows[j][7].ToString() + "\t" + "breite" + "\t" + dttuer.Rows[j][8].ToString() + "\t" + "glasflaeche einseitig" + "\t" + dttuer.Rows[j][9].ToString() + "\t" + "antrieb" + "\t" + dttuer.Rows[j][10].ToString();
                                txt += "\r\n";
                                sw.Write(txt);
                                fileRowId = fileRowId + 1;
                                txt = "";
                            }

                        }
                    }
                    sw.Close();
                    fileStream.Close();
                    Process.Start(folderPath + "\\ausstattungu.txt", "notepad.exe");
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
