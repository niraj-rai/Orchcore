namespace Orchcore
{
    public interface IEvent
    {
        string Id { get; }
        string Name { get; }
        object Data { get; }
        string Type { get; }
        DateTime CreatedAt { get; }
    }
}
