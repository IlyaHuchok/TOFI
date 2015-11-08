using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class RequestStatusDB
    {
        public int RequestStatusId { get; set; }
        public string Status { get; set; }

        public virtual ICollection<RequestDB> Requests { get; set; }
    }
}
