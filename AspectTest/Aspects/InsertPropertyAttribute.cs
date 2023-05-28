using AspectTest.Attributes;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Code.SyntaxBuilders;
using Metalama.Framework.Eligibility;

namespace AspectTest.Aspects;

internal sealed class InsertPropertyAttribute : TypeAspect
{
    /// <inheritdoc />
    public override void BuildEligibility(IEligibilityBuilder<INamedType> builder)
    {
        builder.If(it => it.Attributes.OfAttributeType(typeof(TargetTypeAttribute)).Any());
        builder.If(it =>
            it.Attributes.OfAttributeType(typeof(TargetTypeAttribute)).First().ConstructorArguments.Length == 1);
    }

    /// <inheritdoc />
    public override void BuildAspect(IAspectBuilder<INamedType> builder)
    {
        var attr = builder.Target.Attributes.OfAttributeType(typeof(TargetTypeAttribute)).FirstOrDefault();
        if (attr == null)
            throw new Exception("This aspect expects the TargetType attribute to be applied to the target");

        var entityType = (INamedType) attr.ConstructorArguments[0].Value;
        if (entityType == null)
            throw new Exception("This aspect expects the first constructor argument to contain the entity type");
        
        var listType =
            ((INamedType) TypeFactory.GetType(typeof(List<>))).WithTypeArguments(entityType);
        

        builder.Advice.IntroduceField(builder.Target, "_elements", listType, IntroductionScope.Instance,
            OverrideStrategy.Ignore,
            b =>
            {
                b.Accessibility = Accessibility.Private;
                b.InitializerExpression = ExpressionFactory.Parse("new()");
            });
    }
}