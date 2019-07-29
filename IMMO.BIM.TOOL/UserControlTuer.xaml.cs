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
    /// Interaction logic for UserControlTuer.xaml
    /// </summary>
    public partial class UserControlTuer : UserControl
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
        public UserControlTuer(string[] controlValues)
        {
            InitializeComponent();
            string query = "select * from code_tuertyp";
            DataTable dt = DataConnection.GetData(query);
            cbTürtyp.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cbTürtyp.Items.Add(dt.Rows[i][1].ToString());
            }
            if (cbTürtyp.Items.Count > 0)
            {
                if (controlValues != null)
                    cbTürtyp.SelectedItem = controlValues[1];
            }
            query = "select * from code_tuerblattmaterial";
            dt = DataConnection.GetData(query);
            cbTürblattmaterial.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cbTürblattmaterial.Items.Add(dt.Rows[i][1].ToString());
            }
            if (cbTürblattmaterial.Items.Count > 0)
            {
                if (controlValues != null)
                    cbTürblattmaterial.SelectedItem = controlValues[2];
            }
            query = "select * from code_tuerzargenmaterial";
            dt = DataConnection.GetData(query);
            cbTuerzargenmaterial.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cbTuerzargenmaterial.Items.Add(dt.Rows[i][1].ToString());
            }
            if (cbTuerzargenmaterial.Items.Count > 0)
            {
                if (controlValues != null)
                    cbTuerzargenmaterial.SelectedItem = controlValues[3];
            }
            query = "select * from code_antrieb";
            dt = DataConnection.GetData(query);
            cbAntrieb.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cbAntrieb.Items.Add(dt.Rows[i][1].ToString());
            }
            if (cbTuerzargenmaterial.Items.Count > 0)
            {
                if (controlValues != null)
                {
                    cbTuerzargenmaterial.SelectedItem = controlValues[5];
                    txtGlasflaeche.Text = controlValues[4];
                    if (controlValues != null)
                    {
                        string[] heightwidth = controlValues[0].ToString().Split(' ')[1].ToString().Split('x');
                        txtHohe.Text = heightwidth[0].ToString().Replace("(", "");
                        txtBreite.Text = heightwidth[1].ToString().Replace(")", "");                       
                    }
                }
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            UpdateChildGetSet = true;
            SelectChildTypeValues = "tuer" + " (" + txtHohe.Text + "x" + txtBreite.Text + ")," + cbTürtyp.SelectedValue + "," + cbTürblattmaterial.SelectedValue + "," + cbTuerzargenmaterial.SelectedValue + ","+ txtGlasflaeche.Text+"," + cbAntrieb.SelectedValue ;
            //var myWindow = 
            Window.GetWindow(this).Close();
        }
    }
}
