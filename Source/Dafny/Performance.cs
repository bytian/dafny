using Microsoft.Boogie;
using System;

namespace Microsoft.Dafny {

public class Performance {

    public static bool showToken = true;

    public static Token getPerformanceToken(Scanner scanner) {
        Token t = new Token();
        t.pos = scanner.get_pos();
        t.col = scanner.get_col();
        t.line = scanner.get_line();
        t.filename = scanner.get_Filename();
        t.kind = 1;
        t.val = "_StepCnt";
        return t;
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
}

}
