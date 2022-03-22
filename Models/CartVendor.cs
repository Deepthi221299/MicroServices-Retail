using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace ProceedToBuy.Models
{
    [ExcludeFromCodeCoverage]
    public class CartVendor
    {
        public Cart Carts { get; set; }
        public CartVendor Vendor { get; set; }

       
    }
}
