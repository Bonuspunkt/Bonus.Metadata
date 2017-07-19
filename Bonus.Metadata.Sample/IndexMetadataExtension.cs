using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Bonus
{
    public static class IndexMetadataExtension {

        private static readonly string Key = typeof(IndexMetadataExtension).FullName;

        public static MetadataEntityBuilder<T> Index<T>(this MetadataEntityBuilder<T> builder,
            Expression<Func<T, object>> expression, int index) {
            return builder.AddAction(store => {
                Dictionary<string, int> indexDict = null;
                if (store.TryGetValue(Key, out var indexDictObject)) {
                    indexDict = indexDictObject as Dictionary<string, int>;
                }
                if (indexDict == null) {
                    indexDict = new Dictionary<string, int>();
                    store[Key] = indexDict;
                }
                indexDict.Add(ExpressionUtils.GetPropertyName(expression.Body), index);
            });
        }

        public static Dictionary<string, int> GetIndexes<T>(this Metadata metadata) {
            return metadata.ReadInherited<Dictionary<string, int>>(typeof(T), Key)
                .Reverse()
                .Aggregate(new Dictionary<string, int>(), (prev, curr) => {
                    foreach (var key in curr.Value.Keys) {
                        prev[key] = curr.Value[key];
                    }

                    return prev;
                });
        }
    }
}