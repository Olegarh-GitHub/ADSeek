using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ADSeek.Domain.Enums;
using Novell.Directory.Ldap;

namespace ADSeek.Domain.Extensions
{
    public static class LdapAttributeSetExtensions
    {
        public static ObjectClass? ToObjectClass(this LdapAttribute ldapAttribute)
        {
            if (ldapAttribute.Name is not "objectClass")
                return null;

            var textInfo = new CultureInfo("en-us").TextInfo;


            var ldapObjectClasses = ldapAttribute.StringValueArray.Select(x =>
            {
                var firstLetter = x[0];
                return textInfo.ToTitleCase(firstLetter.ToString().ToUpper()) + x.Substring(1);
            }).ToList();

            var objectClassesStringVariants = string.Join(",", ldapObjectClasses);

            return Enum.Parse<ObjectClass>(objectClassesStringVariants);
        }

        public static IEnumerable<string> ToLDAPObjectClass(this ObjectClass objectClass)
        {
            var objectClasses = objectClass.ToString().Split(',');

            return objectClasses;
        }

        public static UserAccountControl? ToUserAccountControl(this LdapAttribute ldapAttribute)
        {
            if (ldapAttribute.Name is not "userAccountControl")
                return null;

            var ldapUserAccountControl = Convert.ToInt32(ldapAttribute.StringValue);

            return (UserAccountControl) ldapUserAccountControl;
        }

        public static DateTime? ToDateTime(this LdapAttribute ldapAttribute)
        {
            var ldapDateTime = ldapAttribute.StringValue;

            if (ldapDateTime.Length < 16)
                return null;

            var convertedDateTime
                = $"{ldapDateTime.Substring(6, 2)}" +
                  $".{ldapDateTime.Substring(4, 2)}" +
                  $".{ldapDateTime.Substring(0, 4)}" +
                  $" {ldapDateTime.Substring(8, 2)}" +
                  $":{ldapDateTime.Substring(10, 2)}";

            var canConvertDoDateTime = DateTime.TryParse(convertedDateTime, out var dateTime);

            if (!canConvertDoDateTime)
                return null;

            return dateTime;
        }
    }
}