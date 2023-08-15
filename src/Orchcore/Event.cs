namespace Orchcore
{
    public abstract class Event : IEvent
    {
        private readonly string _id;
        private readonly string _name;
        private readonly object _data;
        private readonly string _type;
        private readonly DateTime _createdAt;

        public Event(string name, object data)
        {
            _id = Guid.NewGuid().ToString();
            _name = name ?? GetType().Name;
            _data = data;
            _type = GetType().FullName;
            _createdAt = DateTime.Now;
        }

        public string Id => _id;
        public string Name => _name;
        public DateTime CreatedAt => _createdAt;
        public object Data => _data;
        public string Type => _type;
    }
}
