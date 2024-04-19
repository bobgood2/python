using Antlr4.Runtime.Misc;
using Microsoft.VisualBasic;
using Python.Antlr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static Python.Antlr.Python3Parser;

namespace Python
{
    public partial class Compiler : Python3ParserBaseVisitor<IPythonTreeNode>
    {
        public int blockLevel = 0;
        public override IPythonTreeNode VisitSingle_input([NotNull] Python3Parser.Single_inputContext context)
        {
            // single_input
            //         : NEWLINE
            //         | simple_stmts
            //         | compound_stmt NEWLINE
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitFile_input([NotNull] Python3Parser.File_inputContext context)
        {
            // file_input
            //     : (NEWLINE | stmt)* EOF
            //     ;

            List<IPythonTreeNode> lines = new List<IPythonTreeNode>();
            foreach (var child in context.children)
            {
                if (child is StmtContext stmt)
                {
                    var s = this.Visit(stmt);
                    lines.Add(s);
                }
            }

            return new PythonBlock(lines.ToArray());
        }

        public override IPythonTreeNode VisitEval_input([NotNull] Python3Parser.Eval_inputContext context)
        {
            // eval_input
            //         : testlist NEWLINE* EOF
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitDecorator([NotNull] Python3Parser.DecoratorContext context)
        {
            // decorator
            //         : '@' dotted_name ('(' arglist? ')')? NEWLINE
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitDecorators([NotNull] Python3Parser.DecoratorsContext context)
        {
            // decorators
            //         : decorator+
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitDecorated([NotNull] Python3Parser.DecoratedContext context)
        {
            // decorated
            //         : decorators (classdef | funcdef | async_funcdef)
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitAsync_funcdef([NotNull] Python3Parser.Async_funcdefContext context)
        {
            // async_funcdef
            //         : ASYNC funcdef
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitFuncdef([NotNull] Python3Parser.FuncdefContext context)
        {
            // funcdef
            //         : 'def' name parameters ('->' test)? ':' block
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitParameters([NotNull] Python3Parser.ParametersContext context)
        {
            // parameters
            //         : '(' typedargslist? ')'
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitTypedargslist([NotNull] Python3Parser.TypedargslistContext context)
        {
            // typedargslist
            //         : (
            //             tfpdef ('=' test)? (',' tfpdef ('=' test)?)* (
            //                 ',' (
            //                     '*' tfpdef? (',' tfpdef ('=' test)?)* (',' ('**' tfpdef ','?)?)?
            //                     | '**' tfpdef ','?
            //                 )?
            //             )?
            //             | '*' tfpdef? (',' tfpdef ('=' test)?)* (',' ('**' tfpdef ','?)?)?
            //             | '**' tfpdef ','?
            //         )
            //         ;
            string name = null;
            string prefix = null;
            string type = null;
            List<IPythonExpression> arguments = new List<IPythonExpression>();
            IPythonExpression deflt = null;
            for (int i = 0; i < context.children.Count(); i++)
            {
                var child = context.children[i];
                if (child is TfpdefContext tp)
                {
                    if (tp.children.Count == 1)
                    {
                        name = child.GetText();
                    }
                    else
                    {
                        // get type here.
                        System.Diagnostics.Debugger.Break();
                    }
                }
                else if (child is TestContext tc)
                {
                    deflt = this.Visit(tc) as IPythonExpression;
                }
                else
                {
                    var token = child.GetText();
                    switch (token)
                    {
                        case "*":
                        case "**":
                            prefix = token;
                            break;
                        case ",":
                            {
                                arguments.Add(new PythonArgument(name, deflt, prefix, type));
                                name = null;
                                prefix = null;
                                deflt = null;
                                type = null;
                            }
                            System.Diagnostics.Debugger.Break();
                            break;
                        case "=":
                            System.Diagnostics.Debugger.Break();
                            break;
                        default:
                            System.Diagnostics.Debugger.Break();
                            break;
                    }
                }
            }

            if (name != null)
            {
                arguments.Add(new PythonArgument(name, deflt, prefix, type));
            }

            return new PythonExpressionList(arguments.ToArray());
        }

        public override IPythonTreeNode VisitTfpdef([NotNull] Python3Parser.TfpdefContext context)
        {
            // tfpdef
            //         : name (':' test)?
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitVarargslist([NotNull] Python3Parser.VarargslistContext context)
        {
            // varargslist
            //         : (
            //             vfpdef ('=' test)? (',' vfpdef ('=' test)?)* (
            //                 ',' (
            //                     '*' vfpdef? (',' vfpdef ('=' test)?)* (',' ('**' vfpdef ','?)?)?
            //                     | '**' vfpdef (',')?
            //                 )?
            //             )?
            //             | '*' vfpdef? (',' vfpdef ('=' test)?)* (',' ('**' vfpdef ','?)?)?
            //             | '**' vfpdef ','?
            //         )
            //         ;

            string name = null;
            string prefix = null;
            List<IPythonExpression> arguments = new List<IPythonExpression>();
            IPythonExpression deflt = null;
            for (int i = 0; i<context.children.Count(); i++)
            {
                var child = context.children[i];
                if (child is VfpdefContext)
                {
                    name = child.GetText();
                }
                else if (child is TestContext tc)
                {
                    deflt = this.Visit(tc) as IPythonExpression;
                }
                else
                {
                    var token = child.GetText();
                    switch(token)
                    {
                        case "*":
                        case "**":
                            prefix = token;
                            break;
                        case ",":
                            {
                                arguments.Add(new PythonArgument(name, deflt, prefix));
                                name = null;
                                prefix = null;
                                deflt = null;
                            }
                            System.Diagnostics.Debugger.Break();
                            break;
                        case "=":
                            System.Diagnostics.Debugger.Break();
                            break;
                        default :
                            System.Diagnostics.Debugger.Break();
                            break;
                    }
                }
            }

            if (name!=null)
            {
                arguments.Add(new PythonArgument(name, deflt, prefix));
            }

            return new PythonExpressionList(arguments.ToArray());
        }

        public override IPythonTreeNode VisitVfpdef([NotNull] Python3Parser.VfpdefContext context)
        {
            // vfpdef
            //         : name
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitStmt([NotNull] Python3Parser.StmtContext context)
        {
            // stmt
            // : simple_stmts
            // | compound_stmt
            // ;

            return this.Visit(context.children[0]);
        }

        public override IPythonTreeNode VisitSimple_stmts([NotNull] Python3Parser.Simple_stmtsContext context)
        {
            // simple_stmts
            //     : simple_stmt(';' simple_stmt) * ';' ? NEWLINE
            //     ;


            List<IPythonExpression> expressions = new List<IPythonExpression>();
            foreach (var child in context.children)
            {
                if (child is Simple_stmtContext simple_stmt)
                {
                    expressions.Add(this.Visit(simple_stmt) as IPythonExpression);
                }
            }

            if (expressions.Count == 1)
            {
                return expressions[0];
            }
            else
            {
                return new PythonBlock(expressions.ToArray());
            }
        }

        public override IPythonTreeNode VisitExpr_stmt([NotNull] Python3Parser.Expr_stmtContext context)
        {
            // expr_stmt
            //         : testlist_star_expr (
            //             annassign
            //             | augassign (yield_expr | testlist)
            //             | ('=' (yield_expr | testlist_star_expr))*
            //         )
            //         ;
            if (context.children[0] is Testlist_star_exprContext tse)
            {
                var n1 = this.Visit(tse) as IPythonExpression;
                if (context.ChildCount==1)
                {
                    return n1;
                }
                else if (context.children[1].GetText() == "=")
                {
                    List<IPythonExpression> list = new List<IPythonExpression>
                    {
                        n1
                    };

                    for (int i = 2; i < context.children.Count; i += 2)
                    {
                        if (context.children[i] is Yield_exprContext)
                        {
                            System.Diagnostics.Debugger.Break();
                        }
                        else if (context.children[i] is Testlist_star_exprContext)
                        {
                            list.Add(this.Visit(context.children[i]) as IPythonExpression);
                        }
                    }

                    // right associativity detected in constructor
                    return new PythonOperatorExpression("=", list.ToArray());
                }
                else
                {
                    System.Diagnostics.Debugger.Break();
                }
            }

            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitAnnassign([NotNull] Python3Parser.AnnassignContext context)
        {
            // annassign
            //         : ':' test ('=' test)?
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitTestlist_star_expr([NotNull] Python3Parser.Testlist_star_exprContext context)
        {
            // testlist_star_expr
            //         : (test | star_expr) (',' (test | star_expr))* ','?
            //         ;

            List<IPythonExpression> args = new List<IPythonExpression>();
            for (int i = 0; i < context.children.Count; i += 2)
            {
                if (context.children[i] is TestContext tc)
                {
                    args.Add(this.Visit(tc) as IPythonExpression);
                }
                else if (context.children[i] is Star_exprContext se)
                {
                    args.Add(this.Visit(se) as IPythonExpression);
                    System.Diagnostics.Debugger.Break();
                }
            }

            if (args.Count() == 1)
            {
                return args[0];
            }

            return new PythonExpressionList(args.ToArray());
        }

        public override IPythonTreeNode VisitAugassign([NotNull] Python3Parser.AugassignContext context)
        {
            // augassign
            //         : (
            //             '+='
            //             | '-='
            //             | '*='
            //             | '@='
            //             | '/='
            //             | '%='
            //             | '&='
            //             | '|='
            //             | '^='
            //             | '<<='
            //             | '>>='
            //             | '**='
            //             | '//='
            //         )
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitDel_stmt([NotNull] Python3Parser.Del_stmtContext context)
        {
            // del_stmt
            //         : 'del' exprlist
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitPass_stmt([NotNull] Python3Parser.Pass_stmtContext context)
        {
            // pass_stmt
            //         : 'pass'
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitFlow_stmt([NotNull] Python3Parser.Flow_stmtContext context)
        {
            // flow_stmt
            //         : break_stmt
            //         | continue_stmt
            //         | return_stmt
            //         | raise_stmt
            //         | yield_stmt
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitBreak_stmt([NotNull] Python3Parser.Break_stmtContext context)
        {
            // break_stmt
            //         : 'break'
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitContinue_stmt([NotNull] Python3Parser.Continue_stmtContext context)
        {
            // continue_stmt
            //         : 'continue'
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitReturn_stmt([NotNull] Python3Parser.Return_stmtContext context)
        {
            // return_stmt
            //         : 'return' testlist?
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitYield_stmt([NotNull] Python3Parser.Yield_stmtContext context)
        {
            // yield_stmt
            //         : yield_expr
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitRaise_stmt([NotNull] Python3Parser.Raise_stmtContext context)
        {
            // raise_stmt
            //         : 'raise' (test ('from' test)?)?
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitImport_stmt([NotNull] Python3Parser.Import_stmtContext context)
        {
            // import_stmt
            //         : import_name
            //         | import_from
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitImport_name([NotNull] Python3Parser.Import_nameContext context)
        {
            // import_name
            //         : 'import' dotted_as_names
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitImport_from([NotNull] Python3Parser.Import_fromContext context)
        {
            // import_from
            //         : (
            //             'from' (('.' | '...')* dotted_name | ('.' | '...')+) 'import' (
            //                 '*'
            //                 | '(' import_as_names ')'
            //                 | import_as_names
            //             )
            //         )
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitImport_as_name([NotNull] Python3Parser.Import_as_nameContext context)
        {
            // import_as_name
            //         : name ('as' name)?
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitDotted_as_name([NotNull] Python3Parser.Dotted_as_nameContext context)
        {
            // dotted_as_name
            //         : dotted_name ('as' name)?
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitImport_as_names([NotNull] Python3Parser.Import_as_namesContext context)
        {
            // import_as_names
            //         : import_as_name (',' import_as_name)* ','?
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitDotted_as_names([NotNull] Python3Parser.Dotted_as_namesContext context)
        {
            // dotted_as_names
            //         : dotted_as_name (',' dotted_as_name)*
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitDotted_name([NotNull] Python3Parser.Dotted_nameContext context)
        {
            // dotted_name
            //         : name ('.' name)*
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitGlobal_stmt([NotNull] Python3Parser.Global_stmtContext context)
        {
            // global_stmt
            //         : 'global' name (',' name)*
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitNonlocal_stmt([NotNull] Python3Parser.Nonlocal_stmtContext context)
        {
            // nonlocal_stmt
            //         : 'nonlocal' name (',' name)*
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitAssert_stmt([NotNull] Python3Parser.Assert_stmtContext context)
        {
            // assert_stmt
            //         : 'assert' test (',' test)?
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitCompound_stmt([NotNull] Python3Parser.Compound_stmtContext context)
        {
            // compound_stmt
            //     : if_stmt
            //     | while_stmt
            //     | for_stmt
            //     | try_stmt
            //     | with_stmt
            //     | funcdef
            //     | classdef
            //     | decorated
            //     | async_stmt
            //     | match_stmt
            //     ;

            foreach (var child in context.children)
            {
                return this.Visit(context.children[0]);
            }

            return new PythonBlock(null);
        }

        public override IPythonTreeNode VisitAsync_stmt([NotNull] Python3Parser.Async_stmtContext context)
        {
            // async_stmt
            //         : ASYNC (funcdef | with_stmt | for_stmt)
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitIf_stmt([NotNull] Python3Parser.If_stmtContext context)
        {
            // if_stmt
            //         : 'if' test ':' block ('elif' test ':' block)* ('else' ':' block)?
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitWhile_stmt([NotNull] Python3Parser.While_stmtContext context)
        {
            // while_stmt
            //         : 'while' test ':' block ('else' ':' block)?
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitFor_stmt([NotNull] Python3Parser.For_stmtContext context)
        {
            // for_stmt
            //         : 'for' exprlist 'in' testlist ':' block ('else' ':' block)?
            //         ;
            var target = (IPythonExpression) this.Visit(context.children[1]);
            var iterator = (IPythonExpression)this.Visit(context.children[3]) ;
            var b = this.Visit(context.children[5]);
            PythonBlock mainBlock;
            if (b is PythonBlock pb)
            {
                mainBlock = pb;
            }
            else
            {
                mainBlock = new PythonBlock(b);
            }
            if (context.ChildCount > 8)
            {
                var b1 = (PythonBlock)this.Visit(context.children[8]);
                PythonBlock elseBlock;
                if (b1 is PythonBlock pb1)
                {
                    elseBlock = pb1;
                }
                else
                {
                    elseBlock = new PythonBlock(b1);
                }

                System.Diagnostics.Debugger.Break();
                return new PythonForStatement(target, iterator, mainBlock, elseBlock);
            }
            else
            {
                return new PythonForStatement(target, iterator, mainBlock);
            }
        }

        public override IPythonTreeNode VisitTry_stmt([NotNull] Python3Parser.Try_stmtContext context)
        {
            // try_stmt
            //         : (
            //             'try' ':' block (
            //                 (except_clause ':' block)+ ('else' ':' block)? ('finally' ':' block)?
            //                 | 'finally' ':' block
            //             )
            //         )
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitWith_stmt([NotNull] Python3Parser.With_stmtContext context)
        {
            // with_stmt
            //         : 'with' with_item (',' with_item)* ':' block
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitWith_item([NotNull] Python3Parser.With_itemContext context)
        {
            // with_item
            //         : test ('as' expr)?
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitExcept_clause([NotNull] Python3Parser.Except_clauseContext context)
        {
            // except_clause
            //         : 'except' (test ('as' name)?)?
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitBlock([NotNull] Python3Parser.BlockContext context)
        {
            // block
            //         : simple_stmts
            //         | NEWLINE INDENT stmt+ DEDENT
            //
            //    

            List<IPythonExpression> expressions = new List<IPythonExpression>();
            foreach (var child in context.children)
            {
                if (child is Simple_stmtContext simple_stmt)
                {
                    var s = this.Visit(simple_stmt);
                    expressions.Add(s as IPythonExpression);
                }

                else if (child is StmtContext stmt)
                {
                    var s = this.Visit(stmt);
                    expressions.Add(s as IPythonExpression);
                }
            }

            if (expressions.Count == 1)
            {
                return expressions[0];
            }
            else
            {
                return new PythonBlock(expressions.ToArray());
            }
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitMatch_stmt([NotNull] Python3Parser.Match_stmtContext context)
        {
            // match_stmt
            //         : 'match' subject_expr ':' NEWLINE INDENT case_block+ DEDENT
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitSubject_expr([NotNull] Python3Parser.Subject_exprContext context)
        {
            // subject_expr
            //         : star_named_expression ',' star_named_expressions?
            //         | test
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitStar_named_expressions([NotNull] Python3Parser.Star_named_expressionsContext context)
        {
            // star_named_expressions
            //         : ',' star_named_expression+ ','?
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitStar_named_expression([NotNull] Python3Parser.Star_named_expressionContext context)
        {
            // star_named_expression
            //         : '*' expr
            //         | test
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitCase_block([NotNull] Python3Parser.Case_blockContext context)
        {
            // case_block
            //         : 'case' patterns guard? ':' block
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitGuard([NotNull] Python3Parser.GuardContext context)
        {
            // guard
            //         : 'if' test
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitPatterns([NotNull] Python3Parser.PatternsContext context)
        {
            // patterns
            //         : open_sequence_pattern
            //         | pattern
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitPattern([NotNull] Python3Parser.PatternContext context)
        {
            // pattern
            //         : as_pattern
            //         | or_pattern
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitAs_pattern([NotNull] Python3Parser.As_patternContext context)
        {
            // as_pattern
            //         : or_pattern 'as' pattern_capture_target
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitOr_pattern([NotNull] Python3Parser.Or_patternContext context)
        {
            // or_pattern
            //         : closed_pattern ('|' closed_pattern)*
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitClosed_pattern([NotNull] Python3Parser.Closed_patternContext context)
        {
            // closed_pattern
            //         : literal_pattern
            //         | capture_pattern
            //         | wildcard_pattern
            //         | value_pattern
            //         | group_pattern
            //         | sequence_pattern
            //         | mapping_pattern
            //         | class_pattern
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitLiteral_pattern([NotNull] Python3Parser.Literal_patternContext context)
        {
            // literal_pattern
            //         : signed_number { this.CannotBePlusMinus() }?
            //         | complex_number
            //         | strings
            //         | 'None'
            //         | 'True'
            //         | 'False'
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitLiteral_expr([NotNull] Python3Parser.Literal_exprContext context)
        {
            // literal_expr
            //         : signed_number { this.CannotBePlusMinus() }?
            //         | complex_number
            //         | strings
            //         | 'None'
            //         | 'True'
            //         | 'False'
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitComplex_number([NotNull] Python3Parser.Complex_numberContext context)
        {
            // complex_number
            //         : signed_real_number '+' imaginary_number
            //         | signed_real_number '-' imaginary_number
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitSigned_number([NotNull] Python3Parser.Signed_numberContext context)
        {
            // signed_number
            //         : NUMBER
            //         | '-' NUMBER
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitSigned_real_number([NotNull] Python3Parser.Signed_real_numberContext context)
        {
            // signed_real_number
            //         : real_number
            //         | '-' real_number
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitReal_number([NotNull] Python3Parser.Real_numberContext context)
        {
            // real_number
            //         : NUMBER
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitImaginary_number([NotNull] Python3Parser.Imaginary_numberContext context)
        {
            // imaginary_number
            //         : NUMBER
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitCapture_pattern([NotNull] Python3Parser.Capture_patternContext context)
        {
            // capture_pattern
            //         : pattern_capture_target
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitPattern_capture_target([NotNull] Python3Parser.Pattern_capture_targetContext context)
        {
            // pattern_capture_target
            //         : /* cannot be '_' */ name { this.CannotBeDotLpEq() }?
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitWildcard_pattern([NotNull] Python3Parser.Wildcard_patternContext context)
        {
            // wildcard_pattern
            //         : '_'
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitValue_pattern([NotNull] Python3Parser.Value_patternContext context)
        {
            // value_pattern
            //         : attr { this.CannotBeDotLpEq() }?
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitAttr([NotNull] Python3Parser.AttrContext context)
        {
            // attr
            //         : name ('.' name)+
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitName_or_attr([NotNull] Python3Parser.Name_or_attrContext context)
        {
            // name_or_attr
            //         : attr
            //         | name
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitGroup_pattern([NotNull] Python3Parser.Group_patternContext context)
        {
            // group_pattern
            //         : '(' pattern ')'
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitSequence_pattern([NotNull] Python3Parser.Sequence_patternContext context)
        {
            // sequence_pattern
            //         : '[' maybe_sequence_pattern? ']'
            //         | '(' open_sequence_pattern? ')'
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitOpen_sequence_pattern([NotNull] Python3Parser.Open_sequence_patternContext context)
        {
            // open_sequence_pattern
            //         : maybe_star_pattern ',' maybe_sequence_pattern?
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitMaybe_sequence_pattern([NotNull] Python3Parser.Maybe_sequence_patternContext context)
        {
            // maybe_sequence_pattern
            //         : maybe_star_pattern (',' maybe_star_pattern)* ','?
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitMaybe_star_pattern([NotNull] Python3Parser.Maybe_star_patternContext context)
        {
            // maybe_star_pattern
            //         : star_pattern
            //         | pattern
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitStar_pattern([NotNull] Python3Parser.Star_patternContext context)
        {
            // star_pattern
            //         : '*' pattern_capture_target
            //         | '*' wildcard_pattern
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitMapping_pattern([NotNull] Python3Parser.Mapping_patternContext context)
        {
            // mapping_pattern
            //         : '{' '}'
            //         | '{' double_star_pattern ','? '}'
            //         | '{' items_pattern ',' double_star_pattern ','? '}'
            //         | '{' items_pattern ','? '}'
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitItems_pattern([NotNull] Python3Parser.Items_patternContext context)
        {
            // items_pattern
            //         : key_value_pattern (',' key_value_pattern)*
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitKey_value_pattern([NotNull] Python3Parser.Key_value_patternContext context)
        {
            // key_value_pattern
            //         : (literal_expr | attr) ':' pattern
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitDouble_star_pattern([NotNull] Python3Parser.Double_star_patternContext context)
        {
            // double_star_pattern
            //         : '**' pattern_capture_target
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitClass_pattern([NotNull] Python3Parser.Class_patternContext context)
        {
            // class_pattern
            //         : name_or_attr '(' ')'
            //         | name_or_attr '(' positional_patterns ','? ')'
            //         | name_or_attr '(' keyword_patterns ','? ')'
            //         | name_or_attr '(' positional_patterns ',' keyword_patterns ','? ')'
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitPositional_patterns([NotNull] Python3Parser.Positional_patternsContext context)
        {
            // positional_patterns
            //         : pattern (',' pattern)*
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitKeyword_patterns([NotNull] Python3Parser.Keyword_patternsContext context)
        {
            // keyword_patterns
            //         : keyword_pattern (',' keyword_pattern)*
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitKeyword_pattern([NotNull] Python3Parser.Keyword_patternContext context)
        {
            // keyword_pattern
            //         : name '=' pattern
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitTest([NotNull] Python3Parser.TestContext context)
        {
            // test
            //         : or_test ('if' or_test 'else' test)?
            //         | lambdef
            //         ;

            if (context.children.Count==1)
            {
                return this.Visit(context.children[0]);
            }

            if (context.children[0] is Or_testContext ot)
            {
                var e = this.Visit(ot);
                    System.Diagnostics.Debugger.Break();
            }

            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitTest_nocond([NotNull] Python3Parser.Test_nocondContext context)
        {
            // test_nocond
            //         : or_test
            //         | lambdef_nocond
            //         ;

            return this.Visit(context.children[0]);
        }

        public override IPythonTreeNode VisitLambdef([NotNull] Python3Parser.LambdefContext context)
        {
            // lambdef
            //         : 'lambda' varargslist? ':' test
            //         ;

            if (context.ChildCount==4)
            {
                var list = (PythonExpressionList)this.Visit(context.children[1]);
                
                var operation = (IPythonExpression)this.Visit(context.children[3]);
                return new PythonLambdaExpression(list.args.Cast<PythonArgument>().ToArray(), operation);
            }
            else
            {
                var operation = (IPythonExpression)this.Visit(context.children[2]);
                System.Diagnostics.Debugger.Break();
                return new PythonLambdaExpression(new PythonArgument[0], operation);
            }
        }

        public override IPythonTreeNode VisitLambdef_nocond([NotNull] Python3Parser.Lambdef_nocondContext context)
        {
            // lambdef_nocond
            //         : 'lambda' varargslist? ':' test_nocond
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitOr_test([NotNull] Python3Parser.Or_testContext context)
        {
            // or_test
            //         : and_test ('or' and_test)*
            //         ;

            List<IPythonExpression> args = new List<IPythonExpression>();
            for (int i = 0; i < context.children.Count; i += 2)
            {
                args.Add(this.Visit(context.children[i]) as IPythonExpression);
            }

            if (args.Count() == 1)
            {
                return args[0];
            }

            return new PythonOperatorExpression("or", args.ToArray());
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitAnd_test([NotNull] Python3Parser.And_testContext context)
        {
            // and_test
            //         : not_test ('and' not_test)*
            //         ;

            List<IPythonExpression> args = new List<IPythonExpression>();
            for (int i = 0; i < context.children.Count; i += 2)
            {
                args.Add(this.Visit(context.children[i]) as IPythonExpression);
            }

            if (args.Count() == 1)
            {
                return args[0];
            }

            return new PythonOperatorExpression("and", args.ToArray());
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitNot_test([NotNull] Python3Parser.Not_testContext context)
        {
            // not_test
            //         : 'not' not_test
            //         | comparison
            //         ;

            if (context.children[0] is ComparisonContext cc)
            {
                return this.Visit(cc);
            }
            else
            {
                var exp = this.Visit(context.children[1]) as IPythonExpression;
                return new PythonUnaryOperatorExpression("not", exp);
            }
        }

        public override IPythonTreeNode VisitComparison([NotNull] Python3Parser.ComparisonContext context)
        {
            // comparison
            //         : expr (comp_op expr)*
            //         ;

            List<IPythonExpression> args = new List<IPythonExpression>();
            List<string> ops = new List<string>();

            for (int i = 0; i < context.children.Count; i += 2)
            {
                args.Add(this.Visit(context.children[i]) as IPythonExpression);
                if (i > 1)
                {
                    ops.Add(context.children[i - 1].GetText());
                }
            }

            if (args.Count() == 1)
            {
                return args[0];
            }

            List<IPythonExpression> pythonExpressions = new List<IPythonExpression>();
            for (int i = 0; i < ops.Count; i++)
            {
                pythonExpressions.Add(new PythonOperatorExpression(ops[i], args[i], args[i + 1]));
            }

            if (pythonExpressions.Count() == 1)
            {
                return pythonExpressions[0];
            }

            return new PythonOperatorExpression("and", pythonExpressions.ToArray());
        }

        public override IPythonTreeNode VisitComp_op([NotNull] Python3Parser.Comp_opContext context)
        {
            // comp_op
            //         : '<'
            //         | '>'
            //         | '=='
            //         | '>='
            //         | '<='
            //         | '<>'
            //         | '!='
            //         | 'in'
            //         | 'not' 'in'
            //         | 'is'
            //         | 'is' 'not'
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitStar_expr([NotNull] Python3Parser.Star_exprContext context)
        {
            // star_expr
            //         : '*' expr
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitExpr([NotNull] Python3Parser.ExprContext context)
        {
            // expr
            //         : atom_expr
            //         | expr '**' expr
            //         | ('+' | '-' | '~')+ expr
            //         | expr ('*' | '@' | '/' | '%' | '//') expr
            //         | expr ('+' | '-') expr
            //         | expr ('<<' | '>>') expr
            //         | expr '&' expr
            //         | expr '^' expr
            //         | expr '|' expr
            //         ;

            if (context.children[0] is Atom_exprContext ac)
            {
                return this.Visit(ac);
            }
            else if (context.children[0] is ExprContext exp)
            {
                var left = this.Visit(context.children[0]) as IPythonExpression;
                var right = this.Visit(context.children[2]) as IPythonExpression;
                var op = context.children[1].GetText();
                return new PythonOperatorExpression(op, left, right);
            }
            else
            {
                var right = this.Visit(context.children[^1]) as IPythonExpression;
                for (int i = context.children.Count - 1; i >= 0; i--)
                {
                    var op = context.children[i].GetText();
                    right = new PythonUnaryOperatorExpression(op, right);
                }

                return right;
            }

            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitAtom_expr([NotNull] Python3Parser.Atom_exprContext context)
        {
            // atom_expr
            //         : AWAIT? atom trailer*
            //         ;

            int pos = 0;
            bool doAwait = false;
            if (context.children[0].GetText() == "await")
            {
                doAwait = true;
                pos++;
            }

            IPythonExpression atom = (IPythonExpression)this.Visit(context.children[pos++]);

            while (context.ChildCount > pos)
            {
                var trailer = context.children[pos++] as TrailerContext;
                var first = trailer.children[0].GetText();

                // trailer
                //         : '(' arglist? ')'
                //         | '[' subscriptlist ']'
                //         | '.' name
                //         ;

                if (first == "(")
                {
                    if (trailer.children.Count == 2)
                    {
                        atom= new PythonFunctionExpression(atom);
                    }
                    else
                    {
                        var arglist = (PythonExpressionList)this.Visit(trailer.children[1]);
                        atom= new PythonFunctionExpression(atom, arglist.args.OfType<PythonArgument>().ToArray());
                    }
                }
                else if (first == "[")
                {
                    var sublist = (PythonExpressionList)this.Visit(trailer.children[1]);
                    var recastSublist = sublist.args.Cast<PythonExpressionList>().ToArray();
                    atom = new PythonArrayIndexerExpression(atom, recastSublist);
                }
                else if (first == ".")
                {
                    atom = new PythonPropertyExpression(atom, trailer.children[1].GetText(), null);
                }
            }

            if (doAwait)
            {
                atom = new PythonUnaryOperatorExpression("await", atom);
            }

            return atom;
        }

        public override IPythonTreeNode VisitAtom([NotNull] Python3Parser.AtomContext context)
        {
            // atom
            //         : '(' (yield_expr | testlist_comp)? ')'
            //         | '[' testlist_comp? ']'
            //         | '{' dictorsetmaker? '}'
            //         | name
            //         | NUMBER
            //         | STRING+
            //         | '...'
            //         | 'None'
            //         | 'True'
            //         | 'False'
            //

            if (context.children[0] is NameContext nc)
            {
                return this.Visit(nc);
            }

            string first = context.children[0].GetText();
            if (first == "(")
            {
                System.Diagnostics.Debugger.Break();
            }
            else if (first == "[")
            {
                if (context.ChildCount == 2)
                {
                    return new PythonArrayExpression();
                }

                var n = this.Visit(context.children[1]);
                if (n is PythonExpressionList list)
                {
                    return new PythonArrayExpression(list.args);
                }
                else
                {
                    return new PythonArrayExpression(n as IPythonExpression);
                }
            }
            else if (first == "{")
            {
                if (context.ChildCount == 2)
                {
                    return new PythonObjectExpression();
                }

                return this.Visit(context.children[1]);
            }
            else if (first == "...")
            {
                System.Diagnostics.Debugger.Break();
            }
            else if (first == "True" || first == "true" || first == "False" || first == "false" || first == "None" || first == "none")
            {
                return new PythonBoolLiteralExpression(first);
            }
            else if (first.Length > 0 && ((first[0] >= '0' && first[0] <= '9') || first[0] == '.'))
            {
                return new PythonNumberLiteralExpression(first);
            }
            else
            {
                return new PythonStringExpression(first);
            }

            return null;
        }

        public override IPythonTreeNode VisitName([NotNull] Python3Parser.NameContext context)
        {
            // name
            //         : NAME
            //         | '_'
            //         | 'match'
            //         ;

            string first = context.children[0].GetText();
            if (first == "match")
            {
                System.Diagnostics.Debugger.Break();
            }

            return new PythonIdentifierExpression(first);
        }

        public override IPythonTreeNode VisitTestlist_comp([NotNull] Python3Parser.Testlist_compContext context)
        {
            // testlist_comp
            //         : (test | star_expr) (comp_for | (',' (test | star_expr))* ','?)
            //         ;

            var p1 = this.Visit(context.children[0]) as IPythonExpression;
            if (context.ChildCount==1)
            {
                return p1;
            }

            if (context.children[1].GetText() == ",")
            {
                List<IPythonExpression> list = new List<IPythonExpression>() { p1 };
                for (int i = 2; i < context.ChildCount; i += 2)
                {
                    list.Add(this.Visit(context.children[i]) as IPythonExpression);
                }

                return new PythonExpressionList(list.ToArray());
            }
            else if (context.children[1] is Comp_forContext cc)
            {
                var p2 = this.Visit(cc.children[1]);

                System.Diagnostics.Debugger.Break();
            }
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitTrailer([NotNull] Python3Parser.TrailerContext context)
        {
            // trailer
            //         : '(' arglist? ')'
            //         | '[' subscriptlist ']'
            //         | '.' name
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitSubscriptlist([NotNull] Python3Parser.SubscriptlistContext context)
        {
            // subscriptlist
            //         : subscript_ (',' subscript_)* ','?
            //         ;

            List<IPythonExpression> list = new List<IPythonExpression>();
            foreach (var child in context.children)
            {
                if (child is Subscript_Context subscript)
                {
                    var sub=this.Visit(subscript);
                    list.Add((IPythonExpression)sub);
                }
            }

            return new PythonExpressionList(list.ToArray());
        }

        public override IPythonTreeNode VisitSubscript_([NotNull] Python3Parser.Subscript_Context context)
        {
            // subscript_
            //         : test
            //         | test? ':' test? sliceop?
            //         ;


            List<IPythonExpression> list = new List<IPythonExpression>();
            foreach (var child in context.children)
            {
                if (child is TestContext subscript)
                {
                    list.Add((IPythonExpression)this.Visit(child));
                }
                else if (child is SliceopContext sliceop)
                {
                    // sliceop
                    //         : ':' test?
                    //         ;
                    if (sliceop.ChildCount == 2)
                    {
                        while(list.Count<2)
                        {
                            list.Add(new PythonBlankExpression());
                        }

                        list.Add((IPythonExpression)this.Visit(sliceop.children[1]));
                    }
                }
                else
                {
                    if (list.Count==0)
                    { 
                        list.Add(new PythonBlankExpression());
                    }
                }
            }

            return new PythonExpressionList(list.ToArray());
        }

        public override IPythonTreeNode VisitSliceop([NotNull] Python3Parser.SliceopContext context)
        {
            // sliceop
            //         : ':' test?
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitExprlist([NotNull] Python3Parser.ExprlistContext context)
        {
            // exprlist
            //         : (expr | star_expr) (',' (expr | star_expr))* ','?
            //         ;

            if (context.ChildCount == 1)
            {
                return this.Visit(context.children[0]);
            }

            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitTestlist([NotNull] Python3Parser.TestlistContext context)
        {
            // testlist
            //         : test (',' test)* ','?
            //         ;

            if (context.ChildCount == 1)
            {
                return this.Visit(context.children[0]);
            }

            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitDictorsetmaker([NotNull] Python3Parser.DictorsetmakerContext context)
        {
            // dictorsetmaker
            //         : (
            //             ((test ':' test | '**' expr) (comp_for | (',' (test ':' test | '**' expr))* ','?))
            //             | ((test | star_expr) (comp_for | (',' (test | star_expr))* ','?))
            //         )
            //         ;
            string lastToken = null;
            IPythonExpression left = null;
            IPythonExpression right = null;
            IPythonExpression unpack = null;
            IPythonExpression comprehension = null;
            List<IPythonExpression> list = new List<IPythonExpression>();
            foreach (var child in context.children)
            {
                if (child is TestContext tc)
                {
                    var e = (IPythonExpression)this.Visit(child);
                    if (lastToken == "**")
                    {
                        unpack = e;
                    }
                    else if (lastToken == ":")
                    {
                        right = e;
                    }
                    else
                    {
                        left = e;
                    }
                    lastToken = null;
                }
                else if (child is Comp_forContext cfc)
                {
                    comprehension = (IPythonExpression)this.Visit(child);
                    lastToken = null;
                }
                else if (child is Star_exprContext sec)
                {
                    var e = (IPythonExpression)this.Visit(child);
                    System.Diagnostics.Debugger.Break();
                    lastToken = null;
                }
                else if (child is ExprContext expr)
                {
                    var e = (IPythonExpression)this.Visit(child);
                    if (lastToken == "**")
                    {
                        unpack = e;
                    }
                    else if (lastToken == ":")
                    {
                        right = e;
                    }
                    else
                    {
                        left = e;
                    }
                    lastToken = null;
                }
                else if (child.GetText() == "**")
                {
                    lastToken = child.GetText();
                }
                else if (child.GetText() == ":")
                {
                    lastToken = child.GetText();
                }
                else if (child.GetText() == ",")
                {
                    list.Add(DictItem(left, right, unpack, comprehension));
                    left = null;
                    right = null;
                    unpack = null;
                    comprehension = null;
                    lastToken = child.GetText();
                }
                else
                {
                    System.Diagnostics.Debugger.Break();
                }
            }

            if (left != null || unpack != null || comprehension != null)
            {
                list.Add(DictItem(left, right, unpack, comprehension));
            }

            return new PythonObjectExpression(list.ToArray());
        }

        private IPythonExpression DictItem(IPythonExpression left, IPythonExpression right, IPythonExpression unpack, IPythonExpression comprehension)
        {
            if (comprehension!=null)
            {
                return new PythonDictComprehensionExpression(left, right, comprehension);

            }
            if (right!=null)
            {
                return new PythonKeyValuePair(left, right);
            }
            else 
            {
                return new PythonUnaryOperatorExpression("**", unpack);
            }
        }

        public override IPythonTreeNode VisitClassdef([NotNull] Python3Parser.ClassdefContext context)
        {
            // classdef
            //         : 'class' name ('(' arglist? ')')? ':' block
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitArglist([NotNull] Python3Parser.ArglistContext context)
        {
            // arglist
            //         : argument (',' argument)* ','?
            //         ;

            List<IPythonExpression> list = new List<IPythonExpression>();
            for (int i = 0; i < context.ChildCount; i += 2)
            {
                var a = this.Visit(context.children[i]);
                list.Add(a as IPythonExpression);
            }

            return new PythonExpressionList(list.ToArray());
        }

        public override IPythonTreeNode VisitArgument([NotNull] Python3Parser.ArgumentContext context)
        {
            // argument
            //         : (test comp_for? | test '=' test | '**' test | '*' test)
            //         ;

            var first = context.children[0].GetText();
            if (context.ChildCount == 3)
            {
                var c = this.Visit(context.children[2]) as IPythonExpression;
                return new PythonArgument(first, c);
            }
            else if (first == "**")
            {
                System.Diagnostics.Debugger.Break();

            }
            else if (first == "*")
            {
                System.Diagnostics.Debugger.Break();

            }
            else
            {
                var c = this.Visit(context.children[0]) as IPythonExpression;
                if (context.ChildCount == 1)
                {
                    return new PythonArgument(null, c);
                }
                else
                {
                    System.Diagnostics.Debugger.Break();
                }
            }
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitComp_iter([NotNull] Python3Parser.Comp_iterContext context)
        {
            // comp_iter
            //         : comp_for
            //         | comp_if
            //         ;
            return this.Visit(context.children[0]);
        }

        public override IPythonTreeNode VisitComp_for([NotNull] Python3Parser.Comp_forContext context)
        {
            // comp_for
            //         : ASYNC? 'for' exprlist 'in' or_test comp_iter?
            //         ;

            bool hasAsync = false;
            IPythonExpression left = null;
            IPythonExpression right = null;
            IPythonExpression compIter = null;
            foreach (var child in context.children)
            {
                if (child is ExprlistContext)
                {
                   left = (IPythonExpression)this.Visit(child);
                }
                else if (child is Or_testContext)
                {
                    right = (IPythonExpression)this.Visit(child);

                }
                else if (child is Comp_iterContext)
                {
                    compIter = (IPythonExpression)this.Visit(child);
                }
                else if (child.GetText()=="async")
                {
                    hasAsync = true;
                }
            }

            IPythonExpression atom = new PythonForInExpression(left, right, compIter);
            if (hasAsync)
            {
                atom = new PythonUnaryOperatorExpression("async", atom);
            }

            return atom;
        }

        public override IPythonTreeNode VisitComp_if([NotNull] Python3Parser.Comp_ifContext context)
        {
            // comp_if
            //         : 'if' test_nocond comp_iter?
            //         ;

            var nocond = (IPythonExpression)this.Visit(context.children[1]);
            if (context.ChildCount >= 3)
            {
                var iter = (IPythonExpression)this.Visit(context.children[2]);
                System.Diagnostics.Debugger.Break();
            }
            else
            {
                return nocond;
            }

            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitEncoding_decl([NotNull] Python3Parser.Encoding_declContext context)
        {
            // encoding_decl
            //         : name
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitYield_expr([NotNull] Python3Parser.Yield_exprContext context)
        {
            // yield_expr
            //         : 'yield' yield_arg?
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitYield_arg([NotNull] Python3Parser.Yield_argContext context)
        {
            // yield_arg
            //         : 'from' test
            //         | testlist
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

        public override IPythonTreeNode VisitStrings([NotNull] Python3Parser.StringsContext context)
        {
            // strings
            //         : STRING+
            //         ;
            System.Diagnostics.Debugger.Break();
            return VisitChildren(context);
        }

    }
}
