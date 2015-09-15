using System.IO;
using System.Text;
using jstp.Properties;

namespace jstp
{
    public class GenTestGccCommand : GenCommandBase
    {
        public GenTestGccCommand()
        {
            IsCommand("gen-gcc", "Generate test application\n" +
                                 "\t\tUse: gen-gcc <OPTIONS> <ifc1> <ifc1_version> <ifc2> <ifc2_version> ...\n" +
                                 "\t\tFor example command: gen-help -in=./in -out=./out DeviceInfo 1.0.0 Test 1.0.0\n");
        }


        protected override void InternalRun(JstpModel model, string outFolder)
        {
            GenerateGcc(model,outFolder);
        }

        private void GenerateGcc(JstpModel model, string outFolder)
        {
            GenCCommand.GenerateCCode(model, outFolder);
            Jstp.GenerateAndSave(model, null, Encoding.UTF8.GetString(TestResource.jstp_test_c), Path.Combine(outFolder, "jstp_test.c"));
            Jstp.GenerateAndSave(model, null, Encoding.UTF8.GetString(TestResource.gcc_makefile), Path.Combine(outFolder, "makefile"));
            Jstp.GenerateAndSave(model, null, Encoding.UTF8.GetString(TestResource.gcc_in_txt), Path.Combine(outFolder, "in.txt"));
            Jstp.GenerateAndSave(model, null, Encoding.UTF8.GetString(TestResource.run_sh), Path.Combine(outFolder, "run.sh"));
            
        }
    }
}