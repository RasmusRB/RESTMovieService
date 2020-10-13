using System;
using System.Collections.Generic;
using System.Text;

namespace MovieLib.Model
{
    public class Movies
    {

        private int _id;
        private string _title;
        private string _director;
        private string _genre;
        private int _releaseYear;
        private double _imdbRating;

        public Movies()
        {
        }

        public Movies(int id, string title, string director, string genre, int releaseYear, double imdbRating)
        {
            _id = id;
            _title = title;
            _director = director;
            _genre = genre;
            _releaseYear = releaseYear;
            _imdbRating = imdbRating;
        }

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public string Title
        {
            get => _title;
            set => _title = value;
        }

        public string Director
        {
            get => _director;
            set => _director = value;
        }

        public string Genre
        {
            get => _genre;
            set => _genre = value;
        }

        public int ReleaseYear
        {
            get => _releaseYear;
            set => _releaseYear = value;
        }

        public double ImdbRating
        {
            get => _imdbRating;
            set => _imdbRating = value;
        }

        public override string ToString()
        {
            return $"{nameof(_id)}: {_id}, {nameof(_title)}: {_title}, {nameof(_director)}: {_director}, {nameof(_genre)}: {_genre}, {nameof(_releaseYear)}: {_releaseYear}, {nameof(_imdbRating)}: {_imdbRating}";
        }
    }
}
