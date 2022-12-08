using System;
using System.Collections.Generic;

#nullable disable

namespace Sprout.Exam.WebApp.Models
{
    public partial class EmployeeModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Birthdate { get; set; }
        public string Tin { get; set; }
        public int TypeId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
