using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace JavalikeEnums
{
    [Serializable]
    public class TypeMismatchException : Exception
    {
        public TypeMismatchException() { }

        public TypeMismatchException(String message) : base(message) { }

        public TypeMismatchException(String message, Exception innerException) : base(message, innerException) { }

        protected TypeMismatchException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        override public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
    
    [Serializable]
    public class InvalidModifiersException : Exception
    {
        public InvalidModifierType ModifierType { get; private set; }

        public InvalidModifiersException(InvalidModifierType modifierType)
        {
            this.ModifierType = modifierType;
        }

        public InvalidModifiersException(InvalidModifierType modifierType, String message) : base(message)
        {
            this.ModifierType = modifierType;
        }

        public InvalidModifiersException(InvalidModifierType modifierType, String message, Exception innerException) : base(message, innerException)
        {
            this.ModifierType = modifierType;
        }

        protected InvalidModifiersException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            this.ModifierType = (InvalidModifierType) info.GetValue("modifier_type", typeof(InvalidModifierType));
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        override public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("modifier_type", ModifierType, typeof(InvalidModifierType));
        }

        public enum InvalidModifierType
        {
            PRIVATE, INSTANCE_MEMBER, MUTABLE
        }
    }
}
