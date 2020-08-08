using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Health.WebUI.Models.AdminDoctors
{
    public class EditDoctorViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }
        public string Email { get; set; }
        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public byte[] ImageData { get; set; }

        public string ImageMimeType { get; set; }

        public int? GenderId { get; set; }

        public string DoctorPhone { get; set; }

        public int SpecializationId { get; set; }

    }
}