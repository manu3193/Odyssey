using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUIOdyssey.DAL.Persistence.Models
{
    public class UserTrack
    {

        [Key]
        public Guid TrackID { get; set; }

        [Key]
        public Guid UserID { get; set; }

        [Required]
        public bool IsSync { get; set; }

    }
}
