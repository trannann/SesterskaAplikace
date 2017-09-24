
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vsb.UrgentApp.Common.Helpers;
using Vsb.UrgentApp.Common.Mappers;
using Vsb.UrgentApp.Tasks.TagEvent;

namespace Vsb.UrgentApp.API.Controllers
{
    public class TagEventController : ApiController
    {
        private readonly ITagEventTask tagEventTasks;
		private readonly IBaseMapper baseMapper;

		/// <summary>
		/// Initializes a new instance of the <see cref="TagEventController"/> class.
		/// </summary>
		public TagEventController(
			ITagEventTask tagEventTasks,
			IBaseMapper baseMapper
		)
        {
			this.tagEventTasks = Requires.IsNotNull(tagEventTasks, nameof(tagEventTasks));
			this.baseMapper = Requires.IsNotNull(baseMapper, nameof(baseMapper));
		}

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<TagEventDto> Get()
        {
            List<TagEventDto> result = tagEventTasks.GetAll();

            return result;
        }

        /// <summary>
        /// Posts the specified tag event.
        /// </summary>
        /// <param name="tagEvent">The tag event.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">
        /// tagEvent
        /// or
        /// TagEvent
        /// </exception>
        [HttpPost]
        public HttpResponseMessage Post(TagEventDto tagEvent)
        {
            if (tagEvent == null)
            {
                throw new ArgumentNullException(nameof(tagEvent));
            }

            tagEventTasks.Create(tagEvent);

            return Request.CreateResponse(HttpStatusCode.Created);
        }
    }
}