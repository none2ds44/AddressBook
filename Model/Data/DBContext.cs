using Microsoft.EntityFrameworkCore;

namespace AddressBook.Model.Data
{
    public class DBContext: DbContext
    {
        public DbSet<Contact> Contacts { get; set; }

        public DBContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=address_book.db");
        }
    }
}
