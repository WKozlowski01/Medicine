namespace WebApplication4.Exceptions;

public class ToManyMedicamentsExists:Exception
{
    public ToManyMedicamentsExists()
    {
    }

    public ToManyMedicamentsExists(string? message) : base(message)
    {
    }

    public ToManyMedicamentsExists(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}