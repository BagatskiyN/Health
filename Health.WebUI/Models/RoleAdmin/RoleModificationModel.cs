using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Health.WebUI.Models.RoleAdmin
{
    public class RoleModificationModel
    {
        [Required]
        public string RoleName { get; set; }
        public int[] IdsToAdd { get; set; }
        public int[] IdsToDelete { get; set; }
    }
}