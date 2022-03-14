using ProceedToBuy.Context;
using ProceedToBuy.Models;
using ProceedToBuy.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProceedToBuy.Repository
{
    public class ProceedToBuyRepo:IProceedToBuyRepo<Cart>
    {
        IProvider _provider;
        ProceedToBuyContext _proceedToBuyContext;
        public ProceedToBuyRepo(ProceedToBuyContext proceedToBuyContext, IProvider provider)
        {
            _proceedToBuyContext = proceedToBuyContext;
            _provider = provider;
        }
        public List<Cart> GetCart()
        {
            return _proceedToBuyContext.Carts.ToList();
        }
        public bool AddToCart(Cart _cart)
        {
            var vendor = _provider.GetVendors(_cart.ProductId);
            if(vendor==null)
            {
                AddToWishList(_cart.CustomerId, _cart.ProductId);
                return false;
            }
            else
            {
                _cart.VendorId = vendor.Id;
                Cart val = _proceedToBuyContext.Carts.SingleOrDefault(c => c.CustomerId == _cart.CustomerId && c.ProductId == _cart.ProductId);
                if (val != null)
                {
                    val.Quantity += _cart.Quantity;
                }
                else
                    _proceedToBuyContext.Carts.Add(_cart);
                _proceedToBuyContext.SaveChanges();
            }
            return true;
        }
        public bool AddToWishList(int customerId,int productId)
        {
            VendorWishlist vendorWishlist = new VendorWishlist();
            vendorWishlist.CustomerId = customerId;
            vendorWishlist.ProductId = productId;
            vendorWishlist.Quantity = 1;
            vendorWishlist.DateAddedToWishlist = DateTime.Now;
            vendorWishlist.VendorId = 7;
            _proceedToBuyContext.VendorWishlists.Add(vendorWishlist);
            _proceedToBuyContext.SaveChanges();
            return true;
        }
        public List<VendorWishlist> GetWishlist(int id)
        {
            return _proceedToBuyContext.VendorWishlists.Where(v => v.CustomerId == id).ToList();
        }
        public bool DeleteCustomerCart(int customerId)
        {
            List<Cart> cart = GetCart();
            foreach(Cart item in cart)
            {
                if (item.CustomerId == customerId)
                    _proceedToBuyContext.Carts.Remove(item);
            }
            _proceedToBuyContext.SaveChanges();
            return true;
        }
        public bool DeleteCartById(int cartId)
        {
            Cart cart = GetCart().SingleOrDefault(x => x.CartId == cartId);
            _proceedToBuyContext.Carts.Remove(cart);
            _proceedToBuyContext.SaveChanges();
            return true;
        }
    }
}
