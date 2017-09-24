using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Vsb.UrgentApp.Common.Helpers;
using Vsb.UrgentApp.Tasks.Patient;

namespace Vsb.UrgentApp.API.Controllers
{
    public class PatientController : ApiController
    {
		private readonly IPatientTasks patientTasks;

		public PatientController(
			IPatientTasks patientTasks
		)
		{
			this.patientTasks = Requires.IsNotNull(patientTasks, nameof(patientTasks));
		}

        [HttpGet]
        public List<PatientDto> Get()
        {
            List<PatientDto> result = new List<PatientDto>();

			result = patientTasks.GetAll();

			return result;
        }

        [HttpPost]
        public HttpResponseMessage Post(PatientDto patient)
        {
			if (patient == null)
			{
				throw new ArgumentNullException(nameof(patient));
			}

			patientTasks.Create(patient);

			return Request.CreateResponse(HttpStatusCode.Created);
		}

        [HttpPut]
        public HttpResponseMessage Put(PatientDto patient)
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpDelete]
        public HttpResponseMessage Delete(int patientId)
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}