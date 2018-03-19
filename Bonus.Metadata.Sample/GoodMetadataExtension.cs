using System;

namespace Bonus
{
    public static class GoodMetadataExtension
    {

        private static readonly string Key = typeof(GoodMetadataExtension).FullName;
        public static MetadataEntityBuilder<T> IsGood<T>(this MetadataEntityBuilder<T> builder, bool isGood = true)
        {
            return builder.AddAction(store => store[Key] = isGood);
        }

        public static bool IsGood<T>(this Metadata metadata)
        {
            return metadata.IsGood(typeof(T));
        }

        public static bool IsGood(this Metadata metadata, Type type)
        {
            if (metadata.Read<bool>(type, Key, out var value))
            {
                return value;
            }
            return true;
        }
    }
}