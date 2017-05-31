using System;
using System.Collections.Generic;
using System.Linq;
using DotLiquid;
using JetBrains.Annotations;
using jstp;

namespace jstp
{
    public class JstpGenModel : Drop
    {
        public JstpGenModel(IEnumerable<JstpDescInterface> interfaces)
        {
            if (interfaces == null) throw new ArgumentNullException("interfaces");
            Info = new JstpGenInfo();
            Interfaces = interfaces.Select(_ => new JstpGenInterface(_)).ToArray();
        }

        public JstpGenInfo Info { get; private set; }
        public JstpGenInterface[] Interfaces { get; private set; }
        
    }
}