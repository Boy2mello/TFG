using ExcelDataReader;
using Microsoft.Extensions.Hosting.Internal;
using System.Collections.Generic;
using TFG.Interface;
using TFG.Models;

namespace TFG.Repository
{
    public class ExcelRepository : IExcelRepository
    {
        public async Task<List<ExcelModel>> GetExcelRow(IFormFile file, string fileFullPath)
        {
            var fileExtention = new FileInfo(fileFullPath).Extension;
            if (fileExtention != ".xls" && fileExtention != ".xlsx")
            {
                return new List<ExcelModel>()
                    {
                      new ExcelModel()
                      {
                          HasErrors = true,
                          Error = "The file type uploaded is not valid, please upload valid Excel file."
                      }
                    };
            }

            using (FileStream fileStream = System.IO.File.Create(fileFullPath))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }

            return await GetRow(file.FileName);
        }

        private async Task<List<ExcelModel>> GetRow(string fileName)
        {
            List<ExcelModel> excel = new List<ExcelModel>();
            var dirfileName = $"{Directory.GetCurrentDirectory()}\\{@"wwwroot\excelFiles"}\\{fileName}";
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = System.IO.File.Open(dirfileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {

                    while (reader.Read())
                    {
                        var col = new ExcelModel();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            try
                            {
                                col.ColumnItems.Add(int.Parse(Convert.ToString(reader.GetValue(i))));
                            }
                            catch (Exception ex)
                            {
                                col.Error = ex.Message;
                                col.HasErrors = true;
                            }
                        }
                        excel.Add(col);
                    }
                }
            }
            return (excel);
        }
    }
}
