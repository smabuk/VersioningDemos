using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace VersioningDemos.Web.TagHelpers
{
    [HtmlTargetElement("oldversion")]
    public class OldVersionTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

            output.TagName = "span";
            output.Attributes.Add("class", "text-muted");

            var versionInfo = GetType().Assembly.GetName().Version;

            output.Content.Append(versionInfo.ToString());

        }
    }
}
