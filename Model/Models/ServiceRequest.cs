﻿using Model.BaseTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class ServiceRequest : BaseEntity, IauditTracker
    {
        public ServiceRequest() { }
        public ServiceRequest(string email) 
        {
            this.RowKey = Guid.NewGuid().ToString();
            this.PartitionKey = email;
        }
        public string VehicleName { get; set; }
        public string VehicleType { get; set; }
        public string Status { get; set; }
        public string RequestedService { get; set; }
        public DateTime? RequestedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public string ServiceEngineer { get; set; }

    }
}
