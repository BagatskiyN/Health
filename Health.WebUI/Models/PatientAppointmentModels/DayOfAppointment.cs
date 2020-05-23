using Health.Domain.Entities;
using Health.WebUI.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Health.WebUI.Models.PatientAppointmentModels
{
    public class DayOfAppointment
    {
        UnitOfWork unitOfWork;
        public int Number { get; set; }
        public DateTime DateOfAppointment { get;set; }
        public string DayTitle { get; set; }

        public List<DateTime> Times { get; set; }

        public DayOfAppointment(int number,int doctorId)
        {
            Number = number;
            unitOfWork = new UnitOfWork();
            DayTitle = (DateTime.Now).AddDays(number).DayOfWeek.ToString();
            DateOfAppointment = DateTime.Today.AddDays(number);
            Times = new List<DateTime>();
            Times = FullInDateTimes(doctorId);


        }
        public List<DateTime> GetFreeDateTimes(int doctorId) 
        {
            List<DateTime> appointmentsTime = unitOfWork.Appointments.Get()
                .Where(p => p.AppointmentDateTime.Date == DateOfAppointment.Date&& p.DoctorId==doctorId)
                .Select(x=>x.AppointmentDateTime)
                .ToList();
            return appointmentsTime;
        }
        public List<DateTime> FullInDateTimes(int doctorId)
        { DateTime date =DateOfAppointment;
          date= date.AddHours(10);
                for (int i = 0; i < 16; i++)
                {
                    Times.Add(date);
                date = date.AddMinutes(30);
                 }
            
              
             return Times.Except(GetFreeDateTimes(doctorId)).ToList();
            }
           
        
        
        }

    }
