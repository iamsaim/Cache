using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheManager.Application.Common.Dto
{
    public class CacheManagerDTO
    {
        public Guid Key { get; set; }
        public dynamic Value { get; set; }
    }
}
