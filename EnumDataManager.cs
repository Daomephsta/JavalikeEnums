﻿using System;
using System.Collections.Generic;
using System.Reflection;

namespace JavalikeEnums
{
    public static class EnumDataManager
    {
        private static readonly IDictionary<Type, int> TYPE_TO_ORDINAL = new Dictionary<Type, int>();
        private static readonly IDictionary<Type, ConstantStore> TYPE_TO_CONSTANTS = new Dictionary<Type, ConstantStore>();

        internal static int GetNextOrdinal(Type type)
        {
            if (!TYPE_TO_ORDINAL.ContainsKey(type))
            {
                //Add 1 to the dict, but return 0, because the dict stores the next ordinal
                TYPE_TO_ORDINAL.Add(type, 1);
                return 0;
            }
            int next = TYPE_TO_ORDINAL[type];

            TYPE_TO_ORDINAL[type] = TYPE_TO_ORDINAL[type] + 1;
            return next;
        }

        internal static object GetConstant(Type type, String constantName)
        {
            return GetStore(type)[constantName];
        }

        internal static void AddValue(Type type, string name, object value)
        {
            GetStore(type).AddConstant(name, value);
        }

        public static ICollection<object> Values(Type type)
        {
            return GetStore(type).Values;
        }

        private static ConstantStore GetStore(Type type)
        {
            if (!TYPE_TO_CONSTANTS.ContainsKey(type))
            {
                ConstantStore store = new ConstantStore(type);
                TYPE_TO_CONSTANTS[type] = store;
                return store;
            }
            return TYPE_TO_CONSTANTS[type];
        }

        private class ConstantStore
        {
            internal ICollection<object> Values
            {
                get => NAME_TO_CONSTANT.Values;
            }

            private readonly IDictionary<string, object> NAME_TO_CONSTANT = new Dictionary<string, object>();

            internal object this[string name]
            {
                get => NAME_TO_CONSTANT[name];
            }

            private readonly Type type;

            public ConstantStore(Type type)
            {
                this.type = type;
            }

            internal void AddConstant(string name, object value)
            {
                NAME_TO_CONSTANT.Add(name, value);
            }
        }
    }
}
