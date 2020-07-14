using Health.Domain.Abstract;
using Health.Domain.Entities;
using Health.WebUI.Controllers;
using Health.WebUI.Models.MedicalRecord;
using Health.WebUI.Models.PatientModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Health.UnitTests.PatientAppointmentsTests
{
    [TestClass]
    public class MedicalRecordTest
    {
        Mock<IUnitOfWork> mock;
        Mock<IGenericRepository<Doctor>> mock1;
        Mock<IGenericRepository<Appointment>> mockAppointmentRepository;
        Mock<IGenericRepository<Specialization>> mockUoWSpecialization;
        [TestInitialize]
        public void Initialize()
        {
            mock = new Mock<IUnitOfWork>();
            mock1 = new Mock<IGenericRepository<Doctor>>();
            mockAppointmentRepository = new Mock<IGenericRepository<Appointment>>();
            mockUoWSpecialization = new Mock<IGenericRepository<Specialization>>();
            mockUoWSpecialization.Setup(m => m.FindById(1)).Returns(new Specialization() { SpecializationId = 1, SpecializationTitle = "Психолог" });
            mockUoWSpecialization.Setup(m => m.FindById(2)).Returns(new Specialization() { SpecializationId = 2, SpecializationTitle = "Лор" });
            mockUoWSpecialization.Setup(m => m.FindById(3)).Returns(new Specialization() { SpecializationId = 3, SpecializationTitle = "Стоматолог" });
            mock1.Setup(m => m.FindById(1)).Returns(new Doctor()
            {
                Id = 1,
                Name = "Сергей",
                Surname = "Аксенов",
                Patronymic = "Валентинович",
                DoctorPhone = "32423432",
                SpecializationId = 1
            });
            mock1.Setup(m => m.FindById(2)).Returns(new Doctor()
            {
                Id = 2,
                Name = "Олег",
                Surname = "Валов",
                Patronymic = "Валентинович",
                DoctorPhone = "32423432",
                SpecializationId = 2
            });
            mock1.Setup(m => m.FindById(3)).Returns(new Doctor()
            {
                Id = 3,
                Name = "Ксения",
                Surname = "Римова",
                Patronymic = "Оксановна",
                DoctorPhone = "43242432",
                SpecializationId = 3
            });

            mock1.Setup(m => m.Get()).Returns(new List<Doctor>
               {
                 new Doctor(){Id=1,Name="Сергей",Surname="Аксенов",Patronymic="Валентинович",DoctorPhone="32423432",SpecializationId=1 },
                 new Doctor(){Id=2,Name="Олег",Surname="Валов",Patronymic="Валентинович",DoctorPhone="32423432",SpecializationId=2 },
                 new Doctor(){Id=3,Name="Светлана",Surname="Римова",Patronymic="Оксановна",DoctorPhone="43242432",SpecializationId=3 },

            });
            mockAppointmentRepository.Setup(m => m.Get()).Returns(new List<Appointment>
            {
                new Appointment(){AppointmentId=1,AppointmentComment="aaa",AppointmentDateTime=new DateTime(2020, 5, 20, 18, 30, 0),AppointmentPlace="11",DoctorId=2,PatientId=2},
                new Appointment(){AppointmentId=2,AppointmentComment="bbb",AppointmentDateTime=new DateTime(2020, 4, 20, 18, 30, 0),AppointmentPlace="22",DoctorId=1,PatientId=1},
                new Appointment(){AppointmentId=3,AppointmentComment="ccc",AppointmentDateTime=new DateTime(2020, 6, 20, 18, 30, 0),AppointmentPlace="33",DoctorId=1,PatientId=2},
                 new Appointment(){AppointmentId=4,AppointmentComment="ggg",AppointmentDateTime=new DateTime(2020, 5, 20, 14, 30, 0),AppointmentPlace="11",DoctorId=3,PatientId=1},
                new Appointment(){AppointmentId=5,AppointmentComment="rrr",AppointmentDateTime=new DateTime(2020, 4, 20, 12, 30, 0),AppointmentPlace="22",DoctorId=2,PatientId=2},
                new Appointment(){AppointmentId=6,AppointmentComment="yyy",AppointmentDateTime=new DateTime(2020, 6, 4, 11, 30, 0),AppointmentPlace="33",DoctorId=1,PatientId=2},


            }
            );


            mockUoWSpecialization.Setup(m => m.Get()).Returns(new List<Specialization>
            {
            new Specialization() {SpecializationId=1,SpecializationTitle="Психолог" },
            new Specialization() { SpecializationId = 2, SpecializationTitle = "Лор" },
            new Specialization() { SpecializationId = 3, SpecializationTitle = "Стоматолог" },

            });
            mock.Setup(m => m.Doctors).Returns(mock1.Object);
            mock.Setup(m => m.Specializations).Returns(mockUoWSpecialization.Object);
            mock.Setup(m => m.Appointments).Returns(mockAppointmentRepository.Object);
        }

        [TestMethod]
        public void CanGetAppointmentsBySpecialization()
        {
            // Организация (arrange)

            SpecializationRecordsListViewModel recordsListViewModel = new SpecializationRecordsListViewModel(mock.Object, 1, 2);
            // Действие (act)
            List<PatientAppointment> recordsList = recordsListViewModel.GetAppointmentsBySpecialization();
            // Утверждение (assert)
            Assert.AreEqual(recordsList.Count, 2);


        }
        [TestMethod]
        public void Index()
        {
            // Организация (arrange)
            MedicalRecordController medicalRecordController = new MedicalRecordController(mock.Object);

            // Действие (act)
            ActionResult result = medicalRecordController.Index() as ActionResult;


            // Assert
            Assert.IsNotNull(result);


        }
        [TestMethod]
        public void SearchSpecialization()
        {
            // Организация (arrange)
            MedicalRecordController medicalRecordController = new MedicalRecordController(mock.Object);
            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.Headers).Returns(
                new System.Net.WebHeaderCollection {
              {"X-Requested-With", "XMLHttpRequest"}
                });
            var context = new Mock<HttpContextBase>();
            context.SetupGet(x => x.Request).Returns(request.Object);
            medicalRecordController.ControllerContext = new ControllerContext(context.Object, new RouteData(), medicalRecordController);

            // Действие (act)
            ActionResult result = medicalRecordController.SearchSpecialization("") as ActionResult;

            // Assert
            Assert.IsNotNull(result);

        }

    }
}
