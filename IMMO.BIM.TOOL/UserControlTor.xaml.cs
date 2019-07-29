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
    /// Interaction logic for UserControlTor.xaml
    /// </summary>
    public partial class UserControlTor : UserControl
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
        public UserControlTor(string[] controlValues)
        {
            InitializeComponent();
            string query = "select * from code_tortyp";
            DataTable dt = DataConnection.GetData(query);
            cbTortyp.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cbTortyp.Items.Add(dt.Rows[i][1].ToString());
            }
            if (cbTortyp.Items.Count > 0)
            {
                if (controlValues != null)
                {
                    cbTortyp.SelectedItem = controlValues[1];

                }

            }
            query = "select * from code_antrieb";
            dt = DataConnection.GetData(query);
            cbAntrieb.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cbAntrieb.Items.Add(dt.Rows[i][1].ToString());
            }
            if (cbTortyp.Items.Count > 0)
            {
                if (controlValues != null)
                {
                    cbTortyp.SelectedItem = controlValues[2];

                }

            }
            if (controlValues != null)
            {
                string[] heightwidth = controlValues[0].ToString().Split(' ')[1].ToString().Split('x');
                txtHohe.Text = heightwidth[0].ToString().Replace("(", "");
                txtBreite.Text = heightwidth[1].ToString().Replace(")", "");                
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            UpdateChildGetSet = true;
            SelectChildTypeValues = "tor" + " (" + txtHohe.Text + "x" + txtBreite.Text + ")," + cbTortyp.SelectedValue + "," + cbAntrieb.SelectedValue;
            //var myWindow = 
            Window.GetWindow(this).Close();
        }
    }
}
