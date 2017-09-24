using RtlsEngine.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vsb.UrgentApp.Common.Helpers;
using Vsb.UrgentApp.Common.Mappers;
using Vsb.UrgentApp.Infrastructure.Db;

namespace Vsb.UrgentApp.Tasks.TagEventType
{
    public class TagEventTypeTasks : ITagEventTypeTasks
    {
		private IBaseMapper baseMapper;

		public TagEventTypeTasks(
			IBaseMapper baseMapper
		)
		{
			this.baseMapper = Requires.IsNotNull(baseMapper, nameof(baseMapper));
		}


		public List<TagEventTypeDto> GetAll()
        {
            MyDataSet ds = new MyDataSet();
            ds.EnforceConstraints = false;

            DbTools.SelectQuery(
                FireBirdConnection.Connection,
                ds.TagEventType, Queries.TAGEVENTTYPE_SELECT);

			List<TagEventTypeDto> lst = baseMapper.BindDataList<TagEventTypeDto>(ds.TagEventType);

			return lst;
		}

        public TagEventTypeDto GetById(int Id)
        {
            MyDataSet ds = new MyDataSet();
            ds.EnforceConstraints = false;

            DbTools.SelectQuery(
                FireBirdConnection.Connection,
                ds.TagEventType, string.Format(UrgentAppQueries.TAGEVENTTYPE_SELECT_BYID, Id));

			TagEventTypeDto result = baseMapper.BindData<TagEventTypeDto>(ds.Patient);

			return result;
		}
    }
}
