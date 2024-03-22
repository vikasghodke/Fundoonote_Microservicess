using Microsoft.EntityFrameworkCore;
using Note_Microservices.NoteEntity;


namespace Note_Microservices.Context
{
    public class NoteContext : DbContext
    {
       public NoteContext(DbContextOptions options):base(options) 
       {

       }
       public DbSet<NoteEntity1> NoteTable { get; set; }
    }
}
