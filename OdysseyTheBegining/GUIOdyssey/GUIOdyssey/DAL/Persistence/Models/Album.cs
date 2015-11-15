namespace GUIOdyssey.DAL.Persistence.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Album
    {
        public Album()
        {
            Tracks = new List<Track>();
        }

        [Key()]
        public Guid AlbumID { get; set; }

        [Required]
        public Guid ArtistID { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        public Guid? MusicBrainzID { get; set; }

        public string ReleaseYear { get; set; }

        public virtual Artist Artist { get; set; }

        public virtual List<Track> Tracks { get; set; }
    }
}
