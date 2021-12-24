using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StateManagement.Entities
{
    [Table("State")]
    public class StateEntity: BaseEntity
    {
        [Column("FlowId")]
        public int FlowId { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("Sequence")]
        public int Sequence { get; set; }
    }
}
