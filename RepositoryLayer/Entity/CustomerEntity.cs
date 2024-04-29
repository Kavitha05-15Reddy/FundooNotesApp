using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Entity
{
    public class CustomerEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
