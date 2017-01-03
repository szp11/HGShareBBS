using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using HGShare.Common.Attributes;

namespace HGShare.Common
{
    /// <summary>
    /// 
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// 获取枚举类型的文本描述
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static string GetEnumDescription(this Enum item)
        {
            FieldInfo field = item.GetType().GetField(item.ToString());
            object[] objAttrs = field.GetCustomAttributes(typeof(EnumDescriptionAttribute), true);

            if (objAttrs.Length > 0)
            {
                var enumTextAttribute = objAttrs[0] as EnumDescriptionAttribute;
                if (enumTextAttribute != null) return enumTextAttribute.Description;
            }
            return string.Empty;
        }
        /// <summary>
        /// 获取对象的Value与Description
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Dictionary<string, string> GetValueFromDescriptions<T>()
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            var enumsDescription = new Dictionary<string, string>();
            foreach (var field in type.GetFields())
            {
                object[] attribute = field.GetCustomAttributes(typeof(EnumDescriptionAttribute), true);
                if (attribute.Length > 0)
                {
                    var value = field.GetValue(null);
                    enumsDescription.Add(((EnumDescriptionAttribute)attribute[0]).Description, value == null ? "" : Convert.ToInt32(value).ToString());
                }
            }
            return enumsDescription;
        }
    }
}
