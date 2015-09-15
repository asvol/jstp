using System;
using System.Collections.Generic;
using DotLiquid;
using DotLiquid.FileSystems;

namespace jstp
{
    public class VirtualLiquidFileSystem:IFileSystem
    {
        private readonly IDictionary<string,Func<string>> _vfs = new Dictionary<string, Func<string>>(StringComparer.InvariantCultureIgnoreCase);

        public Func<string> this[string template]
        {
            get { return _vfs[template]; }
            set { _vfs[template] = value; }
        }
        public string ReadTemplateFile(Context context, string templateName)
        {
            Func<string> res;
            if (!_vfs.TryGetValue(templateName.Trim('\''), out res))
            {
                throw new Exception(string.Format("Virtual file system doesn't contain template with name '{0}'. Available {1}", templateName,string.Join(",",_vfs.Keys)));
            }
            try
            {
                var str = res();
                return str;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error to get template '{0}' from VFS", templateName), ex);
            }
            
        }
    }
}