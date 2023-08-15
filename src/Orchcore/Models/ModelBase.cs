using System.Runtime.Serialization;

namespace Orchcore.Models
{
    [Serializable]
    public abstract class ModelBase : ISerializable
    {
        public abstract void GetObjectData(SerializationInfo info, StreamingContext context);
    }
}
