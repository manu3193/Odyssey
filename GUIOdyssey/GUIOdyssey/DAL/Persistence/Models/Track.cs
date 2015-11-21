

namespace GUIOdyssey.DAL.Persistence.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Track
    {

        public Track()
        {
            UserTracks = new List<UserTrack>();
        }

        [Key()]
        [Column(Order = 1)]
        public Guid TrackID { get; set; }


        [ForeignKey("Album")]
        [Column(Order = 2)]
        public Guid AlbumID { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Column(TypeName = "text")]
        public string Lyrics { get; set; }

        [Required]
        [StringLength(100)]
        public string Path { get; set; }

        [Required]
        [StringLength(25)]
        public string Genre { get; set; }

        public virtual Album Album { get; set; }

        public virtual List<UserTrack> UserTracks { get; set; }

    }

}

