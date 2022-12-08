using OfficeOpenXml;

namespace Zaku
{
    public class ExcelService
    {
        private readonly string startegyName;

        public ExcelService(string startegyName)
        {
            this.startegyName = startegyName;
        }

        public void OutputAsExcelFile(Report report)
        {
            using (var package = new ExcelPackage())
            {
                var sheet = package.Workbook.Worksheets.Add("backTestReport");
                int row = 1;

                sheet.Cells[row, 1].Value = "OrderId";
                sheet.Cells[row, 2].Value = "Symbol";
                sheet.Cells[row, 3].Value = "Profit";
                sheet.Cells[row, 4].Value = "Type";
                sheet.Cells[row, 5].Value = "Side";
                sheet.Cells[row, 6].Value = "EntryPrice";
                sheet.Cells[row, 7].Value = "SettlementPrice";
                sheet.Cells[row, 8].Value = "Lots";
                sheet.Cells[row, 9].Value = "Win";

                row = 2;

                foreach (var item in report.List)
                {
                    sheet.Cells[row, 1].Value = item.OrderId;
                    sheet.Cells[row, 2].Value = item.Symbol;
                    sheet.Cells[row, 3].Value = item.Profit;
                    sheet.Cells[row, 4].Value = item.Type;
                    sheet.Cells[row, 5].Value = item.Side;
                    sheet.Cells[row, 6].Value = item.EntryPrice;
                    sheet.Cells[row, 7].Value = item.SettlementPrice;
                    sheet.Cells[row, 8].Value = item.Lots;
                    sheet.Cells[row, 9].Value = item.Profit > 0 ? 1 : 0;

                    row++;
                }

                sheet.Cells.AutoFitColumns();

                var excelBytes = package.GetAsByteArray();
                var downloadPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                var distPath = Path.Combine(downloadPath, $"{this.startegyName}.xlsx");
                File.WriteAllBytes(distPath, excelBytes);

                Console.WriteLine(distPath + Environment.NewLine + "The BackTest-Report exported.");
            }
        }
    }
}
