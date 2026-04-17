namespace FunctionApp.Contracts;

public sealed record CreateCustomerResult(CustomerResponse? Customer, IReadOnlyCollection<string> Errors)
{
    public bool IsSuccess => Errors.Count == 0;

    public static CreateCustomerResult Success(CustomerResponse customer) => new(customer, Array.Empty<string>());

    public static CreateCustomerResult Failure(params string[] errors) => new(null, errors);
}
