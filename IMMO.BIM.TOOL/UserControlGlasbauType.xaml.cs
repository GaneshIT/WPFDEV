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
        public UserControlGlasbauType(string[] controlValues)
        {
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
                    cbGlasbauType.SelectedItem = controlValues[0].ToString().Split(' ')[1];
                    txtGlasflaecheeinseitig.Text = controlValues[1];
                    txtReinigungsflaechen.Text = controlValues[2];
                }

            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            UpdateChildGetSet = true;
            SelectChildTypeValues = "glasbau-element " + cbGlasbauType.SelectedValue + "," + txtGlasflaecheeinseitig.Text + "," + txtReinigungsflaechen.Text;
            //var myWindow = 
            Window.GetWindow(this).Close();
        }
    }
}
