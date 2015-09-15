using DotLiquid;

namespace jstp
{
    public class JstpGenError:Drop
    {
        private readonly int _code;
        private readonly JstpDescError _error;

        public JstpGenError(int code, JstpDescError error)
        {
            _code = code;
            _error = error;
        }

        public string Title
        {
            get { return _error.Title; }
        }

        public string Desc
        {
            get { return _error.Desc; }
        }

        public int Code
        {
            get { return _code; }
        }

    }
}