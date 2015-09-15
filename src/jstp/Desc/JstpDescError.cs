namespace jstp
{
    public class JstpDescError : JstpDescEntity
    {
        public override void Validate()
        {
            ValidateHelper.NotNullOrWhiteSpace(()=>Title);
        }
    }
}