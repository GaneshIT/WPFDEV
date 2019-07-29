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

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            UpdateChildGetSet = true;
            SelectChildTypeValues = "bodenbelag " + cbBodenbelagtyp.SelectedValue + "," + txtFlaeche.Text;
            //var myWindow = 
            Window.GetWindow(this).Close();
        }
    }
}
