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
using Infrastructure.ValidationService;
using Infrastructure.FileManager;
using Microsoft.AspNetCore.Http;

namespace Presentation.Controllers
{
    [Route("Api/Client")]
    [ApiController]
    public class ClientController:ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ValidationService _validationService;
        private readonly IFileManager _FileManager;
        public ClientController(IMediator mediator, IMapper mapper, ITokenService tokenService,ValidationService validationService,IFileManager fileManager)
        {
            _mediator = mediator;
            _mapper = mapper;       
            _validationService = validationService;
            _FileManager = fileManager;
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
        public async Task<IActionResult> CreateClient([FromForm]CreateClientDTOWithRegionCode model, CancellationToken cancellationToken = default)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                //if (Image == null)
                //{
                //    return BadRequest("Image Cannot Be null");
                //}
                var AddressResult = await _validationService.IsValidCityAsync(model.CreateClientDTO.Address.City, model.CreateClientDTO.Address.Country);
                if (!AddressResult)
                {
                    return BadRequest("invalid country or city,must input real country and city names");
                }
                var zipCodeResult = await _validationService.IsValidZipCodeAsync(model.CreateClientDTO.Address.ZipCode, model.CreateClientDTO.Address.City);
                if (!zipCodeResult)
                {
                    return BadRequest("invalid zipcode for given city ,must input real zipcode and city");
                }
                var phoneNumberResult = _validationService.IsValidPhoneNumber(model.CreateClientDTO.MobileNumber, model.RegionCode);
                if (!phoneNumberResult)
                {
                    return BadRequest("invalid phone number format");
                }
             
                var client = _mapper.Map<Client>(model.CreateClientDTO);
                var imageUrl = await _FileManager.SaveFileAsync(model.Image);
                if (imageUrl != null)
                {
                    client.ProfilePhotoUrl = imageUrl;
                }
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
