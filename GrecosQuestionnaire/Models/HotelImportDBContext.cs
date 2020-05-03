using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrecosQuestionnaire.Models
{
    public class HotelImportDBContext : DbContext
    {
        public HotelImportDBContext(DbContextOptions<HotelImportDBContext> option) : base(option)
        {

        }

        public DbSet<HotelImportModel> RoomTypes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HotelImportModel>(entity =>
            {
                entity.HasNoKey();
            });
        }
    }
}
