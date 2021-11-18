using System;
using System.Collections.Generic;
using System.Text;

namespace DocFx_Articles
{
    static class StaticFunctions
    {
        public static string GetYamlIndent(int level)
        {
            return new String(' ', level);
        }
    }
}
