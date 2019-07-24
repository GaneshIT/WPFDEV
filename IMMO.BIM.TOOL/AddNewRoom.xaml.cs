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
    /// Interaction logic for AddNewRoom.xaml
    /// </summary>
    public partial class AddNewRoom : Window
    {
        public AddNewRoom()
        {
            InitializeComponent();
        }
        public AddNewRoom(string cadId)
        {
            InitializeComponent();

            this.Title ="IMMO BIM Raum CAD-ID: "+ cadId;
        }
        private void BtnNutzung_Click(object sender, RoutedEventArgs e)
        {
            Nutzung nutzung = new Nutzung();
            nutzung.Show();
        }
    }
}
