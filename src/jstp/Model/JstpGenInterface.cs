using System;
using System.Collections.Generic;
using System.Linq;
using DotLiquid;
using jstp;

namespace jstp
{
    public class JstpGenInterface : Drop
    {
        private readonly JstpDescInterface _ifcDescription;


        public JstpGenInterface(JstpDescInterface ifcDescription)
        {
            _ifcDescription = ifcDescription;
            Types = new SortedDictionary<string, JstpGenType>(StringComparer.CurrentCultureIgnoreCase);
            Errors = new SortedDictionary<int, JstpGenError>();
            RegisterDefaultTypes();
            foreach (var type in ifcDescription.Types)
            {
                Types.Add(type.Key,CreateType(type));
            }

            foreach (var jstpGenType in Types.Values)
            {
                jstpGenType.Initialize(Types);
            }

            Paths = ifcDescription.Paths.Select(_ => new JstpGenPath(_.Key, _.Value, Types)).ToArray();



        }

        private void RegisterDefaultTypes()
        {
            Types.Add(JstpGenType.IntegerType.Name, JstpGenType.IntegerType);
            Types.Add(JstpGenType.BoolType.Name, JstpGenType.BoolType);
            Types.Add(JstpGenType.NullType.Name, JstpGenType.NullType);
            Types.Add(JstpGenType.RealType.Name, JstpGenType.RealType);
            Types.Add(JstpGenType.StringType.Name, JstpGenType.StringType);
        }

        private JstpGenType CreateType(KeyValuePair<string, JstpDescType> keyValuePair)
        {
            switch (keyValuePair.Value.Type)
            {
                case JstpDescMetaType.Integer:
                    return new JstpGenTypeInteger(keyValuePair.Key,keyValuePair.Value);
                case JstpDescMetaType.Real:
                    return new JstpGenTypeReal(keyValuePair.Key, keyValuePair.Value);
                case JstpDescMetaType.Bool:
                    return new JstpGenTypeBool(keyValuePair.Key, keyValuePair.Value);
                case JstpDescMetaType.Null:
                    return new JstpGenTypeNull(keyValuePair.Key, keyValuePair.Value);
                case JstpDescMetaType.String:
                    return new JstpGenTypeString(keyValuePair.Key, keyValuePair.Value);
                case JstpDescMetaType.Array:
                    return new JstpGenTypeArray(keyValuePair.Key, keyValuePair.Value);
                case JstpDescMetaType.Object:
                    return new JstpGenTypeObject(keyValuePair.Key, keyValuePair.Value);
                case JstpDescMetaType.Enum:
                    return new JstpGenTypeEnum(keyValuePair.Key, keyValuePair.Value);
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }

        public IDictionary<string, JstpGenType> Types { get; private set; }

        public JstpGenPath[] Paths { get; private set; }

        public IDictionary<int, JstpGenError> Errors { get; private set; }

        public string Jstp
        {
            get
            {
                return _ifcDescription.JSTP;
            }
        }

        public string Name
        {
            get { return _ifcDescription.Name; }
        }

        public string Version
        {
            get { return _ifcDescription.Version; }
        }

        public string Title
        {
            get { return _ifcDescription.Title; }
        }

        public string Desc
        {
            get { return _ifcDescription.Desc; }
        }

        public string BasePath
        {
            get { return _ifcDescription.BasePath; }
        }
    }
}