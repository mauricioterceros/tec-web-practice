using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models
{
    public class User : Entity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}
