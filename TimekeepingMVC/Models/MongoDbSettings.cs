using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimekeepingMVC.Models
{
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }
    }
}