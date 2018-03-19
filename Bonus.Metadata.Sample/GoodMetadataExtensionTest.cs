using Xunit;

namespace Bonus
{
    public class GoodMetadataExtensionTest
    {

        class Entity { }

        [Fact]
        public void Lifecycle()
        {
            var metadata = new Metadata();
            new MetadataBuilder()
                .RegisterEntity<Entity>(
                    e => e.IsGood()
                )
                .ApplyTo(metadata);

            Assert.True(metadata.IsGood<Entity>());
        }

        [Fact]
        public void Lifecycle2()
        {
            var metadata = new Metadata();
            new MetadataBuilder()
                .RegisterEntity<Entity>(
                    e => e.IsGood()
                )
                .UpdateEntity<Entity>(
                    e => e.IsGood(false)
                )
                .ApplyTo(metadata);

            Assert.False(metadata.IsGood<Entity>());

        }
    }
}