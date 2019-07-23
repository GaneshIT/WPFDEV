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
            AddNewLevel newLevel = new AddNewLevel();
            newLevel.Show();
        }

        private void BtnNewRoom_Click(object sender, RoutedEventArgs e)
        {
            AddNewRoom newRoom = new AddNewRoom();
            newRoom.Show();
        }

        private void BtnEditRoom_Click(object sender, RoutedEventArgs e)
        {
            AddNewRoom newRoom = new AddNewRoom();
            newRoom.Show();
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
    }
}
