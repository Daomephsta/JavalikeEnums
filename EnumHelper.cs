using System;

namespace JavalikeEnums
{
    public static class EnumHelper
    {
        public static object[] Values(Type type)
        {
            return EnumDataManager.GetValuesInternal(type);
        }

        public static object GetConstant(Type type, string name)
        {
            return EnumDataManager.GetConstantInternal(type, name);
        }

        public static object TryGetConstant(Type type, string name)
        {
            return EnumDataManager.TryGetConstantInternal(type, name);
        }
    }
}
