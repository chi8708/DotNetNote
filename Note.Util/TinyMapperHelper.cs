using Nelibur.ObjectMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.Util
{
    public static class TinyMapperHelper
    {
        public static TDestination MapTo<TSource,TDestination>(this TSource source,bool isBind=true) where TSource:class,new()
        {
            if (isBind)
            {
                TinyMapper.Bind<TSource, TDestination>();
            }

            return TinyMapper.Map<TDestination>(source);
        }

        public static TDestination MapTo<TDestination>(this object source)
        {
            return TinyMapper.Map<TDestination>(source);
        }

        public static List<TDestination> MapToList<TSource, TDestination>(this IEnumerable<TSource> source, bool isBind = true) where
            TSource : class,new() 
        {
            if (isBind)
            {
                TinyMapper.Bind<TSource, TDestination>();
            }
            List<TDestination> tResult = new List<TDestination>();

            foreach (var item in source)
            {
                tResult.Add(item.MapTo<TDestination>());
            }

            return tResult;
        }
    }
}
