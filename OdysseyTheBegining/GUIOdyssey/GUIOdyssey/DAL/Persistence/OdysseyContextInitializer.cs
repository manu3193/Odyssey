using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GUIOdyssey.DAL.Persistence.Models;

namespace GUIOdyssey.DAL.Persistence
{
    class OdysseyContextInitializer : DropCreateDatabaseIfModelChanges<OdysseyContext>
    {
        protected override void Seed(OdysseyContext context)
        {
            User newUser = new User()
            {
                Name= "Manuel Zumbado", UserID = new Guid( ),   Nickname = "manu3193"
            };
            User newUser1 = new User()
            {
                Name = "Nico Jimenes",
                UserID = Guid.NewGuid(),
                Nickname = "Majesco"
            };
            User newUser2 = new User()
            {
                Name = "Kevin Uma;a",
                UserID = Guid.NewGuid(),
                Nickname = "charlatan123"
            };
            User newUser3 = new User()
            {
                Name = "Jose Guillen",
                UserID = Guid.NewGuid(),
                Nickname = "guillenaljose"
            };

            context.Users.Add(newUser);
            context.Users.Add(newUser1);
            context.Users.Add(newUser2);
            context.Users.Add(newUser3);

/*
            Artist artistaPF= new Artist() {ArtistID = Guid.NewGuid(),MusicBrainzID = null,Title = "Pink Floyd"};
            context.Artists.Add(artistaPF);

            Album tpatgotd = new Album() {Artist = artistaPF,Title = "The Piper and the Gates of the Down",AlbumID = Guid.NewGuid(),ReleaseYear = 1967};
            Album asos = new Album() { Artist = artistaPF, Title = "A Sourceful of Secrets", AlbumID = Guid.NewGuid(), ReleaseYear = 1968 };
            Album meddle = new Album() { Artist = artistaPF, Title = "Meddle", AlbumID = Guid.NewGuid(), ReleaseYear = 1971 };
            Album tdsotm = new Album() { Artist = artistaPF, Title = "The Dark Side of the Moon", AlbumID = Guid.NewGuid(), ReleaseYear = 1973 };
            Album wywh = new Album() { Artist = artistaPF, Title = "Wish You Where Here", AlbumID = Guid.NewGuid(), ReleaseYear = 1977 };

            Track t1 = new Track() {Album = tpatgotd,Genre = "Psychedelic Rock",Title = "Astronomy Domine",Path = "SomePAth",TrackID = Guid.NewGuid()};
            Track t2 = new Track() { Album = tpatgotd, Genre = "Psychedelic Rock", Title = "Intelestar Overdiver", Path = "SomePAth", TrackID = Guid.NewGuid() };
            Track t3 = new Track() { Album = tpatgotd, Genre = "Psychedelic Rock", Title = "The Gnome", Path = "SomePAth", TrackID = Guid.NewGuid() };

            Track t4 = new Track() { Album = asos, Genre = "Psychedelic Rock", Title = "A Sourceful of Secrets", Path = "SomePAth", TrackID = Guid.NewGuid() };
            Track t5 = new Track() { Album = asos, Genre = "Psychedelic Rock", Title = "Apples and Oranges", Path = "SomePAth", TrackID = Guid.NewGuid() };
            Track t6 = new Track() { Album = asos, Genre = "Psychedelic Rock", Title = "Let be more than light", Path = "SomePAth", TrackID = Guid.NewGuid() };

            Track t7 = new Track() { Album = meddle, Genre = "Psychedelic Rock", Title = "One of These Days", Path = "SomePAth", TrackID = Guid.NewGuid() };
            Track t8 = new Track() { Album = meddle, Genre = "Psychedelic Rock", Title = "Echoes", Path = "SomePAth", TrackID = Guid.NewGuid() };
            Track t9 = new Track() { Album = meddle, Genre = "Psychedelic Rock", Title = "Saint Tropez", Path = "SomePAth", TrackID = Guid.NewGuid() };

            Track t10 = new Track() { Album = tdsotm, Genre = "Progressive Rock", Title = "Breathe", Path = "SomePAth", TrackID = Guid.NewGuid() };
            Track t11 = new Track() { Album = tdsotm, Genre = "Progressive Rock", Title = "Money", Path = "SomePAth", TrackID = Guid.NewGuid() };
            Track t12 = new Track() { Album = tdsotm, Genre = "Progressive Rock", Title = "Us and Then", Path = "SomePAth", TrackID = Guid.NewGuid() };

            Track t13 = new Track() { Album = wywh, Genre = "Progressive Rock", Title = "Shine on You Crazy Diamond", Path = "SomePAth", TrackID = Guid.NewGuid() };
            Track t14 = new Track() { Album = wywh, Genre = "Progressive Rock", Title = "Wish You Where Here", Path = "SomePAth", TrackID = Guid.NewGuid() };
            Track t15 = new Track() { Album = wywh, Genre = "Progressive Rock", Title = "Welcome to the Machine", Path = "SomePAth", TrackID = Guid.NewGuid() };

            context.UserTracks.Add(new UserTrack()
            {
                TrackID = context.Tracks.Add(t1).TrackID,
                IsSync = true,
                UserID = newUser.UserID
            });

            context.UserTracks.Add(new UserTrack()
            {
                TrackID = context.Tracks.Add(t15).TrackID,
                IsSync = true,
                UserID = newUser.UserID
            });
            context.UserTracks.Add(new UserTrack()
            {
                TrackID = context.Tracks.Add(t2).TrackID,
                IsSync = true,
                UserID = newUser.UserID
            });


            context.UserTracks.Add(new UserTrack()
            {
                TrackID = context.Tracks.Add(t3).TrackID,
                IsSync = true,
                UserID = newUser1.UserID
            });

            context.UserTracks.Add(new UserTrack()
            {
                TrackID = context.Tracks.Add(t14).TrackID,
                IsSync = true,
                UserID = newUser1.UserID
            });
            context.UserTracks.Add(new UserTrack()
            {
                TrackID = context.Tracks.Add(t4).TrackID,
                IsSync = true,
                UserID = newUser1.UserID
            });



            context.UserTracks.Add(new UserTrack()
            {
                TrackID = context.Tracks.Add(t5).TrackID,
                IsSync = true,
                UserID = newUser2.UserID
            });

            context.UserTracks.Add(new UserTrack()
            {
                TrackID = context.Tracks.Add(t13).TrackID,
                IsSync = true,
                UserID = newUser2.UserID
            });
            context.UserTracks.Add(new UserTrack()
            {
                TrackID = context.Tracks.Add(t6).TrackID,
                IsSync = true,
                UserID = newUser2.UserID
            });




            context.UserTracks.Add(new UserTrack()
            {
                TrackID = context.Tracks.Add(t7).TrackID,
                IsSync = true,
                UserID = newUser3.UserID
            });

            context.UserTracks.Add(new UserTrack()
            {
                TrackID = context.Tracks.Add(t12).TrackID,
                IsSync = true,
                UserID = newUser3.UserID
            });
            context.UserTracks.Add(new UserTrack()
            {
                TrackID = context.Tracks.Add(t8).TrackID,
                IsSync = true,
                UserID = newUser3.UserID
            });



            context.UserTracks.Add(new UserTrack()
            {
                TrackID = context.Tracks.Add(t9).TrackID,
                IsSync = true,
                UserID = newUser1.UserID
            });

            context.UserTracks.Add(new UserTrack()
            {
                TrackID = context.Tracks.Add(t11).TrackID,
                IsSync = true,
                UserID = newUser2.UserID
            });
            context.UserTracks.Add(new UserTrack()
            {
                TrackID = context.Tracks.Add(t10).TrackID,
                IsSync = true,
                UserID = newUser3.UserID
            });


            context.SaveChanges();

    */
        }
    }
}
