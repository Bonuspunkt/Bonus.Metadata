using System;
using Xunit;

namespace Bonus
{
    public class PropertyMetadataExtensionTest
    {

        class Entity
        {
            public DateTime DateTime { get; set; }
        }

        [Fact]
        public void Lifecycle()
        {
            var metadata = new Metadata();
            new MetadataBuilder()
                .RegisterEntity<Entity>(
                    e => e.Disable(en => en.DateTime)
                )
                .ApplyTo(metadata);

            Assert.True(metadata.IsDisabled<Entity>(e => e.DateTime));
        }
    }
}