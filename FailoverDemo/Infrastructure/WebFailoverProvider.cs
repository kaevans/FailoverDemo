using System.Configuration;
using System.Web;

namespace FailoverDemo.Infrastructure
{
    public class WebFailoverProvider : IFailoverProvider
    {
        public string GetConnectionString()
        {
            if (this.ShouldFailover)
            {
                int currentIndex = this.CurrentIndex;
                //copy by value, won't affect this.CurrentIndex
                currentIndex += 1;

                //Could do something like get all connection strings
                //by prefix.  Left as exercise to the reader.
                if (currentIndex >= ConfigurationManager.ConnectionStrings.Count)
                {
                    currentIndex = 0;
                }

                //Update the pointer to the new index, which
                //also resets the failover flag
                this.CurrentIndex = currentIndex;
            }

            return ConfigurationManager.ConnectionStrings[this.CurrentIndex].ConnectionString;
        }

        public bool ShouldFailover
        {
            get
            {
                bool ret = false;
                var s = HttpContext.Current.Application.Get("_shouldFail");
                if(null != s)
                {
                    ret = (bool)s;
                }
                return ret;
            }
            set
            {
                //How to avoid 20,000 failovers due to timing?
                //   Need to change this to use 
                //   Circuit Breaker Pattern
                //   https://msdn.microsoft.com/en-us/library/dn589784.aspx
            }
        }


        private int CurrentIndex
        {
            get
            {
                int ret = 0;
                var s = HttpContext.Current.Application.Get("_current");
                if (null != s)
                {
                    ret = (int)s;
                }
                return ret;
            }

            set
            {
                HttpContext.Current.Application.Lock();
                HttpContext.Current.Application["_current"] = value;
                HttpContext.Current.Application["_shouldFail"] = false;
                HttpContext.Current.Application.UnLock();
            }
        }
    }
}
