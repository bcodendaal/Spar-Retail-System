using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Core.Logging
{
    public class Logger : ILogger
    {
        
        public void Info(string tagGroup, string tagName, string message)
        {
            Trace.WriteLine(string.Format("TagGroup: {0}\nTagName:{1}\nMessage{2}", tagGroup, tagName, message));
        }

        public void Error(string tagGroup, string tagName, string message)
        {
            Trace.TraceError(string.Format("TagGroup: {0}\nTagName:{1}\nMessage{2}", tagGroup, tagName, message));
        }

        public void Error(string tagGroup, string tagName, Exception ex)
        {
            Trace.TraceError(string.Format("TagGroup: {0}\nTagName:{1}\nMessage{2}", tagGroup, tagName, ex.ToString()));
        }
    }
}
