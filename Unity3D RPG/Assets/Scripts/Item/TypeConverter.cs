using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;
using System.ComponentModel;

namespace CSVReader
{
    //-------------------------------------------------------------------------------------------------
    public static class TypeConverter
    {
        public static T ConvertType<T>(string value)
        {
            if (typeof(T).IsEnum)
                return (T)Enum.Parse(typeof(T), value);

            System.ComponentModel.TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
            if (converter != null && converter.CanConvertFrom(value.GetType()))
            {
                return (T)converter.ConvertFrom(value);
            }
            else
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
        }

        //-------------------------------------------------------------------------------------------------
        public static object ConvertType(string value, Type type)
        {
            if (type.IsEnum)
                return Enum.Parse(type, value);

            System.ComponentModel.TypeConverter converter = TypeDescriptor.GetConverter(type);
            if(converter != null && converter.CanConvertFrom(value.GetType()))
            {
                return converter.ConvertFrom(value);
            }
            else
            {
                return Convert.ChangeType(value, type);
            }
        }
    }
}
