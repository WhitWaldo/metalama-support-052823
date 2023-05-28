namespace AspectTest.Attributes;

[AttributeUsage(AttributeTargets.Class)]
internal sealed class TargetTypeAttribute : Attribute
{
    public Type EntityType { get; init; }

    public TargetTypeAttribute(Type entityType)
    {
        EntityType = entityType;
    }
}