using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagementSystem.Domain.Domain
{
    public class user
    {
        public long User_id { get; set; }
        public string User_name { get; set; }
        public string password { get; set; }
        public Nullable<long> customer_id { get; set; }
        public bool status { get; set; }
        public System.DateTime created_at { get; set; }
        public Nullable<System.DateTime> updated_at { get; set; }
        public Nullable<System.DateTime> deleted_at { get; set; }
        public string Role { get; set; }
        public Nullable<long> Updated_by { get; set; }
        public Nullable<long> Admin_id { get; set; }
        public string ImageUrl { get; set; }
        public virtual Admin Admin { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
