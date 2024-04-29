using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RepositoryLayer.Entity
{
    public class CollaboratorEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long C_Id { get; set; }
        public string C_Email { get; set; }

        [ForeignKey("User")]
        public long UserId { get; set; }
        [JsonIgnore]
        public virtual UserEntity User { get; set; }

        [ForeignKey("Notes")]
        public long NotesId { get; set; }
        [JsonIgnore]
        public virtual NotesEntity Notes { get; set; }
    }
}
