using System;
using System.Collections;
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
       private static ArrayList selectFloorList = null;
        bool UpdateParentWindow;
        string selectLevel;
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
        public string SelectLevel
        {
            set
            {
                selectLevel = value;
            }
            get
            {
                return selectLevel;
            }
        }
        public AddNewLevel()
        {
            InitializeComponent();
            string query = "select geb_id from kl_gebaeude";
            DataTable dtResult = DataConnection.GetData(query);
            selectFloorList  = new ArrayList();
            selectFloorList.Add("UG4:00");
            selectFloorList.Add("UG3:01");
            selectFloorList.Add("UG2:02");
            selectFloorList.Add("UG1:03");
            selectFloorList.Add("EG:04");
            for (int i = 1; i <= 25; i++)
            {
                if ((4 + i)<=9)
                    selectFloorList.Add("OG" + i + ":0" + (4 + i) + "");
                else
                    selectFloorList.Add("OG" + i + ":" + (4 + i) + "");
            }
            selectFloorList.Add("UG4Z:30");
            selectFloorList.Add("UG3Z:31");
            selectFloorList.Add("UG2Z:32");
            selectFloorList.Add("UG1Z:33");
            selectFloorList.Add("EGZ:34");
            for (int i = 1; i <= 25; i++)
            {
                selectFloorList.Add("OG" + i + "Z:" + (34 + i) + "");
               
            }
            //for (int i = 0; i < dtResult.Rows.Count; i++)
            //{
            //    selectFloorList.Add(dtResult.Rows[i][0].ToString());
            //}
            cbSelectYesOrNo.Items.Clear();
            cbSelectYesOrNo.Items.Add("Yes");
            cbSelectYesOrNo.Items.Add("No");
        }

        private void BtnActivate_Click(object sender, RoutedEventArgs e)
        {
            UpdateParentWindow = true;
            SelectLevel = cbSelectFloor.SelectedItem.ToString();
            this.Close();
        }

        private void CbSelectYesOrNo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbSelectFloor.Items.Clear();
            if (cbSelectYesOrNo.SelectedValue.ToString() == "Yes")
            {
                for (int i = 0; i < selectFloorList.Count; i++)
                {
                    if (selectFloorList[i].ToString().Contains("Z"))
                        cbSelectFloor.Items.Add(selectFloorList[i].ToString());
                }
            }
            else
            {
                for (int i = 0; i < selectFloorList.Count; i++)
                {
                    if (!selectFloorList[i].ToString().Contains("Z"))
                        cbSelectFloor.Items.Add(selectFloorList[i].ToString());
                }
            }
        }
    }
}
