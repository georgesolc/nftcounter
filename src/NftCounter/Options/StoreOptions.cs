using System.ComponentModel.DataAnnotations;

namespace NftCounter.Api.Options;

public sealed class StoreOptions
{
    public const string SectionName = "StoresConfig";

    [Required]
    public StoreOptionItem[] Stores { get; init; }
}

public sealed record StoreOptionItem
(
    string Name,
    string ScriptAddress
);
