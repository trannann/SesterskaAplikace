using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Vsb.UrgentApp.Common.Helpers;
using Vsb.UrgentApp.Tasks.Tag;
using static RtlsEngine.DB.MyDataSet;

namespace Vsb.UrgentApp.API.Controllers
{
    public class TagController : ApiController
    {
		private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		private readonly ITagTasks tagTasks;

		/// <summary>
		/// Initializes a new instance of the <see cref="TagController"/> class.
		/// </summary>
		/// <param name="tagTasks">The tag tasks.</param>
		public TagController(
			ITagTasks tagTasks
		)
		{
			this.tagTasks = Requires.IsNotNull(tagTasks, nameof(tagTasks));
		}

		/// <summary>
		/// Gets this instance.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
        public List<TagDto> Get()
        {
			List<TagDto> result = tagTasks.GetAll();

            return result;
        }
    }
}