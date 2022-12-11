using OfficeOpenXml;

namespace Zaku
{
    public class ExcelService
    {
        private readonly string outFileName;

        public ExcelService(string startegyName, string fileName)
        {
            this.outFileName = $"{startegyName}_{fileName}.xlsx";
        }

        public void OutputAsExcelFile(Report report)
        {
            using (var package = new ExcelPackage())
            {
                var sheet = package.Workbook.Worksheets.Add("backTestReport");

                // サマリー
                sheet.Cells[1, 1].Value = "Total Profit";
                sheet.Cells[2, 1].Value = report.Profit;

                sheet.Cells[1, 2].Value = "Win (%)";
                sheet.Cells[2, 2].Value = report.RateWin;

                sheet.Cells[1, 3].Value = "Lose (%)";
                sheet.Cells[2, 3].Value = report.RateLose;

                sheet.Cells[1, 4].Value = "Lose (%)";
                sheet.Cells[2, 4].Value = report.RateLose;

                sheet.Cells[1, 5].Value = "Max Drawdown";
                sheet.Cells[2, 5].Value = report.MaxDrawdown;

                // 各トレードの結果
                int row = 3;
                sheet.Cells[row, 1].Value = "OrderId";
                sheet.Cells[row, 2].Value = "Symbol";
                sheet.Cells[row, 3].Value = "Profit";
                sheet.Cells[row, 4].Value = "Type";
                sheet.Cells[row, 5].Value = "Side";
                sheet.Cells[row, 6].Value = "EntryPrice";
                sheet.Cells[row, 7].Value = "ClosePrice";
                sheet.Cells[row, 8].Value = "Lots";
                sheet.Cells[row, 9].Value = "Win";

                row = 4;
                foreach (var item in report.List)
                {
                    sheet.Cells[row, 1].Value = item.OrderId;
                    sheet.Cells[row, 2].Value = item.Symbol;
                    sheet.Cells[row, 3].Value = item.Profit;
                    sheet.Cells[row, 4].Value = item.Type;
                    sheet.Cells[row, 5].Value = item.Side;
                    sheet.Cells[row, 6].Value = item.EntryPrice;
                    sheet.Cells[row, 7].Value = item.ClosePrice;
                    sheet.Cells[row, 8].Value = item.Lots;
                    sheet.Cells[row, 9].Value = item.Profit > 0 ? 1 : 0;

                    row++;
                }

                //sheet.Cells.AutoFitColumns();

                var excelBytes = package.GetAsByteArray();
                var downloadPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                var distPath = Path.Combine(downloadPath, this.outFileName);
                File.WriteAllBytes(distPath, excelBytes);

                Console.WriteLine(distPath + Environment.NewLine + "The BackTest-Report exported.");
            }
        }
    }
}
