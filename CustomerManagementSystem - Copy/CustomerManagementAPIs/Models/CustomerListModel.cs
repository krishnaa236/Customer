using CustomerManagementSystem.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerManagementAPIs.Models
{
    public class CustomerListModel
    {
        public IEnumerable<Customer> customers { get; set; } = new List<Customer>();
    }
}