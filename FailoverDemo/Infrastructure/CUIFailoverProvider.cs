using System;
using System.Configuration;

namespace FailoverDemo.Infrastructure
{
    public class CUIFailoverProvider : IFailoverProvider
    {
        private static bool _shouldFail = false;
        private static int _current = 0;
        private static bool? _isReadOnly = false;

        public string GetConnectionString()
        {
            if (_shouldFail)
            {
                int currentIndex = _current;
                currentIndex += 1;
                if (currentIndex >= ConfigurationManager.ConnectionStrings.Count)
                {
                    currentIndex = 0;
                }

                //TODO:  Need a lock here.  This code
                //  is not thread safe.  Neither is the
                //  logic, because every individual 
                //  failure causes a failover... resulting
                //  in a domino effect.

                //Update the pointer to the new index
                _current = currentIndex;
                //Reset the failover flag, assume the new
                //connection string will succeed
                _shouldFail = false;

                //We won't know if the database is read only
                //until a connection is opened against it.
                _isReadOnly = null;
            }

            return ConfigurationManager.ConnectionStrings[_current].ConnectionString;
        }

        
        public bool ShouldFailover
        {
            get
            {
                return _shouldFail;
            }

            set
            {
                _shouldFail = value;
            }
        }

        public bool? IsReadOnly
        {
            get
            {
                return _isReadOnly;
            }
            set
            {
                _isReadOnly = value;
            }
        }
    }
}
