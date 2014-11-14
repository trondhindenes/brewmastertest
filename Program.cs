using Brewmaster.TemplateSDK.Contracts.Fluent;
using Brewmaster.TemplateSDK.Contracts.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BrewMasterTEst1
{
    class Program
    {
        static void Main(string[] args)
        {
            var template = WithTemplateExtensions
                    .CreateTemplate("Brewmaster.DemoTemplate", " This is a demo template.")
                    .WithParameter("Region", ParameterType.String, "Name of Azure region.", "AzureRegionName")
                    .WithParameter("VmName", ParameterType.String, "Name of the VM", "VmName")
                    .WithCloudService("{{VmName}}", "Brewmaster ARR Cloud Service",
                    cs => cs.WithDeployment(null, d =>
                    d.UsingDefaultDiskStorage("{{DiskStore}}")
                        .WithVirtualMachine("{{VmName}}", "{{Medium}}", "arr-avset", vm =>
                                            vm.WithWindowsConfigSet("vmadmin")
                                            .UsingConfigSet("ArrServer")))
                );



            string path = Directory.GetCurrentDirectory();
            //string target = @"c:\temp";
            Console.WriteLine("The current directory is {0}", path);

            template.Save(path);
        }
    }
}
