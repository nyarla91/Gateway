namespace Extentions.Pause
{
    public interface IPauseReadService
    {
        bool IsPaused { get; }
        bool IsUnpaused { get; }
    }
}