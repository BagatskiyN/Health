using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Health.Domain.Abstract;

using System.Web.Mvc;
using Health.Domain.Entities;
using Health.Domain.Concrete;
using Health.WebUI.Controllers;
using Health.WebUI.Models.PatientAppointmentModels;

namespace Health.UnitTests.PatientAppointmentsTests
{
    [TestClass]
    public class PatientAppointmentsTest
    {

        Mock<IUnitOfWork> mock;
        Mock<IGenericRepository<Doctor>> mock1;
        Mock<IGenericRepository<Specialization>> mockUoWSpecialization;
        [TestInitialize]
        public void Initialize()
        {
            mock = new Mock<IUnitOfWork>();
            mock1 = new Mock<IGenericRepository<Doctor>>();
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
                SpecializationId = 3
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
                SpecializationId = 1
            });
            mock1.Setup(m => m.Get()).Returns(new List<Doctor>
               {
                 new Doctor(){Id=1,Name="Сергей",Surname="Аксенов",Patronymic="Валентинович",DoctorPhone="32423432",SpecializationId=3 },
                 new Doctor(){Id=2,Name="Олег",Surname="Валов",Patronymic="Валентинович",DoctorPhone="32423432",SpecializationId=2 },
                 new Doctor(){Id=3,Name="Светлана",Surname="Римова",Patronymic="Оксановна",DoctorPhone="43242432",SpecializationId=1 }
            });
            mockUoWSpecialization.Setup(m => m.Get()).Returns(new List<Specialization>
            {
            new Specialization() {SpecializationId=1,SpecializationTitle="Психолог" },
            new Specialization() { SpecializationId = 2, SpecializationTitle = "Лор" },
            new Specialization() { SpecializationId = 3, SpecializationTitle = "Стоматолог" }
            });
            mock.Setup(m => m.Doctors).Returns(mock1.Object);
            mock.Setup(m => m.Specializations).Returns(mockUoWSpecialization.Object);

        }
        [TestMethod]
        public void CanSearchDoctords()
        {

            // Организация (arrange)

            PatientAppointmentController controller = new PatientAppointmentController(mock.Object);

            // Действие (act)
            List<PatientAppointmentDoctor> patientAppointmentDoctors = controller.DoctorListViewModel.GetPatientAppointmentDoctorsSearch("С", "Психолог");

            // Утверждение (assert)

            Assert.AreEqual(patientAppointmentDoctors.Count, 1);

        }




    }
}
