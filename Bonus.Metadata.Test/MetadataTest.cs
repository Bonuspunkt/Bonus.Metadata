using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Bonus
{
    public class MetadataTest {

        [Fact]
        public void Simple() {
            var metadata = new Metadata();

            new MetadataBuilder()
                .RegisterEntity<Entity>()
                .ApplyTo(metadata);
            
            Assert.True(metadata.RegisteredTypes.Contains(typeof(Entity)));
        }

        [Fact]
        public void ShouldThrowWhenUpdateAndNotRegistered() {
            var metadata = new Metadata();
            
            var builder = new MetadataBuilder()
                .UpdateEntity<Entity>();
            
            Assert.Throws<Exception>(() => builder.ApplyTo(metadata));
        }

        [Fact]
        public void ShouldThrowOnDoubleRegister() {
            var metadata = new Metadata();

            var builder = new MetadataBuilder()
                .RegisterEntity<Entity>();

            builder.ApplyTo(metadata);
            Assert.Throws<Exception>(() => builder.ApplyTo(metadata));
        }
    }
}