using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Context
{
    public class FundooContext : DbContext
    {
        public FundooContext(DbContextOptions options) : base(options) 
        { 
        }
        public DbSet<UserEntity> UserTable { get; set; }
        public DbSet<CustomerEntity> CustomerTable { get; set; }
        public DbSet<NotesEntity> NotesTable { get; set; }
        public DbSet<LabelEntity> LabelTable { get; set; }
        public DbSet<CollaboratorEntity> CollaboratorTable { get; set; }
    }
}
