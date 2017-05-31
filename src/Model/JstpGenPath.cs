using System;
using System.Collections;
using System.Collections.Generic;
using DotLiquid;

namespace jstp
{
    public class JstpGenPath:Drop
    {
        private readonly string _name;
        private readonly JstpDescPath _path;
        private readonly IDictionary<string, JstpGenType> _types;

        public JstpGenPath(string name, JstpDescPath path, IDictionary<string, JstpGenType> types)
        {
            _name = name;
            _path = path;
            _types = types;
            Args = string.IsNullOrWhiteSpace(path.Args) ? JstpGenType.NullType : GetIfcType(path.Args);
            Result = string.IsNullOrWhiteSpace(path.Result) ? JstpGenType.NullType : GetIfcType(path.Result);
        }

        private JstpGenType GetIfcType(string typeName)
        {
            JstpGenType res;
            if (_types.TryGetValue(typeName,out res))
            {
                return res;
            }
            else
            {
                throw new Exception(string.Format("Type '{0}' not found", typeName));
            }
            
        }

        public string Title
        {
            get { return _path.Title; }
        }

        public string Desc
        {
            get { return _path.Desc; }
        }

        public string Path
        {
            get { return _name; }
        }

        public JstpGenType Args { get; private set; }
        public JstpGenType Result { get; private set; }


    }


    
}