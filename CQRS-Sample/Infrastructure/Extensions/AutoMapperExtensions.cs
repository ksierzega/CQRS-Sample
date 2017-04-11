

using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;


namespace CQRS_Sample.Infrastructure.Extensions
{
    public static class AutoMapperExtensions
    {
        public static MapperConfiguration MapperConfiguration { get; set; }

        public static IQueryable<TDestination> ProjectTo<TDestination>(this IQueryable source)
        {
            return source.ProjectTo<TDestination>(MapperConfiguration);
        }
    }
}