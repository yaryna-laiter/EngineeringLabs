using System.Data.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace EfCoreQueryOptimizationLab.Diagnostics;

public class QueryCounterInterceptor : DbCommandInterceptor
{
    private int _queryCount;

    public int QueryCount => _queryCount;

    public void Reset()
    {
        Interlocked.Exchange(ref _queryCount, 0);
    }

    private void Increment()
    {
        Interlocked.Increment(ref _queryCount);
    }

    public override InterceptionResult<DbDataReader> ReaderExecuting(
        DbCommand command,
        CommandEventData eventData,
        InterceptionResult<DbDataReader> result)
    {
        Increment();

        return base.ReaderExecuting(
            command,
            eventData,
            result);
    }

    public override ValueTask<InterceptionResult<DbDataReader>>
        ReaderExecutingAsync(
            DbCommand command,
            CommandEventData eventData,
            InterceptionResult<DbDataReader> result,
            CancellationToken cancellationToken = default)
    {
        Increment();

        return base.ReaderExecutingAsync(
            command,
            eventData,
            result,
            cancellationToken);
    }
}