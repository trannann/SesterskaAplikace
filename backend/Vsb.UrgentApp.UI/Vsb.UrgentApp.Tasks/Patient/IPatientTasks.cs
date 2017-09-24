using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsb.UrgentApp.Tasks.Patient
{
	public interface IPatientTasks
	{
		List<PatientDto> GetAll();

		void Create(PatientDto patient);
	}
}
