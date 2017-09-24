using RtlsEngine.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vsb.UrgentApp.Common.Helpers;
using Vsb.UrgentApp.Common.Mapper;
using Vsb.UrgentApp.Common.Mappers;
using Vsb.UrgentApp.Infrastructure.Db;
using Vsb.UrgentApp.Tasks.Tag;

namespace Vsb.UrgentApp.Tasks.Patient
{
	public class PatientTasks : IPatientTasks
	{
		private readonly ITagTasks tagTasks;
		private readonly IBaseMapper baseMapper;

		public PatientTasks(
			ITagTasks tagTasks,
			IBaseMapper baseMapper
		)
		{
			this.tagTasks = Requires.IsNotNull(tagTasks, nameof(tagTasks));
			this.baseMapper = Requires.IsNotNull(baseMapper, nameof(baseMapper));
		}

		public void Create(PatientDto patient)
		{
			//MyDataSet ds = new MyDataSet();
			//ds.EnforceConstraints = false;

			//var tagCheck = tagTasks.GetById(patient.TagId);

			//if (tagCheck == null)
			//{
			//	throw new ApplicationException();
			//}

			//string query = string.Format(
			//	UrgentAppQueries.TAGREGISTRATION_INSERT,
			//	tagRegistration.Id,
			//	tagRegistration.Tag_Id,
			//	tagRegistration.Patient_Id
			//	);

			//DbTools.ExecuteQuery(
			//	FireBirdConnection.Connection, query);
		}

		public List<PatientDto> GetAll()
		{
			MyDataSet ds = new MyDataSet();
			ds.EnforceConstraints = false;


			DbTools.SelectQuery(
				FireBirdConnection.Connection,
				ds.Patient, UrgentAppQueries.PATIENT_SELECT);

			List<PatientDto> lst = baseMapper.BindDataList<PatientDto>(ds.Patient);

			return lst;
		}
	}
}
