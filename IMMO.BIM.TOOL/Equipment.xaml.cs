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
        public static string getEquipId { get; set; }
        public Equipment()
        {
            InitializeComponent();
            AddTypes();
        }
        //public Equipment(string ausstatType,string[] controlValues)
        //{
        //    InitializeComponent();
        //    AddTypes();
        //    cbAusstattungstyp.SelectedItem = ausstatType;
        //    AddChildUserControl(controlValues);
        //}
        public Equipment(string equipId,string ausstatType)
        {
            InitializeComponent();
            if (Application.Current.Properties["CopyRowStatus"].ToString() == "No")
                getEquipId = equipId;
            else
                getEquipId = null;
            AddTypes();
            cbAusstattungstyp.SelectedItem = ausstatType;
            string query = "select * from [as_" + ausstatType + "] where gebaeude_id=" + Application.Current.Properties["BuildingId"] + " and geschoss_id='" + Application.Current.Properties["LevelId"] + "' and raum_id='" + Application.Current.Properties["RaumId"] + "' and id=" + equipId + "";
            DataTable dt = DataConnection.GetData(query);
            if (dt != null && dt.Rows.Count>0)
                Application.Current.Properties["RaumId"] = dt.Rows[0][2].ToString();
            else
                Application.Current.Properties["RaumId"] = "";
            AddChildUserControl(dt);
        }
        private void CbAusstattungstyp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataTable dt = null;
            AddChildUserControl(dt);
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

        //private void AddChildUserControl(string[] controlValues)
        //{
        //    gridAddUserCtrlAusstattungstyp.Children.Clear();
        //    SelectAusstattungsType = cbAusstattungstyp.SelectedValue.ToString();
        //    if (cbAusstattungstyp.SelectedValue.ToString() == "fenster")
        //    {
        //        UserControlFensterType userControlFensterType = new UserControlFensterType(controlValues);
        //        gridAddUserCtrlAusstattungstyp.Children.Add(userControlFensterType);
        //    }
        //    else if (cbAusstattungstyp.SelectedValue.ToString() == "bodenbelag")
        //    {
        //        UserControlBodenblagType userControlBodenblagType = new UserControlBodenblagType(controlValues);
        //        gridAddUserCtrlAusstattungstyp.Children.Add(userControlBodenblagType);
        //    }
        //    else if (cbAusstattungstyp.SelectedValue.ToString() == "feuerloescher")
        //    {
        //        UserControlFeuerloeschertyp userControlFeuerloeschertyp = new UserControlFeuerloeschertyp(controlValues);
        //        gridAddUserCtrlAusstattungstyp.Children.Add(userControlFeuerloeschertyp);
        //    }
        //    else if (cbAusstattungstyp.SelectedValue.ToString() == "glasbau-element")
        //    {
        //        UserControlGlasbauType userControlGlasbauType = new UserControlGlasbauType(controlValues);
        //        gridAddUserCtrlAusstattungstyp.Children.Add(userControlGlasbauType);
        //    }
        //    else if (cbAusstattungstyp.SelectedValue.ToString() == "leuchte")
        //    {
        //        UserControlLeuchtentyp userControlLeuchtentyp = new UserControlLeuchtentyp(controlValues);
        //        gridAddUserCtrlAusstattungstyp.Children.Add(userControlLeuchtentyp);
        //    }
        //    else if (cbAusstattungstyp.SelectedValue.ToString() == "oberlicht")
        //    {
        //        UserControlOberlicut userControlOberlicut = new UserControlOberlicut(controlValues);
        //        gridAddUserCtrlAusstattungstyp.Children.Add(userControlOberlicut);
        //    }
        //    else if (cbAusstattungstyp.SelectedValue.ToString() == "sonnenschutz")
        //    {
        //        UserControlFensterType userControlFensterType = new UserControlFensterType(controlValues);
        //        gridAddUserCtrlAusstattungstyp.Children.Add(userControlFensterType);
        //    }
        //    else if (cbAusstattungstyp.SelectedValue.ToString() == "tor")
        //    {
        //        UserControlTor userControlTor = new UserControlTor(controlValues);
        //        gridAddUserCtrlAusstattungstyp.Children.Add(userControlTor);
        //    }
        //    else if (cbAusstattungstyp.SelectedValue.ToString() == "tuer")
        //    {
        //        UserControlTuer userControlTuer = new UserControlTuer(controlValues);
        //        gridAddUserCtrlAusstattungstyp.Children.Add(userControlTuer);
        //    }
        //}

        private void AddChildUserControl(DataTable dt)
        {
            gridAddUserCtrlAusstattungstyp.Children.Clear();
            SelectAusstattungsType = cbAusstattungstyp.SelectedValue.ToString();
            if (cbAusstattungstyp.SelectedValue.ToString() == "fenster")
            {
                UserControlFensterType userControlFensterType = new UserControlFensterType(dt);
                gridAddUserCtrlAusstattungstyp.Children.Add(userControlFensterType);
            }
            else if (cbAusstattungstyp.SelectedValue.ToString() == "bodenbelag")
            {
                UserControlBodenblagType userControlBodenblagType = new UserControlBodenblagType(dt);
                gridAddUserCtrlAusstattungstyp.Children.Add(userControlBodenblagType);
            }
            else if (cbAusstattungstyp.SelectedValue.ToString() == "feuerloescher")
            {
                UserControlFeuerloeschertyp userControlFeuerloeschertyp = new UserControlFeuerloeschertyp(dt);
                gridAddUserCtrlAusstattungstyp.Children.Add(userControlFeuerloeschertyp);
            }
            else if (cbAusstattungstyp.SelectedValue.ToString() == "glasbau-element")
            {
                UserControlGlasbauType userControlGlasbauType = new UserControlGlasbauType(dt);
                gridAddUserCtrlAusstattungstyp.Children.Add(userControlGlasbauType);
            }
            else if (cbAusstattungstyp.SelectedValue.ToString() == "leuchte")
            {
                UserControlLeuchtentyp userControlLeuchtentyp = new UserControlLeuchtentyp(dt);
                gridAddUserCtrlAusstattungstyp.Children.Add(userControlLeuchtentyp);
            }
            else if (cbAusstattungstyp.SelectedValue.ToString() == "oberlicht")
            {
                UserControlOberlicut userControlOberlicut = new UserControlOberlicut(dt);
                gridAddUserCtrlAusstattungstyp.Children.Add(userControlOberlicut);
            }
            else if (cbAusstattungstyp.SelectedValue.ToString() == "sonnenschutz")
            {
                UserControlSonnenschutz userControlSonnenschutz = new UserControlSonnenschutz(dt);
                gridAddUserCtrlAusstattungstyp.Children.Add(userControlSonnenschutz);
            }
            else if (cbAusstattungstyp.SelectedValue.ToString() == "tor")
            {
                UserControlTor userControlTor = new UserControlTor(dt);
                gridAddUserCtrlAusstattungstyp.Children.Add(userControlTor);
            }
            else if (cbAusstattungstyp.SelectedValue.ToString() == "tuer")
            {
                UserControlTuer userControlTuer = new UserControlTuer(dt);
                gridAddUserCtrlAusstattungstyp.Children.Add(userControlTuer);
            }
        }
    }
}
