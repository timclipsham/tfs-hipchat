using TfsHipChat.Tfs.Events;
using Xunit;

namespace TfsHipChat.Tests.Tfs.Events
{
    public class WorkItemChangedEventTests
    {
        [Fact]
        public void GetFieldOrNull_ShouldReturnField_WhenFieldIsInCoreFieldsOrChangedFields()
        {
            var changedEvent = new WorkItemChangedEvent()
                {
                    CoreFields = new FieldGroup()
                        {
                            StringFields = new[] {new Field {Name = "coreString"}},
                            IntegerFields = new[] {new Field {Name = "coreInt"}},
                        },
                    ChangedFields = new FieldGroup()
                        {
                            StringFields = new[] {new Field {Name = "changedString"}},
                            IntegerFields = new[] {new Field {Name = "changedInt"}},
                        }
                };
            Assert.Null(changedEvent.GetFieldOrNull("notAField"));
            Assert.NotNull(changedEvent.GetFieldOrNull("coreString"));
            Assert.NotNull(changedEvent.GetFieldOrNull("coreInt"));
            Assert.NotNull(changedEvent.GetFieldOrNull("changedString"));
            Assert.NotNull(changedEvent.GetFieldOrNull("changedInt"));
        }
    }
}
