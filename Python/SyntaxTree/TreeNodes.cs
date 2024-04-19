
namespace Python
{
    using Antlr4.Runtime.Misc;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.WebSockets;
    using static Python.Util;

    public static class Util
    {
        public static string I(int i)
        {
            return new string(' ', i);
        }
    }


    public interface IPythonStatement : IPythonTreeNode
    { }

    public class PythonForStatement : IPythonStatement
    {
        public IPythonExpression target;
        public IPythonExpression iterator;
        public PythonBlock block;
        public PythonBlock? elseblock;
        public int indent;
        public string ToString(int indent)
        {
            List<string> linestr = new List<string>();
            var str = $"{I(indent)}for {target} in {iterator}:\n{block.ToString(indent + 4)}";
            if (elseblock != null)
            {
                str += $"{I(indent)}else:\n{elseblock.ToString(indent + 4)}";
            }
            return str;
        }

        public override string ToString() => ToString(0);

        public IEnumerable<IPythonTreeNode> GetChildren()
        {
            yield return target;
            yield return iterator;
            yield return block;
            if (elseblock != null)
            {
                yield return elseblock;
            }
        }

        public void Replace(IPythonTreeNode orig, IPythonTreeNode replacement)
        {
            if (this.target == orig)
            {
                this.target = (IPythonExpression)replacement;
            }

            if (this.iterator == orig)
            {
                this.iterator = (IPythonExpression)replacement;
            }

            if (this.block == orig)
            {
                this.block = (PythonBlock)replacement;
            }

            if (elseblock != null && this.elseblock == orig)
            {
                this.elseblock = (PythonBlock)replacement;
            }
        }

        public static string[] newLineSplitStrings = new string[] { "\n", "\\n" };

        public PythonForStatement(IPythonExpression target, IPythonExpression iterator, PythonBlock block, PythonBlock? elseBlock = null)
        {
            this.target = target;
            this.iterator = iterator;
            this.block = block;
            this.elseblock = elseblock;
        }
    }

    public class PythonBlock : IPythonStatement
    {
        public IPythonTreeNode[] lines;
        public string ToString(int indent)
        {
            List<string> linestr = new List<string>();
            foreach (var n in lines)
            {
                if (n is PythonBlock b)
                {
                    linestr.Add(b.ToString());
                }
                else
                {
                    linestr.Add(I(indent) + n.ToString().Trim() + "\n");
                }
            }

            return string.Join("", linestr);
        }

        public IEnumerable<IPythonTreeNode> GetChildren()
        {
            foreach (var line in this.lines)
            {
                yield return line;
            }
        }

        public void Replace(IPythonTreeNode orig, IPythonTreeNode replacement)
        {
            for (int i = 0; i < this.lines.Length; i++)
            {
                if (this.lines[i] == orig)
                {
                    this.lines[i] = (IPythonTreeNode)replacement;
                }
            }
        }

        public override string ToString() => ToString(0);

        public static string[] newLineSplitStrings = new string[] { "\n", "\\n" };

        public PythonBlock(params IPythonTreeNode[] segments)
        {
            List<IPythonTreeNode> nodes = new List<IPythonTreeNode>();
            foreach (var n in segments)
            {
                if (n is PythonBlock b)
                {
                    foreach (var m in b.lines)
                    {
                        nodes.Add(m);
                    }
                }
                else
                {
                    nodes.Add(n);
                }

                this.lines = nodes.ToArray();
            }
        }
    }

    public interface IPythonTreeNode
    {
        IEnumerable<IPythonTreeNode> GetChildren();
        void Replace(IPythonTreeNode orig, IPythonTreeNode replacement);
        string ToString(int indent);
    }

    public interface IPythonExpression : IPythonTreeNode
    {
        void GetDependencies(HashSet<PythonIdentifierExpression> identifiers);
    }

    public class PythonBlankExpression : IPythonExpression, IPythonTreeNode
    {
        public PythonBlankExpression()
        {
        }

        public IEnumerable<IPythonTreeNode> GetChildren()
        {
            yield break;
        }

        public void GetDependencies(HashSet<PythonIdentifierExpression> identifiers)
        {
        }

        public void Replace(IPythonTreeNode orig, IPythonTreeNode replacement)
        {
        }

        public override string ToString()
        {
            return "";
        }

        public string ToString(int indent) => ToString();

    }


    public class PythonParenthesisExpression : IPythonExpression, IPythonTreeNode
    {
        public IPythonExpression value;

        public PythonParenthesisExpression(string v)
        {
        }

        public IEnumerable<IPythonTreeNode> GetChildren()
        {
            yield return this.value;
        }

        public void GetDependencies(HashSet<PythonIdentifierExpression> identifiers)
        {
            this.value.GetDependencies(identifiers);
        }

        public void Replace(IPythonTreeNode orig, IPythonTreeNode replacement)
        {
            if (this.value == orig)
            {
                this.value = (IPythonExpression)replacement;
            }
        }

        public override string ToString()
        {
            return "(" + this.value + ")";
        }

        public string ToString(int indent) => ToString();
    }

    public class PythonStringExpression : IPythonExpression, IPythonTreeNode
    {
        public string value;
        public string raw;

        public PythonStringExpression(string raw)
        {
            this.raw = raw;
            if (raw.StartsWith("'") && raw.EndsWith("'"))
            {
                this.value = Compiler.UnescapeString(raw[1..^1]);
            }
            else if (raw.StartsWith('"') && raw.EndsWith('"'))
            {
                this.value = Compiler.UnescapeString(raw[1..^1]);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public IEnumerable<IPythonTreeNode> GetChildren()
        {
            yield break;
        }

        public void GetDependencies(HashSet<PythonIdentifierExpression> identifiers)
        {
        }

        public void Replace(IPythonTreeNode orig, IPythonTreeNode replacement)
        {
            return;
        }

        public override string ToString()
        {
            return this.raw;
        }

        public string ToString(int indent) => ToString();

    }

    public class PythonArrayIndexerExpression : IPythonExpression, IPythonTreeNode
    {
        public IPythonExpression value;
        public PythonExpressionList[] arguments;
        public PythonArrayIndexerExpression(IPythonExpression value, params PythonExpressionList[] args)
        {
            this.value = value;
            this.arguments = args;
        }

        public IEnumerable<IPythonTreeNode> GetChildren()
        {
            yield return this.value;
        }

        public void GetDependencies(HashSet<PythonIdentifierExpression> identifiers)
        {
            this.value.GetDependencies(identifiers);
        }

        public void Replace(IPythonTreeNode orig, IPythonTreeNode replacement)
        {
            if (this.value == orig)
            {
                this.value = (PythonIdentifierExpression)replacement;
            }
        }

        public override string ToString()
        {
            return $"{this.value}[{string.Join(",", this.arguments.Select(x => x.ToString()))}]";
        }

        public string ToString(int indent) => ToString();

    }
    public class PythonForInExpression : IPythonExpression, IPythonTreeNode
    {
        public IPythonExpression value;
        public IPythonExpression inValue;
        public IPythonExpression compIter;
        public PythonForInExpression(IPythonExpression value, IPythonExpression inValue, IPythonExpression compIter)
        {
            this.value = value;
            this.inValue = inValue;
            this.compIter = compIter;
        }

        public IEnumerable<IPythonTreeNode> GetChildren()
        {
            yield return this.value;
            yield return this.inValue;
            if (this.compIter != null)
            {
                yield return this.compIter;
            }
        }

        public void GetDependencies(HashSet<PythonIdentifierExpression> identifiers)
        {
            this.value.GetDependencies(identifiers);
            this.inValue.GetDependencies(identifiers);
            if (this.compIter != null)
            {
                this.compIter.GetDependencies(identifiers);
            }
        }

        public void Replace(IPythonTreeNode orig, IPythonTreeNode replacement)
        {
            if (this.value == orig)
            {
                this.value = (PythonIdentifierExpression)replacement;
            }
            if (this.inValue == orig)
            {
                this.inValue = (PythonIdentifierExpression)replacement;
            }
            if (this.compIter != null && this.compIter == orig)
            {
                this.compIter = (PythonIdentifierExpression)replacement;
            }
        }

        public override string ToString()
        {
            if (this.compIter == null)
            {
                return $"for {this.value} in {this.inValue}";
            }
            else
            {
                return $"for {this.value} in {this.inValue} if {this.compIter}";
            }
        }

        public string ToString(int indent) => ToString();

    }

    public class PythonIdentifierExpression : IPythonExpression, IPythonTreeNode
    {
        public string value;
        public PythonIdentifierExpression(string value)
        {
            this.value = value;
        }

        public IEnumerable<IPythonTreeNode> GetChildren()
        {
            yield break;
        }

        public void GetDependencies(HashSet<PythonIdentifierExpression> identifiers)
        {
            identifiers.Add(this);
        }

        public void Replace(IPythonTreeNode orig, IPythonTreeNode replacement)
        {
            return;
        }

        public override string ToString()
        {
            return this.value;
        }

        public string ToString(int indent) => ToString();
    }

    public class PythonNumberLiteralExpression : IPythonExpression, IPythonTreeNode
    {
        public string raw;
        public double value;
        public PythonNumberLiteralExpression(string raw)
        {
            this.raw = raw;
            if (TryParse(raw, out double v))
            {
                this.value = v;
            }
        }

        public static bool TryParse(string raw, out double value)
        {
            return double.TryParse(raw, out value);
        }

        public IEnumerable<IPythonTreeNode> GetChildren()
        {
            yield break;
        }

        public void GetDependencies(HashSet<PythonIdentifierExpression> identifiers)
        {
        }

        public void Replace(IPythonTreeNode orig, IPythonTreeNode replacement)
        {
            return;
        }

        public override string ToString()
        {
            return this.raw.ToString();
        }

        public string ToString(int indent) => ToString();
    }

    public class PythonBoolLiteralExpression : IPythonExpression, IPythonTreeNode
    {
        public bool? value;
        public string raw;
        public PythonBoolLiteralExpression(string raw)
        {
            this.raw = raw;
            switch (raw.ToLower())
            {
                case "true":
                    value = true;
                    break;
                case "false":
                    value = false;
                    break;
                case "none":
                    value = null;
                    break;
            }
        }

        public IEnumerable<IPythonTreeNode> GetChildren()
        {
            yield break;
        }


        public void GetDependencies(HashSet<PythonIdentifierExpression> identifiers)
        {
        }

        public void Replace(IPythonTreeNode orig, IPythonTreeNode replacement)
        {
            return;
        }

        public override string ToString()
        {
            return this.raw;
        }

        public string ToString(int indent) => ToString();

    }

    public class PythonLambdaExpression : IPythonExpression, IPythonTreeNode
    {
        public PythonArgument[] variables;
        public IPythonExpression value;
        public PythonLambdaExpression(PythonArgument[] variables, IPythonExpression value)
        {
            this.variables = variables;
            this.value = value;
        }
        public IEnumerable<IPythonTreeNode> GetChildren()

        {
            foreach (var v in variables)
            {
                yield return v;
            }

            yield return this.value;
        }

        public void GetDependencies(HashSet<PythonIdentifierExpression> identifiers)
        {
            foreach (var v in variables)
            {
                v.GetDependencies(identifiers);
            }

            this.value.GetDependencies(identifiers);
        }

        public void Replace(IPythonTreeNode orig, IPythonTreeNode replacement)
        {
            for (int i = 0; i < this.variables.Length; i++)
            {
                if (this.variables[i] == orig)
                {
                    this.variables[i] = (PythonArgument)replacement;
                }
            }

            if (this.value == orig)
            {
                this.value = (IPythonExpression)replacement;
            }
        }

        public override string ToString()
        {
            return $"lambda {string.Join(", ", variables.Select(item => item?.ToString() ?? ""))}: {this.value}";
        }

        public string ToString(int indent) => ToString();
    }

    public class PythonOperatorExpression : IPythonExpression, IPythonTreeNode
    {
        public IPythonExpression left;
        public IPythonExpression right;
        public string op;
        public PythonOperatorExpression(string op, params IPythonExpression[] args)
        {
            this.op = op;
            var rightAssociative = IsRightAssociative(op);
            if (args.Length == 2)
            {
                this.left = args[0];
                this.right = args[1];
            }
            else if (rightAssociative)
            {
                this.left = new PythonOperatorExpression(op, args[0..^1]);
                this.right = args[^1];
            }
            else
            {
                this.left = args[0];
                this.right = new PythonOperatorExpression(op, args[1..]);
            }

        }

        public static bool IsRightAssociative(string op)
        {
            switch (op)
            {
                case "**":
                case "=":
                case "+=":
                case "-=":
                case "*=":
                case "/=":
                case "//=":
                case "%=":
                case "**=":
                case "&=":
                case "|=":
                case "^=":
                case "<<=":
                case ">>=":
                    return true;
                case "+":
                case "-":
                case "*":
                case "/":
                case "%":
                case "//":
                case ">":
                case ">=":
                case "<":
                case "<=":
                case "==":
                case "!=":
                case "in":
                case "not in":
                case "is":
                case "is not":
                case "&":
                case "|":
                case "^":
                case "<<":
                case ">>":
                case "and":
                case "or":
                    return false;
                default:
                    throw new ArgumentException(op);
            }
        }

        public IEnumerable<IPythonTreeNode> GetChildren()
        {
            yield return this.left;
            yield return this.right;
        }

        public void GetDependencies(HashSet<PythonIdentifierExpression> identifiers)
        {
            this.left.GetDependencies(identifiers);
            this.right.GetDependencies(identifiers);
        }

        public void Replace(IPythonTreeNode orig, IPythonTreeNode replacement)
        {
            if (this.left == orig)
            {
                this.left = (IPythonExpression)replacement;
            }
            if (this.right == orig)
            {
                this.right = (IPythonExpression)replacement;
            }
        }

        public override string ToString()
        {
            return $"{this.left} {this.op} {this.right}";
        }

        public string ToString(int indent) => ToString();
    }

    public class PythonUnaryOperatorExpression : IPythonExpression, IPythonTreeNode
    {
        public IPythonExpression arg;
        public string op;
        public PythonUnaryOperatorExpression(string op, IPythonExpression arg)
        {
            this.op = op;
            this.arg = arg;
        }

        public IEnumerable<IPythonTreeNode> GetChildren()
        {
            yield return this.arg;
        }

        public void GetDependencies(HashSet<PythonIdentifierExpression> identifiers)
        {
            this.arg.GetDependencies(identifiers);
        }

        public void Replace(IPythonTreeNode orig, IPythonTreeNode replacement)
        {
            if (this.arg == orig)
            {
                this.arg = (IPythonExpression)replacement;
            }
        }

        public override string ToString()
        {
            return $"{this.op} {this.arg}";
        }

        public string ToString(int indent) => ToString();

    }

    public class PythonExpressionList : IPythonExpression, IPythonTreeNode
    {
        public IPythonExpression[] args;
        public PythonExpressionList(params IPythonExpression[] args)
        {
            this.args = args;
        }

        public IEnumerable<IPythonTreeNode> GetChildren()
        {
            foreach (var arg in this.args)
            {
                yield return arg;
            }
        }

        public void GetDependencies(HashSet<PythonIdentifierExpression> identifiers)
        {
            foreach (var arg in this.args)
            {
                arg.GetDependencies(identifiers);
            }
        }

        public void Replace(IPythonTreeNode orig, IPythonTreeNode replacement)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (this.args[i] == orig)
                {
                    this.args[i] = (IPythonExpression)replacement;
                }
            }
        }

        public override string ToString()
        {
            return string.Join(":", args.Select(n => n.ToString()));
        }

        public string ToString(int indent) => ToString();
    }

    public class PythonPropertyExpression : IPythonExpression, IPythonTreeNode
    {
        IPythonExpression src;
        public string property;
        public IPythonExpression[]? arguments;
        public PythonPropertyExpression(IPythonExpression src, string property, params IPythonExpression[]? arguments)
        {
            this.src = src;
            this.property = property;
            this.arguments = arguments;
        }

        public IEnumerable<IPythonTreeNode> GetChildren()
        {
            yield return src;
            if (this.arguments != null)
            {
                foreach (var n in this.arguments)
                {
                    yield return n;
                }
            }
        }

        public void GetDependencies(HashSet<PythonIdentifierExpression> identifiers)
        {
            src.GetDependencies(identifiers);
            if (this.arguments != null)
            {
                foreach (var n in this.arguments)
                {
                    n.GetDependencies(identifiers);
                }
            }
        }

        public void Replace(IPythonTreeNode orig, IPythonTreeNode replacement)
        {
            if (this.src == orig)
            {
                this.src = (IPythonExpression)replacement;
            }

            if (this.arguments != null)
            {
                for (int i = 0; i < this.arguments.Length; i++)
                {
                    if (this.arguments[i] == orig)
                    {
                        this.arguments[i] = (IPythonExpression)replacement;
                    }
                }
            }
        }

        public override string ToString()
        {
            if (this.arguments == null)
            {
                return $"{this.src}.{this.property}";
            }
            else
            {
                var args = string.Join(", ", this.arguments.Select(x => x.ToString()));
                return $"{this.src}.{this.property}({args})";
            }
        }

        public string ToString(int indent) => ToString();

    }

    public class PythonArgument : IPythonExpression
    {
        public string argumentName;
        public IPythonExpression value;
        public string prefix; //* or ** or null
        public string type;

        public PythonArgument(string argumentName, IPythonExpression value, string prefix = null, string type = null)
        {
            this.argumentName = argumentName;
            this.value = value;
            this.prefix = prefix;
            this.type = type;
        }

        public IEnumerable<IPythonTreeNode> GetChildren()
        {
            yield return this.value;
        }

        public void GetDependencies(HashSet<PythonIdentifierExpression> identifiers)
        {
            this.value.GetDependencies(identifiers);
        }

        public void Replace(IPythonTreeNode orig, IPythonTreeNode replacement)
        {
            if (this.value == orig)
            {
                this.value = (IPythonExpression)replacement;
            }
        }

        public override string ToString()
        {
            if (this.argumentName == null)
            {
                return this.value.ToString();
            }
            else
            {
                var typeS = this.type == null ? "" : ":" + this.type;
                var assn = this.value == null ? "" : "=" + this.value;
                return $"{this.prefix ?? ""}{typeS}{this.argumentName}{assn}";
            }
        }

        public string ToString(int indent) => ToString();

    }

    public class PythonFunctionExpression : IPythonExpression, IPythonTreeNode
    {
        public IPythonExpression function;
        public PythonArgument[] arguments;
        public PythonFunctionExpression(IPythonExpression function, params PythonArgument[] arguments)
        {
            this.function = function;
            this.arguments = arguments;
        }

        public IEnumerable<IPythonTreeNode> GetChildren()
        {
            yield return function;
            foreach (var arg in this.arguments)
            {
                yield return arg;
            }
        }

        public void GetDependencies(HashSet<PythonIdentifierExpression> identifiers)
        {
            foreach (var arg in this.arguments)
            {
                arg.GetDependencies(identifiers);
            }
        }

        public void Replace(IPythonTreeNode orig, IPythonTreeNode replacement)
        {
            for (int i = 0; i < this.arguments.Length; i++)
            {
                if (this.arguments[i] == orig)
                {
                    this.arguments[i] = (PythonArgument)replacement;
                }
            }
        }

        public override string ToString()
        {
            return this.function + "(" + string.Join(",", this.arguments.ToList()) + ")";
        }

        public string ToString(int indent) => ToString();

    }

    public class PythonArrayExpression : IPythonExpression, IPythonTreeNode
    {
        public IPythonExpression[] values;
        public PythonArrayExpression(params IPythonExpression[] vals)
        {
            this.values = vals;
        }

        public PythonArrayExpression()
        {
            this.values = new IPythonExpression[0];
        }

        public IEnumerable<IPythonTreeNode> GetChildren()
        {
            foreach (var value in this.values)
            {
                yield return value;
            }
        }

        public void GetDependencies(HashSet<PythonIdentifierExpression> identifiers)
        {
            foreach (var v in this.values)
            {
                v.GetDependencies(identifiers);
            }
        }

        public void Replace(IPythonTreeNode orig, IPythonTreeNode replacement)
        {
            for (int i = 0; i < this.values.Length; i++)
            {
                if (this.values[i] == orig)
                {
                    this.values[i] = (IPythonExpression)replacement;
                }
            }
        }

        public override string ToString()
        {
            return "[" + string.Join(", ", this.values.ToList()) + "]";
        }

        public string ToString(int indent) => ToString();
    }

    public class PythonKeyValuePair : IPythonExpression
    {
        public IPythonExpression key;
        public IPythonExpression value;
        public PythonKeyValuePair(IPythonExpression key, IPythonExpression value)
        {
            this.key = key;
            this.value = value;
        }

        public IEnumerable<IPythonTreeNode> GetChildren()
        {
            yield return this.key;
            yield return this.value;
        }

        public void GetDependencies(HashSet<PythonIdentifierExpression> identifiers)
        {
            this.key.GetDependencies(identifiers);
            this.value.GetDependencies(identifiers);
        }

        public void Replace(IPythonTreeNode orig, IPythonTreeNode replacement)
        {
            if (this.key == orig)
            {
                this.key = (IPythonExpression)replacement;
            }

            if (this.value == orig)
            {
                this.value = (IPythonExpression)replacement;
            }
        }

        public override string ToString()
        {
            return $"{this.key}: {this.value}";
        }

        public string ToString(int indent) => ToString();
    }

    public class PythonDictComprehensionExpression : IPythonExpression
    {
        public IPythonExpression key;
        public IPythonExpression value;
        public IPythonExpression comprehension;
        public PythonDictComprehensionExpression(IPythonExpression key, IPythonExpression value, IPythonExpression comprehension)
        {
            this.key = key;
            this.value = value;
            this.comprehension= comprehension;
        }

        public IEnumerable<IPythonTreeNode> GetChildren()
        {
            yield return this.key;
            yield return this.value;
            yield return this.comprehension;
        }

        public void GetDependencies(HashSet<PythonIdentifierExpression> identifiers)
        {
            this.key.GetDependencies(identifiers);
            this.value.GetDependencies(identifiers);
            this.comprehension.GetDependencies(identifiers);
        }

        public void Replace(IPythonTreeNode orig, IPythonTreeNode replacement)
        {
            if (this.key == orig)
            {
                this.key = (IPythonExpression)replacement;
            }

            if (this.value == orig)
            {
                this.value = (IPythonExpression)replacement;
            }

            if (this.comprehension == orig)
            {
                this.comprehension = (IPythonExpression)replacement;
            }
        }

        public override string ToString()
        {
            return $"{this.key}={this.value} {this.comprehension}";
        }

        public string ToString(int indent) => ToString();
    }

    public class PythonObjectExpression : IPythonExpression, IPythonTreeNode
    {
        public IPythonExpression[] values;

        public PythonObjectExpression(params IPythonExpression[] values)
        {
            this.values = values;
        }

        public IEnumerable<IPythonTreeNode> GetChildren()
        {
            foreach (var value in this.values)
            {
                yield return value;
            }
        }

        public void GetDependencies(HashSet<PythonIdentifierExpression> identifiers)
        {
            foreach (var v in this.values)
            {
                v.GetDependencies(identifiers);
            }
        }

        public void Replace(IPythonTreeNode orig, IPythonTreeNode replacement)
        {
            for (int i = 0; i < this.values.Length; i++)
            {
                if (this.values[i] == orig)
                {
                    this.values[i] = (PythonKeyValuePair)replacement;
                }
            }
        }

        public override string ToString()
        {
            return "{" + string.Join(", ", this.values.Select(x => x.ToString())) + "}";
        }

        public string ToString(int indent) => ToString();

    }
}

