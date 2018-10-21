using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Newtonsoft.Json.Serialization;

namespace MvcBookStore.Utils
{
    public class UnderlineSplitContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            //return base.ResolvePropertyName(propertyName);
            return CamelCaseToUnderlineSplit(propertyName);
        }

        private string CamelCaseToUnderlineSplit(string propertyName)
        {
            StringBuilder builder = new StringBuilder();
            for (var i = 0; i < propertyName.Length; i++)
            {
                var ch = propertyName[i];
                if (char.IsUpper(ch) && i > 0)
                {
                    var prev = propertyName[i - 1];
                    if (prev != '_')
                    {
                        if (char.IsUpper(prev))
                        {
                            if (i < propertyName.Length - 1)
                            {
                                var next = propertyName[i + 1];
                                if (char.IsLower(next))
                                {
                                    builder.Append("_");
                                }
                            }
                        }
                        else
                        {
                            builder.Append("_");
                        }
                    }
                }

                builder.Append(char.ToLower(ch));
            }

            return builder.ToString();
        }
        
    }
}