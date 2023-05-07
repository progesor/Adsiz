// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Control
{
    public interface IRaycastable
    {
        CursorType GetCursorType();
        bool HandleRaycast(PlayerController callingController);
    }
}