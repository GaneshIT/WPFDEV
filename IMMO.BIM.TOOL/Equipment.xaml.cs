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
    /// Interaction logic for Equipment.xaml
    /// </summary>
    public partial class Equipment : Window
    {
        bool UpdateParentWindow;
        string ausstattungstyp;
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
        public string SelectAusstattungsType
        {
            set
            {
                ausstattungstyp = value;
            }
            get
            {
                return ausstattungstyp;
            }
        }
        public Equipment()
        {
            InitializeComponent();
            AddTypes();
        }
        public Equipment(string ausstatType)
        {
            InitializeComponent();
            AddTypes();
            cbAusstattungstyp.SelectedItem = ausstatType;
            AddChildUserControl();
        }

        private void CbAusstattungstyp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AddChildUserControl();
        }

        private void AddTypes()
        {
            cbAusstattungstyp.Items.Clear();
            cbAusstattungstyp.Items.Add("fenster");
            cbAusstattungstyp.Items.Add("bodenbelag");
            cbAusstattungstyp.Items.Add("feuerloescher");
            cbAusstattungstyp.Items.Add("glasbau-element");
            cbAusstattungstyp.Items.Add("leuchte");
            cbAusstattungstyp.Items.Add("oberlicht");
            cbAusstattungstyp.Items.Add("sonnenschutz");
            cbAusstattungstyp.Items.Add("tor");
            cbAusstattungstyp.Items.Add("tuer");
        }

        private void AddChildUserControl()
        {
            gridAddUserCtrlAusstattungstyp.Children.Clear();
            SelectAusstattungsType = cbAusstattungstyp.SelectedValue.ToString();
            if (cbAusstattungstyp.SelectedValue.ToString() == "fenster")
            {
                UserControlFensterType userControlFensterType = new UserControlFensterType();
                gridAddUserCtrlAusstattungstyp.Children.Add(userControlFensterType);



            }
            else if (cbAusstattungstyp.SelectedValue.ToString() == "bodenbelag")
            {
                UserControlBodenblagType userControlBodenblagType = new UserControlBodenblagType();
                gridAddUserCtrlAusstattungstyp.Children.Add(userControlBodenblagType);
            }
            else if (cbAusstattungstyp.SelectedValue.ToString() == "feuerloescher")
            {
                UserControlFeuerloeschertyp userControlFeuerloeschertyp = new UserControlFeuerloeschertyp();
                gridAddUserCtrlAusstattungstyp.Children.Add(userControlFeuerloeschertyp);
            }
            else if (cbAusstattungstyp.SelectedValue.ToString() == "glasbau-element")
            {
                UserControlGlasbauType userControlGlasbauType = new UserControlGlasbauType();
                gridAddUserCtrlAusstattungstyp.Children.Add(userControlGlasbauType);
            }
            else if (cbAusstattungstyp.SelectedValue.ToString() == "leuchte")
            {
                UserControlLeuchtentyp userControlLeuchtentyp = new UserControlLeuchtentyp();
                gridAddUserCtrlAusstattungstyp.Children.Add(userControlLeuchtentyp);
            }
            else if (cbAusstattungstyp.SelectedValue.ToString() == "oberlicht")
            {
                UserControlOberlicut userControlOberlicut = new UserControlOberlicut();
                gridAddUserCtrlAusstattungstyp.Children.Add(userControlOberlicut);
            }
            else if (cbAusstattungstyp.SelectedValue.ToString() == "sonnenschutz")
            {
                UserControlFensterType userControlFensterType = new UserControlFensterType();
                gridAddUserCtrlAusstattungstyp.Children.Add(userControlFensterType);
            }
            else if (cbAusstattungstyp.SelectedValue.ToString() == "tor")
            {
                UserControlTor userControlTor = new UserControlTor();
                gridAddUserCtrlAusstattungstyp.Children.Add(userControlTor);
            }
            else if (cbAusstattungstyp.SelectedValue.ToString() == "tuer")
            {
                UserControlTuer userControlTuer = new UserControlTuer();
                gridAddUserCtrlAusstattungstyp.Children.Add(userControlTuer);
            }
        }
    }
}
