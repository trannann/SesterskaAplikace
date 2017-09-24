using RtlsEngine.DB;
using System;
using System.Collections.Generic;
using Vsb.UrgentApp.Common.Helpers;
using Vsb.UrgentApp.Common.Mappers;
using Vsb.UrgentApp.Infrastructure.Db;
using Vsb.UrgentApp.Tasks.TagEventType;

namespace Vsb.UrgentApp.Tasks.TagEvent
{
	public class TagEventTasks : ITagEventTask
    {

		private readonly ITagEventTypeTasks tagEventTypeTasks;
		private readonly IBaseMapper baseMapper;

		public TagEventTasks(
			IBaseMapper baseMapper,
			ITagEventTypeTasks tagEventTypeTasks
		)
		{
			this.baseMapper = Requires.IsNotNull(baseMapper, nameof(baseMapper));
			this.tagEventTypeTasks = Requires.IsNotNull(tagEventTypeTasks, nameof(tagEventTypeTasks));
		}


		public void Create(TagEventDto tagEvent)
        {         
            MyDataSet ds = new MyDataSet();
            ds.EnforceConstraints = false;

            var type = tagEventTypeTasks.GetById(tagEvent.TagEventType_Id);

            if (type == null)
            {
                throw new ApplicationException();
            }

            string query = string.Format(
                Queries.TAGEVENT_INSERT_WITH_PATITIENT,
                tagEvent.Tag_Id,
                tagEvent.TagEventType_Id,
                tagEvent.Created,
                tagEvent.Patient_Id);

            DbTools.ExecuteQuery(
                FireBirdConnection.Connection, query);
        }

        public List<TagEventDto> GetAll()
        {     
            MyDataSet ds = new MyDataSet();
            ds.EnforceConstraints = false;

            DbTools.SelectQuery(
                FireBirdConnection.Connection,
                ds.TagEvent, Queries.TAGEVENT_SELECT);

			List<TagEventDto> lst = baseMapper.BindDataList<TagEventDto>(ds.TagEvent);

			return lst;
        }
    }
}
