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
using System.Windows.Shapes;

namespace IMMO.BIM.TOOL
{
    /// <summary>
    /// Interaction logic for Nutzung.xaml
    /// </summary>
    public partial class Nutzung : Window
    {
        bool UpdateParentWindow;
        string nutzungResult;
        public bool UpdateGetSet
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
        public string NutzungResult
        {
            set
            {
                nutzungResult = value;
            }
            get
            {
                return nutzungResult;
            }
        }
        public Nutzung()
        {
            InitializeComponent();
            string query = "select id,value from DIN277_VF";
            DataTable dtResult = DataConnection.GetData(query);
            cbnutzuist.Items.Clear();
            cbntzumax.Items.Clear();
            for (int i = 0; i < dtResult.Rows.Count; i++)
            {
                cbnutzuist.Items.Add(dtResult.Rows[i][0].ToString() + " " + dtResult.Rows[i][1].ToString());
                cbntzumax.Items.Add(dtResult.Rows[i][0].ToString() + " " + dtResult.Rows[i][1].ToString());
            }
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            UpdateGetSet = true;
            NutzungResult = "IST: " + cbnutzuist.SelectedItem + "\n" + "MAX: " + cbntzumax.SelectedItem + "\n" + "Beziechnung: " + cbbezei.SelectedItem;
            this.Close();
        }
    }
}
