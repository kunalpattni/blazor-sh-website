namespace CommerceApp;

public class Calculator
{
    public List<string> input = new();
    public List<Expression> expressions = new();

    public void AppendInput(string next)
    {
        input.Add(next);
    }
    
    public void AppendOperator(Operator op)
    {
        if (input.Count <= 0 && expressions.Count > 0 && expressions[^1] is Operator)
        {
            expressions[^1] = op;
        } else if (input.Count <= 0 && expressions.Count > 0 && expressions[^1] is not Operator)
        {
            expressions.Add(op);
        }
        else if (expressions.Count > 0 && expressions[^1] is Operand)
        {
            expressions.Add(op);
            expressions.Add
            (
                new IntegerOperand
                {
                    value = Convert.ToInt32(string.Join("", input))
                }
            );
        }
        else
        {
            expressions.Add
            (
                new IntegerOperand
                {
                    value = Convert.ToInt32(string.Join("", input))
                }
            );
            expressions.Add(op);
        }

        input.Clear();
    }
    
    public void Equals()
    {
        if (input.Count <= 0)
        {
            return;
        }

        if (expressions.Count > 0 && expressions[^1] is Operand)
        {
            return;
        }
        expressions.Add
        (
            new IntegerOperand
            {
                value = Convert.ToInt32(string.Join("", input))
            }
        );
        input.Clear();
    }

    public string Calculate() 
    {
        Operator? currentOperator = null;
        var sum = 0;
        var first = true;
        try
        {
            foreach (var expression in expressions)
            {
                if (first)
                {
                    if (expression is not IntegerOperand integer) throw new Exception("no operand");
                    sum += integer.value;
                    first = false;
                    continue;
                }

                switch (expression)
                {
                    case Operator op:
                        currentOperator = op;
                        break;
                    case Operand rand:
                        var value = (rand as IntegerOperand).value;
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
        catch (DivideByZeroException ex)
        {
            return "undefined";
        }

        return sum.ToString();
    }

    public string GetInput()
    {
        var newInput = string.Join("", input);
        return newInput.Length < 1 ? "0" : newInput;
    }
}