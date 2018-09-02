using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BetaViews.Core.Framework.Extension
{
    public static class Enums
    {

        /// <summary>
        /// retorna o enum e o valor em string do enum
        /// ex. var stuff = typeof(Models.Enuns.PropertyType).GetEnumValuesWithDescription<Models.Enuns.PropertyType>();
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IDictionary<T, string> GetEnumValuesWithDescription<T>(this Type type) where T : struct, IConvertible
        {
            if (!type.IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            return type.GetEnumValues()
                    .OfType<T>()
                    .ToDictionary(
                        key => key,
                        val => (val as Enum).GetDescription()
                    );
        }


        /// <summary>
        /// obtem apenas a 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    DescriptionAttribute attr = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                    if (attr != null)
                    {
                        return attr.Description;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// obtem a lista de enums
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>

        public static IEnumerable<string> GetDescriptions(Type type)
        {
            var descs = new List<string>();
            var names = Enum.GetNames(type);
            foreach (var name in names)
            {
                var field = type.GetField(name);
                var fds = field.GetCustomAttributes(typeof(DescriptionAttribute), true);
                foreach (DescriptionAttribute fd in fds)
                {
                    descs.Add(fd.Description);
                }
            }
            return descs;
        }        

    }
}
