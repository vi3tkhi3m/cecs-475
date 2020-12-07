using Microsoft.AspNetCore.Mvc.Rendering;
using MvcMovieAm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMovieAm.Models
{
    public class MovieGenreAmViewModel
    {
        public List<MovieAm> Movies { get; set; }
        public SelectList Genres { get; set; }
        public string MovieGenre { get; set; }
        public string SearchString { get; set; }
    }
}
