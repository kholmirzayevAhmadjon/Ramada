using Newtonsoft.Json;
using Ramada.Domain.Commons;
using Ramada.Service.Configurations;
using Ramada.Service.Exceptions;
using Ramada.Service.Helpers;
using System.Linq.Expressions;

namespace Ramada.Service.Extensions;

public static class CollectionExtensions
{
    public static IQueryable<T> OrderBy<T>(this IQueryable<T> entities,
                                           Filter filter) where T : Auditable
    {
        var expression = entities.Expression;

        var parameter = Expression.Parameter(typeof(T), "x");

        MemberExpression selector;
        try
        {
            selector = Expression.PropertyOrField(parameter, filter?.OrderBy ?? "Id");
        }
        catch (Exception)
        {

            throw new ArgumentIsNotValidException("Specified Property Is Not Found");
        }

        var method = string.Equals(filter?.OrderType 
                                    ?? "asc", "desc", StringComparison.OrdinalIgnoreCase) 
                                ? "OrderByDescending" : "OrderBy";

        expression = Expression.Call(typeof(Queryable),
                                     method,
                                     [entities.ElementType, selector.Type],
                                     expression,
                                     Expression.Quote(Expression.Lambda(selector)));

        return entities.Provider.CreateQuery<T>(expression);
    }

    public static IQueryable<T> ToPaginate<T>(this IQueryable<T> entities,
                                              PaginationParams @params) where T : Auditable
    {
        var metaData = new PaginationMetaData(entities.Count(), @params);
        var json = JsonConvert.SerializeObject(metaData);
        var key = "X-Pagination";

        if (HttpContextHelper.ResponseHeaders is not null &&
            HttpContextHelper.ResponseHeaders.ContainsKey(key))
            HttpContextHelper.ResponseHeaders.Remove(key);

        HttpContextHelper.ResponseHeaders?.Add(key, json);

        return @params.PageIndex > 0 && @params.PageSize > 0 ?
            entities.Skip((@params.PageIndex - 1) * @params.PageSize).Take(@params.PageSize) :
            throw new ArgumentIsNotValidException("Pagination parameters is not valid");
    }
}
