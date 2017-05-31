using System.IO;
using System.Text;
using jstp.Properties;

namespace jstp
{
    public class GenCCommand : GenCommandBase
    {
       


        public GenCCommand()
        {
            IsCommand("gen-c", "Generate C code from interface description files.\n" +
                               "\t\tUse: <CMD> <OPTIONS> <ifc1> <ifc1_version> <ifc2> <ifc2_version> ...\n" +
                               "\t\tFor example command: gen-c -in=./in -out=./out DeviceInfo 1.0.0 Test 1.0.0\n");

            
        }

        protected override void InternalRun(JstpModel model, string outFolder)
        {
            GenerateCCode(model, outFolder);
        }

        public static void GenerateCCode(JstpModel model, string outFolder)
        {
            var vfs = new VirtualLiquidFileSystem();
            vfs["argArray"] = () => Encoding.UTF8.GetString(CCodeResource._argArray);
            vfs["argObject"] = () => Encoding.UTF8.GetString(CCodeResource._argObject);
            vfs["resObject"] = () => Encoding.UTF8.GetString(CCodeResource._resObject);

            Jstp.GenerateAndSave(model, vfs, Encoding.UTF8.GetString(CCodeResource.jsmn_c), Path.Combine(outFolder, "jsmn.c"));
            Jstp.GenerateAndSave(model, vfs, Encoding.UTF8.GetString(CCodeResource.jsmn_h), Path.Combine(outFolder, "jsmn.h"));
            Jstp.GenerateAndSave(model, vfs, Encoding.UTF8.GetString(CCodeResource.jstp_gen_c), Path.Combine(outFolder, "jstp_gen.c"));
            Jstp.GenerateAndSave(model, vfs, Encoding.UTF8.GetString(CCodeResource.jstp_gen_h), Path.Combine(outFolder, "jstp_gen.h"));
            Jstp.GenerateAndSave(model, vfs, Encoding.UTF8.GetString(CCodeResource.jstp_c), Path.Combine(outFolder, "jstp.c"));
            Jstp.GenerateAndSave(model, vfs, Encoding.UTF8.GetString(CCodeResource.jstp_h), Path.Combine(outFolder, "jstp.h"));
        }
    }
}