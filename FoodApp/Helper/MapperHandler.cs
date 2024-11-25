using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace ProjectManagementSystem.Helper
{
    public static class MapperHandler
    {
        public static IMapper mapper { get; set; } = null!;

        public static TResult Map<TResult>(this object source)
        {
            return mapper.Map<TResult>(source);
        }
        public static TDestination Map<TSource, TDestination>(this TSource source, TDestination destination)
        {
            return mapper.Map(source, destination);
        }
        public static IEnumerable<TResult> Map<TResult>(this IQueryable source)
        {
            return source.ProjectTo<TResult>(mapper.ConfigurationProvider);
        }
    }
}