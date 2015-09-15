using System;
using System.Collections.Generic;
using System.Linq;
using DotLiquid;
using DotLiquid.Exceptions;
using ArgumentException = System.ArgumentException;

namespace jstp
{
    public class JstpGenType : Drop
    {
        #region static

        static JstpGenType()
        {
            NullType = new JstpGenTypeNull("NULL", new JstpDescType {Type = JstpDescMetaType.Null});
            BoolType = new JstpGenTypeBool("Bool", new JstpDescType { Type = JstpDescMetaType.Bool });
            IntegerType = new JstpGenTypeInteger("Integer", new JstpDescType { Type = JstpDescMetaType.Integer, Min = long.MinValue, Max = long.MaxValue });
            RealType = new JstpGenTypeReal("Real", new JstpDescType { Type = JstpDescMetaType.Real, Min = decimal.MinValue, Max = decimal.MaxValue });
            StringType = new JstpGenTypeString("String", new JstpDescType { Type = JstpDescMetaType.String });
        }
        public static JstpGenTypeNull NullType { get; private set; }
        public static JstpGenTypeBool BoolType { get; set; }
        public static JstpGenTypeInteger IntegerType { get; set; }
        public static JstpGenTypeReal RealType { get; set; }
        public static JstpGenTypeString StringType { get; set; }

        #endregion


        protected readonly JstpDescType JstpDescType;

        public JstpGenType(string name, JstpDescType jstpDescType)
        {
            JstpDescType = jstpDescType;
            Name = name;
            Title = jstpDescType.Title;
            Desc = jstpDescType.Desc;
        }

        internal virtual void Initialize(IDictionary<string, JstpGenType> types)
        {
            
        }

        public string Name { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public JstpDescMetaType Type { get; set; }
        
    }

    

    public class JstpGenTypeInteger : JstpGenType
    {
        public JstpGenTypeInteger(string name, JstpDescType jstpDescType):base(name,jstpDescType)
        {
            Type = JstpDescMetaType.Integer;
            Min = long.MinValue;
            Max = long.MaxValue;
        }

        public long Max { get; set; }
        public long Min { get; set; }
    }

    public class JstpGenTypeReal : JstpGenType
    {
        public JstpGenTypeReal(string name, JstpDescType jstpDescType)
            : base(name, jstpDescType)
        {
            Type = JstpDescMetaType.Real;
            Min = double.MinValue;
            Max = double.MaxValue;
        }

        public double Max { get; set; }
        public double Min { get; set; }
    }

    public class JstpGenTypeString : JstpGenType
    {
        public JstpGenTypeString(string name, JstpDescType jstpDescType)
            : base(name, jstpDescType)
        {
            Type = JstpDescMetaType.String;
            MaxLength = int.MaxValue;  
        }

        public int MaxLength { get; set; }
    }

    public class JstpGenTypeNull : JstpGenType
    {
        public JstpGenTypeNull(string name, JstpDescType jstpDescType)
            : base(name, jstpDescType)
        {
            Type = JstpDescMetaType.Null;    
        }
    }

    public class JstpGenTypeBool : JstpGenType
    {
        public JstpGenTypeBool(string name, JstpDescType jstpDescType)
            : base(name, jstpDescType)
        {
            Type = JstpDescMetaType.Bool;
        }
    }

    public class JstpGenTypeObject : JstpGenType
    {
        public JstpGenTypeObject(string name, JstpDescType jstpDescType)
            : base(name, jstpDescType)
        {
            Type = JstpDescMetaType.Object;
            Properties = new List<JstpGenTypeProperty>();
        }

        internal override void Initialize(IDictionary<string, JstpGenType> types)
        {
            base.Initialize(types);
            Properties = JstpDescType.Properties.Select(_ => new JstpGenTypeProperty(_.Key, _.Value,types)).ToList();
        }

        public List<JstpGenTypeProperty> Properties { get; private set; }
    }

    public class JstpGenTypeProperty : Drop
    {
        private readonly JstpDescObjectItem _desc;
        private readonly IDictionary<string, JstpGenType> _types;
        private JstpGenType _type;

        public JstpGenTypeProperty(string name, JstpDescObjectItem desc, IDictionary<string, JstpGenType> types)
        {
            _desc = desc;
            _types = types;
            Name = name;
            if (!types.TryGetValue(desc.Type,out _type))
            {
                throw new ArgumentException(string.Format("Type '{0}' not found", desc.Type));
            }
        }

        public string Name { get; set; }

        public string Title
        {
            get { return _desc.Title; }
        }

        public string Desc
        {
            get { return _desc.Desc; }
        }

        public JstpGenType Type
        {
            get { return _type; }
            set { _type = value; }
        }
    }

    public class JstpGenTypeArray : JstpGenType
    {

        public JstpGenTypeArray(string name, JstpDescType jstpDescType)
            : base(name, jstpDescType)
        {
            Type = JstpDescMetaType.Array;
            
        }

        internal override void Initialize(IDictionary<string, JstpGenType> types)
        {
            Item = types[JstpDescType.Item];
        }

        public JstpGenType Item { get; set; }
    }

    public class JstpGenTypeEnum : JstpGenType
    {
        public JstpGenTypeEnum(string name, JstpDescType jstpDescType)
            : base(name, jstpDescType)
        {
            Type = JstpDescMetaType.Enum;
            Items = jstpDescType.Items.Select(_ => new JstpGenTypeEnumItem(_.Key, _.Value)).ToList();
        }

        public List<JstpGenTypeEnumItem> Items { get; private set; }
    }

    public class JstpGenTypeEnumItem:Drop
    {
        public JstpGenTypeEnumItem(string name, JstpDescEnumItem jstpDescEnumItem)
        {
            Name = name;
            Title = jstpDescEnumItem.Title;
            Desc = jstpDescEnumItem.Desc;
        }

        public string Name { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
    }


    
      
}