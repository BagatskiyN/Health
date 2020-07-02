﻿using Health.Domain.Entities;
using Health.WebUI.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Health.Domain.Concrete
{
    public class AppointmentManipulator : IWorkWithAppointments
    {
        public List<Appointment> SkipUsedAppointments(List<Appointment> appointments,int page,int pageSize)
        {

            var skipRecords = page * pageSize;
            if (appointments.Count() - skipRecords >= 0)
            {
                if (appointments.Count() - skipRecords < pageSize)
                {
                    return appointments.OrderByDescending(t => t.AppointmentDateTime)
                           .Skip(skipRecords).Take(appointments.Count() - skipRecords)
                           .ToList();
                }
                else
                {
                    return appointments.OrderByDescending(t => t.AppointmentDateTime)
                      .Skip(skipRecords).Take(pageSize).ToList();
                }
            }
            else
            {
                return new List<Appointment> { };
            }
        }
        public List<Appointment> SkipUsedNextAppointments(List<Appointment> appointments, int page,int nextPageSize)
        {
            var skipRecords = page * nextPageSize;
            if (appointments.Count() - skipRecords >= 0)
            {
                if (appointments.Count() - skipRecords < 4)
                {
                    return appointments.OrderBy(t => t.AppointmentDateTime)
                           .Skip(skipRecords).Take(appointments.Count() - skipRecords)
                           .ToList();
                }
                else
                {
                    return appointments.OrderBy(t => t.AppointmentDateTime)
                      .Skip(skipRecords).Take(nextPageSize).ToList();
                }
            }
            else
            {
                return new List<Appointment> { };
            }
        }
    }
}
