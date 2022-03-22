using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
   /* [Produces("application/json")]
    [Authorize(Roles = "User")]*/
    public class ProceedToBuyController : ControllerBase
    {
        private readonly ILogger<ProceedToBuyController> _logger;
        IProceedToBuyRepo<Cart> _repository;
        IProvider _provider;



        private readonly log4net.ILog _log4net;



        public ProceedToBuyController(IProceedToBuyRepo<Cart> repository, ILogger<ProceedToBuyController> logger, IProvider provider)
        {
            _log4net = log4net.LogManager.GetLogger(typeof(ProceedToBuyController));
            _log4net.Info("Getting Started");
            _repository = repository;
            _provider = provider;
            _logger = logger;




        }
        // GET: api/<ProceedToBuyController>
        //[Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("GetAllcarts")]
        public IActionResult GetAllCarts()
        {
            _log4net.Info("ProductToBuyController - GetAllCarts");

            var result = _repository.GetCart();
            if (result != null)
            {
                _log4net.Info("ProceedToBuyController - GetAllCarts - All Carts Fetched");
                return Ok(result);
            }
            else
            {
                _log4net.Info("ProceedToBuyController - GetAllCarts - Carts Not Found");
                return NotFound();
            }
        }



        //[Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("AddingTocart")]
        public IActionResult AddingToCart([FromBody] Cart _cart)
        {
            _log4net.Info("Controller ready for creating new record ");
            if (_repository.AddToCart(_cart))
            {
                return Ok(_cart);
            }
            return NotFound("Product Not Found");
        }


        //[Authorize(AuthenticationSchemes = "Bearer")]
        [Route("AddingToWishList")]
        [HttpPost]
        public IActionResult AddingToWishList(int customerId, int productId,int quantity, int vendorid)
        {
            _log4net.Info(" ProceedToBuyController- Addding to WishList");
            if (_repository.AddToWishList(customerId, productId,quantity,vendorid))
            {
                return Ok("Successfully Added to WishList");
            }
            return new ObjectResult("Product was already present in the list") { StatusCode = 404 };
         }

        //[Authorize(AuthenticationSchemes = "Bearer")]
        [Route("GetAllWishList")]
        [HttpGet]
        public IActionResult GetAllWishList()
        {
            _log4net.Info("ProductToBuyController - GetAllCarts");

            var result = _repository.GetWishlist();
            if (result != null)
            {
                _log4net.Info("ProceedToBuyController - GetAllWishList - All Carts Fetched");
                return Ok(result);
            }
            else
            {
                _log4net.Info("ProceedToBuyController - GetAllWishList - Carts Not Found");
                return NotFound();
            }

        }

        //[Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        [Route("{productId}")]
        public IActionResult GetVendorDetails(int productId)
        {
            var v= _repository.GetVendor(productId);
            return Ok(v);
        }

        //[Authorize(AuthenticationSchemes= "Bearer")]
        [Route("DeleteAll/{id}")]
        [HttpGet]
        public IActionResult DeleteAll(int id)
        {
            _log4net.Info("Checking out");
            if (_repository.DeleteCustomerCart(id))
                return Ok("Success");
            return BadRequest("Failed");
        }
    }
}
