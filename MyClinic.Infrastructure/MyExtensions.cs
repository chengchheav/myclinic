using System.Collections.Generic;
using System.Reflection;

namespace MyClinic.Infrastructure
{
    public static class MyExtensions
    {
        public static IEnumerable<string> EnumeratePropertyDifferences<T>(this T obj1, T obj2)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            List<string> changes = new List<string>();

            foreach (PropertyInfo pi in properties)
            {
                object value1 = typeof(T).GetProperty(pi.Name).GetValue(obj1, null);
                object value2 = typeof(T).GetProperty(pi.Name).GetValue(obj2, null);

                if (value1 != value2 && (value1 == null || !value1.Equals(value2)))
                {
                    changes.Add(string.Format("Property {0} changed from {1} to {2}", pi.Name, value1, value2));
                }
            }
            return changes;
        }

        public static string EnumeratePropertyDifferencesInString<T>(this T obj1, T obj2)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            string changes = "";

            foreach (PropertyInfo pi in properties)
            {
                object value1 = typeof(T).GetProperty(pi.Name).GetValue(obj1, null);
                object value2 = typeof(T).GetProperty(pi.Name).GetValue(obj2, null);
                if (value1 == null)
                {
                    value1 = string.Empty;
                }

                if (value2 == null)
                {
                    value2 = string.Empty;
                }

                if (value1 != value2 && (value1 == null || !value1.Equals(value2)))
                {
                    changes += string.Format("Property {0} changed from {1} to {2}, ", pi.Name, value1, value2);
                }
            }

            if (changes.Length > 0)
                changes = changes.Substring(0, changes.Length - 2);
            return changes;
        }
    }
}