using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;
using System.Text.RegularExpressions;

namespace Payments.Infrastructure.Persistence.Configurations.Interceptors;

public sealed class CommandsInterceptor : DbCommandInterceptor
{
    private static readonly Regex _tableAliasRegex =
        new(@"
        (?<tableAlias>FROM\s+\[dbo\]\.\[(?<tableName>\w+)\]\s+AS\s+\w+\s+WITH\s+\(NOLOCK\))",
            RegexOptions.Compiled |
            RegexOptions.IgnoreCase |
            RegexOptions.Multiline);
    public override ValueTask<InterceptionResult<DbDataReader>> ReaderExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result, CancellationToken cancellationToken = default)
    {
        if(!command.CommandText.Contains("WITH (NOLOCK)")
            && command.CommandText.StartsWith("-- DONT USE NOLOCK"))
        {
            command.CommandText = _tableAliasRegex.Replace(command.CommandText, "${tableName} (WITH NOLOCK)");
        }

        return new ValueTask<InterceptionResult<DbDataReader>>(result);
    }
}
