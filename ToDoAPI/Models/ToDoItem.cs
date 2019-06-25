using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "varchar(32)")]
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
