//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.13.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from Python3Parser.g4 by ANTLR 4.13.1

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace Python.Antlr {
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete generic visitor for a parse tree produced
/// by <see cref="Python3Parser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.13.1")]
[System.CLSCompliant(false)]
public interface IPython3ParserVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.single_input"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSingle_input([NotNull] Python3Parser.Single_inputContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.file_input"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFile_input([NotNull] Python3Parser.File_inputContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.eval_input"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitEval_input([NotNull] Python3Parser.Eval_inputContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.decorator"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDecorator([NotNull] Python3Parser.DecoratorContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.decorators"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDecorators([NotNull] Python3Parser.DecoratorsContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.decorated"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDecorated([NotNull] Python3Parser.DecoratedContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.async_funcdef"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAsync_funcdef([NotNull] Python3Parser.Async_funcdefContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.funcdef"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFuncdef([NotNull] Python3Parser.FuncdefContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.parameters"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitParameters([NotNull] Python3Parser.ParametersContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.typedargslist"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTypedargslist([NotNull] Python3Parser.TypedargslistContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.tfpdef"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTfpdef([NotNull] Python3Parser.TfpdefContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.varargslist"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVarargslist([NotNull] Python3Parser.VarargslistContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.vfpdef"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVfpdef([NotNull] Python3Parser.VfpdefContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.stmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStmt([NotNull] Python3Parser.StmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.simple_stmts"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSimple_stmts([NotNull] Python3Parser.Simple_stmtsContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.simple_stmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSimple_stmt([NotNull] Python3Parser.Simple_stmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.expr_stmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExpr_stmt([NotNull] Python3Parser.Expr_stmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.annassign"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAnnassign([NotNull] Python3Parser.AnnassignContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.testlist_star_expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTestlist_star_expr([NotNull] Python3Parser.Testlist_star_exprContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.augassign"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAugassign([NotNull] Python3Parser.AugassignContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.del_stmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDel_stmt([NotNull] Python3Parser.Del_stmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.pass_stmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPass_stmt([NotNull] Python3Parser.Pass_stmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.flow_stmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFlow_stmt([NotNull] Python3Parser.Flow_stmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.break_stmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBreak_stmt([NotNull] Python3Parser.Break_stmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.continue_stmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitContinue_stmt([NotNull] Python3Parser.Continue_stmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.return_stmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitReturn_stmt([NotNull] Python3Parser.Return_stmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.yield_stmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitYield_stmt([NotNull] Python3Parser.Yield_stmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.raise_stmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRaise_stmt([NotNull] Python3Parser.Raise_stmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.import_stmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitImport_stmt([NotNull] Python3Parser.Import_stmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.import_name"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitImport_name([NotNull] Python3Parser.Import_nameContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.import_from"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitImport_from([NotNull] Python3Parser.Import_fromContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.import_as_name"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitImport_as_name([NotNull] Python3Parser.Import_as_nameContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.dotted_as_name"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDotted_as_name([NotNull] Python3Parser.Dotted_as_nameContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.import_as_names"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitImport_as_names([NotNull] Python3Parser.Import_as_namesContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.dotted_as_names"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDotted_as_names([NotNull] Python3Parser.Dotted_as_namesContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.dotted_name"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDotted_name([NotNull] Python3Parser.Dotted_nameContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.global_stmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitGlobal_stmt([NotNull] Python3Parser.Global_stmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.nonlocal_stmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNonlocal_stmt([NotNull] Python3Parser.Nonlocal_stmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.assert_stmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAssert_stmt([NotNull] Python3Parser.Assert_stmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.compound_stmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCompound_stmt([NotNull] Python3Parser.Compound_stmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.async_stmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAsync_stmt([NotNull] Python3Parser.Async_stmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.if_stmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIf_stmt([NotNull] Python3Parser.If_stmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.while_stmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitWhile_stmt([NotNull] Python3Parser.While_stmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.for_stmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFor_stmt([NotNull] Python3Parser.For_stmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.try_stmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTry_stmt([NotNull] Python3Parser.Try_stmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.with_stmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitWith_stmt([NotNull] Python3Parser.With_stmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.with_item"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitWith_item([NotNull] Python3Parser.With_itemContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.except_clause"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExcept_clause([NotNull] Python3Parser.Except_clauseContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBlock([NotNull] Python3Parser.BlockContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.match_stmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMatch_stmt([NotNull] Python3Parser.Match_stmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.subject_expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSubject_expr([NotNull] Python3Parser.Subject_exprContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.star_named_expressions"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStar_named_expressions([NotNull] Python3Parser.Star_named_expressionsContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.star_named_expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStar_named_expression([NotNull] Python3Parser.Star_named_expressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.case_block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCase_block([NotNull] Python3Parser.Case_blockContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.guard"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitGuard([NotNull] Python3Parser.GuardContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.patterns"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPatterns([NotNull] Python3Parser.PatternsContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.pattern"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPattern([NotNull] Python3Parser.PatternContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.as_pattern"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAs_pattern([NotNull] Python3Parser.As_patternContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.or_pattern"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOr_pattern([NotNull] Python3Parser.Or_patternContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.closed_pattern"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitClosed_pattern([NotNull] Python3Parser.Closed_patternContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.literal_pattern"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLiteral_pattern([NotNull] Python3Parser.Literal_patternContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.literal_expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLiteral_expr([NotNull] Python3Parser.Literal_exprContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.complex_number"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitComplex_number([NotNull] Python3Parser.Complex_numberContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.signed_number"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSigned_number([NotNull] Python3Parser.Signed_numberContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.signed_real_number"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSigned_real_number([NotNull] Python3Parser.Signed_real_numberContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.real_number"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitReal_number([NotNull] Python3Parser.Real_numberContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.imaginary_number"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitImaginary_number([NotNull] Python3Parser.Imaginary_numberContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.capture_pattern"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCapture_pattern([NotNull] Python3Parser.Capture_patternContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.pattern_capture_target"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPattern_capture_target([NotNull] Python3Parser.Pattern_capture_targetContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.wildcard_pattern"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitWildcard_pattern([NotNull] Python3Parser.Wildcard_patternContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.value_pattern"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitValue_pattern([NotNull] Python3Parser.Value_patternContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.attr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAttr([NotNull] Python3Parser.AttrContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.name_or_attr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitName_or_attr([NotNull] Python3Parser.Name_or_attrContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.group_pattern"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitGroup_pattern([NotNull] Python3Parser.Group_patternContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.sequence_pattern"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSequence_pattern([NotNull] Python3Parser.Sequence_patternContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.open_sequence_pattern"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOpen_sequence_pattern([NotNull] Python3Parser.Open_sequence_patternContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.maybe_sequence_pattern"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMaybe_sequence_pattern([NotNull] Python3Parser.Maybe_sequence_patternContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.maybe_star_pattern"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMaybe_star_pattern([NotNull] Python3Parser.Maybe_star_patternContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.star_pattern"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStar_pattern([NotNull] Python3Parser.Star_patternContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.mapping_pattern"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMapping_pattern([NotNull] Python3Parser.Mapping_patternContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.items_pattern"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitItems_pattern([NotNull] Python3Parser.Items_patternContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.key_value_pattern"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitKey_value_pattern([NotNull] Python3Parser.Key_value_patternContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.double_star_pattern"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDouble_star_pattern([NotNull] Python3Parser.Double_star_patternContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.class_pattern"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitClass_pattern([NotNull] Python3Parser.Class_patternContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.positional_patterns"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPositional_patterns([NotNull] Python3Parser.Positional_patternsContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.keyword_patterns"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitKeyword_patterns([NotNull] Python3Parser.Keyword_patternsContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.keyword_pattern"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitKeyword_pattern([NotNull] Python3Parser.Keyword_patternContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.test"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTest([NotNull] Python3Parser.TestContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.test_nocond"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTest_nocond([NotNull] Python3Parser.Test_nocondContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.lambdef"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLambdef([NotNull] Python3Parser.LambdefContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.lambdef_nocond"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLambdef_nocond([NotNull] Python3Parser.Lambdef_nocondContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.or_test"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOr_test([NotNull] Python3Parser.Or_testContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.and_test"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAnd_test([NotNull] Python3Parser.And_testContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.not_test"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNot_test([NotNull] Python3Parser.Not_testContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.comparison"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitComparison([NotNull] Python3Parser.ComparisonContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.comp_op"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitComp_op([NotNull] Python3Parser.Comp_opContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.star_expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStar_expr([NotNull] Python3Parser.Star_exprContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExpr([NotNull] Python3Parser.ExprContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.atom_expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAtom_expr([NotNull] Python3Parser.Atom_exprContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.atom"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAtom([NotNull] Python3Parser.AtomContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.name"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitName([NotNull] Python3Parser.NameContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.testlist_comp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTestlist_comp([NotNull] Python3Parser.Testlist_compContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.trailer"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTrailer([NotNull] Python3Parser.TrailerContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.subscriptlist"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSubscriptlist([NotNull] Python3Parser.SubscriptlistContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.subscript_"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSubscript_([NotNull] Python3Parser.Subscript_Context context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.sliceop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSliceop([NotNull] Python3Parser.SliceopContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.exprlist"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExprlist([NotNull] Python3Parser.ExprlistContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.testlist"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTestlist([NotNull] Python3Parser.TestlistContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.dictorsetmaker"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDictorsetmaker([NotNull] Python3Parser.DictorsetmakerContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.classdef"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitClassdef([NotNull] Python3Parser.ClassdefContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.arglist"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitArglist([NotNull] Python3Parser.ArglistContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.argument"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitArgument([NotNull] Python3Parser.ArgumentContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.comp_iter"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitComp_iter([NotNull] Python3Parser.Comp_iterContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.comp_for"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitComp_for([NotNull] Python3Parser.Comp_forContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.comp_if"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitComp_if([NotNull] Python3Parser.Comp_ifContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.encoding_decl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitEncoding_decl([NotNull] Python3Parser.Encoding_declContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.yield_expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitYield_expr([NotNull] Python3Parser.Yield_exprContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.yield_arg"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitYield_arg([NotNull] Python3Parser.Yield_argContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="Python3Parser.strings"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStrings([NotNull] Python3Parser.StringsContext context);
}
} // namespace Python.Antlr
