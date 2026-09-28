using DomainFramework;
using Tasks.Domain.Tasks;

namespace Tasks.Domain.Tags
{
    public class Tag : EntityBase
    {
        public string Name { get; private set; } = null!;

        public ICollection<TaskTag> TaskTags { get; private set; } = new List<TaskTag>();

        private Tag() { }

        private Tag(string name)
        {
            Name = name;
        }

        public static Tag Create(string name)
        {
            var tag = new Tag(name);
            tag.OnCreate();
            return tag;
        }
    }
}
