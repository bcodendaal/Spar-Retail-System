using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparRetail.Core.Logging
{
    public interface ILogger
    {
        void Info(string tagGroup, string tagName, string message);
        void Error(string tagGroup, string tagName, string message);
        void Error(string tagGroup, string tagName, Exception ex);
    }
}
