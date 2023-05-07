// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Core
{
    public interface IPredicateEvaluator
    {
        bool? Evaluate(string predicate, string[] parameters);
    }
}