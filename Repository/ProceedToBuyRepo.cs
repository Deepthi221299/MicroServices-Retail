using ProceedToBuy.Context;
using ProceedToBuy.Models;
using ProceedToBuy.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace ProceedToBuy.Repository
{
    [ExcludeFromCodeCoverage]
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
            List<Vendor> vendor = _provider.GetVendors(_cart.ProductId);
            if(vendor==null)
            {
                AddToWishList(_cart.CustomerId, _cart.ProductId,_cart.Quantity,_cart.VendorId);
                return false;
            }
            else
            {
               // _cart.VendorId = vendor.Id;
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
        public bool AddToWishList(int customerId,int productId,int quantity,int vendorid)
        {
            VendorWishlist vendorWishlist = new VendorWishlist();
            vendorWishlist.CustomerId = customerId;
            vendorWishlist.ProductId = productId;
            vendorWishlist.Quantity = quantity;
            vendorWishlist.DateAddedToWishlist = DateTime.Now;
            vendorWishlist.VendorId = vendorid;
            _proceedToBuyContext.VendorWishlists.Add(vendorWishlist);
            _proceedToBuyContext.SaveChanges();
            return true;
        }
        public List<VendorWishlist> GetWishlist()
        {
            return _proceedToBuyContext.VendorWishlists.ToList();
        }
        public List<Vendor> GetVendor(int productId)
        {
            List<Vendor> v = _provider.GetVendors(productId);
            return v;
        }

        public bool DeleteCustomerCart(int customerId)
        {

            List<Cart> cart = GetCart();
            foreach (Cart item in cart)
            {
                if (item.CustomerId == customerId)
                    _proceedToBuyContext.Carts.Remove(item);
            }

            _proceedToBuyContext.SaveChanges();

            return true;
        }

    }
}
