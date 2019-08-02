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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IMMO.BIM.TOOL
{
    /// <summary>
    /// Interaction logic for UserControlGlasbauType.xaml
    /// </summary>
    public partial class UserControlGlasbauType : UserControl
    {
        public static bool UpdateParentWindow;
        public static string selectChildValues;
        public static bool UpdateChildGetSet
        {
            set
            {
                UpdateParentWindow = value;
            }
            get
            {
                return UpdateParentWindow;
            }
        }
        public static string SelectChildTypeValues
        {
            set
            {
                selectChildValues = value;
            }
            get
            {
                return selectChildValues;
            }
        }
        public UserControlGlasbauType(DataTable controlValues)
        {
            UpdateChildGetSet = false;
            InitializeComponent();
            string query = "select * from code_glasbauelementtyp";
            DataTable dt = DataConnection.GetData(query);
            cbGlasbauType.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cbGlasbauType.Items.Add(dt.Rows[i][1].ToString());
            }
            if (cbGlasbauType.Items.Count > 0)
            {
                if (controlValues != null)
                {
                    cbGlasbauType.SelectedItem = controlValues.Rows[0][5].ToString();
                    txtGlasflaecheeinseitig.Text = controlValues.Rows[0][6].ToString();
                    txtReinigungsflaechen.Text = controlValues.Rows[0][7].ToString();
                }

            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            string msg = string.Empty;
            if (Equipment.getEquipId == null)
            {
                string query = "select top 1 id from [as_glasbau-element] order by id desc";
                DataTable dt = DataConnection.GetData(query);
                int id = 1;
                if (dt != null && dt.Rows.Count > 0)
                {
                    id = Convert.ToInt32(dt.Rows[0][0].ToString()) + 1;
                    query = "insert into [as_glasbau-element] values('" + Application.Current.Properties["BuildingId"] + "','" + Application.Current.Properties["LevelId"] + "','" + Application.Current.Properties["CadId"] + "'," + id + ",'','" + cbGlasbauType.SelectedValue + "','" + txtGlasflaecheeinseitig.Text + "','" + txtReinigungsflaechen.Text + "')";
                    msg = DataConnection.ExecuteQuery(query);
                }
                else if (dt != null && dt.Rows.Count >= 1)
                {
                    int status = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i][0].ToString() != "")
                        {
                            id = Convert.ToInt32(dt.Rows[0][0].ToString()) + 1;
                            query = "insert into [as_glasbau-element] values('" + Application.Current.Properties["BuildingId"] + "','" + Application.Current.Properties["LevelId"] + "','" + Application.Current.Properties["CadId"] + "'," + id + ",'','" + cbGlasbauType.SelectedValue + "','" + txtGlasflaecheeinseitig.Text + "','" + txtReinigungsflaechen.Text + "')";
                            msg = DataConnection.ExecuteQuery(query);
                            status = 1;
                            break;
                        }
                    }
                    if (status == 0)
                    {
                        query = "insert into [as_glasbau-element] values('" + Application.Current.Properties["BuildingId"] + "','" + Application.Current.Properties["LevelId"] + "','" + Application.Current.Properties["CadId"] + "'," + id + ",'','" + cbGlasbauType.SelectedValue + "','" + txtGlasflaecheeinseitig.Text + "','" + txtReinigungsflaechen.Text + "')";
                        msg = DataConnection.ExecuteQuery(query);
                    }

                }
                else
                {
                    query = "insert into [as_glasbau-element] values('" + Application.Current.Properties["BuildingId"] + "','" + Application.Current.Properties["LevelId"] + "','" + Application.Current.Properties["CadId"] + "'," + id + ",'','" + cbGlasbauType.SelectedValue + "','" + txtGlasflaecheeinseitig.Text + "','" + txtReinigungsflaechen.Text + "')";
                    msg = DataConnection.ExecuteQuery(query);
                }
                if (msg == "Executed")
                {
                    query = "select top 1 id from [as_glasbau-element] order by id desc";
                    dt = DataConnection.GetData(query);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        UpdateChildGetSet = true;
                        SelectChildTypeValues = "EquipId " + dt.Rows[0][0].ToString() + ": " + "glasbau-element " + cbGlasbauType.SelectedValue + "," + txtGlasflaecheeinseitig.Text + "," + txtReinigungsflaechen.Text;
                        //var myWindow = 
                        Window.GetWindow(this).Close();
                    }
                }
                else
                {
                    MessageBox.Show("Please enter correct input values");
                }
            }

            else
            {
                string updatedcolumns = "typ='" + cbGlasbauType.SelectedValue + "' , [glasflaeche einseitig]='"+txtGlasflaecheeinseitig.Text+ "' , reinigungsflaechen='"+txtReinigungsflaechen.Text+"'";
                msg = EquipmentData.UpdateEquipment("[as_glasbau-element]", updatedcolumns, Equipment.getEquipId);
                if (msg == "Executed")
                {
                    
                        UpdateChildGetSet = true;
                        SelectChildTypeValues = "EquipId " + Equipment.getEquipId + ": " + "glasbau-element " + cbGlasbauType.SelectedValue + "," + txtGlasflaecheeinseitig.Text + "," + txtReinigungsflaechen.Text;
                        //var myWindow = 
                        Window.GetWindow(this).Close();
                    
                }
                else
                {
                    MessageBox.Show("Please enter correct input values");
                }
            }
        }
    }
}
