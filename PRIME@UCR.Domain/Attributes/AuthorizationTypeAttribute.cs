using System;

namespace PRIME_UCR.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class AuthorizationTypeAttribute : Attribute
    {
        public Type AuthorizationClassType { get; private set; } 
        
        public AuthorizationTypeAttribute(Type authorizationType)
        {
            if (authorizationType == null)
                throw new ArgumentException("AuthorizationType cannot be null");
            AuthorizationClassType = authorizationType;
        }
    }
}