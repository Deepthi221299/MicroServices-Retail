using Microsoft.EntityFrameworkCore;
using ProceedToBuy.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace ProceedToBuy.Context
{
    [ExcludeFromCodeCoverage]
    public class ProceedToBuyContext:DbContext
    {
        public ProceedToBuyContext(DbContextOptions<ProceedToBuyContext> options):base(options)
        {

        }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<VendorWishlist> VendorWishlists { get; set; }
    }
}
