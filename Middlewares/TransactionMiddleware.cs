
using KOG.ECommerce.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace KOG.ECommerce.Middlewares;

public class TransactionMiddleware : IMiddleware
{
    Entities _context;

    public TransactionMiddleware( Entities context)
    {
        _context = context;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var method = context.Request.Method.ToUpper();
        if (method == "POST" || method == "PUT" || method == "DELETE")
        {
            var transaction = _context.Database.BeginTransaction();

            try
            {
                await next(context);
                _context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                throw;
            }
        }
        else
        {
            await next(context);
        }
    }
}
