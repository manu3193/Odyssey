using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUIOdyssey.LogicLayer.ObjectModels
{
    /// <summary>
    /// Objeto que representa la metadata de una cancion para su manejo en la logica
    /// </summary>
    public class TrackInfo
    {
        public Guid TrackId { get; set; }

        public string Title { get; set; }
        public string ArtistTitle { get; set; }
        public string AlbumTitle { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
        public string Lyric { get; set; }
        public string SongPath { get; set; }
        public string songURI { get; set; }
        public bool isSynced { get; set; }

        public Guid userId { get; set; }

        public override string ToString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(TrackId);
            sb.Append("  ");
            sb.Append(Title);
            sb.Append("  ");
            sb.Append(ArtistTitle);
            sb.Append("  ");
            sb.Append(AlbumTitle);
            sb.Append("  ");
            sb.Append(Year);
            sb.Append("  ");
            sb.Append(Genre);
            sb.Append("  ");
            //sb.Append(Lyric);
            sb.Append("  ");
            sb.Append(SongPath);

            return sb.ToString();
        }
    }
}
