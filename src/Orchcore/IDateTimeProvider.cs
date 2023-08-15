namespace Orchcore
{
    public interface IDateTimeProvider
    {
        virtual DateTime GetDate() => DateTime.Now;
    }
}
