using GrecosQuestionnaire.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrecosQuestionnaire.Data
{
    public class HotelDBContext : IdentityDbContext
    {
        public HotelDBContext(DbContextOptions<HotelDBContext> option) : base (option)
        {

        }

        public DbSet<SeasonModel> Seasons { get; set; }
        public DbSet<HotelModel> Hotels { get; set; }
        public DbSet<MainRoomModel> MainRooms { get; set; }
        public DbSet<SharedUnitModel> SharedUnits { get; set; }
        public DbSet<PartnerModel> Partners { get; set; }
        public DbSet<UserPartnerModel> UsersPartners { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionItem> QuestionItems { get; set; }
        public DbSet<ResponseModel> Responses { get; set; }
        public DbSet<ResponseItemModel> ResponseItems { get; set; }
    }
}
