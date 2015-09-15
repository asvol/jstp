using System.IO;
using System.Text;
using jstp.Properties;

namespace jstp
{
    public class GenHelpCommand : GenCommandBase
    {
        public GenHelpCommand()
        {
            IsCommand("gen-help", "Generate help files from interface description files.\n" +
                                  "\t\tUse: gen-help <OPTIONS> <ifc1> <ifc1_version> <ifc2> <ifc2_version> ...\n" +
                                  "\t\tFor example command: gen-help -in=./in -out=./out DeviceInfo 1.0.0 Test 1.0.0\n");
        }


        protected override void InternalRun(JstpModel model, string outFolder)
        {
            GenerateHelp(model, outFolder);
        }

        private static void GenerateHelp(JstpModel model, string outFolder)
        {
            Jstp.GenerateAndSave(model, null, Encoding.UTF8.GetString(HelpResource.help_html), Path.Combine(outFolder, "help.html"));
        }
    }
}