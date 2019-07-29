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
    /// Interaction logic for UserControlLeuchtentyp.xaml
    /// </summary>
    public partial class UserControlLeuchtentyp : UserControl
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
        public UserControlLeuchtentyp(string[] controlValues)
        {
            InitializeComponent();
            string query = "select * from code­_leuchtentyp";
            DataTable dt = DataConnection.GetData(query);
            cbLeuchtentyp.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cbLeuchtentyp.Items.Add(dt.Rows[i][1].ToString());
            }
            if (cbLeuchtentyp.Items.Count > 0)
            {
                if (controlValues != null)
                {
                    cbLeuchtentyp.SelectedItem = controlValues[0].ToString().Split(' ')[1];
                }

            }
            query = "select * from code_leuchtmitteltyp";
            dt = DataConnection.GetData(query);
            cbLeuchtmitteltyp.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cbLeuchtmitteltyp.Items.Add(dt.Rows[i][1].ToString());
            }
            if (cbLeuchtmitteltyp.Items.Count > 0)
            {
                if (controlValues != null)
                {
                    cbLeuchtmitteltyp.SelectedItem = controlValues[1];
                }

            }
            query = "select * from code_montage";
            dt = DataConnection.GetData(query);
            cbMontage.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cbMontage.Items.Add(dt.Rows[i][1].ToString());
            }
            if (cbMontage.Items.Count > 0)
            {
                if (controlValues != null)
                {
                    cbMontage.SelectedItem = controlValues[3];
                    txtAnzahlleuchtmittel.Text = controlValues[2];
                }

            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            UpdateChildGetSet = true;
            SelectChildTypeValues = "leuchte " + cbLeuchtentyp.SelectedValue+","+ cbLeuchtmitteltyp.SelectedValue+","+ txtAnzahlleuchtmittel.Text+","+ cbMontage.SelectedValue;
            //var myWindow = 
            Window.GetWindow(this).Close();
        }
    }
}
