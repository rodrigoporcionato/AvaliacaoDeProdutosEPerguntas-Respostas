using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetaViews.Messages
{
    [Serializable]
    public class GenericMessage
    {
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
