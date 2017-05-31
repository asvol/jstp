using System;
using System.Collections.Generic;
using System.Linq;
using DotLiquid;
using JetBrains.Annotations;

namespace jstp
{
    public class JstpModel:Drop
    {
        public JstpModel([NotNull] IDictionary<InterfaceId, JstpDescInterface> ifcToGenerate)
        {
            if (ifcToGenerate == null) throw new ArgumentNullException("ifcToGenerate");
            Info = new JstpGenInfo();
            Interfaces = ifcToGenerate.Select(_ => new JstpGenInterface(_.Value)).ToArray();
        }




        public JstpGenInfo Info { get; private set; }
        public JstpGenInterface[] Interfaces { get; private set; }

    }
}