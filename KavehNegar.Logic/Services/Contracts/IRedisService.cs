using KavehNegar.Logic.Contracts;

namespace KavehNegar.Logic.Services.Contracts
{
    public interface IRedisService<T>:IReadStructure<List<T>>,IWriteStructure<T>
    {
    }
}
