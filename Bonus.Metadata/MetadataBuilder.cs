using System;
using System.Collections.Generic;

namespace Bonus
{
    public class MetadataBuilder
    {
        private List<Action<MetadataStore>> actions = new List<Action<MetadataStore>>();

        public MetadataBuilder RegisterEntity<T>(Action<MetadataEntityBuilder<T>> action = null)
        {

            actions.Add((store) =>
            {
                if (store.ContainsKey(typeof(T)))
                {
                    throw new Exception($"{ typeof(T).FullName } already registered");
                }

                var typeData = new MetaEntityStore();
                store.Add(typeof(T), typeData);

                var entityBuilder = new MetadataEntityBuilder<T>();
                if (action != null) { action(entityBuilder); }
                entityBuilder.ApplyTo(typeData);
            });

            return this;
        }

        public MetadataBuilder UpdateEntity<T>(Action<MetadataEntityBuilder<T>> action = null)
        {

            actions.Add(store =>
            {
                if (!store.TryGetValue(typeof(T), out var typeData))
                {
                    throw new Exception($"{ typeof(T).FullName } was not registered yet");
                }

                var entityBuilder = new MetadataEntityBuilder<T>();
                if (action != null) { action(entityBuilder); }
                entityBuilder.ApplyTo(typeData);
            });

            return this;
        }

        public Metadata ApplyTo(Metadata metadata)
        {
            foreach (var action in actions)
            {
                action(metadata.Store);
            }
            return metadata;
        }
    }
}