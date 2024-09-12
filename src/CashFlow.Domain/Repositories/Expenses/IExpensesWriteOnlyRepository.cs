using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses;
public interface IExpensesWriteOnlyRepository
{
    Task Add(Expense expense);
    /// <summary>
    /// This function return TRUE if the deletion was succesfull
    /// </summary>
    /// <param name="expense"></param>
    /// <returns></returns>
    Task<bool> Delete(long id);
}
