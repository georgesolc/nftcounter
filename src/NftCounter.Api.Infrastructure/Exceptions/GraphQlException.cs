using GraphQL;

namespace NftCounter.Api.Infrastructure.Exceptions;

public sealed class GraphQlException : Exception
{
    public GraphQlException(string message) : base(message) { }
    public GraphQlException(GraphQLError[] errors) : base(string.Join(Environment.NewLine, errors.Select(e => e.Message))) { }
}
