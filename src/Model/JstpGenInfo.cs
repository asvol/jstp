using System;
using Console734.Connector;
using DotLiquid;

namespace jstp
{
    public class JstpGenInfo : Drop
    {
        public JstpGenInfo()
        {
            DateTime = System.DateTime.Now.ToString("g");
        }

        public string DateTime { get; private set; }
        public string Version { get { return AppInfo.Version.ToString(); } }
        public string UserName { get { return Environment.UserName; } }
        public string UserDomain { get { return Environment.UserDomainName; } }
    }
}