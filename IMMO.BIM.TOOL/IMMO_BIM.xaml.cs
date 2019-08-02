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
                {
                    string status = string.Empty;
                    if (lbDisplayLevels.Items.Count > 0)
                    {

                        for (int i = 0; i < lbDisplayLevels.Items.Count; i++)
                        {
                            if (lbDisplayLevels.Items[i].ToString() == newLevel.SelectLevel)
                            {
                                status = "yes";
                                MessageBox.Show("Already this level activated");
                                break;
                            }
                        }

                    }
                    if (status != "yes")
                    {
                        lbDisplayRooms.Items.Clear();
                        lbDisplayLevels.Items.Add(newLevel.SelectLevel);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select building");
            }
        }

        private void BtnNewRoom_Click(object sender, RoutedEventArgs e)
        {
            if (lbDisplayLevels.SelectedItem != null)
            {
                string level = lbDisplayLevels.SelectedItem.ToString();
                string query = "select top 1 cad_id from kl_raum where geb_id=" + lbDisplayBuilding.SelectedItem + " and geschoss_id='" + lbDisplayLevels.SelectedValue + "' order by cad_id desc";
                DataTable dtResult = DataConnection.GetData(query);
                if (dtResult != null && dtResult.Rows.Count > 0)
                {

                    if (dtResult.Rows[0][0].ToString() != "" || dtResult.Rows[0][0] != null)
                    {
                        string getRoom = dtResult.Rows[0][0].ToString().Substring(2, dtResult.Rows[0][0].ToString().Length - 2).ToString();
                        if (getRoom.StartsWith("000"))
                        {
                            getRoom = "001";
                        }
                        else if (getRoom.StartsWith("00"))
                        {
                            getRoom = getRoom.Substring(2, 1).ToString();
                            int value = Int32.Parse(getRoom) + 1;
                            if (value == 10)
                                getRoom = "0" + value.ToString();
                            else
                                getRoom = "00" + value.ToString();
                        }
                        else if (getRoom.StartsWith("0"))
                        {
                            getRoom = getRoom.Substring(1, 2).ToString();
                            int value = Int32.Parse(getRoom) + 1;
                            if (value == 100)
                                getRoom = value.ToString();
                            else
                                getRoom = "0" + value.ToString();
                        }
                        else
                        {

                        }
                        string[] levels = level.Split(':');
                        if (levels.Length == 2)
                        {
                            Application.Current.Properties["CadId"] = levels[1] + getRoom;
                            AddNewRoom newRoom = new AddNewRoom(lbDisplayBuilding.SelectedItem.ToString(), lbDisplayLevels.SelectedItem.ToString(), levels[1] + getRoom,"NewRoom");
                            newRoom.ShowDialog();
                            info.Content = AddNewRoom.Info;
                            lbDisplayRooms.Items.Add(levels[1] + getRoom);
                        }
                    }

                }
                else
                {
                    string[] levels = level.Split(':');
                    if (levels.Length == 2)
                    {
                        Application.Current.Properties["CadId"] = levels[1] + "000";
                        AddNewRoom newRoom = new AddNewRoom(lbDisplayBuilding.SelectedItem.ToString(), lbDisplayLevels.SelectedItem.ToString(), levels[1] + "000","NewRoom");
                        newRoom.ShowDialog();
                        info.Content = AddNewRoom.Info;
                        lbDisplayRooms.Items.Add(levels[1] + "000");
                    }
                }
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
                string query = "select * from kl_raum where geb_id=" + lbDisplayBuilding.SelectedValue + " and geschoss_id='" + Application.Current.Properties["LevelId"] + "' and cad_id='" + lbDisplayRooms.SelectedValue + "'";
                DataTable dt = DataConnection.GetData(query);
                if (dt != null && dt.Rows.Count > 0)
                {
                    //DataTable selectedTable = dt.AsEnumerable()
                    //            .Where(r => r.Field<int>("gebaeude_id") ==Convert.ToInt32(lbDisplayBuilding.SelectedValue) && r.Field<string>("geschoss_id")==lbDisplayLevels.SelectedValue.ToString() && r.Field<int>("cad_id")==Convert.ToInt32(lbDisplayRooms.SelectedValue))
                    //            .CopyToDataTable();
                    AddNewRoom newRoom = new AddNewRoom(lbDisplayRooms.SelectedItem.ToString(), dt, "EditRoom");
                    newRoom.ShowDialog();
                    info.Content = AddNewRoom.Info;
                }
                
            }
            else
                MessageBox.Show("Select room");

        }

        private void BtnDeleteRoom_Click(object sender, RoutedEventArgs e)
        {

            if (lbDisplayRooms.SelectedItem != null)
            {
                string msg = " Are you sure want to delete room?";
                MessageBoxResult result =
                  MessageBox.Show(
                    msg,
                    "",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    // If user doesn't want to close, cancel closure

                    string query = "delete from kl_raum where geb_id=" + lbDisplayBuilding.SelectedItem + " and geschoss_id='" + lbDisplayLevels.SelectedValue + "' and cad_id='" + lbDisplayRooms.SelectedItem + "'";
                    msg = DataConnection.ExecuteQuery(query);
                    if (msg == "Executed")
                    {
                        MessageBox.Show("Successfully deleted");
                    }
                    FillRoomsByLevelAndBuilding();
                }
            }
            else
            {
                MessageBox.Show("Select room id");
            }
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
            info.Content = "";
            string query = "select geschoss_id,cad_id from kl_raum where geb_id=" + lbDisplayBuilding.SelectedItem + "";
            DataTable dtResult = DataConnection.GetData(query);
            lbDisplayLevels.Items.Clear();
            lbDisplayRooms.Items.Clear();
            for (int i = 0; i < dtResult.Rows.Count; i++)
            {
                lbDisplayLevels.Items.Add(dtResult.Rows[i][0].ToString());
            }
            Application.Current.Properties["BuildingId"] = lbDisplayBuilding.SelectedItem;
        }

        private void LbDisplayLevels_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            info.Content = "";
            string query = "select cad_id from kl_raum where geb_id=" + lbDisplayBuilding.SelectedItem + " and geschoss_id='" + lbDisplayLevels.SelectedValue + "'";
            DataTable dtResult = DataConnection.GetData(query);
            lbDisplayRooms.Items.Clear();
            for (int i = 0; i < dtResult.Rows.Count; i++)
            {
                lbDisplayRooms.Items.Add(dtResult.Rows[i][0].ToString());
            }
            Application.Current.Properties["LevelId"] = lbDisplayLevels.SelectedItem;
        }

        private void FillRoomsByLevelAndBuilding()
        {
            info.Content = "";
            string query = "select cad_id from kl_raum where geb_id=" + lbDisplayBuilding.SelectedItem + " and geschoss_id='" + lbDisplayLevels.SelectedValue + "'";
            DataTable dtResult = DataConnection.GetData(query);
            lbDisplayRooms.Items.Clear();
            for (int i = 0; i < dtResult.Rows.Count; i++)
            {
                lbDisplayRooms.Items.Add(dtResult.Rows[i][0].ToString());
            }
            Application.Current.Properties["LevelId"] = lbDisplayLevels.SelectedItem;
        }

        private void LbDisplayRooms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbDisplayRooms.SelectedItem != null)
            {
                string query = "select * from kl_raum where geb_id=" + lbDisplayBuilding.SelectedItem + " and geschoss_id='" + lbDisplayLevels.SelectedItem + "' and cad_id='" + lbDisplayRooms.SelectedItem + "'";
                DataTable dt = DataConnection.GetData(query);
                if(dt!=null && dt.Rows.Count > 0)
                {
                    info.Content = "Raum-Nr:" + dt.Rows[0][4].ToString() + "\n" + "Flaeche:" + dt.Rows[0][8].ToString() + " m\u00b2" + "\n" + "Lichte Hohe:" + dt.Rows[0][9].ToString() + " m"+"\n" + "Anz. A.P:" + dt.Rows[0][10].ToString() + "\n" + "Anz. M.A:" + dt.Rows[0][11].ToString() + "\n" + "zustand sch:" + dt.Rows[0][12].ToString() + "\n" + "Bemerkung:" + dt.Rows[0][13].ToString() + "\n" + "Umfang [m]:" + dt.Rows[0][14].ToString() + " m"+"\n" + "Anz. Heizho:" + dt.Rows[0][16].ToString() + "\n" + "Anz. Patchsc:" + dt.Rows[0][18].ToString() + "\n" + "Bezeichnug:" + dt.Rows[0][5].ToString() + "\n" + "MAX:" + dt.Rows[0][6].ToString() + "\n" + "IST:" + dt.Rows[0][7].ToString();
                }
                else
                {
                    info.Content = "\n\n\n" + "No Data Available";
                }
            }
        }
    }
}
