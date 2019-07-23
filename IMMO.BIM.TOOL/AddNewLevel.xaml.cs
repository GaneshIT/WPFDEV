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
    /// Interaction logic for AddNewLevel.xaml
    /// </summary>
    public partial class AddNewLevel : Window
    {
        public AddNewLevel()
        {
            InitializeComponent();
            //string query = "select geb_id from kl_gebaeude";
            //DataTable dtResult = DataConnection.GetData(query);
            //lbDisplayBuilding.Items.Clear();
            //for (int i = 0; i < dtResult.Rows.Count; i++)
            //{
            //    lbDisplayBuilding.Items.Add(dtResult.Rows[i][0].ToString());
            //}
        }

        private void BtnActivate_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
