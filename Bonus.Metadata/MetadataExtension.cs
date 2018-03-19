using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Bonus
{
    public static class MetadataExtension
    {
        public static IEnumerable<(Type Type, T Value)> ReadInherited<T>(this Metadata metadata, Type type, string key)
        {
            var loopType = type;
            do
            {
                if (metadata.Read<T>(loopType, key, out var value))
                {
                    yield return (loopType, value);
                }


                var baseType = loopType.GetTypeInfo().BaseType;
                if (baseType == null) { yield break; }

                var subInterfaces = loopType.GetTypeInfo().GetInterfaces();
                var superInterfaces = baseType.GetTypeInfo().GetInterfaces();

                var addedInterfaces = subInterfaces.Where(@interface => !superInterfaces.Contains(@interface));
                foreach (var @interface in addedInterfaces)
                {
                    if (metadata.Read<T>(@interface, key, out var interfaceValue))
                    {
                        yield return (@interface, interfaceValue);
                    }
                }

                loopType = baseType;
            } while (true);
        }
    }
}