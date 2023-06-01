using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagementSystem.Domain.Domain
{
    public class Admin
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Admin()
        {
            this.users = new HashSet<user>();
        }

        public long Admin_id { get; set; }
        public string Admin_FirstName { get; set; }
        public string Admin_LastName { get; set; }
        public string Admin_Email { get; set; }
        public string Admin_PhoneNumber { get; set; }
        public string Admin_city { get; set; }
        public string Admin_country { get; set; }
        public System.DateTime created_at { get; set; }
        public Nullable<System.DateTime> updated_at { get; set; }
        public Nullable<System.DateTime> deleted_at { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<user> users { get; set; }
    }
}
