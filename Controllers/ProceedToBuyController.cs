using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProceedToBuy.Models;
using ProceedToBuy.Repository;
using ProceedToBuy.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProceedToBuy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProceedToBuyController : ControllerBase
    {
        IProceedToBuyRepo<Cart> _repository;
        IProvider _provider;

        private readonly log4net.ILog _log4net;

        public ProceedToBuyController(IProceedToBuyRepo<Cart> repository, IProvider provider)
        {
            _log4net = log4net.LogManager.GetLogger(typeof(ProceedToBuyController));
           _log4net.Info("Getting Started");
            _repository = repository;
            _provider=provider;


    }
        /*[HttpGet("ID")]
        public Vendor GetVen(int id)
        {
            Cart _cart=new Cart();
            var vendor=_provider.GetVendors(_cart.ProductId);
            return vendor;
        }*/

        // GET: api/<ProceedToBuyController>
        [HttpGet]
        public IEnumerable<Cart> Get()
        {
            return _repository.GetCart();
        }

        
        [HttpPost]
        public Boolean Post([FromBody] Cart _cart)
        {
            _log4net.Info("Adding to Cart");
            return _repository.AddToCart(_cart);

        }


        [HttpGet("GetWishList/{id}")]
        public IEnumerable<VendorWishlist> GetWishList(int id)
        {
            return _repository.GetWishlist(id);
        }


        [Route("WishList")]
        [HttpPost]
        public IActionResult Post(int customerId, int productId)
        {
            _log4net.Info("Addding to WishList");
            _repository.AddToWishList(customerId, productId);
            return Ok("Success");
        }

        
    }
}
