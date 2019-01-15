using Microsoft.Boogie;
using System;
using System.Collections.Generic;

namespace Microsoft.Dafny {

public class Performance {

    public static bool showToken = true;
    public static string performanceVariableName = "StepCnt";
    public static bool performanceOn = true;

    public static void modifyMethodBody(BlockStmt body) {
        if (!performanceOn) return;
        for (int i = body.Body.Count - 1; i >= 0; --i) {
            body.Body.Insert(i, getUpdateStmt(body.Body[i].Tok, 1));
        }
    }

    public static Token getPerformanceToken(IToken tt) {
        return getToken(tt, performanceVariableName, 1);
    }

    public static Token getNumberToken(IToken tt, int i) {
        return getToken(tt, i.ToString(), 2);
    }

    public static Token getAddToken(IToken tt) {
        return getToken(tt, "+", 102);
    }

    public static Token getAssignToken(IToken tt) {
        return getToken(tt, ":=", 29);;
    }

    public static Token getToken(IToken tt, string str, int kind) {
        Token t = new Token();
        t.pos = tt.pos;
        t.col = tt.col;
        t.line = tt.line;
        t.filename = tt.filename;
        t.kind = kind;
        t.val = str;
        return t;
    }

    public static NameSegment getStepCntNameSegment(IToken tt) {
        return new NameSegment(getPerformanceToken(tt), performanceVariableName, null);
    }

    public static LiteralExpr getNumberLiteral(IToken tt, int i) {
        return new LiteralExpr(getNumberToken(tt, i), i);
    }

    public static BinaryExpr getAddExpr(IToken tt, int i) {
        return new BinaryExpr(getAddToken(tt), BinaryExpr.Opcode.Add, getStepCntNameSegment(tt), getNumberLiteral(tt, i));
    }

    public static ExprRhs getRhs(IToken tt, int i) {
        return new ExprRhs(getAddExpr(tt, i));
    }
    
    public static UpdateStmt getUpdateStmt(IToken tt, int i) {
        List<Expression> Lhss = new List<Expression>();
        List<AssignmentRhs> Rhss = new List<AssignmentRhs>();
        Lhss.Add(getStepCntNameSegment(tt));
        Rhss.Add(getRhs(tt, i));
        return new UpdateStmt(getAddToken(tt), getToken(tt, ";", 34), Lhss, Rhss);
    }

    public static void printToken(Token t) {
        if (!showToken) return;
        Console.WriteLine("Token " + t.val);
        Console.WriteLine("\tpos: " + t.pos.ToString());
        Console.WriteLine("\tcol: " + t.col.ToString());
        Console.WriteLine("\tline: " + t.line.ToString());
        Console.WriteLine("\tfilename: " + t.filename);
        Console.WriteLine("\tkind: " + t.kind.ToString());
    }

    public static void printIToken(IToken t) {
        Console.WriteLine("Token " + t.val);
        Console.WriteLine("\tpos: " + t.pos.ToString());
        Console.WriteLine("\tcol: " + t.col.ToString());
        Console.WriteLine("\tline: " + t.line.ToString());
        Console.WriteLine("\tfilename: " + t.filename);
        Console.WriteLine("\tkind: " + t.kind.ToString());
    }

    public static string whatStmt(Statement stmt) {
        if (stmt is AssignSuchThatStmt) {
            return "AssignSuchThatStmt";
        } else if (stmt is UpdateStmt) {
            return "UpdateStmt";
        } else if (stmt is AssignStmt) {
            return "AssignStmt";
        } else if (stmt is CallStmt) {
            return "CallStmt";
        } else if (stmt is BlockStmt) {
            return "BlockStmt";
        } else if (stmt is IfStmt) {
            return "IfStmt";
        } else if (stmt is AlternativeStmt) {
            return "AlternativeStmt";
        } else if (stmt is LoopStmt) {
            return "LoopStmt";
        } else if (stmt is ForallStmt) {
            return "ForallStmt";
        } else if (stmt is ModifyStmt) {
            return "ModifyStmt";
        } else if (stmt is CalcStmt) {
            return "CalcStmt";
        } else if (stmt is MatchStmt) {
            return "MatchStmt";
        } else if (stmt is SkeletonStatement) {
            return "SkeletonStmt";
        } else if (stmt is PredicateStmt) {
            return "PredicateStmt";
        } else if (stmt is PrintStmt) {
            return "PrintStmt";
        } else if (stmt is RevealStmt) {
            return "RevealStmt";
        } else if (stmt is BreakStmt) {
            return "BreakStmt";
        } else if (stmt is ReturnStmt) {
            return "ReturnStmt";
        } else if (stmt is YieldStmt) {
            return "YieldStmt";
        } else if (stmt is VarDeclStmt) {
            return "VarDeclStmt";
        } else if (stmt is LetStmt) {
            return "LetStmt";
        } else return "null";
    }

    public static string whatExpr(Expression expr) {
        if (expr is LiteralExpr) {
            return "LiteralExpr";
        } else if (expr is IdentifierExpr) {
            return "IdentifierExpr";
        } else if (expr is BinaryExpr) {
            return "BinaryExpr";
        } else if (expr is DisplayExpression) {
            return "DisplayExpression";
        } else if (expr is MapDisplayExpr) {
            return "MapDisplayExpr";
        } else if (expr is MemberSelectExpr) {
            return "MemberSelectExpr"; 
        } else if (expr is SeqSelectExpr) {
            return "SeqSelectExpr";
        } else if (expr is MultiSelectExpr) {
            return "MultiSelectExpr";
        } else if (expr is SeqUpdateExpr) {
            return "SeqUpdateExpr";
        } else if (expr is ApplyExpr) {
            return "ApplyExpr";
        } else if (expr is RevealExpr) {
            return "RevealExpr";
        } else if (expr is FunctionCallExpr) {
            return "FunctionCallExpr";
        } else if (expr is MultiSetFormingExpr) {
            return "MultiSetFormingExpr";
        } else if (expr is OldExpr) {
            return "OldExpr";
        } else if (expr is UnchangedExpr) {
            return "UnchangedExpr";
        } else if (expr is UnaryOpExpr) {
            return "UnaryOpExpr";
        } else if (expr is ConversionExpr) {
            return "ConversionExpr";
        } else if (expr is TernaryExpr) {
            return "TernaryExpr";
        } else if (expr is LetExpr) {
            return "LetExpr";
        } else if (expr is NamedExpr) {
            return "NamedExpr";
        } else if (expr is ComprehensionExpr) {
            return "ComprehensionExpr";
        } else if (expr is WildcardExpr) {
            return "WildcardExpr";
        } else if (expr is StmtExpr) {
            return "StmtExpr";
        } else if (expr is ITEExpr) {
            return "ITEExpr";
        } else if (expr is MatchExpr) {
            return "MatchExpr";
        } else if (expr is BoxingCastExpr) {
            return "BoxingCastExpr";
        } else if (expr is UnboxingCastExpr) {
            return "UnboxingCastExpr";
        } else if (expr is SuffixExpr) {
            return "SuffixExpr";
        } else if (expr is DatatypeUpdateExpr) {
            return "DatatypeUpdateExpr";
        } else if (expr is NameSegment) {
            return "NameSegment";
        } else if (expr is ChainingExpression) {
            return "ChainingExpression";
        } else if (expr is ParensExpression) {
            return "ParensExpression";
        } else if (expr is NegationExpression) {
            return "NegationExpression";
        }
        else if (expr is ConcreteSyntaxExpression) {
            return "ConcreteSyntaxExpression";
        } else if (expr is DatatypeValue) {
            return "DatatypeValue";
        } else if (expr is ThisExpr) {
            return "ThisExpr";
        } else return "null";
    }

    public static void printStmt(Statement stmt) {
        Console.WriteLine("================================");
        Console.WriteLine(whatStmt(stmt));
        Console.WriteLine("Tok = ");
        printIToken(stmt.Tok);
        Console.WriteLine("EndTok = ");
        printIToken(stmt.EndTok);
        Console.WriteLine("Label Start");
        for (LList<Label> l = stmt.Labels; l != null; l = l.Next) {
            printIToken(l.Data.Tok);
            Console.WriteLine("Name = " + l.Data.Name);
            // Console.WriteLine("UniqueId = " + l.Data.uniqueId);
        }
        Console.WriteLine("Label End");
        if (stmt.Attributes == null) {
            Console.WriteLine("No Attributes");
        } else {
            Console.WriteLine("Attributes : " + stmt.Attributes.Name);
        }
        if (stmt is UpdateStmt) {
            Console.WriteLine("Lhss Start");
            foreach (var expr in ((UpdateStmt) stmt).Lhss) {
                Console.WriteLine(whatExpr(expr));
                printIToken(expr.tok);
                if (expr is NameSegment) {

                    Console.WriteLine(((NameSegment) expr).Name);
                    if (((NameSegment) expr).OptTypeArguments == null) {
                        Console.WriteLine("null");
                    } else {
                        Console.WriteLine(((NameSegment) expr).OptTypeArguments.Count);
                    }
                }
            }
                    
            Console.WriteLine("Lhss End");
            Console.WriteLine("Rhss Start");
            foreach (var rhs in ((UpdateStmt) stmt).Rhss) {
                printIToken(rhs.Tok);
                if (rhs is ExprRhs) {
                    Console.WriteLine("ExprRhs " + whatExpr(((ExprRhs) rhs).Expr));
                    printIToken(((ExprRhs) rhs).Expr.tok);
                    if (((ExprRhs) rhs).Expr is BinaryExpr) {
                        Console.WriteLine("E0: " + whatExpr(((BinaryExpr) ((ExprRhs) rhs).Expr).E0));
                        printIToken(((BinaryExpr) ((ExprRhs) rhs).Expr).E0.tok);
                        Console.WriteLine("E1: " + whatExpr(((BinaryExpr) ((ExprRhs) rhs).Expr).E1));
                        printIToken(((BinaryExpr) ((ExprRhs) rhs).Expr).E1.tok);
                    }
                } else {
                    Console.WriteLine("OtherRhs");
                }
            }
            Console.WriteLine("Rhss End");
        }
        Console.WriteLine("================================");
    }

    public static void printMethodBody(BlockStmt body) {
        body.Body.ForEach(stmt => printStmt(stmt));
    }
}

}
