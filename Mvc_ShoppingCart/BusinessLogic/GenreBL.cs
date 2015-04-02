using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess;
using Common;

namespace BusinessLogic
{
    public class GenreBL
    {
        public IEnumerable<Genre> GetAllGenres()
        {
            return new GenreRepository().GetAllGenres();
        }

        public Genre GetGenreByID(int id)
        {
            return new GenreRepository().GetGenreByID(id);
        }
    }
}
