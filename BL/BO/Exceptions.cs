
namespace BO;
[Serializable]
public class BlDoesNotExistException : Exception
{
    public BlDoesNotExistException(string? message) : base(message) { }
    public BlDoesNotExistException(string message, Exception innerException)
                : base(message, innerException) { }
}

public class BlNullPropertyException : Exception
{
    public BlNullPropertyException(string? message) : base(message) { }
}
public class BlAlreadyExistsException : Exception
{
    public BlAlreadyExistsException(string message) : base(message) { }
    public BlAlreadyExistsException(string message, Exception innerException) : base(message, innerException) { }
}
public class BlDependesOnIt: Exception
{
    public BlDependesOnIt(string? message) : base(message) { }
}

public class BlIncorrectDetails : Exception
{
    public BlIncorrectDetails(string? message) : base(message) { }
}
public class EngineerHaveTask : Exception
{
    public EngineerHaveTask(string? message) : base(message) { }
}