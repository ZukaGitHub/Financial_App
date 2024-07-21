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
    public class GetClientReponseModel : ResponseModel
    {
        public Client Client { get; set; }
    }
    public class DeleteClientResponseModel : ResponseModel
    {
        public bool IsDeleted { get; set; }
    }
    public class UpdateClientResponseModel : ResponseModel
    {
        public bool IsUpdated { get; set; }
    }
}
