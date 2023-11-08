using Generator_Jmen_05_11_2023_v1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NameGenerator_cs_se_no
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Manager m;
        public MainWindow()
        {
            InitializeComponent();
            m = new Manager();
            DataContext = m;
            //RepairButton.IsEnabled = false;
            GenerateButton.IsEnabled = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e) //GENERATE button
        {
            string gender;
            if (RBMale.IsChecked == true) gender = "Male";
            else gender = "Female";
            TBoutput.Text = m.GetRandomName(gender);
            SetOptions(false);
        }
        private void SetOptions(bool state)
        {
            CBcs.IsEnabled = state;
            CBse.IsEnabled = state;
            CBno.IsEnabled = state;
            RBMale.IsEnabled = state;
            RBFemale.IsEnabled = state;
            LoadButton.IsEnabled = state;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) //REPAIR button
        {
            //m.RepairCSV("seFemaleFirst", "seFemaleFirst");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) //LOAD button
        {
            SetOptions(true);
            m.ClearLists();

            string gender;
            List<string> countries = new List<string>();
            if (RBMale.IsChecked == true) gender = "Male";
            else gender = "Female";
            if (CBcs.IsChecked == true) countries.Add("cs");
            if (CBse.IsChecked == true) countries.Add("se");
            if (CBno.IsChecked == true) countries.Add("no");
            if (CBcs.IsChecked == false && CBse.IsChecked == false && CBno.IsChecked == false)
            {
                countries.Add("cs");
                countries.Add("se");
                countries.Add("no");
            }
            m.LoadCSV(gender, countries);
            m.CheckValidLists();
            GenerateButton.IsEnabled = true;
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            m.ClearLists();
            SetOptions(true);
        }
    }
}
