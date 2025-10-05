namespace BlazorShWebsite.Client.Calculator;

public interface Expression
{
    string GetClassName();
}

public abstract class Operand : Expression
{
    public string GetClassName() => "operand";
}

public abstract class Operator : Expression
{
    public string GetClassName() => "operator";
}

public class IntegerOperand : Operand
{
    public int value { get; init; }

    public override string ToString()
    {
        return value.ToString();
    }
}

public class AdditionOperator : Operator
{
    public override string ToString()
    {
        return "+";
    }
}

public class SubtractionOperator : Operator
{
    public override string ToString()
    {
        return "-";
    }
}

public class DivisionOperator : Operator
{
    public override string ToString()
    {
        return "/";
    }
}

public class MultiplicationOperator : Operator
{
    public override string ToString()
    {
        return "*";
    }
}