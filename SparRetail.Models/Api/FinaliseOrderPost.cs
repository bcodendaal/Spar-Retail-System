﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Models.Api
{
    public class FinaliseOrderPost : RetailerIdPost
    {
        public int OrderBasketId { get; set; }
    }
}