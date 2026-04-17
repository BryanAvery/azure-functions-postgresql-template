using System.ComponentModel.DataAnnotations;

namespace FunctionApp.Configuration;

public sealed class PostgresOptions
{
    public const string SectionName = "Postgres";

    [Required(AllowEmptyStrings = false)]
    public string ConnectionString { get; init; } = string.Empty;
}
