using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Vsb.UrgentApp.Common.Helpers;
using Vsb.UrgentApp.Common.Mappers;
using Vsb.UrgentApp.Tasks.TagRegistration;

namespace Vsb.UrgentApp.API.Controllers
{
    public class TagRegistrationController : ApiController
    {

        private readonly ITagRegistrationTasks tagRegistrationTasks;
		private readonly IBaseMapper baseMapper;

		public TagRegistrationController(
			ITagRegistrationTasks tagRegistrationTasks,
			IBaseMapper baseMapper
		)
		{
			this.tagRegistrationTasks = Requires.IsNotNull(tagRegistrationTasks, nameof(tagRegistrationTasks));
			this.baseMapper = Requires.IsNotNull(baseMapper, nameof(baseMapper));
		}

		/// <summary>
		/// Gets this instance.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
        public List<TagRegistrationDto> Get()
        {
            List<TagRegistrationDto> result = tagRegistrationTasks.GetAll();

            return result;
        }


        /// <summary>
        /// Posts the specified tag registration.
        /// </summary>
        /// <param name="tagRegistration">The tag registration.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">tagRegistration</exception>
        [HttpPost]
        public HttpResponseMessage Post(TagRegistrationDto tagRegistration)
        {
            if (tagRegistration == null)
            {
                throw new ArgumentNullException(nameof(tagRegistration));
            }

            tagRegistrationTasks.Create(tagRegistration);

            return Request.CreateResponse(HttpStatusCode.Created);
        }

        /// <summary>
        /// Deletes the specified patient identifier.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        
        [HttpDelete]
        public HttpResponseMessage Delete(int patientId)
        {


            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}