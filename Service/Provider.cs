using Microsoft.Extensions.Configuration;
using ProceedToBuy.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProceedToBuy.Service
{
    [ExcludeFromCodeCoverage]
    public class Provider:IProvider
    {
        private IConfiguration _configure;
        static private string apiBaseUrl;
        public Provider(IConfiguration configure)
        {
            _configure = configure;
            apiBaseUrl = _configure.GetValue<string>("WebAPIBaseUrl");
        }
        public List<Vendor> GetVendors(int productId)
        {
            IList<Vendor> vendors = null;
            //string apiBaseUrl = $"https://localhost:44330/api/Vendor/{productId}";
            string apiBaseUrl = $"{_configure["ExternalURI:Vendorapi"]}/api/Vendor/{productId}";
            using (var client=new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseUrl);
                //string api = "Vendor/" + productId;
                var responseTask = client.GetAsync(apiBaseUrl);
                responseTask.Wait();
                var result = responseTask.Result;
                if(result.IsSuccessStatusCode)
                {
                    var readData = result.Content.ReadAsAsync<IList<Vendor>>();
                    readData.Wait();
                    vendors = readData.Result;
                }

            }

            if(vendors.Count>0)
            {
               // List<Vendor> vendor = new List<Vendor>();
                //List<Vendor> v = vendor.Where(v => v.Id == productId).ToList();
                return vendors.ToList();

            }
            return null;
        }
        
    }
}
