using Application.Auth.Register;
using Domain.Entities;
using Domain.SharedModels;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Auth.Login
{
    public class LoginCommanHandler : IRequestHandler<LoginCommand, LoginResponseModel>
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public LoginCommanHandler(UserManager<User> userManager, SignInManager<User> signInManager)
        {

            _userManager = userManager;
            _signInManager = signInManager;
        }


        public async Task<LoginResponseModel> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(command.UserName);
                if (user == null)
                {
                    return new LoginResponseModel { Errors = new List<string>() { "Incorrect User name or Password" } };
                }
                var result = await _signInManager.CheckPasswordSignInAsync(user, command.Password, false);
                if(!result.Succeeded)
                {
                    return new LoginResponseModel { Errors = new List<string>() { "Incorrect User name or Password" } };
                   
                }
                return new LoginResponseModel { User = user };
            }
            catch(Exception ex)
            {
                return new LoginResponseModel { Errors = new List<string>() { ex.Message } };
            }

        }
    }
}
