using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeePayRoll.Models
{
    public class EmployeeModel
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "First Name Field is Required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name Field is Required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Age Field is Required")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Salary Field is Required")]
        public int Salary { get; set; }

        [Required(ErrorMessage = "Department Field is Required")]
        public string Department { get; set; }

        public DateTime JoinedDate { get; set; }



    }
}
