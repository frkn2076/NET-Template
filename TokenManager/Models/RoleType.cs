using System.Collections.Generic;

namespace TokenManager.Models
{
    public sealed class RoleType
    {
        public static readonly SortedList<string, RoleType> Values = new SortedList<string, RoleType>();
        private readonly string _value;

        public static readonly RoleType Normal = new RoleType("Normal");
        public static readonly RoleType Admin = new RoleType("Admin");


        public RoleType(string value)
        {
            _value = value;
            Values.Add(value, this);
        }

        public static implicit operator string(RoleType value) => value._value;
    }
}
