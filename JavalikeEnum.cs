using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace JavalikeEnums
{
    public abstract class JavalikeEnumBase { }

    public abstract class JavalikeEnum<T> : JavalikeEnumBase where T : JavalikeEnumBase
    {
        public string Name { get; internal set; }
        public int Ordinal { get; internal set; }

        protected static EnumConstantCreator<T> newConstant([CallerMemberName] string callerName = "")
        {
            return new EnumConstantCreator<T>(callerName);
        }

        public class EnumConstantCreator<U> where U : JavalikeEnumBase
        {
            private static int nextOrdinal = 0;
            private static Type currentType;

            private readonly string name;

            internal EnumConstantCreator(String name)
            {
                this.name = name;
            }

            public U create(params object[] args)
            {
                Type enumType = typeof(U);
                if (enumType != currentType)
                {
                    currentType = enumType;
                    nextOrdinal = 0;
                }

                Type[] types = args.Select(o => o.GetType()).ToArray();
                ConstructorInfo matchingCtor = enumType.GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, types, null);
                if (matchingCtor == null)
                    throw new InvalidOperationException("class " + enumType + " does not have a constructor that takes args: " + String.Join(", ", (object[])types));

                JavalikeEnum<U> constant = (JavalikeEnum<U>)matchingCtor.Invoke(args);
                constant.Name = this.name;
                constant.Ordinal = nextOrdinal++;

                U constantU = constant as U;
                if (constantU == null)
                    /*TODO: Throw exception*/
                    ;
                return constantU;
            }
        }
    }
}
