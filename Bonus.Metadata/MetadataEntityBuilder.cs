using System;
using System.Collections.Generic;

namespace Bonus
{
    public class MetadataEntityBuilder<T> {

        private List<Action<Dictionary<string, object>>> _actions =
            new List<Action<Dictionary<string, object>>>();

        public MetadataEntityBuilder<T> AddAction(Action<Dictionary<string, object>> action) {
            _actions.Add(action);
            return this;
        }

        internal void ApplyTo(MetaEntityStore store) {
            foreach (var action in _actions) {
                action(store);
            }
        }
    }
}