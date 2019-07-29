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
        string ist;
        string max;
        string bezeichnug;
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
        public string IST
        {
            set
            {
                ist = value;
            }
            get
            {
                return ist;
            }
        }
        public string MAX
        {
            set
            {
                max = value;
            }
            get
            {
                return max;
            }
        }
        public string Bezeichnug
        {
            set
            {
                bezeichnug = value;
            }
            get
            {
                return bezeichnug;
            }
        }
        public Nutzung()
        {
            InitializeComponent();
            
            cbflacheist.Items.Clear();
            cbflacheist.Items.Add("NUF 1 (Wohnen und Aufenthalt)");
            cbflacheist.Items.Add("NUF 2 (Buroarbit)");
            cbflacheist.Items.Add("NUF 3 (Produktion, Experimente, Arbeit)");
            cbflacheist.Items.Add("NUF 4 (Lagern, Verteilen und Verkaufen)");
            cbflacheist.Items.Add("NUF 5 (Bildung, Unterricht und kultur)");
            cbflacheist.Items.Add("NUF 6 (Hilen und Pflegen)");
            cbflacheist.Items.Add("NUF 7 (Sonstige Nutzflachen)");
            cbflacheist.Items.Add("TF (Technikflache)");
            cbflacheist.Items.Add("VF (Verkehrflache)");
            cbflachemax.Items.Clear();
            cbflachemax.Items.Add("NUF 1 (Wohnen und Aufenthalt)");
            cbflachemax.Items.Add("NUF 2 (Buroarbit)");
            cbflachemax.Items.Add("NUF 3 (Produktion, Experimente, Arbeit)");
            cbflachemax.Items.Add("NUF 4 (Lagern, Verteilen und Verkaufen)");
            cbflachemax.Items.Add("NUF 5 (Bildung, Unterricht und kultur)");
            cbflachemax.Items.Add("NUF 6 (Hilen und Pflegen)");
            cbflachemax.Items.Add("NUF 7 (Sonstige Nutzflachen)");
            cbflachemax.Items.Add("TF (Technikflache)");
            cbflachemax.Items.Add("VF (Verkehrflache)");

        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            UpdateGetSet = true;
            NutzungResult = "IST: " + cbnutzuist.SelectedItem + "\n" + "MAX: " + cbntzumax.SelectedItem + "\n" + "Beziechnung: " + txtBezeichnug.Text;
            IST = cbnutzuist.SelectedItem.ToString();
            MAX = cbntzumax.SelectedItem.ToString();
            Bezeichnug = txtBezeichnug.Text;
            this.Close();
        }

        private void Cbflacheist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbflacheist.SelectedItem.ToString().StartsWith("NUF"))
            {
                string query = "select id,value from DIN277_NUF"+ cbflacheist.SelectedItem.ToString().Substring(4, 1);
                DataTable dtResult = DataConnection.GetData(query);
                cbnutzuist.Items.Clear();
                for (int i = 0; i < dtResult.Rows.Count; i++)
                {
                    cbnutzuist.Items.Add(dtResult.Rows[i][0].ToString() + " " + dtResult.Rows[i][1].ToString());
                }
            }
            else if (cbflacheist.SelectedItem.ToString().StartsWith("TF"))
            {
                string query = "select id,value from DIN277_TF";
                DataTable dtResult = DataConnection.GetData(query);
                cbnutzuist.Items.Clear();
                for (int i = 0; i < dtResult.Rows.Count; i++)
                {
                    cbnutzuist.Items.Add(dtResult.Rows[i][0].ToString() + " " + dtResult.Rows[i][1].ToString());
                }
            }
            else if (cbflacheist.SelectedItem.ToString().StartsWith("VF"))
            {
                string query = "select id,value from DIN277_VF";
                DataTable dtResult = DataConnection.GetData(query);
                cbnutzuist.Items.Clear();
                for (int i = 0; i < dtResult.Rows.Count; i++)
                {
                    cbnutzuist.Items.Add(dtResult.Rows[i][0].ToString() + " " + dtResult.Rows[i][1].ToString());
                }
            }
        }

        private void Cbflachemax_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbflachemax.SelectedItem.ToString().StartsWith("NUF"))
            {
                string query = "select id,value from DIN277_NUF" + cbflachemax.SelectedItem.ToString().Substring(4, 1);
                DataTable dtResult = DataConnection.GetData(query);
                cbntzumax.Items.Clear();
                for (int i = 0; i < dtResult.Rows.Count; i++)
                {
                    cbntzumax.Items.Add(dtResult.Rows[i][0].ToString() + " " + dtResult.Rows[i][1].ToString());
                }
            }
            else if (cbflachemax.SelectedItem.ToString().StartsWith("TF"))
            {
                string query = "select id,value from DIN277_TF";
                DataTable dtResult = DataConnection.GetData(query);
                cbntzumax.Items.Clear();
                for (int i = 0; i < dtResult.Rows.Count; i++)
                {
                    cbntzumax.Items.Add(dtResult.Rows[i][0].ToString() + " " + dtResult.Rows[i][1].ToString());
                }
            }
            else if (cbflachemax.SelectedItem.ToString().StartsWith("VF"))
            {
                string query = "select id,value from DIN277_VF";
                DataTable dtResult = DataConnection.GetData(query);
                cbntzumax.Items.Clear();
                for (int i = 0; i < dtResult.Rows.Count; i++)
                {
                    cbntzumax.Items.Add(dtResult.Rows[i][0].ToString() + " " + dtResult.Rows[i][1].ToString());
                }
            }
        }
    }
}
