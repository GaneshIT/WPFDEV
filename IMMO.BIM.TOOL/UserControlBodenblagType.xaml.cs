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
    /// Interaction logic for UserControlBodenblagType.xaml
    /// </summary>
    public partial class UserControlBodenblagType : UserControl
    {
        private static string getEqupId { get; set; }
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
        public UserControlBodenblagType(string[] controlValues)
        {
            InitializeComponent();
            string query = "select * from code_bodenbelagtyp";
            DataTable dt = DataConnection.GetData(query);
            cbBodenbelagtyp.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cbBodenbelagtyp.Items.Add(dt.Rows[i][1].ToString());
            }
            if (cbBodenbelagtyp.Items.Count > 0)
            {
                if (controlValues != null && controlValues.Length == 2)
                {
                    cbBodenbelagtyp.SelectedItem = controlValues[0].ToString().Split(' ')[1];
                    txtFlaeche.Text = controlValues[1];
                }
            }
        }

        public UserControlBodenblagType(DataTable dtControlValues)
        {
            InitializeComponent();
            string query = "select * from code_bodenbelagtyp";
            DataTable dt = DataConnection.GetData(query);
            cbBodenbelagtyp.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cbBodenbelagtyp.Items.Add(dt.Rows[i][1].ToString());
            }
            if (cbBodenbelagtyp.Items.Count > 0)
            {
                if (dtControlValues!=null)
                {
                    cbBodenbelagtyp.SelectedItem = dtControlValues.Rows[0][5].ToString();
                    txtFlaeche.Text = dtControlValues.Rows[0][6].ToString();
                    getEqupId = dtControlValues.Rows[0][3].ToString();
                }
            }
        }
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            string msg = string.Empty;

            if (Equipment.getEquipId == null)
            {
                string query = "select top 1 id from as_bodenbelag order by id desc";
                DataTable dt = DataConnection.GetData(query);
                int id = 1;
                if (dt != null && dt.Rows.Count == 1)
                {
                    id = Convert.ToInt32(dt.Rows[0][0].ToString()) + 1;
                    query = "insert into as_bodenbelag values('" + Application.Current.Properties["BuildingId"] + "','" + Application.Current.Properties["LevelId"] + "','" + Application.Current.Properties["RaumId"] + "'," + id + ",'','" + cbBodenbelagtyp.SelectedValue + "'," + txtFlaeche.Text + ")";
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
                            query = "insert into as_bodenbelag values('" + Application.Current.Properties["BuildingId"] + "','" + Application.Current.Properties["LevelId"] + "','" + Application.Current.Properties["RaumId"] + "'," + id + ",'','" + cbBodenbelagtyp.SelectedValue + "'," + txtFlaeche.Text + ")";
                            msg = DataConnection.ExecuteQuery(query);
                            status = 1;
                            break;
                        }
                    }
                    if (status == 0)
                    {
                        query = "insert into as_bodenbelag values('" + Application.Current.Properties["BuildingId"] + "','" + Application.Current.Properties["LevelId"] + "','" + Application.Current.Properties["RaumId"] + "'," + id + ",'','" + cbBodenbelagtyp.SelectedValue + "'," + txtFlaeche.Text + ")";
                        msg = DataConnection.ExecuteQuery(query);
                    }

                }
                else
                {
                    query = "insert into as_bodenbelag values('" + Application.Current.Properties["BuildingId"] + "','" + Application.Current.Properties["LevelId"] + "','" + Application.Current.Properties["RaumId"] + "'," + id + ",'','" + cbBodenbelagtyp.SelectedValue + "'," + txtFlaeche.Text + ")";
                    msg = DataConnection.ExecuteQuery(query);
                }
                //var myWindow = 
                if (msg == "Executed")
                {
                    query = "select top 1 id from as_bodenbelag order by id desc";
                    dt = DataConnection.GetData(query);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        UpdateChildGetSet = true;
                        SelectChildTypeValues = "EquipId " + dt.Rows[0][0].ToString() + ": " + "bodenbelag " + cbBodenbelagtyp.SelectedValue + "," + txtFlaeche.Text;
                        Window.GetWindow(this).Close();
                    }
                }
                else
                {
                    MessageBox.Show("Please select/enter correct input values");
                }
            }
            else
            {
                string updatedcolumns = "bodenbelagstyp='" + cbBodenbelagtyp.SelectedValue + "' , flaeche='" + txtFlaeche.Text + "'";
                msg=EquipmentData.UpdateEquipment("as_bodenbelag", updatedcolumns, Equipment.getEquipId);
                if (msg == "Executed")
                {                   
                        UpdateChildGetSet = true;
                        SelectChildTypeValues = "EquipId " + Equipment.getEquipId + ": " + "bodenbelag " + cbBodenbelagtyp.SelectedValue + "," + txtFlaeche.Text;
                        Window.GetWindow(this).Close();                   
                }
                else
                {
                    MessageBox.Show("Please select/enter correct input values");
                }
            }
        }
    }
}
