namespace Parse;

using System;
using System.Collections.Generic;
using System.Linq;

public static class Parse
{
    private static List<Token>
        expCondition = new List<Token> { Token.highExp, Token.medExp, Token.lowExp, Token.NUM };

    private static List<Token>
        highExpCondition = new List<Token> { Token.OPENPARENTHESIS, Token.exp, Token.CLOSEPARENTHESIS };

    private static List<List<Token>>
        medExpCondition = new List<List<Token>> {
            new List<Token> { Token.exp, Token.OPMUL, Token.exp },
            new List<Token> { Token.exp, Token.OPDIV, Token.exp }};

    private static List<List<Token>>
        lowExpCondition = new List<List<Token>> {
            new List<Token> { Token.exp, Token.OPSUM, Token.exp },
            new List<Token> { Token.exp, Token.OPSUB, Token.exp }};


    public static float CalculateString(string exp)
    {
        var splited = SplitExpression(exp);
        var tokens = Tokenizer(splited);
        var decomposed = Decompose(tokens);

        return decomposed.Calculate();
    }

    private static ParseTree Decompose(List<ParseTree> tokens)
    {
        int limit = tokens.Count;
        int count = 0;
        bool shouldContinue = false;

        foreach (var t in tokens)
            Console.Write(t.Token + " ");
        Console.WriteLine("");
        while (tokens.Count > 1)
        {
            var startIndex = tokens.TakeWhile(t => t.Token != Token.OPENPARENTHESIS).Count();
            startIndex = startIndex == tokens.Count ? 0 : startIndex;

            shouldContinue = false;
            for (int i = startIndex; i < tokens.Count; i++)
                if (highExpCondition.SequenceEqual(tokens.Select(t => t.Token).Skip(i).Take(3)))
                {
                    reduceList(tokens, i, Token.highExp);
                    shouldContinue = true;
                    break;
                }

            if (shouldContinue)
                continue;

            for (int i = startIndex; i < tokens.Count; i++)
                if (medExpCondition.Any(c => c.SequenceEqual(tokens.Select(t => t.Token).Skip(i).Take(3))))
                {
                    reduceList(tokens, i, Token.medExp);
                    shouldContinue = true;
                    break;
                }

            if (shouldContinue)
                continue;


            for (int i = startIndex; i < tokens.Count; i++)
                if (lowExpCondition.Any(c => c.SequenceEqual(tokens.Select(t => t.Token).Skip(i).Take(3))))
                {
                    reduceList(tokens, i, Token.lowExp);
                    shouldContinue = true;
                    break;
                }

            if (shouldContinue)
                continue;

            for (int i = 0; i < tokens.Count; i++)
                if (expCondition.Contains(tokens[i].Token))
                {
                    var pt = new ParseTree();
                    pt.Token = Token.exp;
                    pt.NodeList.Add(tokens[i]);
                    tokens[i] = pt;
                }

            count++;
            if (count > limit)
                throw new Exception("Sintaxe Inválida");
        }

        return tokens[0];
    }

    private static void reduceList(List<ParseTree> tokens, int i, Token token)
    {
        foreach (var t in tokens)
            Console.Write(t.Token + " ");
        Console.WriteLine("");


        var pt = new ParseTree();
        pt.Token = token;

        pt.NodeList.Add(tokens[i]);
        pt.NodeList.Add(tokens[i + 1]);
        pt.NodeList.Add(tokens[i + 2]);

        tokens[i] = pt;
        tokens.RemoveAt(i + 1);
        tokens.RemoveAt(i + 1);
    }

    private static List<ParseTree> Tokenizer(List<string> list)
    {
        List<ParseTree> tokenList = new List<ParseTree>();

        foreach (var obj in list)
        {
            ParseTree tk = new ParseTree();
            tk.Value = obj;
            switch (obj)
            {
                case "+":
                    tk.Token = Token.OPSUM;
                    break;
                case "-":
                    tk.Token = Token.OPSUB;
                    break;
                case "*":
                    tk.Token = Token.OPMUL;
                    break;
                case "/":
                    tk.Token = Token.OPDIV;
                    break;
                case "(":
                    tk.Token = Token.OPENPARENTHESIS;
                    break;
                case ")":
                    tk.Token = Token.CLOSEPARENTHESIS;
                    break;
                default:
                    tk.Token = Token.NUM;
                    break;
            }
            tokenList.Add(tk);
        }
        return tokenList;
    }

    private static bool isOperator(string str)
    {
        string[] symbols = new string[] { "+", "-", "/", "*" };
        return symbols.Contains(str);
    }

    private static bool isDelimiter(string str)
    {
        string[] delimiters = new string[] { "(", "+", "-", "/", "*", ")" };
        return delimiters.Contains(str);
    }

    private static List<string> SplitExpression(string expr)
    {
        if (expr.Count(c => c == '(') != expr.Count(c => c == ')'))
            throw new Exception("Parentêses não fecham!");

        // TODO Add all your delimiters here
        var delimiters = new[] { '(', '+', '-', '*', '/', ')' };
        var buffer = string.Empty;
        var ret = new List<string>();
        expr = expr.Replace(" ", "");
        expr = expr.Replace(".", ",");
        foreach (var c in expr)
        {
            if (delimiters.Contains(c))
            {
                if (buffer.Length > 0)
                {
                    ret.Add(buffer.ToString());
                    buffer = string.Empty;
                }
                ret.Add(c.ToString());
            }
            else
                buffer += c;
        }
        if (buffer.Length > 0)
            ret.Add(buffer.ToString());


        int retLength = ret.Count();
        for (int i = 0; i < retLength; i++)
        {
            if (ret[i] != "-")
                continue;

            if (i == retLength - 1)
                throw new Exception("Expressão Inválida");

            if (i == 0)
                if (Double.TryParse(ret[i + 1], out _))
                {
                    ret[i] += ret[i + 1];
                    ret.RemoveAt(i + 1);
                    retLength--;
                }

            if (Double.TryParse(ret[i + 1], out _) && isDelimiter(ret[i - 1]))
            {
                ret[i] += ret[i + 1];
                ret.RemoveAt(i + 1);
                retLength--;
            }
        }

        if (ret.Count() == 1)
            return ret;


        var checkOp = ret.Count(obj => Double.TryParse(obj, out _)) - ret.Count(obj => isOperator(obj));
        Console.WriteLine(checkOp);
        if (checkOp != 1)
            throw new Exception("Expressão Inválida");

        return ret;
    }
}

public class ParseTree
{
    public Token Token { get; set; }
    public object Value { get; set; }
    public List<ParseTree> NodeList = new List<ParseTree>();

    public float Calculate()
    {
        switch (Token)
        {
            case Token.NUM:
                return float.Parse(Value.ToString());
            case Token.lowExp:
                if (NodeList[1].Token == Token.OPSUM)
                    return NodeList[0].Calculate() + NodeList[2].Calculate();
                else
                    return NodeList[0].Calculate() - NodeList[2].Calculate();
            case Token.medExp:
                if (NodeList[1].Token == Token.OPMUL)
                    return NodeList[0].Calculate() * NodeList[2].Calculate();
                else
                    return NodeList[0].Calculate() / NodeList[2].Calculate();
            case Token.highExp:
                return NodeList[1].Calculate();
            case Token.exp:
                return NodeList[0].Calculate();
            default:
                throw new Exception("Token Inválida: Possível erro na sintaxe da string");

        }
    }
}


public enum Token
{
    NUM,
    OPSUM,
    OPSUB,
    OPMUL,
    OPDIV,
    OPENPARENTHESIS,
    CLOSEPARENTHESIS,
    exp,
    lowExp,
    medExp,
    highExp,
}

