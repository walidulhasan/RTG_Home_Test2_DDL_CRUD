using AddressBook.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBook.Data
{
    public class AddressBookDbContext:DbContext
    {
        public AddressBookDbContext(DbContextOptions<AddressBookDbContext> options) : base(options)
        {

        }
        public DbSet<Country> countries { get; set; }
        public DbSet<Division> divisions { get; set; }
        public DbSet<District> districts { get; set; }
        public DbSet<Person> persons { get; set; }
    }
}
