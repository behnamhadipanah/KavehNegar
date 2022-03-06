namespace KavehNegar.Logic.Context
{
    public class RedisContext : IRedisContext
    {
        #region Property
        public IRedisClient context { get; set; }

        private readonly RedisDBConfigModel _radisDBConfig;
        #endregion

        #region Body

        public RedisContext(IOptions<RedisDBConfigs> options)
        {
            _radisDBConfig = options.Value.Configs.FirstOrDefault(c => c.Name == "redisDb");

            context = new RedisClient(_radisDBConfig.Host, _radisDBConfig.Port);
            try
            {
                context.Db = _radisDBConfig.DBNumber;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }


        #endregion

        #region dispose
        
        public void Dispose()
        {
            if (context != null)
                context.Dispose();
            context = null;
        }

        #endregion

    }
}
