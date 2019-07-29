using System;
using System.Collections.Generic;
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
using System.Data;
namespace IMMO.BIM.TOOL
{
    /// <summary>
    /// Interaction logic for IMMO_BIM.xaml
    /// </summary>
    public partial class IMMO_BIM : Window
    {
        public IMMO_BIM()
        {
            InitializeComponent();
            FillBuildingList();
        }

        private void BtnNewBuilding_Click(object sender, RoutedEventArgs e)
        {
            AddNewBuilding newBuilding = new AddNewBuilding();
            newBuilding.ShowDialog();
            if (newBuilding.UpdateGetSet)
            {
                FillBuildingList();
                lbDisplayBuilding.SelectedItem = lbDisplayBuilding.Items[lbDisplayBuilding.Items.Count - 1];
            }
        }

        private void BtnAddLevel_Click(object sender, RoutedEventArgs e)
        {
            if (lbDisplayBuilding.SelectedItem != null)
            {
                AddNewLevel newLevel = new AddNewLevel();
                newLevel.ShowDialog();
                if (newLevel.UpdateGetSet)
                    lbDisplayLevels.Items.Add(newLevel.SelectLevel);
            }
            else
            {
                MessageBox.Show("Please select building");
            }
        }

        private void BtnNewRoom_Click(object sender, RoutedEventArgs e)
        {
            if (lbDisplayLevels.SelectedItem!=null)
            {
                string level = lbDisplayLevels.SelectedItem.ToString();
                string room = level.Split(':')[1];
                AddNewRoom newRoom = new AddNewRoom(lbDisplayBuilding.SelectedItem.ToString(),lbDisplayLevels.SelectedItem.ToString(), room.ToString() + "001");
                newRoom.Show();
            }
            else
            {
                MessageBox.Show("Select levels");
            }
                
        }

        private void BtnEditRoom_Click(object sender, RoutedEventArgs e)
        {
            if (lbDisplayRooms.SelectedItem != null)
            {
                AddNewRoom newRoom = new AddNewRoom(lbDisplayRooms.SelectedItem.ToString());
                newRoom.Show();
            }
            else
                MessageBox.Show("Select room");
           
        }

        private void BtnDeleteRoom_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnExport_Click(object sender, RoutedEventArgs e)
        {
            Export export = new Export();
            export.Show();
        }

        private void FillBuildingList()
        {
            string query = "select geb_id from kl_gebaeude";
            DataTable dtResult = DataConnection.GetData(query);
            lbDisplayBuilding.Items.Clear();
            for (int i = 0; i < dtResult.Rows.Count; i++)
            {
                lbDisplayBuilding.Items.Add(dtResult.Rows[i][0].ToString());
            }
        }

        private void LbDisplayBuilding_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string query = "select geschoss_id,cad_id from kl_raum where geb_id=" + lbDisplayBuilding.SelectedItem + "";
            DataTable dtResult = DataConnection.GetData(query);
            lbDisplayLevels.Items.Clear();
            for (int i = 0; i < dtResult.Rows.Count; i++)
            {
                lbDisplayLevels.Items.Add(dtResult.Rows[i][0].ToString() + ":" + dtResult.Rows[i][1].ToString());
            }
        }

        private void LbDisplayLevels_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string query = "select cad_id from kl_raum";// where geb_id=" + lbDisplayBuilding.SelectedItem + "";
            DataTable dtResult = DataConnection.GetData(query);
            lbDisplayRooms.Items.Clear();
            for (int i = 0; i < dtResult.Rows.Count; i++)
            {
                lbDisplayRooms.Items.Add(dtResult.Rows[i][0].ToString());
            }
        }
    }
}
