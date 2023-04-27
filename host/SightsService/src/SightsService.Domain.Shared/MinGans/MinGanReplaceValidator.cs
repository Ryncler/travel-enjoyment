﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace SightsService.MinGans
{

    [Obsolete]
    public class MinGanReplaceValidator : IMinGanReplaceValidator
    {
        public void Replace(object value, ValidationContext validationContext)
        {
            if (value is string v)
            {
                if (!String.IsNullOrEmpty(v))
                {
                    v = MinGanProvider.Instance.IllegalWordsSearch.Replace(v);
                    SetPropertyByName(validationContext.ObjectInstance, validationContext.MemberName, v);
                }
            }
        }

        static bool SetPropertyByName(object obj, string name, object value)
        {
            var type = obj.GetType();
            var prop = type.GetProperty(name, BindingFlags.Public | BindingFlags.Instance);
            if (null == prop || !prop.CanWrite) return false;
            prop.SetValue(obj, value, null);
            return true;
        }
    }
}
