using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsb.UrgentApp.Tasks.Patient
{
    public class PatientDto
    {
        public int Id { get; set; }

        public string Card_Id { get; set; }

        public string SocialSecurityNumber { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public int Tag_Id { get; set; }

        public DateTime? Deleted { get; set; }
    }
}
