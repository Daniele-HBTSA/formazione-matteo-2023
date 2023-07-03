using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBTSA.Libraries.ExtensionMethods
{
    public static class ListExtension
    {
        //Il nome del metodo è fuorviante. Ritorna true se almeno un elemento è presente
        public static bool ContainsAllItems<T>(this List<T> a, List<T> b)
        {
            return !b.Except(a).Any();
        }

        public static string ToFlat(this List<int> a)
        {
            List<string> result = new List<string>();
            a.ForEach(item => {
                result.Add(item.ToString());
            });
            return ToFlat(result);
        }

        public static string ToFlat(this List<int?> a)
        {
            List<string> result = new List<string>();
            a.ForEach(item => {
                if (item.HasValue) result.Add(item.Value.ToString());                
            });
            return ToFlat(result);
        }

        public static string ToFlat(this List<string> a)
        {
            string flat = ",";
            a.ForEach(item => {
                if (!string.IsNullOrWhiteSpace(item)) flat = $"{flat},{item}";
            });
            return flat.Replace(",,","");    
        }
    }
}
