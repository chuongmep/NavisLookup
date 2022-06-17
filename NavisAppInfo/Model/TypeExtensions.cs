namespace AppInfo.Model
{
    internal static class TypeExtensions
    {
        /// <summary>
        /// Get info implement of the type.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="intrface"></param>
        /// <returns></returns>
        public static bool IsImplementationOf(this Type type, Type intrface)
        {
            foreach (Type impl in type.GetInterfaces())
            {
                if (intrface == impl)
                    return true;
            }

            return false;
        }

        public static bool IsRelatedTo(this Type tested_type, ref Type related_type)
        {
            while (tested_type != typeof(object))
            {
                if (tested_type == related_type ||
                    (tested_type.IsGenericType && tested_type.GetGenericTypeDefinition() == related_type))
                {
                    related_type = tested_type;
                    return true;
                }

                tested_type = tested_type.BaseType;
            }

            return false;
        }
    }
}