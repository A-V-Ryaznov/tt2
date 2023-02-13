using NewHospitalWPF.Entities;
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

namespace NewHospitalWPF
{
    /// <summary>
    /// Логика взаимодействия для WndFreeTime.xaml
    /// </summary>
    public partial class WndFreeTime : Window
    {
        public WndFreeTime()
        {
            InitializeComponent();
        }
        private Entities.Doctor selectedDoctor;

        public WndFreeTime(Entities.Doctor selectedDoctor)
        {
            InitializeComponent();
            this.selectedDoctor = selectedDoctor;

            TbDoctor.Text = selectedDoctor.FullName;
            TbSpecialization.Text = selectedDoctor.Specialization.Name;

            GenerateSchedule(selectedDoctor);

            
        }

        private void GenerateSchedule(Doctor selectedDoctor)
        {
            var startDate = DateTime.Now.Date;
            startDate = new DateTime(2023, 01, 31).Date;
            var endDate = startDate.AddDays(5);

            var headers = new Utils.SheduleGenerator(startDate
                , endDate , selectedDoctor.DoctorSchedules.ToList()).GenerateHeaders();
            var schedules = new Utils.SheduleGenerator(startDate, endDate, selectedDoctor.DoctorSchedules.ToList()).GenerateAppointments(headers);
            LoadShedule(headers, schedules);

        }

        private void LoadShedule(List<ScheduleHeader> headers, List<List<ScheduleAppointment>> schedules)
        {
            TbTime.Text = "";
            for(int i = 0; i<headers.Count(); i++)
            {
                String result = $"{headers[i].Date.ToString("ddd (dd.MM.yyyy): \n")}";
                TimeSpan start = default;
                TimeSpan end = default;

                for(int j = 0; j<schedules.Count(); j++)
                {
                    if(schedules[j][i].AppointmentType != Entities.AppointmentType.Free)
                    {
                        if(start != default)
                        {
                            String startTime = start.ToString(@"hh\:mm");
                            String endTime = start.ToString(@"hh\:mm");

                            result += $"\t {startTime} - {endTime} \n";

                            start = default;
                            end = default;
                        }
                        continue;
                    }
                    else
                    {
                        if(start == default)
                        {
                            start = schedules[j][i].StartTime;
                            end = schedules[j][i].EndTime;
                        }
                        else
                        {
                            end = schedules[j][i].EndTime;
                        }
                    }
                }
                
                if(start != default)
                {
                    TbTime.Text = TbTime.Text + result;
                }

                if(TbTime.Text == "")
                {
                    TbTime.Text = "Свободного времени для записи нет. Врач занят";
                }
            }
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            printDialog.PageRangeSelection = PageRangeSelection.UserPages;
            printDialog.PrintVisual(StackPanel, "");
        }
    }
}
