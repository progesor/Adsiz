// ReSharper disable once CheckNamespace
namespace ProgesorCreating.RPG.Control
{
    public interface IRaycastable
    {
        CursorType getCursorType();
        bool HandleRaycast(PlayerController callingController);
    }
}