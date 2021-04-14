using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Zaidimas_kartuves.Inicijavimas;
using Zaidimas_kartuves.Modeliai;

namespace Zaidimas_kartuves.Duomenubaze
{
    public class KartuvesContext: DbContext
    {
        public DbSet<Zodis> Zodis { get; set; }
        public DbSet<Statistika> Statistika { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = KartuviuDuombaze");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Zodis>().HasData(ZodziuInicijavimas.Rinkinys);
        }
    }
}
