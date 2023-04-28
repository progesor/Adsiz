namespace ProgesorCreating.Core
{
    public interface IPredicateEvaluator
    {
        bool? Evaluate(Predicate predicate, string[] parameters);
    }
}