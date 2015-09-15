using System.Collections.Generic;

namespace jstp
{

    public enum JstpDescMetaType
    {
        Integer,
        Real,
        Bool,
        Null,
        String,
        Array,
        Object,
        Enum,
    }

    public class JstpDescType:JstpDescEntity
    {
        public JstpDescType()
        {
            Properties = new Dictionary<string, JstpDescObjectItem>();
        }
        public JstpDescMetaType Type { get; set; }
        public decimal Max { get; set; }
        public decimal Min { get; set; }
        public string Regex { get; set; }
        public int MaxLength { get; set; }

        public string Item { get; private set; }
        public IDictionary<string, JstpDescObjectItem> Properties { get; private set; }
        public IDictionary<string, JstpDescEnumItem> Items { get; private set; }

        
        public override void Validate()
        {
            ValidateHelper.NotNullOrWhiteSpace(()=>Title);
            
        }
    }

    public class JstpDescObjectItem:JstpDescEntity
    {
        public string Type { get; set; }
        public override void Validate()
        {
            ValidateHelper.NotNullOrWhiteSpace(() => Title);
            ValidateHelper.NotNullOrWhiteSpace(() => Type);
            ValidateHelper.Name(() => Type);
        }
    }

    public class JstpDescEnumItem : JstpDescEntity
    {
        public override void Validate()
        {
            ValidateHelper.NotNullOrWhiteSpace(() => Title);
        }
    }
}