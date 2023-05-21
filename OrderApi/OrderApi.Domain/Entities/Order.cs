﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApi.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }

        public int OrderState { get; set; }

        public Guid CustomerId { get; set; }

        public string CustomerFullName { get; set; }
    }
}