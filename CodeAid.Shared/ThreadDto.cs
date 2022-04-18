using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAid.Shared
{
    public class ThreadDto
    {
        public int ThreadId { get; set; }
        public string QuestionTitle { get; set; } = String.Empty;
        public string Question { get; set; } = String.Empty;
        public int InterestId { get; set; }
    }
}
