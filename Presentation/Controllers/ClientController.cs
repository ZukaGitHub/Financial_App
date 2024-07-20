using AutoMapper;
using Domain.Entities;
using Infrastructure.JWT;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Client.CreateClient;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Presentation.Controllers
{
    [Route("Api/Client")]
    [ApiController]
    public class ClientController:ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
       
        public ClientController(IMediator mediator, IMapper mapper, ITokenService tokenService)
        {
            _mediator = mediator;
            _mapper = mapper;           
        }

        //[HttpGet]
        //public async Task<IActionResult> GetClient()
        //{

        //}
        //[HttpGet]
        //public async Task<IActionResult> GetClientList()
        //{

        //}
        [HttpPost("Create")]
        public async Task<IActionResult> CreateClient(CreateClientDTO model, CancellationToken cancellationToken = default)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var client = _mapper.Map<Client>(model);
                var command = new CreateClientCommand() { Client = client };
                var result = await _mediator.Send(command, cancellationToken);
                if (result.Errors == null)
                {
                    return Ok("Client Created Successfully");
                }
                 else
                {
                    return BadRequest(result.Errors);
                }            
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
        //[HttpPut]
        //public async Task<IActionResult> UpdateClient()
        //{

        //}
        //[HttpDelete]
        //public async Task<IActionResult> DeleteClient()
        //{

        //}
    }
}
