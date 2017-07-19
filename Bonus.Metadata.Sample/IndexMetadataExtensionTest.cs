using System;
using System.Collections.Generic;
using Xunit;

namespace Bonus
{
    public class IndexMetadataExtensionTest {

        interface IEntity {
            DateTime DateTime { get; set; }
        }
        class Entity {
            public DateTime DateTime { get; set; }
        }

        interface A {
            int Number { get; set; }
        }

        class AEntity : Entity, A {
            public int Number {get;set;}
        }


        [Fact]
        public void Lifecycle() {
            var metadata = new Metadata();
            new MetadataBuilder()
                .RegisterEntity<IEntity>(e => e.Index(en => en.DateTime, 1))
                .RegisterEntity<Entity>(e => e.Index(en => en.DateTime, 2))
                .RegisterEntity<A>(e => e.Index(en => en.Number, 3))
                .RegisterEntity<AEntity>(e => e.Index(en => en.DateTime, 4))
                .ApplyTo(metadata);

            var expected = new Dictionary<string, int>(){
                { "DateTime", 4 },
                { "Number", 3 }
            };
            var actual = metadata.GetIndexes<AEntity>();

            Assert.Equal(expected.Count, actual.Count);
            Assert.Equal(expected["DateTime"], actual["DateTime"]);
            Assert.Equal(expected["Number"], actual["Number"]);
        }
    }
}