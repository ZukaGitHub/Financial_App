using Application.Auth.Login;
using Application.Auth.Register;
using AutoMapper;
using Infrastructure.JWT;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Presentation.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Presentation.Controllers
{
    [Route("Api/Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        public AuthController(IMediator mediator, IMapper mapper,ITokenService tokenService)
        {
            _mediator = mediator;
            _mapper = mapper;   
            _tokenService=tokenService;
        }

        [HttpPost("Login")]

        public async Task<IActionResult> Login(LoginDTO model, CancellationToken cancellationToken = default)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                    var command = new LoginCommand() { UserName = model.UserName, Password = model.Password };
                    var result = await _mediator.Send(command);
                    if (result.Errors == null)
                    {
                        return Ok(new NewUserDTO()
                        {
                            UserName=model.UserName,
                            Token=await _tokenService.CreateTokenAsync(result.User)
                            
                        });
                    }
                    return BadRequest("Incorrect Username Or Password");

            }

            
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }

        }
            [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO model, CancellationToken cancellationToken = default)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var command = new RegisterCommand() { Password=model.Password,UserName=model.UserName,Role=model.Role };
                var result = await _mediator.Send(command, cancellationToken);
                if (result.Errors == null)
                {
                    return Ok(new NewUserDTO
                    {
                        Role=model.Role,
                        Token=await _tokenService.CreateTokenAsync(result.User),
                        UserName=model.UserName
                    });
                }
                return BadRequest("Could Not Register User");
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
