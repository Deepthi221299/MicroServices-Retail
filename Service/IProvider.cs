using ProceedToBuy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProceedToBuy.Service
{
    public interface IProvider
    {
        public List<Vendor> GetVendors(int productId);
        
        
    }
}
