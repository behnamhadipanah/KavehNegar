using ServiceStack.Redis;

namespace KavehNegar.Logic.Context
{

    public interface IRedisContext : System.IDisposable
    {
        IRedisClient context { get; set; }
    }
}
