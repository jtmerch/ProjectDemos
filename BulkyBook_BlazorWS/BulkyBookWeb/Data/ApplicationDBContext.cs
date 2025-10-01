using BulkyBookWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookWeb.Data
{
    public class ApplicationDBContext: DbContext
    {
        //be sure to add Nuget package for EntityFrameworkCore to have DbContext recognized
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options): base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }


        //commands to run DBContext via Package Manager Console
        //1. First be sure to add Nuget Microsoft.EntityFrameworkCore.Tools
        //2. add-migration AddCategoryToDatabase
        //3. update-database

    }
}
