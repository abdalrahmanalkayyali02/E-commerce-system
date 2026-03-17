using Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Persistence.Model
{
    public class UserDataModel
    {
        public Guid id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public string phoneNumber { get; set; } 
        public string password { get; set; }
        public UserRole Role {  get; set; }
        public AccountStatus AccountStatus { get; set; }
        public string profilePhoto { get; set; }
        

        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public bool isDelete { get; set; } = false;
        public DateTime DeleteAt { get; set; }


    }
}
