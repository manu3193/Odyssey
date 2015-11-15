namespace GUIOdyssey.DAL.Persistence.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Artist
    {
        
        public Artist()
        {
            Albums = new List<Album>();
        }

        [Key()]
        public Guid ArtistID { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        public Guid? MusicBrainzID { get; set; }

        public virtual List<Album> Albums { get; set; }
    }
}
