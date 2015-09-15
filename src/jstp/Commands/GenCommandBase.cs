using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ManyConsole;
using NLog;

namespace jstp
{
    public abstract class GenCommandBase : ConsoleCommand
    {
        private string _in;
        private string _out;
        protected readonly Logger Logger = LogManager.GetCurrentClassLogger();


        protected GenCommandBase()
        {
            HasRequiredOption("in|input=", "Directory with interface descriptions (YAML) files", _ => _in = _);
            HasRequiredOption("out|output=", "Output path. If directory not exist, it will be created.", _ => _out = _);
            HasAdditionalArguments(count: null);
        }
        public override int Run(string[] remainingArguments)
        {
            try
            {
                var ifNames = remainingArguments.Where((key, inx) => inx % 2 == 0);
                var ifcVersions = remainingArguments.Where((key, inx) => (inx + 1) % 2 == 0);
                var ifcs = ifNames.Zip(ifcVersions, (name, version) => name + " " + version).Select(InterfaceId.Parse).ToArray();

                Console.WriteLine("Start load model for '{0}'. Directory:{1}", string.Join(",", ifcs), _in);

                var allIfcs = Jstp.LoadAllDescrpitionInterfaces(_in);

                // check ifcs exist
                var generate = new SortedDictionary<InterfaceId, JstpDescInterface>();
                foreach (var interfaceId in ifcs)
                {
                    JstpDescInterface ifc;

                    if (!allIfcs.TryGetValue(interfaceId, out ifc))
                    {
                        throw new Exception(string.Format("Interface {0} not exist. Avalbale {1}", interfaceId, string.Join(",", allIfcs.Keys.OrderBy(_ => _.Name))));
                    }
                    generate.Add(interfaceId, ifc);
                }

                var model = Jstp.LoadModel(generate);

                if (!Directory.Exists(_out)) Directory.CreateDirectory(_out);

                InternalRun(model, _out);

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return -1;
            }
            return 0;
        }

        protected abstract void InternalRun(JstpModel model, string outFolder);
    }
}