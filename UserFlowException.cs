namespace ConnectFour;

public class UserFlowException : Exception
{
    public UserFlowAction Action { get; }

    public UserFlowException(UserFlowAction action) : base($"User flow action: {action}")
    {
        Action = action;
    }
}
