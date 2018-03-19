using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace VersioningDemos.Web.Pages
{
    public class IndexModel : PageModel
    {
        public class AssemblyInfo
        {
            public string Name { get; set; }
            public string Location { get; set; }
            public string FileVersion { get; set; }
            public string ProductVersion { get; set; }
            public string AssemblyVersion { get; set; }
        }

        public List<AssemblyInfo> Assemblies { get; set; } = new List<AssemblyInfo>();
        public List<AssemblyInfo> MicrosoftAssemblies { get; set; } = new List<AssemblyInfo>();

        public void OnGet()
        {
            // If we don't do anything with the other assemblies anywhere they won't show up here
            object p1 = new Default.Class1();
            p1 = new BuildDateTime.Class1();
            p1 = new BuildRevision.Class1();
            p1 = new EnvironmentVariable.Class1();
            p1 = new Manual.Class1();
            p1 = new Manual.AllDifferent.Class1();
            p1 = new SemanticVersioning.Class1();
            p1 = new VersionPrefix.Class1();
            p1 = new VersionSuffix.Class1();
            p1 = new VSTS.Class1();
            p1 = new VSTS.WithGit.Class1();

            var assemblies = from a in AppDomain.
                CurrentDomain.
                GetAssemblies()
                             orderby a.FullName
                             //where (a.FullName.StartsWith("Web") || a.FullName.StartsWith("Application") || a.FullName.StartsWith("Infrastructure"))
                             select a;

            foreach (var item in assemblies)
            {
                var ai = new AssemblyInfo();

                ai.Name = item.GetName().Name;
                ai.AssemblyVersion = item.GetName().Version.ToString();
                foreach (var ca in item.CustomAttributes.Where(ca => ca.AttributeType.ToString().StartsWith("System.Reflection.Assembly")))
                {
                    switch (ca.AttributeType.ToString().Replace("System.Reflection.Assembly", "").Replace("Attribute", ""))
                    {
                        case "FileVersion":
                            ai.FileVersion = ca.ConstructorArguments[0].Value.ToString();
                            break;
                        case "InformationalVersion":
                            ai.ProductVersion = ca.ConstructorArguments[0].Value.ToString();
                            break;
                        default:
                            break;
                    }
                }
                if (!item.IsDynamic)
                {
                    ai.Location = item.Location;
                }

                if (item.GetName().Name.StartsWith("Microsoft.")
                   || item.GetName().Name.StartsWith("Newtonsoft.")
                   || item.GetName().Name.StartsWith("ProductionBreakpoints")  // Application Insights
                   || item.GetName().Name.StartsWith("System.")
                   || item.GetName().Name.StartsWith("dotnet")
                   || item.GetName().Name.StartsWith("netstandard")
                   )
                {
                    MicrosoftAssemblies.Add(ai);
                }
                else
                {
                    Assemblies.Add(ai);
                }
            }
        }
    }
}