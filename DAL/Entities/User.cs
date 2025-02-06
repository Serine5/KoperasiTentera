using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoperasiTenteraDAL.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ICNumber { get; set; }
        public int MobileNumber { get; set; }
        public string Email { get; set; }
    }
}
