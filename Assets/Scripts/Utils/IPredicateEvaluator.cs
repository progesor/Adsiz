// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Utils
{
    public interface IPredicateEvaluator
    {
        bool? Evaluate(string predicate, string[] parameters);
    }
}