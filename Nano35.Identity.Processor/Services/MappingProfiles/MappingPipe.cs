using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Nano35.Identity.Processor.Services.MappingProfiles
{
    public static class MappingPipe
    {
        public static IMapper Mapper; 
        public static List<TOut> MapAllTo<TOut>(this IQueryable<object> value)
        {
            return value.Select(a => Mapper.Map<TOut>(a)).ToList();
        }
        public static async Task<List<TOut>> MapAllToAsync<TOut>(this IQueryable<object> value)
        {
            return await value.Select(a => Mapper.Map<TOut>(a)).ToListAsync();
        }
        public static TOut MapTo<TOut>(this object value)
        {
            return Mapper.Map<TOut>(value);
        }
    }
}