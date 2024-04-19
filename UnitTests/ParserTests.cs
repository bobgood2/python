namespace UnitTests
{
    using Python;
    using System.Numerics;
    using System.Text.RegularExpressions;
    using System.Text;
    using static Antlr4.Runtime.Atn.SemanticContext;

    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void Parser()
        {
            T("a = {\"other_key\": \"value\",\"x\":3}\r\ndynamic_part = {x: x**2 for x in range(10) if x % 2 == 0}\r\n\r\nA = {\"start\": [3, True], \"end\": 5, **a, **dynamic_part}");
            T("A=now([\"startofweek\"])\nB=now([\"endofweek\"])\nC=searchMeetings(startingAfter=A, endingBefore=B)\nD=sorted(C, key=lambda x: x.sender.count() + x.recipients.count(), reverse=True)[:3]");
            T("A=2**3**2");
            T("A = b + c + true - 3 / \"h\"");
            T("for user in ['Alice', 'Bob', '']:\r\n    greet(x=user)");
        }

        private static void T(string python)
        {
            var c = new Compiler();
            var r = c.Run(python);
            var plan = r.ToString();
            var str = RemoveWhitespaceNotInQuotes(plan.ToString().Trim());
            string orig = RemoveWhitespaceNotInQuotes(python.Trim());
            Assert.AreEqual(orig, str);
        }

        public static string RemoveWhitespaceNotInQuotes(string input)
        {
            bool inSingleQuote = false;
            bool inDoubleQuote = false;
            StringBuilder output = new StringBuilder();

            for (int i = 0; i < input.Length; i++)
            {
                char currentChar = input[i];
                char? nextChar = i + 1 < input.Length ? input[i + 1] : (char?)null;

                // Check for escape character
                if (currentChar == '\\' && nextChar.HasValue && (nextChar == '"' || nextChar == '\''))
                {
                    output.Append(currentChar);   // Add the escape character
                    output.Append(nextChar);      // Add the quoted character
                    i++;                          // Skip the next character
                    continue;
                }

                // Toggle inDoubleQuote flag
                if (currentChar == '"' && !inSingleQuote)
                {
                    inDoubleQuote = !inDoubleQuote;
                    output.Append(currentChar);
                    continue;
                }

                // Toggle inSingleQuote flag
                if (currentChar == '\'' && !inDoubleQuote)
                {
                    inSingleQuote = !inSingleQuote;
                    output.Append(currentChar);
                    continue;
                }

                // Append character if it's a non-whitespace or if inside quotes
                if (inSingleQuote || inDoubleQuote || !char.IsWhiteSpace(currentChar))
                {
                    output.Append(currentChar);
                }
            }

            return output.ToString();
        }
    }
}
