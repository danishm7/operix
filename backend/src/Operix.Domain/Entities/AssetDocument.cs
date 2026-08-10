using Operix.Domain.Common;

namespace Operix.Domain.Entities;

public sealed class AssetDocument : AuditableEntity
{
    public int AssetId { get; private set; }
    public string FileName { get; private set; } = string.Empty;
    public string FilePath { get; private set; } = string.Empty;
    public string FileType { get; private set; } = string.Empty;
    public long FileSize { get; private set; }
    public string? Description { get; private set; }

    // Navigation Properties
    public Asset Asset { get; private set; } = null!;
}