using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsb.UrgentApp.Tasks.TagEvent
{
    public interface ITagEventTask
    {
        List<TagEventDto> GetAll();

        void Create(TagEventDto tagEvent);
    }
}
