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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NewHospitalWPF.UserControls
{
    /// <summary>
    /// Логика взаимодействия для SheduleHeaderControl.xaml
    /// </summary>
    public partial class SheduleHeaderControl : UserControl
    {
        public SheduleHeaderControl()
        {
            InitializeComponent();
        }

        public void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is Entities.ScheduleHeader currentHeader)
            {
                currentHeader.Date.ToString("dd MMMM YYYY");
                BlockDay.Text = currentHeader.Date.ToString("ddd");
                BlockDate.Text = currentHeader.Date.ToString("dd MMMM yyyy");
            }
        }
    }
}
