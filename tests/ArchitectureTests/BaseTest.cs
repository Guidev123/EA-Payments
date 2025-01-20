using Microsoft.VisualStudio.TestPlatform.TestHost;
using Payments.Application;
using Payments.Domain.DomainObjects;
using Payments.Infrastructure;
using System.Reflection;

namespace ArchitectureTests;

public abstract class BaseTest
{
    protected static readonly Assembly DomainAssembly = typeof(Entity).Assembly;
    protected static readonly Assembly ApplicationAssembly = typeof(ApplicationModule).Assembly;
    protected static readonly Assembly InfrastructureAssembly = typeof(InfrastructureModule).Assembly;
    protected static readonly Assembly PresentationAssembly = typeof(Program).Assembly;
}
