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
    /// Interaction logic for UserControlFensterType.xaml
    /// </summary>
    public partial class UserControlFensterType : UserControl
    {
        public static string getEqupId { get; set; }
        public static bool UpdateParentWindow;
        public static string selectChildValues;
        private bool statusYesOrNo;
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

        public UserControlFensterType(string[] controlValues)
        {
            InitializeComponent();
            //cbFeststellanlage.Items.Clear();
            //cbFeststellanlage.Items.Add("Test");

            string query = "select * from code_fenstertyp";
            DataTable dt = DataConnection.GetData(query);
            cbFenstertyp.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cbFenstertyp.Items.Add(dt.Rows[i][1].ToString());
            }
            if (cbFenstertyp.Items.Count > 0)
            {
                if (controlValues != null)
                    cbFenstertyp.SelectedItem = controlValues[1];
            } 
            query = "select * from code_fensterrahmenmaterial";
            dt = DataConnection.GetData(query);
            cbFensterrahmenmaterial.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cbFensterrahmenmaterial.Items.Add(dt.Rows[i][1].ToString());
            }
            if (cbFensterrahmenmaterial.Items.Count > 0)
            {
                if (controlValues != null)
                    cbFensterrahmenmaterial.SelectedItem = controlValues[2];
            }
            query = "select * from code_verglasung";
            dt = DataConnection.GetData(query);
            cbVerglasung.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cbVerglasung.Items.Add(dt.Rows[i][1].ToString());
            }
            if (cbVerglasung.Items.Count > 0)
            {
                if (controlValues != null)
                    cbVerglasung.SelectedItem = controlValues[3];
            }
            if (controlValues != null)
            {
                string[] heightwidth = controlValues[0].ToString().Split(' ')[1].ToString().Split('x');
                txtHohe.Text = heightwidth[0].ToString().Replace("(", "");
                txtBreite.Text = heightwidth[1].ToString().Replace(")", "");
                //cbFeststellanlage.SelectedItem = controlValues[4];
                if (controlValues[4].ToString() != "")
                {
                    if (controlValues[4].ToString() == "Y")
                    {
                        rbYes.IsChecked = true;
                        rbNo.IsChecked = false;
                    }
                    else
                    {
                        rbNo.IsChecked = true;
                        rbYes.IsChecked = false;

                    }
                }
                    txtTuernummer.Text = controlValues[5];
            }

            
        }

        public UserControlFensterType(DataTable dtControlValues)
        {
            InitializeComponent();
            //cbFeststellanlage.Items.Clear();
            //cbFeststellanlage.Items.Add("Test");

            string query = "select * from code_fenstertyp";
            DataTable dt = DataConnection.GetData(query);
            cbFenstertyp.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cbFenstertyp.Items.Add(dt.Rows[i][1].ToString());
            }
            if (cbFenstertyp.Items.Count > 0)
            {
                if (dtControlValues != null)
                    cbFenstertyp.SelectedItem = dt.Rows[0][5].ToString();
            }
            query = "select * from code_fensterrahmenmaterial";
            dt = DataConnection.GetData(query);
            cbFensterrahmenmaterial.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cbFensterrahmenmaterial.Items.Add(dt.Rows[i][1].ToString());
            }
            if (cbFensterrahmenmaterial.Items.Count > 0)
            {
                if (dtControlValues != null)
                    cbFenstertyp.SelectedItem = dt.Rows[0][6].ToString();
            }
            query = "select * from code_verglasung";
            dt = DataConnection.GetData(query);
            cbVerglasung.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cbVerglasung.Items.Add(dt.Rows[i][1].ToString());
            }
            if (cbVerglasung.Items.Count > 0)
            {
                if (dtControlValues != null)
                    cbFenstertyp.SelectedItem = dt.Rows[0][7].ToString();
            }
            if (dtControlValues != null)
            {
                txtHohe.Text = dt.Rows[0][8].ToString();
                txtBreite.Text = dt.Rows[0][9].ToString();
                //cbFeststellanlage.SelectedItem = controlValues[4];
                //if (controlValues[4].ToString() != "")
                //{
                //    if (controlValues[4].ToString() == "Y")
                //    {
                //        rbYes.IsChecked = true;
                //        rbNo.IsChecked = false;
                //    }
                //    else
                //    {
                //        rbNo.IsChecked = true;
                //        rbYes.IsChecked = false;

                //    }
                //}
                txtTuernummer.Text = dt.Rows[0][11].ToString();
            }


        }
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            string msg = string.Empty;

            if (Equipment.getEquipId == null)
            {
                string query = "select top 1 id from as_fenster order by id desc";
                DataTable dt = DataConnection.GetData(query);
                int id = 1;
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][0].ToString() != "")
                        id = Convert.ToInt32(dt.Rows[0][0].ToString()) + 1;
                    query = "insert into as_fenster values('" + Application.Current.Properties["BuildingId"] + "','" + Application.Current.Properties["LevelId"] + "','" + Application.Current.Properties["RaumId"] + "'," + id + ",'','" + cbFenstertyp.SelectedValue + "','" + cbFensterrahmenmaterial.SelectedValue + "','" + cbVerglasung.SelectedValue + "','" + txtBreite.Text + "','" + txtHohe.Text + "'," + statusYesOrNo + ",'" + txtTuernummer.Text + "')";
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
                            query = "insert into as_fenster values('" + Application.Current.Properties["BuildingId"] + "','" + Application.Current.Properties["LevelId"] + "','" + Application.Current.Properties["RaumId"] + "'," + id + ",'','" + cbFenstertyp.SelectedValue + "','" + cbFensterrahmenmaterial.SelectedValue + "','" + cbVerglasung.SelectedValue + "','" + txtBreite.Text + "','" + txtHohe.Text + "'," + statusYesOrNo + ",'" + txtTuernummer.Text + "')";
                            msg = DataConnection.ExecuteQuery(query);
                            status = 1;
                            break;
                        }
                    }
                    if (status == 0)
                    {
                        query = "insert into as_fenster values('" + Application.Current.Properties["BuildingId"] + "','" + Application.Current.Properties["LevelId"] + "','" + Application.Current.Properties["RaumId"] + "'," + id + ",'','" + cbFenstertyp.SelectedValue + "','" + cbFensterrahmenmaterial.SelectedValue + "','" + cbVerglasung.SelectedValue + "','" + txtBreite.Text + "','" + txtHohe.Text + "'," + statusYesOrNo + ",'" + txtTuernummer.Text + "')";
                        msg = DataConnection.ExecuteQuery(query);
                    }

                }
                else
                {
                    query = "insert into as_fenster values('" + Application.Current.Properties["BuildingId"] + "','" + Application.Current.Properties["LevelId"] + "','" + Application.Current.Properties["RaumId"] + "'," + id + ",'','" + cbFenstertyp.SelectedValue + "','" + cbFensterrahmenmaterial.SelectedValue + "','" + cbVerglasung.SelectedValue + "','" + txtBreite.Text + "','" + txtHohe.Text + "'," + statusYesOrNo + ",'" + txtTuernummer.Text + "')";
                    msg = DataConnection.ExecuteQuery(query);
                }
                if (msg == "Executed")
                {
                    query = "select top 1 id from as_fenster order by id desc";
                    dt = DataConnection.GetData(query);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        UpdateChildGetSet = true;
                        SelectChildTypeValues = "EquipId " + dt.Rows[0][0].ToString() + ": " + "fenster" + " (" + txtHohe.Text + "x" + txtBreite.Text + ")," + cbFenstertyp.SelectedValue + "," + cbFensterrahmenmaterial.SelectedValue + "," + cbVerglasung.SelectedValue + "," + statusYesOrNo + "," + txtTuernummer.Text;
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
                string updatedcolumns = "fenstertyp='" + cbFenstertyp.SelectedValue + "' , fensterrahmenmaterial='" + cbFensterrahmenmaterial.SelectedValue + "' , verglasung='" + cbVerglasung.SelectedValue + "' , breite='" + txtBreite.Text + "' , hoehe='" + txtHohe.Text + "' , feststellanlage=" + statusYesOrNo + " , tuernummer='" + txtTuernummer.Text + "'";
                msg = EquipmentData.UpdateEquipment("as_fenster", updatedcolumns, Equipment.getEquipId);
                if (msg == "Executed")
                {
                   
                        UpdateChildGetSet = true;
                        SelectChildTypeValues = "EquipId " + Equipment.getEquipId + ": " + "fenster" + " (" + txtHohe.Text + "x" + txtBreite.Text + ")," + cbFenstertyp.SelectedValue + "," + cbFensterrahmenmaterial.SelectedValue + "," + cbVerglasung.SelectedValue + "," + statusYesOrNo + "," + txtTuernummer.Text;

                        Window.GetWindow(this).Close();
                    
                }
                else
                {
                    MessageBox.Show("Please enter correct input values");
                }
            }
        }

        private void RbYes_Checked(object sender, RoutedEventArgs e)
        {
            if (rbYes.IsChecked == true)
            {
                rbNo.IsChecked = false;
                statusYesOrNo = true;
            }
        }

        private void RbNo_Checked(object sender, RoutedEventArgs e)
        {
            if (rbNo.IsChecked == true)
            {
                rbYes.IsChecked = false;
                statusYesOrNo = true;
            }

        }
    }
}
