using System;
using System.Collections.Generic;
using System.Data;
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
            MessageBox.Show(msg);
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
                string type = lbEquipments.SelectedItem.ToString().Split(' ')[0];
                Equipment equipment = new Equipment(type);
                equipment.ShowDialog();
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
