using System;
using System.Collections.Generic;
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
    /// Interaction logic for AddNewBuilding.xaml
    /// </summary>
    public partial class AddNewBuilding : Window
    {
        bool UpdateParentWindow;

        public delegate void DataChangedEventHandler(object sender, EventArgs e);

        public event DataChangedEventHandler DataChanged;

        public AddNewBuilding()
        {
            InitializeComponent();
        }
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
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            string query = "insert into kl_gebaeude values('" + txtBuildingId.Text + "','')";
            DataConnection.ExecuteQuery(query);
            UpdateGetSet = true;
            this.Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
