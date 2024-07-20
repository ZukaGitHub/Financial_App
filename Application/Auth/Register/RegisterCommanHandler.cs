using Domain.Entities;
using Domain.SharedModels;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Auth.Register
{
    public class RegisterCommanHandler : IRequestHandler<RegisterCommand,RegisterResponseModel>
    {
        private readonly UserManager<User> _userManager;
        public RegisterCommanHandler(UserManager<User> userManager) {

            _userManager = userManager;

        }


        public async Task<RegisterResponseModel> Handle (RegisterCommand command,CancellationToken cancellationToken)
        {
            try
            {
                var user = new User
                {
                    UserName = command.UserName,

                };
                var userResult= await _userManager.CreateAsync(user,command.Password);
                if (userResult.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, command.Role);
                    if(roleResult.Succeeded)
                    {
                        return new RegisterResponseModel { User = await _userManager.FindByNameAsync(command.UserName) };
                    };
                    
                }
                return new RegisterResponseModel { Errors=new List<string>() { "Could Not Create User"} };
            }
            catch(Exception ex) 
            {
                return new RegisterResponseModel { Errors = new List<string>() { ex.Message } };
            }

        }
    }
}
