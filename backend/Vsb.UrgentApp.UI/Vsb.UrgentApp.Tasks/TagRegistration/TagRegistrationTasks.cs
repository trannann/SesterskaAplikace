using RtlsEngine.DB;
using System;
using System.Collections.Generic;
using Vsb.UrgentApp.Common.Helpers;
using Vsb.UrgentApp.Common.Mappers;
using Vsb.UrgentApp.Infrastructure.Db;
using Vsb.UrgentApp.Tasks.Tag;

namespace Vsb.UrgentApp.Tasks.TagRegistration
{
	public class TagRegistrationTasks : ITagRegistrationTasks
    {
        private readonly ITagTasks tagTasks;
		private readonly IBaseMapper baseMapper;

		public TagRegistrationTasks(
			ITagTasks tagTasks,
			IBaseMapper baseMapper
		)
        {
			this.tagTasks = Requires.IsNotNull(tagTasks, nameof(tagTasks));
			this.baseMapper = Requires.IsNotNull(baseMapper, nameof(baseMapper));
		}

        public List<TagRegistrationDto> GetAll()
        {
            MyDataSet ds = new MyDataSet();
            ds.EnforceConstraints = false;

            DbTools.SelectQuery(
                FireBirdConnection.Connection,
                ds.TagRegistration, UrgentAppQueries.TAGREGISTRATION_SELECT);

			List<TagRegistrationDto> lst = baseMapper.BindDataList<TagRegistrationDto>(ds.TagRegistration);

			return lst;
		}

        public void Create(TagRegistrationDto tagRegistration)
        {
            MyDataSet ds = new MyDataSet();
            ds.EnforceConstraints = false;

            CheckForPKViolations(tagRegistration);

            string query = string.Format(
                UrgentAppQueries.TAGREGISTRATION_INSERT,
                tagRegistration.Id,
                tagRegistration.Tag_Id,
                tagRegistration.Patient_Id
                );

            DbTools.ExecuteQuery(
                FireBirdConnection.Connection, query);
        }

        public void Delete(int patientId)
        {
            var check = GetByPatientId(patientId);

            if (check == null)
            {
                throw new ApplicationException();
            }

            MyDataSet ds = new MyDataSet();
            ds.EnforceConstraints = false;

            DbTools.ExecuteQuery(
                FireBirdConnection.Connection, 
                string.Format(UrgentAppQueries.TAGREGISTRATION_DELETE_BYPATIENTID, patientId));
        }
        
        public TagRegistrationDto GetByTagId(int Id)
        {
			TagRegistrationDto result = new TagRegistrationDto();

			MyDataSet ds = new MyDataSet();
            ds.EnforceConstraints = false;

            DbTools.SelectQuery(
                FireBirdConnection.Connection,
                ds.TagRegistration, string.Format(UrgentAppQueries.TAGREGISTRATION_SELECT_BYTAGID, Id));

			result = baseMapper.BindData<TagRegistrationDto>(ds.TagRegistration);

			return result;
		}

        #region PRIVATE METHODS

        private TagRegistrationDto GetByPatientId(int patientId)
        {
			TagRegistrationDto result = new TagRegistrationDto();

			MyDataSet ds = new MyDataSet();
            ds.EnforceConstraints = false;

            DbTools.SelectQuery(
                FireBirdConnection.Connection,
                ds.TagRegistration, string.Format(UrgentAppQueries.TAGREGISTRATION_SELECT_BYPATIENTID, patientId));

			result = baseMapper.BindData<TagRegistrationDto>(ds.TagRegistration);

			return result;
		}

        private void CheckForPKViolations(TagRegistrationDto tagRegistration)
        {
            var check = GetByTagId(tagRegistration.Tag_Id);

            if (check != null)
            {
                throw new ApplicationException();
            }

            var tagCheck = tagTasks.GetById(tagRegistration.Tag_Id);

            if (tagCheck == null)
            {
                throw new ApplicationException();
            }
        }

        #endregion // PRIVATE METHODS
    }
}
