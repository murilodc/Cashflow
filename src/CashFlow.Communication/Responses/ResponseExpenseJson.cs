using CashFlow.Communication.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.Communication.Responses;
public class ResponseExpenseJson
{
    public long Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime Date { get; set; } 
    public decimal Amount { get; set; }
    public PaymentType PaymentType { get; set; }
}
