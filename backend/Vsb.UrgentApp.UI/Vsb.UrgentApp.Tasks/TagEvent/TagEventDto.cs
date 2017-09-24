using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RtlsEngine.DB.MyDataSet;

namespace Vsb.UrgentApp.Tasks.TagEvent
{
    public class TagEventDto
    {
        public int Id { get; set; }

        public int Tag_Id { get; set; }

        public int TagEventType_Id { get; set; }

        public DateTime Created { get; set; }
        
        public int Patient_Id { get; set; }
    }
}
