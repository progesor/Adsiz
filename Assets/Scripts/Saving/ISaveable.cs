// ReSharper disable once CheckNamespace
namespace ProgesorCreating.RPG.Saving
{
    public interface ISaveable
    {
        object CaptureState();
        void RestoreState(object state);
    }
}