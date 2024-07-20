using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.SharedModels
{
    public class ResponseModel
    {
        public List<string> Errors { get; set; }
    }
    public class LoginResponseModel:ResponseModel 
    {
        public User User { get; set; }
    }
    public class RegisterResponseModel : ResponseModel
    {
        public User User { get; set; }
    }
    public class CreateClientResponseModel : ResponseModel
    {
        public bool IsCreated { get; set; }
    }
}
