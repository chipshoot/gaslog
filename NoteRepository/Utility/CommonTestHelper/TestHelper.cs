using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;

namespace NoteRepository.Common.TestHelper
{
    public static class TestHelper
    {
        public static object DeepCopy(object src)
        {
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

            if (src == null)
            {
                return null;
            }

            var type = src.GetType();
            if (type.IsValueType || type == typeof(string))
            {
                return src;
            }

            if (type.IsArray)
            {
                var typeName = type.FullName.Replace("[]", string.Empty);
                var fullName = $"{typeName},{type.Assembly.FullName}";
                var elementType = Type.GetType(fullName);
                var array = src as Array;
                var copied = Array.CreateInstance(elementType, array.Length);
                for (var i = 0; i < array.Length; i++)
                {
                    copied.SetValue(DeepCopy(array.GetValue(i)), i);
                }

                return Convert.ChangeType(copied, src.GetType());
            }

            if (type.IsClass)
            {
                var ret = Activator.CreateInstance(src.GetType());
                var fields = type.GetFields(flags);
                foreach (var field in fields)
                {
                    var fieldVal = field.GetValue(src);
                    if (fieldVal == null)
                    {
                        continue;
                    }

                    field.SetValue(ret, DeepCopy(fieldVal));
                }

                return ret;
            }

            throw new ArgumentException("Unknown type");
        }

        private static void ShallowCopy(object dest, object src)
        {
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            var destFields = dest.GetType().GetFields(flags);
            var srcFields = src.GetType().GetFields(flags);

            foreach (var srcField in srcFields)
            {
                var destField = destFields.FirstOrDefault(field => field.Name == srcField.Name);

                if (destField == null || destField.IsLiteral)
                {
                    continue;
                }

                if (srcField.FieldType == destField.FieldType)
                {
                    destField.SetValue(dest, srcField.GetValue(src));
                }
            }

            var destBase = dest.GetType().BaseType;
            var srcBase = src.GetType().BaseType;
            while (destBase != null && srcBase != null)
            {
                destFields = destBase.GetFields(flags);
                srcFields = srcBase.GetFields(flags);

                foreach (var srcField in srcFields)
                {
                    var destField = destFields.FirstOrDefault(field => field.Name == srcField.Name);

                    if (destField != null && !destField.IsLiteral)
                    {
                        if (srcField.FieldType == destField.FieldType)
                            destField.SetValue(dest, srcField.GetValue(src));
                    }
                }

                destBase = destBase.BaseType;
                srcBase = srcBase.BaseType;
            }
        }

        public static IList<T> Clone<T>(this IEnumerable<T> listToCopy) where T : new()
        {
            var newLst = new List<T>();
            foreach (var itm in listToCopy)
            {
                var newObj = new T();
                ShallowCopy(newObj, itm);
                newLst.Add(newObj);
            }

            return newLst;
        }

        /// <summary>
        /// Gets a value indicating whether current test is using real database or just fake data.
        /// <remarks>
        /// the setting should be found from app.config file
        /// <add key="DataEnvironment" value="database" />
        /// if the setting cannot be found or the value is not database, then use fake data as default
        /// </remarks>
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this test is connect to database; otherwise, <c>false</c>.
        /// </value>
        public static bool IsConnectToDatabase
        {
            get
            {
                var setting = ConfigurationManager.AppSettings["DataEnvironment"];
                if (setting == null)
                {
                    return false;
                }

                return setting.Trim().ToLower() == "database";
            }
        }
    }
}