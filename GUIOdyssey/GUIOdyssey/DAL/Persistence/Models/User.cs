namespace GUIOdyssey.DAL.Persistence.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class User
    {

        public User()
        { 
            //UserTracks = new List<UserTrack>();
        }

        [Key()]
        public Guid UserID { get; set; }

        [Required]
        [StringLength(50)]
        public string Nickname { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public virtual List<UserTrack> UserTracks  { get; set; }

    }
}
