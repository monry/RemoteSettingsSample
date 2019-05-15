using ModestTree;

namespace Zenject
{
    public static class Extensions
    {
        public static ConcreteIdBinderNonGeneric BindInterfaces<T>(this DiContainer container)
        {
            return container.Bind(typeof(T).Interfaces());
        }
    }
}