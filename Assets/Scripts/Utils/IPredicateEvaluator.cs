// ReSharper disable once CheckNamespace
namespace ProgesorCreating.Utils
{
    public interface IPredicateEvaluator
    {
        bool? Evaluate(EPredicate predicate, string[] parameters);
    }
}