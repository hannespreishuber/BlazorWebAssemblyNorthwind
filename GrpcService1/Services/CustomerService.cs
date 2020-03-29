using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using GrpcService1.Models;
using Microsoft.Extensions.Logging;

namespace GrpcService1.Services
{
    public class NorthwindDBService : NorthwindDB.NorthwindDBBase
    {
        private readonly ILogger<NorthwindDBService> _logger;
    
        private NORTHWNDContext db;

        public NorthwindDBService(ILogger<NorthwindDBService> logger, NORTHWNDContext db)
        {
            _logger = logger;
            this.db = db;
            _logger.LogWarning("Grpc Konstruktor");
        }

     

        public override Task<Customer> SelectID(CustomerFilter request, ServerCallContext context)
        {
            return base.SelectID(request, context);
        }

        public override Task<Customers> SelectAll(Empty empty, ServerCallContext context)
        {
            
            Customers protoCustomer = new Customers();
            foreach (var item in db.Customers)
            {
                protoCustomer.Items.Add(new Customer()
                {
                    CustomerId = item.CustomerId,
                    Address = item.Address,
                    City = item.City,
                    CompanyName = item.CompanyName,
                    ContactName = item.ContactName,
                    ContactTitle = item.ContactTitle?.ToString() ?? "",
                    Country = item.Country?.ToString() ?? "",
                    Fax = item.Fax?.ToString() ?? "",
                    Phone = item.Phone?.ToString() ?? "",
                    PostalCode = item.PostalCode?.ToString() ?? "",
                    Region = item.Region?.ToString() ?? "" //Kein NUll in Proto


                });
            }

            return Task.FromResult(protoCustomer);
        }
    
    }
}
