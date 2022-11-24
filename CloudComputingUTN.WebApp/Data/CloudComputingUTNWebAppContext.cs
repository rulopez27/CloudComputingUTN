using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CloudComputingUTN.Entities;

namespace CloudComputingUTN.WebApp.Data
{
    public class CloudComputingUTNWebAppContext : DbContext
    {
        public CloudComputingUTNWebAppContext (DbContextOptions<CloudComputingUTNWebAppContext> options)
            : base(options)
        {
        }

        public DbSet<CloudComputingUTN.Entities.Artwork> Artwork { get; set; } = default!;
    }
}
