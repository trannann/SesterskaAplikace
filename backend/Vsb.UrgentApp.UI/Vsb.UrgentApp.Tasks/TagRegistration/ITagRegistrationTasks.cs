using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsb.UrgentApp.Tasks.TagRegistration
{
    public interface ITagRegistrationTasks
    {
        List<TagRegistrationDto> GetAll();

        void Create(TagRegistrationDto tagRegistration);

        TagRegistrationDto GetByTagId(int Id);

        void Delete(int patientId);
    }
}
