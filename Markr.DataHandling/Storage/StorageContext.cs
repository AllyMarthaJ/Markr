using Markr.DataHandling.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markr.DataHandling.Storage {
    public class StorageContext : DbContext {
        public DbSet<McqResultDb> Result { get; set; }

        public StorageContext() : base() { }
    }
}
