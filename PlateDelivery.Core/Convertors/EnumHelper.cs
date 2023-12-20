using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateDelivery.Core.Convertors
{
    public static class EnumHelper
    {
        public static string GetDescription<T>(this T enumValue) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum) return null;
            var description = enumValue.ToString();
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            if (fieldInfo != null)
            {
                var attrs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attrs != null && attrs.Length > 0)
                {
                    description = ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            return description;
        }

        public static SelectList GetSelectList<T>(this T enumValue) where T : struct, IConvertible
        {

            return new SelectList(Enum.GetValues(typeof(T)).Cast<T>().Select(v => new SelectListItem
            {
                Text = v.GetDescription(),
                Value = v.ToString()
            }).ToList(), "Value", "Text");

        }
    }
}
