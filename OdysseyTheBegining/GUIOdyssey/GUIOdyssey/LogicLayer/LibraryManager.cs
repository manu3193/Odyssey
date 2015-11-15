using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Id3;
using GUIOdyssey.DAL.Persistence.Models;
using GUIOdyssey.DAL.Persistence.Repositories;

namespace GUIOdyssey.LogicLayer
{
    class LibraryManager
    {

        /// <summary>
        /// Este metodo se encarga de buscar canciones en una carpeta para su importaci'on en la biblioteca de usuario
        /// </summary>
        /// <param name="directoryPath"></param>
        public void ImportSongsToLibrary(String directoryPath)
        {
            SessionManager sessionManager= SessionManager.Instance;
            string[] musicFiles = Directory.GetFiles(directoryPath, "*.mp3");
            foreach (string musicFile in musicFiles)
            {
                UserTrackRepository userTrackRepository = new UserTrackRepository();
            TrackRepository trackRepository = new TrackRepository();
                
                using (var mp3 = new Mp3File(musicFile))
                {
                    if (mp3.HasTags)
                    {
                        Id3Tag tag = mp3.GetTag(Id3TagFamily.FileStartTag);
                        string artistTitle = tag.Artists.Value;
                        Artist CurrentArtist = new Artist() {Title = artistTitle};

                        //Se obtiene el id del artista en caso de estar registrado en la base local, de otra forma devuelve null
                        var resultArtist = GetArtistByTitle(artistTitle);

                        if (resultArtist != null)
                        {
                            CurrentArtist = resultArtist;
                        }
                        else
                        {
                            CurrentArtist.ArtistID = Guid.NewGuid();
                        }
                        string albumtitle = tag.Album.Value;
                        Album CurrentAlbum = new Album()
                        {
                            Artist = CurrentArtist,
                            Title = albumtitle,
                            ReleaseYear = tag.Year.Value
                        };
                        var albumResult = GetAlbumByTitleAndArtistTitle(albumtitle, artistTitle);
                        if (albumResult != null)
                        {
                            CurrentAlbum = albumResult;
                        }
                        else
                        {
                            CurrentAlbum = new Album()
                            {
                                AlbumID = Guid.NewGuid(),
                                ArtistID = CurrentArtist.ArtistID,
                                Title = albumtitle,
                                ReleaseYear = tag.Year.Value
                            };
                        }
                        string genre = tag.Genre.Value;
                        if (genre == null)
                        {
                            genre = "Unknown";
                        }
                        string trackTitle = tag.Title.Value;
                        if (trackTitle == null)
                        {
                            trackTitle = musicFile;
                        }
                        string lyric = null;
                        if (tag.Lyrics.Count > 0)
                        {
                            lyric = tag.Lyrics[0].Lyrics;
                        }
                        Track currentTrack = new Track()
                        {
                            TrackID = Guid.NewGuid(),
                            Album = CurrentAlbum,
                            Title = trackTitle,
                            Genre = genre,
                            Lyrics = lyric,
                            Path = musicFile
                        };
                        trackRepository.Add(currentTrack);
                        userTrackRepository.Add(new UserTrack()
                        {
                            UserID = sessionManager.UserId,
                            TrackID = currentTrack.TrackID,
                            IsSync = false
                        });
                    }
                    else
                    {
                        
                    }
                }
                trackRepository.SaveChanges();
                userTrackRepository.SaveChanges();
                userTrackRepository.Dispose();
                trackRepository.Dispose();

            }
            

        }

        private Artist GetArtistByTitle(string artistTitle)
        {
            ArtistRepository repository = new ArtistRepository();
            List<Artist> artists = repository.GetArtistByTitle(artistTitle);
            Artist artist=null;
            if (artists.Count>0)
                    artist = artists.First();
            repository.Dispose();
            return artist;
            
        }

        private Album GetAlbumByTitleAndArtistTitle(string albumTitle, string artistTitle)
        {
            AlbumRepository repository = new AlbumRepository();
            List<Album> albums = repository.GetAlbumsByName(albumTitle);
            Album album=null;
            foreach (var a in albums)
            {
                if (a.Artist.Title.Equals(artistTitle))
                    album = a;
            }
            repository.Dispose();
            return album;
        }
    }
}
