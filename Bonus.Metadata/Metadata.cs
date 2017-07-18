using System;
using System.Collections.Generic;

namespace Bonus
{

    public class Metadata {
        private MetadataStore _store = new MetadataStore();

        public IEnumerable<Type> RegisteredTypes => _store.Keys;

        internal MetadataStore Store => _store;

        public T Read<T>(Type type, string key) {
            if (_store.TryGetValue(type, out var metaEntityStore) &&
                metaEntityStore.TryGetValue(key, out var objectValue)) {
                return (T)objectValue;
            }
            return default(T);
        }
    }
}