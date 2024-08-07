﻿using Domain.SharedModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Client.GetClient
{
    public class GetClientCommand:IRequest<GetClientReponseModel>
    {
        public int Id { get; set; }
    }
}
