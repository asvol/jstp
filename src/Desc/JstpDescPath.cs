using System.Collections.Generic;

namespace jstp
{
    public class  JstpDescPath : JstpDescEntity
    {
        public string Result { get; set; }
        public string Args { get; set; }
        public IDictionary<int, JstpDescError> Errors { get; private set; }

        

        public override void Validate()
        {
            ValidateHelper.NotNullOrWhiteSpace(()=>Title);
        }
    }
}