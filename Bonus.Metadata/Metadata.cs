using System;
using System.Collections.Generic;
using System.Reflection;

namespace Bonus
{

    public class Metadata {
        private MetadataStore _store = new MetadataStore();

        public IEnumerable<Type> RegisteredTypes => _store.Keys;

        internal MetadataStore Store => _store;

        public bool Read<T>(Type type, string key, out T value) {
            if (_store.TryGetValue(type, out var metaEntityStore) &&
                metaEntityStore.TryGetValue(key, out var objectValue)) {
                value = (T)objectValue;
                return true;
            }
            value = default(T);
            return false;
        }
    }
}