using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Expenses.GetAll;
public interface IGetExpenseByIdUseCase
{
    Task<ResponseExpenseJson> Execute(long id);
}
