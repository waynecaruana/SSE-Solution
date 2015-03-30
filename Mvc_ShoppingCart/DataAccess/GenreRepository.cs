﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace DataAccess
{
    public class GenreRepository : ConnectionClass
    {
        public GenreRepository() : base() { }

        public IEnumerable<Genre> GetAllGenres()
        {
            return entities.Genres.ToList();
        }
    }
}