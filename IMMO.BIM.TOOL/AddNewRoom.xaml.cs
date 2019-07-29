using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for AddNewRoom.xaml
    /// </summary>
    public partial class AddNewRoom : Window
    {
        public string Building { get; set; }
        public string CadId { get; set; }
        public string Level { get; set; }
        public string Bezeichnug { get; set; }
        public string Max { get; set; }
        public string IST { get; set; }
        public AddNewRoom()
        {
            InitializeComponent(); cbzustand.Items.Clear();
            cbzustand.Items.Add("neuwertig");
            cbzustand.Items.Add("in Ordnung");
            cbzustand.Items.Add("renovierungsbedürftig");
        }
        public AddNewRoom(string cadId)
        {
            InitializeComponent();

            this.Title = "IMMO BIM Raum CAD-ID: " + cadId;
        }
        public AddNewRoom(string buildng,string level, string cadId)
        {
            InitializeComponent();

            this.Title = "IMMO BIM Raum CAD-ID: " + cadId;
            Level = level;
            Building = buildng;
            CadId = cadId;
        }
        private void BtnNutzung_Click(object sender, RoutedEventArgs e)
        {
            Nutzung nutzung = new Nutzung();
            nutzung.ShowDialog();
            if (nutzung.UpdateGetSet)
            {
                lblnutzungResult.Content = nutzung.NutzungResult;
                Bezeichnug = nutzung.Bezeichnug;
                Max = nutzung.MAX;
                IST = nutzung.IST;
            }
        }

        private void Btnsaveandclose_Click(object sender, RoutedEventArgs e)
        {
            string gasabsperrsch = "N";
            string heizungabsperr = "N";
            string Hydrant = "N";
            string schukostdose = "N";
            string sprinkler = "N";
            string telnetzdose = "N";
            string wasserabsperr = "N";
            if (chkgasabsperrsch.IsChecked == true)
                gasabsperrsch = "Y";
            if (chkheizungabsperr.IsChecked == true)
                heizungabsperr = "Y";
            if (chkHydrant.IsChecked == true)
                Hydrant = "Y";
            if (chkschukostdose.IsChecked == true)
                schukostdose = "Y";
            if (chksprinkler.IsChecked == true)
                sprinkler = "Y";
            if (chktelnetzdose.IsChecked == true)
                telnetzdose = "Y";
            if (chkwasserabsperr.IsChecked == true)
                wasserabsperr = "Y";
            int id = 2;
            string query = "insert into kl_raum values("+id+",'"+Building+"','"+Level+"','"+CadId+"','"+txtraum.Text+"','"+Bezeichnug+"','"+Max+"','"+IST+"',"+txtflaeche.Text+","+txtLichte.Text+","+txtAnzap.Text+","+txtAnzma.Text+",'"+ cbzustand.SelectionBoxItem+ "','"+txtbemerku.Text+"',"+txtumfang.Text+","+chkschukostdose.IsChecked + ","+txtanzheizho.Text+","+chktelnetzdose.IsChecked +","+txtanzpatchsc.Text+","+chksprinkler.IsChecked +","+chkgasabsperrsch.IsChecked+","+chkwasserabsperr.IsChecked+","+chkheizungabsperr.IsChecked+","+chkHydrant.IsChecked+")";
            string msg = DataConnection.ExecuteQuery(query);
            MessageBox.Show("Saved Successfully");
            string txt = string.Empty;
            FileStream fileStream = new FileStream("" + AppDomain.CurrentDomain.BaseDirectory + "\\file2.txt", FileMode.Create);
            TextWriter sw = new StreamWriter(fileStream);
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
            //foreach (DataRow row in dt.Rows)
            //{
            //    foreach (DataColumn column in dt.Columns)
            //    {
            //        //Add the Data rows.
            //        txt += row[column.ColumnName].ToString() + "\t\t";
            //    }

            //    //Add new line.
            //    txt += "\r\n";
            //}
            sw.Write(txt);

            sw.Close();
            fileStream.Close();
        }

        private void Btnaddequipment_Click(object sender, RoutedEventArgs e)
        {
            Equipment equipment = new Equipment();
            equipment.ShowDialog();
            //UserControlFensterType u = new UserControlFensterType();
            
            if(UserControlFensterType.UpdateChildGetSet)
            {
                lbEquipments.Items.Add(UserControlFensterType.SelectChildTypeValues);
                UserControlFensterType.UpdateChildGetSet = false;
            }
            else if (UserControlBodenblagType.UpdateChildGetSet)
            {
                lbEquipments.Items.Add(UserControlBodenblagType.SelectChildTypeValues);
                UserControlBodenblagType.UpdateChildGetSet = false;
            }
            else if (UserControlFeuerloeschertyp.UpdateChildGetSet)
            {
                lbEquipments.Items.Add(UserControlFeuerloeschertyp.SelectChildTypeValues);
                UserControlFeuerloeschertyp.UpdateChildGetSet = false;
            }
            else if (UserControlGlasbauType.UpdateChildGetSet)
            {
                lbEquipments.Items.Add(UserControlFeuerloeschertyp.SelectChildTypeValues);
                UserControlFeuerloeschertyp.UpdateChildGetSet = false;
            }
            else if (UserControlLeuchtentyp.UpdateChildGetSet)
            {
                lbEquipments.Items.Add(UserControlLeuchtentyp.SelectChildTypeValues);
                UserControlLeuchtentyp.UpdateChildGetSet = false;
            }
            else if (UserControlOberlicut.UpdateChildGetSet)
            {
                lbEquipments.Items.Add(UserControlOberlicut.SelectChildTypeValues);
                UserControlOberlicut.UpdateChildGetSet = false;
            }
            else if (UserControlSonnenschutz.UpdateChildGetSet)
            {
                lbEquipments.Items.Add(UserControlSonnenschutz.SelectChildTypeValues);
                UserControlSonnenschutz.UpdateChildGetSet = false;
            }
            else if (UserControlTor.UpdateChildGetSet)
            {
                lbEquipments.Items.Add(UserControlTor.SelectChildTypeValues);
                UserControlTor.UpdateChildGetSet = false;
            }
            else if (UserControlTuer.UpdateChildGetSet)
            {
                lbEquipments.Items.Add(UserControlTuer.SelectChildTypeValues);
                UserControlTuer.UpdateChildGetSet = false;
            }
        }

        private void Btneditequipment_Click(object sender, RoutedEventArgs e)
        {
            if (lbEquipments.Items.Count > 0)
            {
                int index = lbEquipments.SelectedIndex;
                string[] controlValues = null;
                string[] type = lbEquipments.SelectedItem.ToString().Split(' ');
                if (type.Length >= 2)
                {
                    controlValues = type[1].ToString().Split(',');
                }
                Equipment equipment = new Equipment(type[0], lbEquipments.SelectedItem.ToString().Split(','));
                equipment.ShowDialog();
                if (UserControlFensterType.UpdateChildGetSet)
                {
                    lbEquipments.Items.RemoveAt(index);
                    lbEquipments.Items.Add(UserControlFensterType.SelectChildTypeValues);
                    UserControlFensterType.UpdateChildGetSet = false;
                }
                else if (UserControlBodenblagType.UpdateChildGetSet)
                {
                    lbEquipments.Items.RemoveAt(index);
                    lbEquipments.Items.Add(UserControlBodenblagType.SelectChildTypeValues);
                    UserControlBodenblagType.UpdateChildGetSet = false;
                }
                else if (UserControlFeuerloeschertyp.UpdateChildGetSet)
                {
                    lbEquipments.Items.RemoveAt(index);
                    lbEquipments.Items.Add(UserControlFeuerloeschertyp.SelectChildTypeValues);
                    UserControlFeuerloeschertyp.UpdateChildGetSet = false;
                }
                else if (UserControlGlasbauType.UpdateChildGetSet)
                {
                    lbEquipments.Items.RemoveAt(index);
                    lbEquipments.Items.Add(UserControlFeuerloeschertyp.SelectChildTypeValues);
                    UserControlFeuerloeschertyp.UpdateChildGetSet = false;
                }
                else if (UserControlLeuchtentyp.UpdateChildGetSet)
                {lbEquipments.Items.RemoveAt(index);
                    lbEquipments.Items.Add(UserControlLeuchtentyp.SelectChildTypeValues);
                    UserControlLeuchtentyp.UpdateChildGetSet = false;
                }
                else if (UserControlOberlicut.UpdateChildGetSet)
                {
                    lbEquipments.Items.RemoveAt(index);
                    lbEquipments.Items.Add(UserControlOberlicut.SelectChildTypeValues);
                    UserControlOberlicut.UpdateChildGetSet = false;
                }
                else if (UserControlSonnenschutz.UpdateChildGetSet)
                {
                    lbEquipments.Items.RemoveAt(index);
                    lbEquipments.Items.Add(UserControlSonnenschutz.SelectChildTypeValues);
                    UserControlSonnenschutz.UpdateChildGetSet = false;
                }
                else if (UserControlTor.UpdateChildGetSet)
                {
                    lbEquipments.Items.RemoveAt(index);
                    lbEquipments.Items.Add(UserControlTor.SelectChildTypeValues);
                    UserControlTor.UpdateChildGetSet = false;
                }
                else if (UserControlTuer.UpdateChildGetSet)
                {
                    lbEquipments.Items.RemoveAt(index);
                    lbEquipments.Items.Add(UserControlTuer.SelectChildTypeValues);
                    UserControlTuer.UpdateChildGetSet = false;
                }
            }
        }

        private void Btnremoveequipment_Click(object sender, RoutedEventArgs e)
        {
            if (lbEquipments.Items.Count > 0)
                lbEquipments.Items.RemoveAt(lbEquipments.SelectedIndex);
        }

        private void Btncopyequipment_Click(object sender, RoutedEventArgs e)
        {
            if (lbEquipments.Items.Count > 0)
                lbEquipments.Items.Add(lbEquipments.SelectedItem);
        }
    }
}
