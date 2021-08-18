using System.IO;
using System.Text;
using jstp.Properties;

namespace jstp
{
    public class GenTestSdccCommand : GenCommandBase
    {
        public GenTestSdccCommand()
        {
            IsCommand("gen-sdcc", "Generate example code for sdcc compiler (ADuC847)\n" +
                                  "\t\tUse: <CMD> <OPTIONS> <ifc1> <ifc1_version> <ifc2> <ifc2_version> ...\n" +
                                  "\t\tFor example command: gen-sdcc -in=./in -out=./out DeviceInfo 1.0.0 Test 1.0.0\n");
        }


        protected override void InternalRun(JstpModel model, string outFolder)
        {
            GenerateGcc(model, outFolder);
        }

        private void GenerateGcc(JstpModel model, string outFolder)
        {
            GenCCommand.GenerateCCode(model, outFolder);
            Jstp.GenerateAndSave(model, null, Encoding.UTF8.GetString(TestResource.sdcc_main_c), Path.Combine(outFolder, "main.c"));
            Jstp.GenerateAndSave(model, null, Encoding.UTF8.GetString(TestResource.sdcc_makefile), Path.Combine(outFolder, "makefile"));
            Jstp.GenerateAndSave(model, null, Encoding.UTF8.GetString(TestResource.ADuC847_h), Path.Combine(outFolder, "ADuC847.h"));
        }
    }
}
