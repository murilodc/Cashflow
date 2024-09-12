
using CashFlow.Domain.Enums;
using CashFlow.Domain.Reports;
using CashFlow.Domain.Repositories.Expenses;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;

namespace CashFlow.Application.UseCases.Expenses.Reports.Excel;
public class GenerateExpensesReportExcelUseCase : IGenerateExpensesReportExcelUseCase
{
    private const string CURRENCY_SYMBOL = "$";
    private readonly IExpenseReadOnlyRepository _repository;

    public GenerateExpensesReportExcelUseCase(IExpenseReadOnlyRepository repository)
    {
        _repository = repository;
    }

    public async Task<byte[]> Execute(DateOnly month)
    {
        var expenses = await _repository.FilterByMonth(month);
        if(expenses.Count == 0)
        {
            return [];
        }
        var workbook = new XLWorkbook();

        workbook.Author = "Murilo De Carli";
        workbook.Style.Font.FontSize = 12;
        workbook.Style.Font.FontName = "Times New Roman";

        var worksheet = workbook.Worksheets.Add(month.ToString("Y"));

        InsertHeader(worksheet);

        var row = 2;
        foreach(var expense in  expenses)
        {
            worksheet.Cell($"A{row}").Value = expense.Title;
            worksheet.Cell($"B{row}").Value = expense.Date;
            worksheet.Cell($"C{row}").Value = ConvertPaymentType(expense.PaymentType);

            worksheet.Cell($"D{row}").Value = expense.Amount;
            worksheet.Cell($"D{row}").Style.NumberFormat.Format = $"-{CURRENCY_SYMBOL} #,##0.00";

            worksheet.Cell($"E{row}").Value = expense.Description;
            row++;
        }

        worksheet.Columns().AdjustToContents();

        var file = new MemoryStream();
        workbook.SaveAs(file);

        return file.ToArray();  
    }

    private string ConvertPaymentType(PaymentType payment)
    {
        return payment switch
        {
            PaymentType.Cash => "Dinheiro",
            PaymentType.CreditCard => "Cartão de crédito",
            PaymentType.DebitCard => "Cartão de débito",
            PaymentType.EletronicTransfer => "Transferência bancária",
            _ => String.Empty
        };
    }

    private void InsertHeader(IXLWorksheet worksheet)
    {
        worksheet.Cell("A1").Value = ResourceReportGeneratorMessages.TITLE;
        worksheet.Cell("B1").Value = ResourceReportGeneratorMessages.DATE;
        worksheet.Cell("C1").Value = ResourceReportGeneratorMessages.PAYMENT_TYPE;
        worksheet.Cell("D1").Value = ResourceReportGeneratorMessages.AMOUNT;
        worksheet.Cell("E1").Value = ResourceReportGeneratorMessages.DESCRIPTION;

        worksheet.Cells("A1:E1").Style.Font.Bold = true;

        worksheet.Cells("A1:E1").Style.Fill.BackgroundColor = XLColor.FromHtml("#F5C2B6");

        worksheet.Cells("A1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cells("B1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cells("C1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cells("D1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
        worksheet.Cells("E1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
    }
}
