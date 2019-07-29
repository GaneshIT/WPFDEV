﻿using System;
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
    /// Interaction logic for UserControlFeuerloeschertyp.xaml
    /// </summary>
    public partial class UserControlFeuerloeschertyp : UserControl
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
        public UserControlFeuerloeschertyp()
        {
            InitializeComponent();
            string query = "select * from code_feuerloeschertyp";
            DataTable dt = DataConnection.GetData(query);
            cbFeuerloeschertyp.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cbFeuerloeschertyp.Items.Add(dt.Rows[i][1].ToString());
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            UpdateChildGetSet = true;
            SelectChildTypeValues = "feuerloescher " + cbFeuerloeschertyp.SelectedValue;
            //var myWindow = 
            Window.GetWindow(this).Close();
        }
    }
}