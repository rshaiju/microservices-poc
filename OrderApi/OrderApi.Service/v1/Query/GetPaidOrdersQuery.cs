﻿using MediatR;
using OrderApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApi.Service.v1.Query
{
    public class GetPaidOrdersQuery : IRequest<List<Order>>
    {
    }
}
