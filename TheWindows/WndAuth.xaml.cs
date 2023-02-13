using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NewHospitalWPF.TheWindows
{
    /// <summary>
    /// Логика взаимодействия для WndAuth.xaml
    /// </summary>
    public partial class WndAuth : Window
    {
        public WndAuth()
        {
            InitializeComponent();
        }

        private void BtnSign_Click(object sender, RoutedEventArgs e)
        {
            if (TheClasses.Authorization.SignIn(tbLogin.Text, pbPassword.Password))
            {
                Close();
            }
            else
            {
                pbPassword.Clear();
            }
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void VisiblePass_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            tbPassword.Visibility = Visibility.Collapsed;
            pbPassword.Visibility = Visibility.Visible;

        }

        private void VisiblePass_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tbPassword.Text = pbPassword.Password;
            pbPassword.Visibility = Visibility.Collapsed;
            tbPassword.Visibility = Visibility.Visible;
        }
    }
}
