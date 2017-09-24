using RtlsEngine.DB;
using System.Collections.Generic;
using Vsb.UrgentApp.Common.Helpers;
using Vsb.UrgentApp.Common.Mappers;
using Vsb.UrgentApp.Infrastructure.Db;

namespace Vsb.UrgentApp.Tasks.Tag
{
	public class TagTasks : ITagTasks
    {
		private readonly IBaseMapper baseMapper;

		public TagTasks(
			IBaseMapper baseMapper)
        {		
			this.baseMapper = Requires.IsNotNull(baseMapper, nameof(baseMapper));
		}

        public List<TagDto> GetAll()
        {
            MyDataSet ds = new MyDataSet();
			ds.EnforceConstraints = false;


			DbTools.SelectQuery(
                FireBirdConnection.Connection,
                ds.Tag, Queries.TAG_SELECT);

			List<TagDto> lst = baseMapper.BindDataList<TagDto>(ds.Tag);

			return lst;
		}

		public TagDto GetById(int Id)
        {
			TagDto result = new TagDto();

			MyDataSet ds = new MyDataSet();
            ds.EnforceConstraints = false;

            DbTools.SelectQuery(
                FireBirdConnection.Connection,
                ds.Tag, string.Format(UrgentAppQueries.TAG_SELECT_BYID, Id));

			result = baseMapper.BindData<TagDto>(ds.Tag);

			return result;
        }
	}
}
