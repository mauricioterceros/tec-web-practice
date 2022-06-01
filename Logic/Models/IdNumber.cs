using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Models
{
    public class IdNumber
    {
        public int Id { get; set; }
        public string Uid { get; set; }
        public string ValidUsSsn { get; set; }
        public string InvalidUsSsn { get; set; }
    }
}
