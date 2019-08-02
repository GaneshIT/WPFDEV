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
    /// Interaction logic for UserControlFeuerloeschertyp.xaml
    /// </summary>
    public partial class UserControlFeuerloeschertyp : UserControl
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

        public UserControlFeuerloeschertyp(string[] controlValues)
        {
            InitializeComponent();
            string query = "select * from code_feuerloeschertyp";
            DataTable dt = DataConnection.GetData(query);
            cbFeuerloeschertyp.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cbFeuerloeschertyp.Items.Add(dt.Rows[i][1].ToString());
            }
            if (cbFeuerloeschertyp.Items.Count > 0)
            {
                if (controlValues != null)
                {
                    cbFeuerloeschertyp.SelectedItem = controlValues[0].ToString().Split(' ')[1];
                }
            }
        }

        public UserControlFeuerloeschertyp(DataTable controlValues)
        {
            InitializeComponent();
            string query = "select * from code_feuerloeschertyp";
            DataTable dt = DataConnection.GetData(query);
            cbFeuerloeschertyp.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cbFeuerloeschertyp.Items.Add(dt.Rows[i][1].ToString());
            }
            if (cbFeuerloeschertyp.Items.Count > 0)
            {
                if (controlValues != null)
                {
                    cbFeuerloeschertyp.SelectedItem = controlValues.Rows[0][5].ToString();
                }
            }
        }
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            string msg = string.Empty;
            if (Equipment.getEquipId == null)
            {
                string query = "select top 1 id from as_feuerloescher order by id desc";
                DataTable dt = DataConnection.GetData(query);
                int id = 1;
                if (dt != null && dt.Rows.Count > 0)
                {
                    id = Convert.ToInt32(dt.Rows[0][0].ToString()) + 1;
                    query = "insert into as_feuerloescher values('" + Application.Current.Properties["BuildingId"] + "','" + Application.Current.Properties["LevelId"] + "','" + Application.Current.Properties["CadId"] + "'," + id + ",'','" + cbFeuerloeschertyp.SelectedValue + "')";
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
                            query = "insert into as_feuerloescher values('" + Application.Current.Properties["BuildingId"] + "','" + Application.Current.Properties["LevelId"] + "','" + Application.Current.Properties["CadId"] + "'," + id + ",'','" + cbFeuerloeschertyp.SelectedValue + "')";
                            msg = DataConnection.ExecuteQuery(query);
                            status = 1;
                            break;
                        }
                    }
                    if (status == 0)
                    {
                        query = "insert into as_feuerloescher values('" + Application.Current.Properties["BuildingId"] + "','" + Application.Current.Properties["LevelId"] + "','" + Application.Current.Properties["CadId"] + "'," + id + ",'','" + cbFeuerloeschertyp.SelectedValue + "')";
                        msg = DataConnection.ExecuteQuery(query);
                    }

                }
                else
                {
                    query = "insert into as_feuerloescher values('" + Application.Current.Properties["BuildingId"] + "','" + Application.Current.Properties["LevelId"] + "','" + Application.Current.Properties["CadId"] + "'," + id + ",'','" + cbFeuerloeschertyp.SelectedValue + "')";
                    msg = DataConnection.ExecuteQuery(query);
                }
                if (msg == "Executed")
                {
                    query = "select top 1 id from as_feuerloescher order by id desc";
                    dt = DataConnection.GetData(query);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        UpdateChildGetSet = true;
                        SelectChildTypeValues = "EquipId " + dt.Rows[0][0].ToString() + ": " + "feuerloescher " + cbFeuerloeschertyp.SelectedValue;
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
                string updatedcolumns = "feuerloeschertyp='" + cbFeuerloeschertyp.SelectedValue + "'";
                msg = EquipmentData.UpdateEquipment("as_feuerloescher", updatedcolumns, Equipment.getEquipId);
                if (msg == "Executed")
                {

                    UpdateChildGetSet = true;
                    SelectChildTypeValues = "EquipId " + Equipment.getEquipId + ": " + "feuerloescher " + cbFeuerloeschertyp.SelectedValue;

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
