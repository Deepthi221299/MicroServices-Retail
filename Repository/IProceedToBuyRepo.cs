using ProceedToBuy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProceedToBuy.Repository
{
    public interface IProceedToBuyRepo<T>
    {
        public bool AddToCart(Cart t);
        public List<Cart> GetCart();
        public bool AddToWishList(int customerId, int productId, int quantity, int vendorid);
        public List<VendorWishlist> GetWishlist();
        public List<Vendor> GetVendor(int productId);
        public bool DeleteCustomerCart(int customerId);


    }
}
