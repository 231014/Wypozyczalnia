using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WypozyczalniaDLL
{
    public abstract class Category
    {
        protected List<Movie> movies; //klucz dostepu mowiacy kto moze miec dostep do funkcji lub obiektu


        public void AddMovie(Movie movie)
        {
            if (movies.Contains(movie))
                return;
            movies.Add(movie);
            if(!movie.Categories.Contains(this))
                movie.Categories.Add(this);
        }

        public void RemoveMovie(Movie movie)
        {
            if (movies.Contains(movie))
            {
                if(movie.Categories.Contains(this))
                    movie.Categories.Remove(this);
                movies.Remove(movie);
            }
        }

        public abstract string ReturnCategoryName();

        public List<Movie> ReturnListOfMovies()
        {
            return movies;
        }
    }
}
