﻿using Model.BaseTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class MasterDataKey : BaseEntity
    {
        public MasterDataKey() { }
        public MasterDataKey(string key) 
        {
            this.RowKey = Guid.NewGuid().ToString();
            this.PartitionKey = key;
        }
    }
}
