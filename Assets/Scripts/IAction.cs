/// <summary>
/// Any behaviour classes should be inheraited from that interface to block any action if action performed
/// </summary>
public interface IAction 
{
    /// <summary>
    /// Should return TRUE, If the class in the state that doesnt allow interruption.
    /// </summary>
    /// <returns></returns>
    bool IsInAction();
}
