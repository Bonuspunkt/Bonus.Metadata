using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Bonus
{
    public static class PropertyMetadataExtension {

        private static readonly string Key = typeof(PropertyMetadataExtension).FullName;

        public static MetadataEntityBuilder<T> Disable<T>(this MetadataEntityBuilder<T> builder, Expression<Func<T, object>> expression) {
            return builder.AddAction(store => {
                List<string> disabled = null;
                if (store.TryGetValue(Key, out var disabledObject)) {
                    disabled = disabledObject as List<string>;
                }
                if (disabled == null) {
                    disabled = new List<string>();
                    store[Key] = disabled;
                }
                disabled.Add(GetPropertyName(expression.Body));
            });
        }

        private static string GetPropertyName(Expression expression) {
            switch (expression) {
                case UnaryExpression unary:
                    return GetPropertyName(unary.Operand);
                case MemberExpression property:
                    if (!(property.Expression is ParameterExpression)) {
                        throw new Exception();
                    }
                    return property.Member.Name;
                default:
                    throw new NotSupportedException();
            }
        }

        public static bool IsDisabled<T>(this Metadata metadata, Expression<Func<T, object>> expression) {
            return metadata.Read<List<string>>(typeof(T), Key)?.Contains(GetPropertyName(expression.Body)) ?? false;
        }
    }
}