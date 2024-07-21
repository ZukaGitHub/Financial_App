﻿using Domain.SharedModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SearchEngine.CreateSearch
{
    public class CreateSearchEngineCommand :IRequest<SearchEngineResponseModel>
    {
        public Domain.Entities.SearchEngine SearchEngine { get; set; }

    }
}