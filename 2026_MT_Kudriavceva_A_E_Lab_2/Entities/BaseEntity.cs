namespace Entities;

public abstract class BaseEntity<TKey> where TKey : struct
{
    public abstract TKey Id { get; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public virtual string ToLogString(string additionalInfo = "")
    {
        return $"[{GetType().Name}] Id={Id} {additionalInfo}".Trim();
    }
}