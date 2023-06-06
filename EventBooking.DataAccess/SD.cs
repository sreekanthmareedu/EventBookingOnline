using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEvents.DataAccess
{
    public class SD
    {
        public enum ApiType
        {

            GET,
            POST,
            PUT,
            DELETE
        }

        public const string Role_Customer = "Customer";
        public const string Role_Admin = "Admin";
        public const string Role_Employee = "Employee";
    }
}
