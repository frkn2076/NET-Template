using System.Collections.Generic;

namespace Infra.Resources.Implementation
{
    public class Resource : IResource
    {
        private Dictionary<string, string> _pairs;

        public Resource() => _pairs = new Dictionary<string, string>();

        public string this[string key]
        {
            get { return _pairs.GetValueOrDefault(key); }
            set { _pairs[key] = value; }
        }
    }
}
