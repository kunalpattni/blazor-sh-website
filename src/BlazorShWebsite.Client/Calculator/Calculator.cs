namespace BlazorShWebsite.Client.Calculator;

public class Calculator
{
    private readonly List<string> _input = new();
    public readonly List<Expression> Expressions = new();

    public void AppendInput(string next)
    {
        _input.Add(next);
    }
    
    public void AppendOperator(Operator op)
    {
        if (_input.Count <= 0 && Expressions.Count > 0 && Expressions[^1] is Operator)
        {
            Expressions[^1] = op;
        } 
        else if (_input.Count <= 0 && Expressions.Count > 0 && Expressions[^1] is not Operator)
        {
            Expressions.Add(op);
        }
        else if (Expressions.Count > 0 && Expressions[^1] is Operand)
        {
            Expressions.Add(op);
            Expressions.Add
            (
                new IntegerOperand
                {
                    Value = Convert.ToInt32(string.Join("", _input))
                }
            );
        }
        else
        {
            Expressions.Add
            (
                new IntegerOperand
                {
                    Value = Convert.ToInt32(string.Join("", _input))
                }
            );
            Expressions.Add(op);
        }

        _input.Clear();
    }
    
    public void Equals()
    {
        if (_input.Count <= 0)
        {
            return;
        }

        if (Expressions.Count > 0 && Expressions[^1] is Operand)
        {
            return;
        }
        Expressions.Add
        (
            new IntegerOperand
            {
                Value = Convert.ToInt32(string.Join("", _input))
            }
        );
        _input.Clear();
    }

    public string Calculate() 
    {
        Operator? currentOperator = null;
        var sum = 0;
        var first = true;
        try
        {
            foreach (var expression in Expressions)
            {
                if (first)
                {
                    if (expression is not IntegerOperand integer) throw new Exception("no operand");
                    sum += integer.Value;
                    first = false;
                    continue;
                }

                switch (expression)
                {
                    case Operator op:
                        currentOperator = op;
                        break;
                    case Operand rand:
                        var value = (rand as IntegerOperand).Value;
                        sum = currentOperator switch
                        {
                            AdditionOperator => sum + value,
                            SubtractionOperator => sum - value,
                            MultiplicationOperator => sum * value,
                            DivisionOperator => sum / value,
                            _ => throw new ArgumentOutOfRangeException()
                        };
                        break;
                }
            }
        }
        catch (DivideByZeroException)
        {
            return "undefined";
        }

        return sum.ToString();
    }

    public string GetInput()
    {
        var newInput = string.Join("", _input);
        return newInput.Length < 1 ? "0" : newInput;
    }
}