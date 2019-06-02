using Microsoft.EntityFrameworkCore;
using GestionBares.Data;
using GestionBares.Models.AlmacenModels;
using System;

namespace GestionBaresTest
{
    public class SetupContext
    {
        public static DbContext GetContext()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;
            var context = new ApplicationDbContext(options);
            context.Database.EnsureDeleted();

            //agregando datos    

            context.SaveChanges();
            return context;
        }

        public static DbContext GetAlmacenContext()
        {

            DbContextOptions<AlmacenDbContext> options = new DbContextOptionsBuilder<AlmacenDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;
            var context = new AlmacenDbContext(options);
            context.Database.EnsureDeleted();
            //agregando datos

            context.SaveChanges();
            return context;
        }

    }
}