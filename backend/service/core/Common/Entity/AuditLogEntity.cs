using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Common.Entity
{
    public class AuditLogEntity
    {
        public DateTime CreateAt { get; set; } = DateTime.MinValue;
        public DateTime UpdateAt { get; set; }
        public DateTime DeleteAt { get; set; }
        public bool isDeleting { get; set; }
        public List<string> ipAddress { get; set; } = new List<string>();
        
        public Guid UserIdThatMakeChange { get; set; }
        public Guid clientUserID { get; set; }

        public bool isSameClientMakeChange { get; set; } 
        public string Location { get; set; }
       


        public AuditLogEntity() { }

        public AuditLogEntity
            (
            DateTime createAt, DateTime updateAt, DateTime deleteAt, bool isDeleting, 
            List<string> ipAddress, Guid userIdThatMakeChange, Guid clientUserID, 
            bool isSameClientMakeChange,string Location
            )
        {
            CreateAt = createAt;
            UpdateAt = updateAt;
            DeleteAt = deleteAt;
            this.isDeleting = isDeleting;
            this.ipAddress = ipAddress;
            UserIdThatMakeChange = userIdThatMakeChange;
            this.clientUserID = clientUserID;
            this.isSameClientMakeChange = isSameClientMakeChange;
            this.Location = Location;
        }

        public void markAsDelete()
        {
            isDeleting = true;
            DeleteAt = DateTime.MinValue;
            UpdateAt = DateTime.MinValue;
        }

        public void markAsNotDelete()
        {
            isDeleting = false;
            UpdateAt = DateTime.MinValue;
        }


        public bool IsSameClientMakeChange ()
        {
            return (UserIdThatMakeChange == clientUserID);
        }

        public void addIpAddress(string ipAddress)
        {
            this.ipAddress.Add(ipAddress);
        }

        public List<string> GetIpAddreses()
        {
            return ipAddress;
        }



        // MAC Address 


        // location 

    }
}
