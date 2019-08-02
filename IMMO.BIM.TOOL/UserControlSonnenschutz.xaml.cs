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
    /// Interaction logic for UserControlSonnenschutz.xaml
    /// </summary>
    public partial class UserControlSonnenschutz : UserControl
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
        public UserControlSonnenschutz(DataTable controlValues)
        {
            InitializeComponent();
            string query = "select * from code_sonnenschutztyp";
            DataTable dt = DataConnection.GetData(query);
            cbtype.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cbtype.Items.Add(dt.Rows[i][1].ToString());
            }
            if (cbtype.Items.Count > 0)
            {
                if (controlValues != null)
                {
                    cbtype.SelectedItem = controlValues.Rows[0][5].ToString();

                }

            }
            query = "select * from code_lage";
            dt = DataConnection.GetData(query);
            cbLage.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cbLage.Items.Add(dt.Rows[i][1].ToString());
            }
            if (cbLage.Items.Count > 0)
            {
                if (controlValues != null)
                {
                    cbLage.SelectedItem = controlValues.Rows[0][6].ToString();

                }

            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            string msg = string.Empty;
            if (Equipment.getEquipId == null)
            {
                string query = "select top 1 id from as_sonnenschutz order by id desc";
                DataTable dt = DataConnection.GetData(query);
                int id = 1;
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][0].ToString() != "")
                        id = Convert.ToInt32(dt.Rows[0][0].ToString()) + 1;
                    query = "insert into as_sonnenschutz values(" + Application.Current.Properties["BuildingId"] + ",'" + Application.Current.Properties["LevelId"] + "','" + Application.Current.Properties["RaumId"] + "'," + id + ",'','" + cbtype.SelectedValue + "','" + cbLage.SelectedValue + "')";
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
                            query = "insert into as_sonnenschutz values('" + Application.Current.Properties["BuildingId"] + "','" + Application.Current.Properties["LevelId"] + "','" + Application.Current.Properties["RaumId"] + "'," + id + ",'','" + cbtype.SelectedValue + "','" + cbLage.SelectedValue + "')";
                            msg = DataConnection.ExecuteQuery(query);
                            status = 1;
                            break;
                        }
                    }
                    if (status == 0)
                    {
                        query = "insert into as_sonnenschutz values('" + Application.Current.Properties["BuildingId"] + "','" + Application.Current.Properties["LevelId"] + "','" + Application.Current.Properties["RaumId"] + "'," + id + ",'','" + cbtype.SelectedValue + "','" + cbLage.SelectedValue + "')";
                        msg = DataConnection.ExecuteQuery(query);
                    }

                }
                else
                {
                    query = "insert into as_sonnenschutz values('" + Application.Current.Properties["BuildingId"] + "','" + Application.Current.Properties["LevelId"] + "','" + Application.Current.Properties["RaumId"] + "'," + id + ",'','" + cbtype.SelectedValue + "','" + cbLage.SelectedValue + "')";
                    msg = DataConnection.ExecuteQuery(query);
                }
                if (msg == "Executed")
                {
                    query = "select top 1 id from as_sonnenschutz order by id desc";
                    dt = DataConnection.GetData(query);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        UpdateChildGetSet = true;
                        SelectChildTypeValues = "EquipId " + dt.Rows[0][0].ToString() + ": " + "sonnenschutz " + cbtype.SelectedValue + "," + cbLage.SelectedValue;
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
                string updatedcolumns = "sonnenschutztyp='" + cbtype.SelectedValue + "' , lage='" + cbLage.SelectedValue + "'";
                msg = EquipmentData.UpdateEquipment("as_sonnenschutz", updatedcolumns, Equipment.getEquipId);
                if (msg == "Executed")
                {

                    UpdateChildGetSet = true;
                    SelectChildTypeValues = "EquipId " + Equipment.getEquipId + ": " + "sonnenschutz " + cbtype.SelectedValue + "," + cbLage.SelectedValue;
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
