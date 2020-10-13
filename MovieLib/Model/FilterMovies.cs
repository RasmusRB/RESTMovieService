using System;
using System.Collections.Generic;
using System.Text;

namespace MovieLib.Model
{
    public class FilterMovies
    {
        private double _lowRating;
        private double _highRating;

        public FilterMovies()
        {
        }

        public FilterMovies(double lowRating, double highRating)
        {
            _lowRating = lowRating;
            _highRating = highRating;
        }

        public double LowRating
        {
            get => _lowRating;
            set => _lowRating = value;
        }

        public double HighRating
        {
            get => _highRating;
            set => _highRating = value;
        }
    }
}
