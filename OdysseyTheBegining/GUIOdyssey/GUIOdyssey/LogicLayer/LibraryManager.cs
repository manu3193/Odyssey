using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using Id3;
using GUIOdyssey.DAL.Persistence.Models;
using GUIOdyssey.DAL.Persistence.Repositories;
using GUIOdyssey.LogicLayer.ObjectModels;

namespace GUIOdyssey.LogicLayer
{
    /// <summary>
    /// Clase encargada de manejar  lo relacionado a la biblioteca de usuario
    /// </summary>
    class LibraryManager
    {

        public string[] genreList { get; set; }

        public List<TrackInfo> userTracks { get; set; }

        /// <summary>
        /// Metodo encargado de inicializar la biblioteca, carga los datos de las canciones desde la base de datos
        /// </summary>
        public void InitializeLibrary()
        {
            FileManager fileManager =new FileManager();
            fileManager.CreateUserDirectory();
            SessionManager sessionManager = SessionManager.Instance;
            TrackRepository trackRepo = new TrackRepository();
            UserTrackRepository userTrackRepo =new UserTrackRepository();
            List<Track> userStoredTracks= trackRepo.GetTraksByUserId(sessionManager.UserId);
            foreach (var userTrack in userStoredTracks)
            {
                TrackInfo trackInfo =new TrackInfo() {Title = userTrack.Title,TrackId = userTrack.TrackID,AlbumTitle = userTrack.Album.Title,
                                                      ArtistTitle = userTrack.Album.Artist.Title,SongPath = userTrack.Path,Year = userTrack.Album.ReleaseYear,
                                                      Lyric = userTrack.Lyrics,Genre = userTrack.Genre};
                trackInfo.isSynced = userTrackRepo.GetUserTrackByPK(sessionManager.UserId,trackInfo.TrackId).IsSync;
                this.userTracks.Add(trackInfo);
            }
            trackRepo.Dispose();
            userTrackRepo.Dispose();

        }
        /// <summary>
        /// Constructor por defecto, inicializa la lista de tracks
        /// </summary>
        public LibraryManager()
        {
            //userTracks = getUserTracks();
            userTracks= new List<TrackInfo>();



            this.genreList = new string[]{
                "Blues", "Classic Rock", "Country", "Dance", "Disco", "Funk", "Grunge", "Hip-Hop", "Jazz",

                "Metal", "New Age", "Oldies", "Other Genre", "Pop", "R&B", "Rap", "Reggae", "Rock", "Techno",

                "Industrial", "Alternative", "Ska", "Death Metal", "Pranks", "Soundtrack", "Euro-Techno",

                "Ambient", "Trip-Hop", "Vocal", "Jazz&Funk", "Fusion", "Trance", "Classical", "Instrumental",

                "Acid", "House", "Game", "Sound Clip", "Gospel", "Noise", "Alternativ Rock", "Bass", "Soul",

                "Punk", "Space", "Meditative", "Instrumental Pop", "Instrumental Rock", "Ethnic", "Gothic",

                "Darkwave", "Techno-Industrial", "Electronic", "Pop-Folk", "Eurodance", "Dream", "Southern Rock",

                "Comedy", "Cult", "Gangsta", "Top 40", "Christian Rap", "Pop/Funk", "Jungle", "Native US",

                "Carbaret", "New Wave", "Psychedelic", "Rave", "Showtunes", "Trailer", "Lo-Fi", "Tribal", "Acid Punk",

                "Acid Jazz", "Polka", "Retro", "Musical", "Rock & Roll", "Hard Rock", "Folk",

                "Folk-Rock", "National Folk", "Swing", "Fast Fusion", "Bebop", "Latin", "Revival", "Celtic",

                "Bluegrass", "Avantgarde", "Gothic Rock", "Progressive Rock", "Psychadelic Rock", "Symphonic Rock",

                "Slow Rock", "Big Band", "Chorus", "Easy Listening", "Acoustic", "Homour", "Speech", "Chanson",

                "Opera", "Chamber Music", "Sonata", "Symphony", "Booty Bass", "Primus", "Porn Groove",

                "Satire", "Slow Jam", "Club", "Tango", "Samba", "Folklore", "Ballad", "Power Ballad",

                "Rythmic Soul", "Freestyle", "Duet", "Punk Rock", "Drum Solo", "Acapella", "Euor-House",

                "Dance Hall", "Goa", "Drum & Bass", "Club-House", "Hardcore", "Terror", "Indie", "BritPop",

                "Negerpunk", "Polsk Punk", "Beat", "Christian Gangsta", "Heavy Metal", "Black Metal", "Crossover",

                "Contemporary", "Christian Rock", "Merengue", "Salsa", "Trash Metal", "Anime", "JPop", "SynthPop"};
        }

        /// <summary>
        /// Metodo encargado de buscarcanciones en una carpeta para su importaci'on en la biblioteca de usuario
        /// </summary>
        /// <param name="directoryPath">Ruta de acceso a directorio</param>
        public void ImportSongsToLibrary(String directoryPath)
        {
            List<TrackInfo> tracksToInsert = new List<TrackInfo>();
            SessionManager sessionManager= SessionManager.Instance;
            string unknownFrame = "Unknown";
            string[] musicFiles = Directory.GetFiles(directoryPath, "*.mp3");
            foreach (string musicFile in musicFiles)
            {
                UserTrackRepository userTrackRepository = new UserTrackRepository();
                TrackRepository trackRepository = new TrackRepository();

                using (var mp3 = new Mp3File(musicFile))
                {
                    TrackInfo currentTrack = new TrackInfo();

                    if (mp3.HasTags)
                    {
                        Id3Tag tag = mp3.GetTag(Id3TagFamily.FileStartTag);
                        if (tag != null)
                        {
                            currentTrack.Title = tag.Title.IsAssigned ? tag.Title.Value : unknownFrame;
                            currentTrack.AlbumTitle = tag.Album.IsAssigned ? tag.Album.Value : unknownFrame;
                            currentTrack.ArtistTitle = tag.Artists.IsAssigned ? tag.Artists : unknownFrame;
                            currentTrack.Genre = getGenre(tag.Genre.Value);
                            if (tag.Lyrics.Count != 0)
                            {
                                currentTrack.Lyric = tag.Lyrics[0].IsAssigned ? tag.Lyrics[0].Lyrics : "";
                            }
                            else
                            {
                                currentTrack.Lyric = "";
                            }
                            if (tag.Year.IsAssigned)
                            {
                                int year;
                                if (Int32.TryParse(tag.Year.Value, out year))
                                {
                                    currentTrack.Year = year;
                                }
                            }
                        }
                        else
                        {
                            currentTrack.Title = unknownFrame;
                            currentTrack.AlbumTitle =unknownFrame;
                            currentTrack.ArtistTitle = unknownFrame;
                            currentTrack.Genre = unknownFrame;
                            currentTrack.Lyric = "";
                            currentTrack.Year = 0;
                        }
                                     
                        currentTrack.SongPath = musicFile;
                        currentTrack.TrackId = Guid.NewGuid();
                        tracksToInsert.Add(currentTrack);
                    }
                    else
                    {
                        currentTrack.Title =unknownFrame;
                        currentTrack.AlbumTitle = unknownFrame;
                        currentTrack.ArtistTitle =unknownFrame;
                        currentTrack.Genre = unknownFrame;
                        currentTrack.Lyric = "";
                        currentTrack.SongPath = musicFile;
                        currentTrack.TrackId= Guid.NewGuid();
                        currentTrack.Year = 0;
                        tracksToInsert.Add(currentTrack);
                    }
                }
            }
            this.userTracks.AddRange(tracksToInsert);
            this.InsertIntoDatabase(tracksToInsert);
        }

        /// <summary>
        /// Metodo encargado de interactuar con la capa de datos para realizar las inserciones en la base de datos local
        /// </summary>
        /// <param name="tracksToInsert">Lista de tracks por ingresar</param>
        private void InsertIntoDatabase(List<TrackInfo> tracksToInsert)
         {
            ArtistRepository artistRepo = new ArtistRepository();
            AlbumRepository albumRepo =new AlbumRepository();
            TrackRepository trackRepo =new TrackRepository();
            UserTrackRepository usertracksRepo = new UserTrackRepository();
            foreach (TrackInfo trackInfo in tracksToInsert)
            {
                Artist trackArtist = GetArtistByTitle(trackInfo.ArtistTitle);
                if (trackArtist == null)
                {
                    //Creates new artist and insert into database
                    trackArtist = new Artist() {ArtistID = Guid.NewGuid(), Title = trackInfo.ArtistTitle};
                    artistRepo.Add(trackArtist);
                    artistRepo.SaveChanges();

                }
                else
                {
                    //artistRepo.Attach(trackArtist);
                }
                
                Album trackAlbum = GetAlbumByTitleAndArtistTitle(trackInfo.AlbumTitle,trackArtist.Title);
                if (trackAlbum == null)
                {
                    //Set trackAlbum as new Album
                    trackAlbum= new Album() {AlbumID = Guid.NewGuid(),ArtistID = trackArtist.ArtistID,Title = trackInfo.AlbumTitle, ReleaseYear = trackInfo.Year};
                    albumRepo.Add(trackAlbum);
                    albumRepo.SaveChanges();
                }
                else
                {
                    //albumRepo.Attach(trackAlbum);
                }
                //Creates new track
                Track newTrack=new Track() {AlbumID = trackAlbum.AlbumID,Title = trackInfo.Title, Genre =trackInfo.Genre,Lyrics = trackInfo.Lyric,Path = trackInfo.SongPath,TrackID = trackInfo.TrackId};
                usertracksRepo.Add(new UserTrack() {UserID = SessionManager.Instance.UserId,TrackID = trackRepo.Add(newTrack).TrackID,IsSync = false});
                //artistRepo.SaveChanges();
                
                
                
                trackRepo.SaveChanges();
                
            }
            usertracksRepo.SaveChanges();
            artistRepo.Dispose();
            trackRepo.Dispose();
            usertracksRepo.Dispose();

        }

        /// <summary>
        /// Obtiene el artista almacenado en la base de datos por su nombre
        /// </summary>
        /// <param name="artistTitle">Nombre de Artista</param>
        /// <returns>Retorna el artista si existe, sino retorna null</returns>
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

        /// <summary>
        /// Obtiene el album por su nombre de artista y nombre de album
        /// </summary>
        /// <param name="albumTitle">Nombre de Album</param>
        /// <param name="artistTitle">Nombre de Artista</param>
        /// <returns>Retorna el Album en caso de existir, de otra forma retorna null</returns>
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


        /// <summary>
        /// Obtiene el genero de una lista de generos preestablecidos
        /// </summary>
        /// <param name="genreString"></param>
        /// <returns>genero</returns>
        private string getGenre(string genreString)
        {
            string resultGenre="Unknown";
            foreach (var definedGenre in genreList)
            {
                if (definedGenre.Equals(genreString))
                {
                    resultGenre = definedGenre;
                }
            }
            return resultGenre;

        }

        public async void SyncUserLibrary()
        {
            FileManager fileManager = new FileManager();
            OdysseyCloudAPIConsumer ApiConsumer = new OdysseyCloudAPIConsumer();

            foreach (var trackInfo in userTracks)
            {
                if (!trackInfo.isSynced)
                {
                    string fileUploadedUri = fileManager.uploadFile(trackInfo.SongPath);
                    trackInfo.isSynced = true;
                    trackInfo.songURI = fileUploadedUri;
                    await ApiConsumer.InsertTrackMetadata(trackInfo);
                    fileManager.uploadFile(trackInfo.SongPath);
                }
            }
        }
    }
}
