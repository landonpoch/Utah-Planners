﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UtahPlanners.Domain.DTO
{
    public class AddressDTO
    {
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
    }
}
