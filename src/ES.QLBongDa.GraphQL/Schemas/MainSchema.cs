using Abp.Dependency;
using GraphQL.Types;
using GraphQL.Utilities;
using ES.QLBongDa.Queries.Container;
using System;

namespace ES.QLBongDa.Schemas
{
    public class MainSchema : Schema, ITransientDependency
    {
        public MainSchema(IServiceProvider provider) :
            base(provider)
        {
            Query = provider.GetRequiredService<QueryContainer>();
        }
    }
}