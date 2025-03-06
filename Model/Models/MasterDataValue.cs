using Model.BaseTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class MasterDataValue : BaseEntity,IauditTracker
    {
        public MasterDataValue() { }    
        public MasterDataValue(string masterDataPartitionKey, string value) 
        {
            this.PartitionKey = masterDataPartitionKey;
            this.RowKey = value;
        }    
        public bool IsActive { get; set; }
        public string Name { get; set; }
    }
}
