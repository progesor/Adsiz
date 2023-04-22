// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Control
{
    public interface IRaycastable
    {
        CursorType getCursorType();
        bool HandleRaycast(PlayerController callingController);
    }
}