using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StateManagement.Entities
{
    [Table("Task")]
    public class TaskEntity: BaseEntity
    {
        [Column("FlowId")]
        public int FlowId { get; set; }

        [Column("StateId")]
        public int StateId { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("Description")]
        public string Description { get; set; }
    }
}
