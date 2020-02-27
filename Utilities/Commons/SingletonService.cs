namespace Utilities.Commons
{
    public sealed class SingletonService
    {
        private static SingletonService _instance = null;
        private static readonly object padlock = new object();

        SingletonService()
        {
        }

        public static SingletonService Instance
        {
            get
            {
                lock (padlock)
                {
                    if (_instance == null)
                    {
                        _instance = new SingletonService();
                    }

                    return _instance;
                }
            }
        }

        public string DefaultConnection { get; set; }
    }
}