using KavehNegar.Logic.Model;

namespace KavehNegar.Logic.Repository.Redis
{
    public class RedisRepository<T> : BaseRepository, IRedisRepository<T> where T:RedisDB
    {
        public RedisRepository(IRedisContext redis) : base(redis)
        {
        }

        #region Body

        public IList<string> GetAllKeys()
        {
            return _redis.GetAllKeys();
        }

      
        public void Add(string key, T entity)
        {
            _redis.AddItemToList(GetFullKeyName(key), ToJsonString(entity));

        }
        //public void Add(string key, string entity)
        //{
        //    _redis.AddItemToList(GetFullKeyName(key), ToJsonString(entity));

        //}

        public IList<T> GetAll(string key)
        {
            var strItems = _redis.GetAllItemsFromList(key);
            return strItems.Select(Deserialize<T>).ToList();
        }

        //public string GetAll(string key)
        //{
        //    return _redis.GetAllItemsFromList(key);
            
        //}

        public IList<T> GetAll()
        {
            return _redis.As<T>().GetAll();
        }

        public IList<string> SearchKeys(string pattern)
        {
            return _redis.SearchKeys($"{pattern}*");
        }

        public List<T> GetValues(List<string> keys)
        {
            return _redis.GetValues<T>(keys);
        }

        public T GetByIndex(string key, int index)
        {
            var item = _redis.GetItemFromList(GetFullKeyName(key), index);
            return Deserialize<T>(item);
        }

        public IDisposable LockEntity(long id)
        {
            throw new NotImplementedException();
        }

        public bool Remove(string key)
        {
            _redis.RemoveAllFromList(GetFullKeyName(key));
            return true;
        }

        public bool RemoveAll(List<string> keys)
        {
            _redis.RemoveAll(keys);
            return true;
        }

        public bool RemoveByIndex(string key, int index)
        {
            throw new NotImplementedException();
        }

        public bool RemoveByValue(string key, T value)
        {
            return _redis.RemoveItemFromList(GetFullKeyName(key), ToJsonString(value)) > 0;
        }

        public bool UpdateItemAt(string key, int index, T entity)
        {
            throw new NotImplementedException();
        }

        protected virtual string GetFullKeyName(string key)
        {
            return $"{GetEntityName<T>()}:{key}";
        }

        #endregion





    }
}
