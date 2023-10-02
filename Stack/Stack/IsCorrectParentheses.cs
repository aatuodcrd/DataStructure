using System;
using System.Collections.Generic;

class InfixToPostfixConverter
{
    private static Dictionary<char, int> Precedence = new Dictionary<char, int>
    {
        { '+', 1 },
        { '-', 1 },
        { '*', 2 },
        { '/', 2 }
    };

    public static string InfixToPostfix(string infixExpression)
    {
        List<char> postfix = new List<char>(); // List to store the postfix expression
        Stack<char> stack = new Stack<char>(); // Stack to temporarily hold operators

        foreach (char character in infixExpression)
        {
            if (char.IsLetterOrDigit(character)) // Operand
            {
                postfix.Add(character); // Append operands to the postfix expression
            }
            else if (character == '(') // Open parenthesis
            {
                stack.Push(character); // Push open parenthesis onto the stack
            }
            else if (character == ')') // Close parenthesis
            {
                while (stack.Count > 0 && stack.Peek() != '(')
                {
                    postfix.Add(stack.Pop()); // Pop operators and append to postfix until open parenthesis is encountered
                }
                stack.Pop(); // Discard the open parenthesis
            }
            else if (Precedence.ContainsKey(character)) // Operator
            {
                while (stack.Count > 0 && stack.Peek() != '(' && Precedence[character] <= Precedence.GetValueOrDefault(stack.Peek(), 0))
                {
                    postfix.Add(stack.Pop()); // Pop operators with higher or equal precedence and append to postfix
                }
                stack.Push(character); // Push the current operator onto the stack
            }
        }

        while (stack.Count > 0)
        {
            postfix.Add(stack.Pop()); // Pop any remaining operators and append to postfix
        }

        return new string(postfix.ToArray()); // Convert the postfix list to a string
    }

    public static double EvaluatePostfix(string postfixExpression)
    {
        Stack<double> stack = new Stack<double>();

        foreach (char character in postfixExpression)
        {
            if (char.IsDigit(character))
            {
                stack.Push(double.Parse(character.ToString()));
            }
            else if (Precedence.ContainsKey(character))
            {
                double operand2 = stack.Pop();
                double operand1 = stack.Pop();
                switch (character)
                {
                    case '+':
                        stack.Push(operand1 + operand2);
                        break;
                    case '-':
                        stack.Push(operand1 - operand2);
                        break;
                    case '*':
                        stack.Push(operand1 * operand2);
                        break;
                    case '/':
                        if (operand2 != 0)
                        {
                            stack.Push(operand1 / operand2);
                        }
                        else
                        {
                            throw new DivideByZeroException("Division by zero");
                        }
                        break;
                }
            }
        }

        return stack.Pop();
    }

    static void Main()
    {
        Console.WriteLine("Enter an infix expression (use characters as operands and operators):");
        string infixExpression = Console.ReadLine();
        string postfixExpression = InfixToPostfix(infixExpression);
        Console.WriteLine("Infix Expression: " + infixExpression);
        Console.WriteLine("Postfix Expression: " + postfixExpression);

        try
        {
            double result = EvaluatePostfix(postfixExpression);
            Console.WriteLine("Result: " + result);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}
