using Microsoft.EntityFrameworkCore;
using MvcMovieAm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMovieAm.DataAm
{
    public class MvcMovieAmContext : DbContext
    {
        public MvcMovieAmContext(DbContextOptions<MvcMovieAmContext> options)
            : base(options)
        {
        }

        public DbSet<MovieAm> Movie { get; set; }
    }
}
