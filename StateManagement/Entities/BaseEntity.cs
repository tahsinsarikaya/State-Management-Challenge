using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StateManagement.Entities
{
    public class BaseEntity
    {
        [Column("Id"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Required]
        public int Id { get; set; }

        [Column("Created")]
        public DateTime Created { get; set; }

        [Column("Updated")]
        public DateTime Updated { get; set; }

        [Column("IsDeleted")]
        public bool IsDeleted { get; set; }
    }
}
