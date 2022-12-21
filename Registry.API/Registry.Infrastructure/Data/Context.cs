using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Registry.Domain.Model;

namespace Registry.Infrastructure.Data
{
  public class Context : DbContext
  {
    public Context(DbContextOptions<Context> options) : base(options)
    {

    }
    public DbSet<Discipline> Disciplines { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }
  }
}
