using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        public static string Raum_Id { get; set; }
        public string Bezeichnug { get; set; }
        public string Max { get; set; }
        public string IST { get; set; }
        private static string RoomAddOrEdit { get; set; }
        private static string saveStatus { get; set; }
        private static int EditRoomId { get; set; }

        public static string Info { get; set; }
        public AddNewRoom()
        {
            InitializeComponent();
            Application.Current.Properties["CopyRowStatus"] = "No";
            cbzustand.Items.Clear();
            cbzustand.Items.Add("neuwertig");
            cbzustand.Items.Add("in Ordnung");
            cbzustand.Items.Add("renovierungsbedürftig");
        }
        public AddNewRoom(string cadId,DataTable dt, string roomAddOrEdit)
        {
            InitializeComponent();
            Application.Current.Properties["CopyRowStatus"] = "No";
            RoomAddOrEdit = roomAddOrEdit;
            cbzustand.Items.Clear();
            cbzustand.Items.Add("neuwertig");
            cbzustand.Items.Add("in Ordnung");
            cbzustand.Items.Add("renovierungsbedürftig");
            this.Title = "IMMO BIM Raum CAD-ID: " + cadId;

            if(dt!=null && dt.Rows.Count > 0)
            {
                EditRoomId =Convert.ToInt32(dt.Rows[0][0].ToString());
                txtraum.Text = dt.Rows[0][4].ToString();
                Application.Current.Properties["RaumId"] = txtraum.Text;
                lblnutzungResult.Content = "IST: " + dt.Rows[0][7].ToString() + "\n" + "MAX: " + dt.Rows[0][6].ToString() + "\n" + "Beziechnung: " + dt.Rows[0][5].ToString();
                Bezeichnug = dt.Rows[0][5].ToString();
                Max = dt.Rows[0][6].ToString();
                IST = dt.Rows[0][7].ToString();
                txtflaeche.Text = dt.Rows[0][8].ToString();
                txtLichte.Text = dt.Rows[0][9].ToString();
                txtAnzap.Text = dt.Rows[0][10].ToString();
                txtAnzma.Text = dt.Rows[0][11].ToString();
                cbzustand.SelectedItem = dt.Rows[0][12].ToString();
                txtbemerku.Text = dt.Rows[0][13].ToString();
                txtumfang.Text = dt.Rows[0][14].ToString();
                chkschukostdose.IsChecked = Convert.ToBoolean(dt.Rows[0][15].ToString());
                txtanzheizho.Text= dt.Rows[0][16].ToString();
                chktelnetzdose.IsChecked = Convert.ToBoolean(dt.Rows[0][17].ToString());
                txtanzpatchsc.Text = dt.Rows[0][18].ToString();
                chksprinkler.IsChecked = Convert.ToBoolean(dt.Rows[0][19].ToString());
                chkgasabsperrsch.IsChecked = Convert.ToBoolean(dt.Rows[0][20].ToString());
                chkwasserabsperr.IsChecked = Convert.ToBoolean(dt.Rows[0][21].ToString());
                chkheizungabsperr.IsChecked = Convert.ToBoolean(dt.Rows[0][22].ToString());
                chkHydrant.IsChecked = Convert.ToBoolean(dt.Rows[0][23].ToString());
                lbEquipments.Items.Clear();
                FillEquipments();
            }
        }
        private void FillEquipments()
        {
            lbEquipments.Items.Clear();
            string query = "select * from as_bodenbelag where gebaeude_id=" + Application.Current.Properties["BuildingId"] + " and geschoss_id='" + Application.Current.Properties["LevelId"] + "' and raum_id='" + Application.Current.Properties["CadId"] + "'";
          DataTable  dt = DataConnection.GetData(query);
            if (dt != null && dt.Rows.Count > 0)
                lbEquipments.Items.Add("EquipId " + dt.Rows[0][3].ToString() + ": " + "bodenbelag " + dt.Rows[0][5].ToString() + "," + dt.Rows[0][6].ToString());
            query = "select * from as_fenster where gebaeude_id=" + Application.Current.Properties["BuildingId"] + " and geschoss_id='" + Application.Current.Properties["LevelId"] + "' and raum_id='" + Application.Current.Properties["CadId"] + "'";
            dt = DataConnection.GetData(query);
            if (dt != null && dt.Rows.Count > 0)
                lbEquipments.Items.Add("EquipId " + dt.Rows[0][3].ToString() + ": " + "fenster" + " (" + dt.Rows[0][8].ToString() + "x" + dt.Rows[0][9].ToString() + ")," + dt.Rows[0][5].ToString() + "," + dt.Rows[0][6].ToString() + "," + dt.Rows[0][7].ToString() + "," + dt.Rows[0][10].ToString() + "," + dt.Rows[0][11].ToString());
            query = "select * from as_feuerloescher where gebaeude_id=" + Application.Current.Properties["BuildingId"] + " and geschoss_id='" + Application.Current.Properties["LevelId"] + "' and raum_id='" + Application.Current.Properties["CadId"] + "'";
            dt = DataConnection.GetData(query);
            if (dt != null && dt.Rows.Count > 0)
                lbEquipments.Items.Add("EquipId " + dt.Rows[0][3].ToString() + ": " + "feuerloescher " + dt.Rows[0][5].ToString());
            query = "select * from [as_glasbau-element] where gebaeude_id=" + Application.Current.Properties["BuildingId"] + " and geschoss_id='" + Application.Current.Properties["LevelId"] + "' and raum_id='" + Application.Current.Properties["CadId"] + "'";
            dt = DataConnection.GetData(query);
            if (dt != null && dt.Rows.Count > 0)
                lbEquipments.Items.Add("EquipId " + dt.Rows[0][3].ToString() + ": " + "glasbau-element " + dt.Rows[0][5].ToString() + "," + dt.Rows[0][6].ToString() + "," + dt.Rows[0][7].ToString());
            query = "select * from as_leuchte where gebaeude_id=" + Application.Current.Properties["BuildingId"] + " and geschoss_id='" + Application.Current.Properties["LevelId"] + "' and raum_id='" + Application.Current.Properties["CadId"] + "'";
            dt = DataConnection.GetData(query);
            if (dt != null && dt.Rows.Count > 0)
                lbEquipments.Items.Add("EquipId " + dt.Rows[0][3].ToString() + ": " + "leuchte " + dt.Rows[0][5].ToString() + "," + dt.Rows[0][6].ToString() + "," + dt.Rows[0][7].ToString() + "," + dt.Rows[0][8].ToString());
            query = "select * from as_oberlicht where gebaeude_id=" + Application.Current.Properties["BuildingId"] + " and geschoss_id='" + Application.Current.Properties["LevelId"] + "' and raum_id='" + Application.Current.Properties["CadId"] + "'";
            dt = DataConnection.GetData(query);
            if (dt != null && dt.Rows.Count > 0)
                lbEquipments.Items.Add("EquipId " + dt.Rows[0][3].ToString() + ": " + "oberlicht " + "(" + dt.Rows[0][5].ToString() + "x" + dt.Rows[0][6].ToString() + ")");
            query = "select * from as_sonnenschutz where gebaeude_id=" + Application.Current.Properties["BuildingId"] + " and geschoss_id='" + Application.Current.Properties["LevelId"] + "' and raum_id='" + Application.Current.Properties["CadId"] + "'";
            dt = DataConnection.GetData(query);
            if (dt != null && dt.Rows.Count > 0)
                lbEquipments.Items.Add("EquipId " + dt.Rows[0][3].ToString() + ": " + "sonnenschutz " + dt.Rows[0][5].ToString() + "," + dt.Rows[0][6].ToString());
            query = "select * from as_tor where gebaeude_id=" + Application.Current.Properties["BuildingId"] + " and geschoss_id='" + Application.Current.Properties["LevelId"] + "' and raum_id='" + Application.Current.Properties["CadId"] + "'";
            dt = DataConnection.GetData(query);
            if (dt != null && dt.Rows.Count > 0)
                lbEquipments.Items.Add("EquipId " + dt.Rows[0][3].ToString() + ": " + "tor" + " (" + dt.Rows[0][7].ToString() + "x" + dt.Rows[0][6].ToString() + ")," + dt.Rows[0][5].ToString() + "," + dt.Rows[0][8].ToString());
            query = "select * from as_tuer where gebaeude_id=" + Application.Current.Properties["BuildingId"] + " and geschoss_id='" + Application.Current.Properties["LevelId"] + "' and raum_id='" + Application.Current.Properties["CadId"] + "'";
            dt = DataConnection.GetData(query);
            if (dt != null && dt.Rows.Count > 0)
                lbEquipments.Items.Add("EquipId " + dt.Rows[0][3].ToString() + ": " + "tuer" + " (" + dt.Rows[0][7].ToString() + "x" + dt.Rows[0][8].ToString() + ")," + dt.Rows[0][5].ToString() + "," + dt.Rows[0][6].ToString() + "," + dt.Rows[0][9].ToString() + "," + dt.Rows[0][10].ToString() + "," + dt.Rows[0][11].ToString());

        }
        public AddNewRoom(string buildng, string level, string cadId, string roomAddOrEdit)
        {
            InitializeComponent();
            Application.Current.Properties["CopyRowStatus"] = "No";
            RoomAddOrEdit = roomAddOrEdit;
            cbzustand.Items.Clear();
            cbzustand.Items.Add("neuwertig");
            cbzustand.Items.Add("in Ordnung");
            cbzustand.Items.Add("renovierungsbedürftig");
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
            if (EditRoomId == 0)
            {
                int id = 1;
                string query = "select top 1 id from kl_raum order by id desc";
                DataTable dt = DataConnection.GetData(query);
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][0].ToString() != "")
                        id = Int32.Parse(dt.Rows[0][0].ToString()) + 1;
                }
                query = "insert into kl_raum values(" + id + "," + Building + ",'" + Level + "','" + CadId + "','" + txtraum.Text + "','" + Bezeichnug + "','" + Max + "','" + IST + "'," + txtflaeche.Text + "," + txtLichte.Text + "," + txtAnzap.Text + "," + txtAnzma.Text + ",'" + cbzustand.SelectionBoxItem + "','" + txtbemerku.Text + "'," + txtumfang.Text + "," + chkschukostdose.IsChecked + "," + txtanzheizho.Text + "," + chktelnetzdose.IsChecked + "," + txtanzpatchsc.Text + "," + chksprinkler.IsChecked + "," + chkgasabsperrsch.IsChecked + "," + chkwasserabsperr.IsChecked + "," + chkheizungabsperr.IsChecked + "," + chkHydrant.IsChecked + ")";
                string msg = DataConnection.ExecuteQuery(query);
                if (msg == "Executed")
                {
                    RoomAddOrEdit = "";
                       saveStatus = "yes";
                    MessageBox.Show("Saved Successfully");
                    
                    Info = "Raum-Nr:" + txtraum.Text + "\n" + "Flaeche:" + txtflaeche.Text+ " m\u00b2" + "\n" + "Lichte Hohe:" + txtLichte.Text + " m"+"\n" + "Anz. A.P:" + txtAnzap.Text + "\n" + "Anz. M.A:" + txtAnzma.Text + "\n" + "zustand sch:" + cbzustand.SelectedValue + "\n" + "Bemerkung:" + txtbemerku.Text + "\n" + "Umfang [m]:" + txtumfang.Text + " m" + "\n" + "Anz. Heizho:" + txtanzheizho.Text + "\n" + "Anz. Patchsc:" + txtanzpatchsc.Text + "\n" + "Bezeichnug:" + Bezeichnug+"\n"+"MAX:"+Max +"\n"+"IST:"+IST;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Please enter correct input fields");
                }
            }
            else
            {
                string query = "update kl_raum set raumnummer='" + txtraum.Text + "' , raumbezeichnung='" + Bezeichnug + "' , DIN277_max='" + Max + "' , DIN277_ist='" + IST + "' , raumflaeche=" + txtflaeche.Text + " , lichte_raumhoehe=" + txtLichte.Text + " , anzahl_arbeitsplaetze=" + txtAnzap.Text + " , anzahl_mitarbeiter=" + txtAnzma.Text + " ,  bemerkung='" + txtbemerku.Text + "' , umfang=" + txtumfang.Text + " , schukosteckdose=" + chkschukostdose.IsChecked + " , anzahl_heizkoerper=" + txtanzheizho.Text + " , telefon_netzwerkdose=" + chktelnetzdose.IsChecked + " , anzahl_patschraenke=" + txtanzpatchsc.Text + " , sprinkler=" + chksprinkler.IsChecked + " , gasabsperrschieber=" + chkgasabsperrsch.IsChecked + " , wasserabsperrschieber=" + chkwasserabsperr.IsChecked + " , heizungsabsperrschieber=" + chkheizungabsperr.IsChecked + " , hydrant=" + chkHydrant.IsChecked + " where id=" + EditRoomId + "";
                string msg = DataConnection.ExecuteQuery(query);
                if (msg == "Executed")
                {
                    RoomAddOrEdit = "";
                       saveStatus = "yes";
                    MessageBox.Show("Saved Successfully");
                    Info = "Raum-Nr:" + txtraum.Text + "\n" + "Flaeche:" + txtflaeche.Text + " m\u00b2"+"\n" + "Lichte Hohe:" + txtLichte.Text + " m" + "\n" + "Anz. A.P:" + txtAnzap.Text + "\n" + "Anz. M.A:" + txtAnzma.Text + "\n" + "zustand sch:" + cbzustand.SelectedValue + "\n" + "Bemerkung:" + txtbemerku.Text + "\n" + "Umfang [m]:" + txtumfang.Text + " m" + "\n" + "Anz. Heizho:" + txtanzheizho.Text + "\n" + "Anz. Patchsc:" + txtanzpatchsc.Text + "\n" + "Bezeichnug:" + Bezeichnug + "\n" + "MAX:" + Max + "\n" + "IST:" + IST;
                    this.Close();
                    
                }
                else
                {
                    MessageBox.Show("Please enter correct input fields");
                }
            }
        }

        private void Btnaddequipment_Click(object sender, RoutedEventArgs e)
        {
            Equipment equipment = new Equipment();
            equipment.ShowDialog();
            //UserControlFensterType u = new UserControlFensterType();

            if (UserControlFensterType.UpdateChildGetSet)
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
                lbEquipments.Items.Add(UserControlGlasbauType.SelectChildTypeValues);
                UserControlGlasbauType.UpdateChildGetSet = false;
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
            else
            {
                FillEquipments();

            }
            Application.Current.Properties["RaumId"] = txtraum.Text;
        }

        private void Btneditequipment_Click(object sender, RoutedEventArgs e)
        {
            GetRow();
        }

        private void Btnremoveequipment_Click(object sender, RoutedEventArgs e)
        {
            if (lbEquipments.Items.Count > 0)
                if (lbEquipments.SelectedValue != null || lbEquipments.SelectedValue.ToString() != "")
                {
                    string[] type = Regex.Split(lbEquipments.SelectedItem.ToString(), ": ");
                    if (type.Length > 1)
                    {
                        string table = "as_" + type[1].ToString().Split(' ')[0];
                        MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure you want to delete?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                        if (messageBoxResult == MessageBoxResult.Yes)
                        {
                            string msg = EquipmentData.DeleteEquipment(table, type[0].ToString().Split(' ')[1]);
                            if (msg == "Executed")
                                lbEquipments.Items.RemoveAt(lbEquipments.SelectedIndex);
                        }
                    }
                }
        }

        private void Btncopyequipment_Click(object sender, RoutedEventArgs e)
        {
            if (lbEquipments.Items.Count > 0)
            {
                Application.Current.Properties["CopyRowStatus"] = "Yes";
                GetRow();
                //lbEquipments.Items.Add(lbEquipments.SelectedItem);
            }
        }

        private void GetRow()
        {
            if (lbEquipments.Items.Count > 0)
            {
                int index = lbEquipments.SelectedIndex;
                string[] controlValues = null;
                if (index != -1)
                {
                    string[] type = Regex.Split(lbEquipments.SelectedItem.ToString(), ": ");// lbEquipments.SelectedItem.ToString().Split(' ');
                    if (type.Length >= 2)
                    {
                        Application.Current.Properties["EquipId"] = type[0].ToString().Split(' ')[1];
                        controlValues = type[1].ToString().Split(',');
                        Equipment equipment = new Equipment(type[0].ToString().Split(' ')[1], type[1].ToString().Split(' ')[0]);
                        equipment.ShowDialog();
                        if (Application.Current.Properties["CopyRowStatus"].ToString() == "No")
                            lbEquipments.Items.RemoveAt(index);
                        if (UserControlFensterType.UpdateChildGetSet)
                        {
                            //lbEquipments.Items.RemoveAt(index);
                            lbEquipments.Items.Add(UserControlFensterType.SelectChildTypeValues);
                            UserControlFensterType.UpdateChildGetSet = false;
                        }
                        else if (UserControlBodenblagType.UpdateChildGetSet)
                        {
                            //lbEquipments.Items.RemoveAt(index);
                            lbEquipments.Items.Add(UserControlBodenblagType.SelectChildTypeValues);
                            UserControlBodenblagType.UpdateChildGetSet = false;
                        }
                        else if (UserControlFeuerloeschertyp.UpdateChildGetSet)
                        {
                            //lbEquipments.Items.RemoveAt(index);
                            lbEquipments.Items.Add(UserControlFeuerloeschertyp.SelectChildTypeValues);
                            UserControlFeuerloeschertyp.UpdateChildGetSet = false;
                        }
                        else if (UserControlGlasbauType.UpdateChildGetSet)
                        {
                            //lbEquipments.Items.RemoveAt(index);
                            lbEquipments.Items.Add(UserControlGlasbauType.SelectChildTypeValues);
                            UserControlFeuerloeschertyp.UpdateChildGetSet = false;
                        }
                        else if (UserControlLeuchtentyp.UpdateChildGetSet)
                        {
                            //lbEquipments.Items.RemoveAt(index);
                            lbEquipments.Items.Add(UserControlLeuchtentyp.SelectChildTypeValues);
                            UserControlLeuchtentyp.UpdateChildGetSet = false;
                        }
                        else if (UserControlOberlicut.UpdateChildGetSet)
                        {
                            //lbEquipments.Items.RemoveAt(index);
                            lbEquipments.Items.Add(UserControlOberlicut.SelectChildTypeValues);
                            UserControlOberlicut.UpdateChildGetSet = false;
                        }
                        else if (UserControlSonnenschutz.UpdateChildGetSet)
                        {
                            //lbEquipments.Items.RemoveAt(index);
                            lbEquipments.Items.Add(UserControlSonnenschutz.SelectChildTypeValues);
                            UserControlSonnenschutz.UpdateChildGetSet = false;
                        }
                        else if (UserControlTor.UpdateChildGetSet)
                        {
                            //lbEquipments.Items.RemoveAt(index);
                            lbEquipments.Items.Add(UserControlTor.SelectChildTypeValues);
                            UserControlTor.UpdateChildGetSet = false;
                        }
                        else if (UserControlTuer.UpdateChildGetSet)
                        {
                            //lbEquipments.Items.RemoveAt(index);
                            lbEquipments.Items.Add(UserControlTuer.SelectChildTypeValues);
                            UserControlTuer.UpdateChildGetSet = false;
                        }
                    }
                }

            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (RoomAddOrEdit == "NewRoom")
            {
                string msg = " Are you sure want to close without saving?";
                MessageBoxResult result =
                  MessageBox.Show(
                    msg,
                    "",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);
                if (result == MessageBoxResult.No)
                {
                    // If user doesn't want to close, cancel closure
                    e.Cancel = true;
                }
                else
                {
                    string query = "delete from as_bodenbelag where gebaeude_id=" + Application.Current.Properties["BuildingId"] + " and geschoss_id='" + Application.Current.Properties["LevelId"] + "' and raum_id='" + Application.Current.Properties["RaumId"] + "'";
                    DataConnection.ExecuteQuery(query);
                    query = "delete from as_fenster where gebaeude_id=" + Application.Current.Properties["BuildingId"] + " and geschoss_id='" + Application.Current.Properties["LevelId"] + "' and raum_id='" + Application.Current.Properties["RaumId"] + "'";
                    DataConnection.ExecuteQuery(query);
                    query = "delete from as_feuerloescher where gebaeude_id=" + Application.Current.Properties["BuildingId"] + " and geschoss_id='" + Application.Current.Properties["LevelId"] + "' and raum_id='" + Application.Current.Properties["RaumId"] + "'";
                    DataConnection.ExecuteQuery(query);
                    query = "delete from [as_glasbau-element] where gebaeude_id=" + Application.Current.Properties["BuildingId"] + " and geschoss_id='" + Application.Current.Properties["LevelId"] + "' and raum_id='" + Application.Current.Properties["RaumId"] + "'";
                    DataConnection.ExecuteQuery(query);
                    query = "delete from as_leuchte where gebaeude_id=" + Application.Current.Properties["BuildingId"] + " and geschoss_id='" + Application.Current.Properties["LevelId"] + "' and raum_id='" + Application.Current.Properties["RaumId"] + "'";
                    DataConnection.ExecuteQuery(query);
                    query = "delete from as_oberlicht where gebaeude_id=" + Application.Current.Properties["BuildingId"] + " and geschoss_id='" + Application.Current.Properties["LevelId"] + "' and raum_id='" + Application.Current.Properties["RaumId"] + "'";
                    DataConnection.ExecuteQuery(query);
                    query = "delete from as_sonnenschutz where gebaeude_id=" + Application.Current.Properties["BuildingId"] + " and geschoss_id='" + Application.Current.Properties["LevelId"] + "' and raum_id='" + Application.Current.Properties["RaumId"] + "'";
                    DataConnection.ExecuteQuery(query);
                    query = "delete from as_tor where gebaeude_id=" + Application.Current.Properties["BuildingId"] + " and geschoss_id='" + Application.Current.Properties["LevelId"] + "' and raum_id='" + Application.Current.Properties["RaumId"] + "'";
                    DataConnection.ExecuteQuery(query);
                    query = "delete from as_tuer where gebaeude_id=" + Application.Current.Properties["BuildingId"] + " and geschoss_id='" + Application.Current.Properties["LevelId"] + "' and raum_id='" + Application.Current.Properties["RaumId"] + "'";
                    DataConnection.ExecuteQuery(query);
                }
            }
        }
        }
    }
