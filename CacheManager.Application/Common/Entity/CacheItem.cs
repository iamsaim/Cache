using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheManager.Application.Common.Entity
{
    public class CacheItem
    {
        public DateTimeOffset LastUsed { get; set; }
        public dynamic Value { get; set; }
    }
}
