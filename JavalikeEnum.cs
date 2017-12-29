using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace JavalikeEnums
{
    public abstract class JavalikeEnum<T> where T : JavalikeEnum<T>
    {
        public string Name { get; internal set; }
        public int Ordinal { get; internal set; }

        public static ICollection<object> Values
        {
            get => EnumHelper.Values(typeof(T));
        }

        protected static EnumConstantCreator<T> newConstant([CallerMemberName] string callerName = "")
        {
            return new EnumConstantCreator<T>(callerName);
        }
    }

    public class EnumConstantCreator<U> where U : JavalikeEnum<U>
    {
        private readonly string name;
        private readonly Type enumType;

        internal EnumConstantCreator(String name)
        {
            this.name = name;
            this.enumType = typeof(U);
        }

        public U create(params object[] args)
        {
            StackFrame callerFrame = new StackFrame(1);

            Type declaringType = callerFrame.GetMethod().DeclaringType;
            if (declaringType != enumType) throw new TypeMismatchException(String.Format("The enum constant is of type {0}. This does not match the declaring type {1}", enumType, declaringType));

            CheckModifiers();

            Type[] types = args.Select(o => o.GetType()).ToArray();
            ConstructorInfo matchingCtor = enumType.GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, types, null);
            if (matchingCtor == null)
                throw new InvalidOperationException("class " + enumType + " does not have a constructor that takes args: " + String.Join(", ", (object[])types));
            
            JavalikeEnum<U> constant = (JavalikeEnum<U>) matchingCtor.Invoke(args);
            constant.Name = this.name;
            constant.Ordinal = EnumHelper.GetNextOrdinal(enumType);
            
            EnumHelper.AddValue(enumType, constant.Name, constant);
            return (U) constant;
        }

        private void CheckModifiers()
        {
            FieldInfo enumConstantField = enumType.GetField(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);

            if (enumConstantField == null) throw new MissingFieldException(String.Format("Could not find a field named {0} in the type {1}", name, enumType));

            if (!enumConstantField.IsPublic) throw new InvalidModifiersException(InvalidModifiersException.InvalidModifierType.PRIVATE, "Invalid modifier 'private'. Enum constants must be public static readonly.");
            if (!enumConstantField.IsStatic) throw new InvalidModifiersException(InvalidModifiersException.InvalidModifierType.INSTANCE_MEMBER, "Missing modifier 'static'. Enum constants must be public static readonly.");
            if (!enumConstantField.IsInitOnly) throw new InvalidModifiersException(InvalidModifiersException.InvalidModifierType.MUTABLE, "Missing modifier 'readonly'. Enum constants must be public static readonly.");
        }
    }
}
