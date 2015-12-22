using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.Models
{
    class Composition:IEquatable<Composition>
    {
        const int MAX_LENGTH = 256;
        const int MAX_RATING = 10;
        const int MIN_RATING = 0;
        public enum Genres
        {
            Rock,
            Pop,
            Rap,
            Jazz,
            Blues
        }
        private Genres genre;
        private int id;
        private string title;
        private string artist;
        private int rating;
        private TimeSpan length;
        public bool IsSelected{ get; set; }
        public int ID
        {
            get { return id; }
            private set {; }
        }
        public TimeSpan Length
        {
            get { return length; }
            private set {}
        }
         public string Genre
        {
            get
            {
                switch (genre)
                {
                    case Composition.Genres.Rock:
                        return "Rock";
                    case Composition.Genres.Pop:
                        return "Pop";
                    case Composition.Genres.Rap:
                        return "Rap";
                    case Composition.Genres.Jazz:
                        return "Jazz";
                    case Composition.Genres.Blues:
                        return "Blues";
                    default:
                        return "Unkown";
                }
            }
            private set {; }
        }

        public string Title
        {
            get
            {
                return title;
            }
            private set
            {
                if (value.Length < MAX_LENGTH)
                {
                    title = value;
                }
            }
        }

        public string Artist
        {
            get
            {
                return artist;
            }
            private set
            {
                if (value.Length < MAX_LENGTH)
                {
                    artist = value;
                }
            }
        }

        public int Rating
        {
            get
            {
                return rating;
            }
            private set
            {
                if(value<MAX_RATING && value > MIN_RATING)
                {
                    rating = value;
                }
            }
        }

        /*public Composition()
        {
            ID = 0;
            Title = "";
            Length = new TimeSpan(0, 0, 0);
            Artist = "";
            Genre = 0;
            Rating = 0;
        }*/

        public Composition(int id, string title, string length, string artist, Genres genre, int rating)
        {
            this.id = id;
            this.title = title;
           
            this.length = TimeSpan.Parse(length);
            this.artist = artist;
            this.genre = genre;
            this.rating = rating;
        }  
        public Composition()
        {
        }
        public bool Equals(Composition other)
        {
            if (this.ID == other.ID)
                return true;
            else
                return false;
        }
    }
}
