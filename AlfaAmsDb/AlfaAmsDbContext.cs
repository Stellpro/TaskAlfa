using AlfaAmsDb.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AlfaAmsDb
{
    public class AlfaAmsDbContext : DbContext
    {
        public AlfaAmsDbContext(DbContextOptions<AlfaAmsDbContext> options)
           : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<UserRole>(e => { e.HasNoKey(); });
        }
        private DbSet<Benutzer> BenutzerDbSet { get; set; }
        private DbSet<UserRole> UserRoleDbSet { get; set; }
        public DbSet<BenutzerModel> BenutzerModelDbSet { get; set; }

        public Benutzer Login(string name, string pass)
        {
            var users = GetAll();
            return users.SingleOrDefault(r => r.Login.ToLower() == name.ToLower() && r.Passwort == pass);
        }

        public List<Benutzer> GetAll()
        {
            var sql = @"select *  from Benutzer";


            var result = BenutzerDbSet.FromSqlRaw(sql).ToList();
            return result;
        }
        public Benutzer GetUserByName(string name)
        {
            var sql = @"select *  from Benutzer where Login='" + name + "'";
            var result = BenutzerDbSet.FromSqlRaw(sql).ToList();
            return result.SingleOrDefault();
        }

        public string GetRoleById(int id)
        {
            var sql = @"select  Bezeichnung  from [Benutzerrolle] where BenutzerrolleId = " + id;
            var list = UserRoleDbSet.FromSqlRaw(sql).ToList();

            return list.SingleOrDefault()?.Bezeichnung;
        }
    }
}
