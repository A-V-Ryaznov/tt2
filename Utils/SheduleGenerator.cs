using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewHospitalWPF.Utils
{
    class SheduleGenerator
    {
        private DateTime _startDate;
        private DateTime _endDate;
        private List<Entities.DoctorSchedule> _doctorSchedule;

        public SheduleGenerator(DateTime startDate, DateTime endDate, List<Entities.DoctorSchedule> schedule)
        {
            _startDate = startDate;
            _endDate = endDate;
            _doctorSchedule = schedule
                .Where(p => p.Date >= _startDate && p.Date <= _endDate.Date).ToList();
        }

        public List<Entities.ScheduleHeader> GenerateHeaders()
        {
            var result = new List<Entities.ScheduleHeader>();

            var startDate = _startDate;
            while (startDate.Date < _endDate.Date)
            {
                result.Add(new Entities.ScheduleHeader
                {
                    Date = startDate.Date
                });

                startDate = startDate.AddDays(1);
            }
            return result;
        }


        public List<List<Entities.ScheduleAppointment>> GenerateAppointments(List<Entities.ScheduleHeader> headers)
        {
            var result = new List<List<Entities.ScheduleAppointment>>();

            if (_doctorSchedule.Count() > 0)
            {
                var minStartTime = _doctorSchedule.Min(p => p.StartTime);
                var maxEndTime = _doctorSchedule.Max(p => p.EndTime);

                var startTime = minStartTime;
                while (startTime < maxEndTime)
                {
                    var appointmentsPerInterval = new List<Entities.ScheduleAppointment>();


                    foreach (var header in headers)
                    {
                        var currentShedule = _doctorSchedule.FirstOrDefault(p => p.Date == header.Date);

                        var scheduleAppointment = new Entities.ScheduleAppointment
                        {
                            ScheduleId = currentShedule?.Id ?? -1,
                            StartTime = startTime,
                            EndTime = startTime.Add(TimeSpan.FromMinutes(30))
                        };

                        if(currentShedule != null)
                        {
                            var busyAppointment = currentShedule.Appointments.FirstOrDefault(p => p.StartTime == startTime);

                            if(busyAppointment != null)
                            {
                                scheduleAppointment.AppointmentType = Entities.AppointmentType.Busy;
                            }
                            else
                            {
                                if(startTime<currentShedule.StartTime || startTime >= currentShedule.EndTime)
                                {
                                    scheduleAppointment.AppointmentType = Entities.AppointmentType.DayOff;
                                }
                                else
                                {
                                    scheduleAppointment.AppointmentType = Entities.AppointmentType.Free;

                                }
                            }
                        }
                        else
                        {
                            scheduleAppointment.AppointmentType = Entities.AppointmentType.DayOff;
                        }




                        appointmentsPerInterval.Add(scheduleAppointment);
                    }
                    result.Add(appointmentsPerInterval);
                    startTime = startTime.Add(TimeSpan.FromMinutes(30));
                }
            }




            return result;
        }
    }
}
