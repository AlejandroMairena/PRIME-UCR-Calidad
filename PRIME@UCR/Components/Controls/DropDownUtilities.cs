using System;
using System.Collections.Generic;
using System.Linq;

namespace PRIME_UCR.Components.Controls
{
    public static class DropDownUtilities
    {
        public static List<Tuple<T, string>> LoadAsTupleList<T>(IEnumerable<T> values, string displayProperty)
        {
            return values
                .Select(val =>
                {
                    var t = typeof(T);
                    return Tuple.Create(val, t.GetProperty(displayProperty).GetValue(val) as string);
                    // possible null reference, intentionally unchecked
                })
                .ToList(); 
        }
        
    }
}