using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FileSorter.Models
{
    public static class OptionsGetter
    {
        public static List<string> GetOptions(Type type)
        {
            var options = new List<string>();
            FieldInfo[] staticFields = type.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (FieldInfo field in staticFields)
            {
                object value = field.GetValue(null);
                options.Add(value.ToString());
            }

            return options;
        }
    }
}
