namespace Infra.Extensions
{
    public static class Primitive
    {
        public static string RemoveText(this string text, string key) => text.Replace(key, string.Empty);
    }
}
