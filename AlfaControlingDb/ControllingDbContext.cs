using AlfaControlingDb.Models;
using DbRepository.Models;
using DbRepository.View;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace AlfaControlingDb
{
    public class ControllingDbContext : DbContext
    {
        public ControllingDbContext(DbContextOptions<ControllingDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
             .Entity<FilterModel>(eb => { eb.HasNoKey(); });
        }

        private DbSet<LogApplicationError> LogApplicationErrorDbset { get; set; }
        private DbSet<LogUserErrorRequest> LogUserErrorRequestDbset { get; set; }
        public DbSet<FilterModel> FilterModelDbset { get; set; }

     
       
  
        }


        
    
   
}
