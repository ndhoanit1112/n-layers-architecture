using System;
using System.ComponentModel;
using System.Reflection;

namespace NC.Common.Helpers
{
    public static class EnumHelper
    {
        public static string GetEnumDescription(Enum value)
        {
            try
            {
                FieldInfo fi = value.GetType().GetField(value.ToString());

                DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attributes.Length > 0)
                {
                    return attributes[0].Description;
                }

                return value.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return string.Empty;
            }
        }

        //public static IEnumerable<SelectListItem> ToSelectListItems<T>()
        //{
        //    return Enum.GetValues(typeof(T)).Cast<Enum>().Select(x => new SelectListItem
        //    {
        //        Text = Text.Get(GetEnumDescription(x)),
        //        Value = Convert.ChangeType(x, TypeCode.Int32)?.ToString()
        //    }).ToList();
        //}
    }
}
