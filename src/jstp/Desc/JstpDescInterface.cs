using System;
using System.Collections.Generic;

namespace jstp
{
    public class JstpDescInterface : JstpDescEntity
    {
        private readonly Version _currentDescVersion = new Version(1,0,0);

        public JstpDescInterface()
        {
            Paths = new Dictionary<string, JstpDescPath>();
            Types = new Dictionary<string, JstpDescType>();
            Errors = new Dictionary<int, JstpDescError>();
        }

        public string JSTP { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string BasePath { get; set; }

        public override void Validate()
        {
            ValidateHelper.CompatibleVersion(() => JSTP,_currentDescVersion);
            ValidateHelper.Name(()=>Name);
            ValidateHelper.NotNullOrWhiteSpace(() => Title);
            ValidateHelper.NotNullOrWhiteSpace(() => BasePath);
            ValidateHelper.NotNullOrWhiteSpace(() => Desc);
        }

        public IDictionary<string, JstpDescPath> Paths { get; private set; }
        public IDictionary<string, JstpDescType> Types { get; private set; }
        public IDictionary<int, JstpDescError> Errors { get; private set; }
    }
}