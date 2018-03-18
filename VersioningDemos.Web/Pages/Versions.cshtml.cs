using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace VersioningDemos.Web.Pages
{
    public class VersionsModel : PageModel
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

        public void OnGet()
        {
            // If we don't do anything with the other assemblies anywhere they won't show up here
            var p1 = new Default.Class1();
            var p2 = new Manual.Class1();

            var assemblies = from a in AppDomain.
                CurrentDomain.
                GetAssemblies()
                             orderby a.FullName
                             //where (a.FullName.StartsWith("Web") || a.FullName.StartsWith("Application") || a.FullName.StartsWith("Infrastructure"))
                             select a;

            foreach (var item in assemblies)
            {
                var ai = new AssemblyInfo();

                try
                {
                    // strText.AppendLine($"AssemblyInfo: {item.GetName().Name} {item.GetName().Version}");
                    if (
                        !( false
                        || item.GetName().Name.StartsWith("Microsoft.")
                        || item.GetName().Name.StartsWith("System.")
                        || item.GetName().Name.StartsWith("dotnet")
                        || item.GetName().Name.StartsWith("netstandard")
                        ))
                    {
                        ai.Name = item.GetName().Name;
                        ai.Location =item.Location;
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
                        Assemblies.Add(ai);
                    }
                }
                catch (Exception)
                {
                }

            }


        }
    }
}