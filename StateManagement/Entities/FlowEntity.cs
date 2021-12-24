using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StateManagement.Entities
{
    [Table("Flow")]
    public class FlowEntity: BaseEntity
    {
        [Column("Name")]
        public string Name { get; set; }
    }
}
