namespace KavehNegar.Logic.Contracts;

public interface IWriteStructure<T>
{
    bool Write(T entity);
}