using System;

namespace ADSeek.Domain.Enums
{
    [Flags]
    public enum ObjectClass
    {
        Top = 1,
        OrganizationalUnit = 2,
        Group = 4,
        Person = 8,
        OrganizationalPerson = 16,
        User = 32,
        Computer = 64, 
        Container = 128
    }
}