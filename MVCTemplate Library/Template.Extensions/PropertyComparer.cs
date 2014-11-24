﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Template.Extensions
{
    //Borrowed from http://stackoverflow.com/questions/11345854/implementing-iequalitycomparert-for-comparing-arbitrary-properties-of-any-clas

    public class PropertyComparer<T> : IEqualityComparer<T>
    {
        private readonly PropertyInfo propertyToCompare;

        public PropertyComparer(string propertyName)
        {
            propertyToCompare = typeof(T).GetProperty(propertyName);
        }
        public bool Equals(T x, T y)
        {
            object xValue = propertyToCompare.GetValue(x, null);
            object yValue = propertyToCompare.GetValue(y, null);
            return xValue.Equals(yValue);
        }

        public int GetHashCode(T obj)
        {
            object objValue = propertyToCompare.GetValue(obj, null);
            return objValue.GetHashCode();
        }
    }
}
