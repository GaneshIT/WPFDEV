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

        public UserControlFensterType(string[] controlValues)
        {
            InitializeComponent();
            cbFeststellanlage.Items.Clear();
            cbFeststellanlage.Items.Add("Test");

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
                cbFeststellanlage.SelectedItem = controlValues[4];
                txtTuernummer.Text = controlValues[5];
            }

            
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            UpdateChildGetSet = true;
            SelectChildTypeValues = "fenster" + " (" + txtHohe.Text + "x" + txtBreite.Text + ")," + cbFenstertyp.SelectedValue + "," + cbFensterrahmenmaterial.SelectedValue + "," + cbVerglasung.SelectedValue + "," + cbFeststellanlage.SelectedValue + "," + txtTuernummer.Text;
            //var myWindow = 
            Window.GetWindow(this).Close();
            //myWindow.Close();
        }
    }
}
