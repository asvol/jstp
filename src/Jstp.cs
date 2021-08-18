using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DotLiquid;
using DotLiquid.FileSystems;
using DotLiquid.NamingConventions;
using JetBrains.Annotations;
using NLog;
using YamlDotNet.Serialization;

namespace jstp
{
    public class Jstp
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();


        
        /// <summary>
        /// Load interface description from YAML file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static KeyValuePair<InterfaceId,JstpDescInterface> LoadDescriptionInterface([NotNull] string file)
        {
            if (file == null) throw new ArgumentNullException("file");

            Logger.Info("Load model from file {0}",file);
            try
            {
                JstpDescInterface ifc;
                using (var rdr = new StreamReader(file))
                {
                    ifc = new Deserializer().Deserialize<JstpDescInterface>(rdr);
                }
                ifc.Validate();
                var key = new InterfaceId(ifc.Name, Version.Parse(ifc.Version));
                Logger.Debug("Load interface '{0}' version '{1}'", key.Name, key.Version);
                return new KeyValuePair<InterfaceId, JstpDescInterface>(key,ifc);
            }
            catch (Exception ex)
            {
                Logger.Error(ex,"Filed to load interface description from file {0}",file);
                throw;
            }
            
        }

        /// <summary>
        /// Load interface description form all YAML files from specifed directory and sub-directories
        /// </summary>
        /// <param name="descDirectoryPath"></param>
        /// <returns></returns>
        public static IDictionary<InterfaceId, JstpDescInterface> LoadAllDescrpitionInterfaces(string descDirectoryPath)
        {
            var ifcs = new SortedDictionary<InterfaceId, JstpDescInterface>(new EntityKeyComparer());

            try
            {
                foreach (var file in Directory.GetFiles(descDirectoryPath, "*.yaml"))
                {
                    var desc = Jstp.LoadDescriptionInterface(file);
                    if (ifcs.ContainsKey(desc.Key))
                    {
                        throw new Exception(string.Format("Interface with name '{0}' version '{1}' already exist.",
                            desc.Value.Name, desc.Key));
                    }
                    ifcs.Add(desc.Key,desc.Value);

                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Filed to load interfaces from directory {0}", descDirectoryPath);
                throw;
            }

            return ifcs;

        }

        public static JstpModel LoadModel(IDictionary<InterfaceId, JstpDescInterface> jstpDescInterfaces)
        {
            return new JstpModel(jstpDescInterfaces);
        }

        public static void GenerateAndSave(JstpModel model, IFileSystem templateFileSystem, string templateText, string fileToSave)
        {
            
            Template.NamingConvention = new CSharpNamingConvention();
            Template.RegisterSafeType(typeof(JstpGenModel), _ => _);
            Template.RegisterSafeType(typeof(JstpDescMetaType), o => o.ToString());
            Template.RegisterFilter(typeof(TextFilter));
            if (templateFileSystem!=null) Template.FileSystem = templateFileSystem;

            var template = Template.Parse(templateText);
            var genResult = template.Render(Hash.FromAnonymousObject(new { model = model }));
            File.WriteAllText(fileToSave, genResult);
        }

       
    }
}
