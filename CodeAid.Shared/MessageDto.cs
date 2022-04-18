using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAid.Shared
{
    public class MessageDto
    {
        public int MessageId { get; set; }
        public string Message { get; set; } = string.Empty;
        public int ThreadId { get; set; }
    }
}
