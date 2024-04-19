using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antlr4.Runtime
{
    using Antlr4.Runtime;
    public abstract class Python3ParserBase : Parser
    {
        public Python3ParserBase(ITokenStream input)
           : base(input)
        {
        }

        public Python3ParserBase(ITokenStream input, TextWriter output, TextWriter errorOutput)
            : base(input)
        {
        }

        public bool CannotBeDotLpEq()
        {
            System.Diagnostics.Debugger.Break();
            return false;
        }

        public bool CannotBePlusMinus()
        {
            System.Diagnostics.Debugger.Break();
            return false;
        }
    }
}
