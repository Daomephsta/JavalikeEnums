using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JavalikeEnums
{
    public static class EnumHelper
    {
        public static ICollection<object> Values(Type type)
        {
            return EnumDataManager.Values(type);
        }
    }
}
