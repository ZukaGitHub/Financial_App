﻿using AutoMapper;
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
using Application.Client.GetClient;
using System.Threading;
using Application.Client.DeleteClient;
using Application.Client.UpdateClient;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Presentation.Models.SearchEngine;
using Application.SearchEngine;
using Microsoft.AspNetCore.Http.HttpResults;
using Application.Client.GetClientList;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.Controllers
{
    [Route("Api/Client")]
    [ApiController]
    [Authorize(Policy = "RequireAdminClaim")]
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

        [HttpGet("Get")]
        public async Task<IActionResult> GetClient(int Id,CancellationToken cancellationToken)
        {
            try
            {
                var command=new GetClientCommand() {  Id = Id };
                var result = await _mediator.Send(command, cancellationToken);
                if (result.Client != null)
                {
                    return Ok(result.Client);
                }
                return BadRequest(result.Errors);
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost("GetListWithSearchEngine")]
      
        public async Task<IActionResult> GetClientListWithSearchEngine(SearchEngineDTO model,CancellationToken cancellationToken)
        {
            try
            {
                var searchEngine = new SearchEngine();
                searchEngine.UserId= HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
               
                if (model == null || !SearchHelper.HasAnySearchCriteria(model))
                {
                   
                    searchEngine.PageNumber = 1;
                    searchEngine.PageSize = 9;
                   
                }
                else
                {
                    searchEngine = _mapper.Map<SearchEngine>(model);
                   
                        if (searchEngine.PageNumber == null|| searchEngine.PageNumber==0 || searchEngine.PageNumber <0)
                        {
                            searchEngine.PageNumber = 1;
                        }
                        if (searchEngine.PageSize == null || searchEngine.PageSize == 0 || searchEngine.PageSize < 0)
                        {
                            searchEngine.PageSize = 9;
                        }                 
                }
                searchEngine.SearchDate = DateTime.Now;
                var command=new GetClientListWithSearchEngineCommand() {SearchEngine=searchEngine};
                var result = await _mediator.Send(command, cancellationToken);
                if(result.Clients!=null && result.Clients.Count > 0)
                {
                    return Ok(result);
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
        [HttpPost("Create")]
      
        public async Task<IActionResult> CreateClient([FromForm]CreateClientDTOWithRegionCode model, CancellationToken cancellationToken = default)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }              
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
                var accounts = _mapper.Map<List<Account>>(model.AccountDTOForCreate);
                client.Accounts= accounts;
                var guid = Guid.NewGuid().ToString().Substring(0, 11);
                client.PersonalId= guid;

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
        [HttpPut("Update")]
   
        public async Task<IActionResult> UpdateClient([FromForm] UpdateClientWithImageAndRegionCode model,CancellationToken cancellationToken)
        {
           
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var client = new Client();
                if (model.UpdateClientDTO != null)
                {
                    if (model.UpdateClientDTO.Address != null)
                    {

                        var AddressResult = await _validationService.IsValidCityAsync(model.UpdateClientDTO.Address.City, model.UpdateClientDTO.Address.Country);
                        if (!AddressResult)
                        {
                            return BadRequest("invalid country or city,must input real country and city names");
                        }
                        var zipCodeResult = await _validationService.IsValidZipCodeAsync(model.UpdateClientDTO.Address.ZipCode, model.UpdateClientDTO.Address.City);
                        if (!zipCodeResult)
                        {
                            return BadRequest("invalid zipcode for given city ,must input real zipcode and city");
                        }

                    }
                    if (model.UpdateClientDTO.MobileNumber != null && model.RegionCode != null)
                    {
                        var phoneNumberResult = _validationService.IsValidPhoneNumber(model.UpdateClientDTO.MobileNumber, model.RegionCode);
                        if (!phoneNumberResult)
                        {
                            return BadRequest("invalid phone number format");
                        }
                    }
                    if ((model.UpdateClientDTO.MobileNumber != null && model.RegionCode == null) || (model.UpdateClientDTO.MobileNumber == null && model.RegionCode != null))
                    {
                        return BadRequest("MobileNumber and RegionCode Have to be provided together,or not at all");
                    }
                    client = _mapper.Map<Client>(model.UpdateClientDTO);
                }

                if (model.Image != null)
                {
                    var imageUrl = await _FileManager.SaveFileAsync(model.Image);
                    if (imageUrl != null)
                    {
                        client.ProfilePhotoUrl = imageUrl;
                    }
                }
                if (!string.IsNullOrEmpty(model.PersonalId))
                {
                    client.PersonalId = model.PersonalId;
                }
                if(model.AccountsDTO != null && model.AccountsDTO.Count>0) 
                {
                    var accounts=_mapper.Map<List<Account>>(model.AccountsDTO);
                    client.Accounts=accounts;              
                }
                var command = new UpdateClientCommand()
                {
                    UpdatedClient = client
                };
                var result = await _mediator.Send(command, cancellationToken);
                if (result.IsUpdated)
                {
                    return Ok("Client Updated successfully");
                }
                return BadRequest(result.Errors);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("Delete")]
     
        public async Task<IActionResult> DeleteClient(int Id,CancellationToken cancellationToken)
        {
            try
            {
                var command = new DeleteClientCommand() { Id = Id };
                var result = await _mediator.Send(command, cancellationToken);
                if (result.IsDeleted)
                {
                    return Ok("Client Was Deleted");
                }
                return BadRequest(result.Errors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }      
    }
}
