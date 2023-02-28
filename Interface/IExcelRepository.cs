using TFG.Models;

namespace TFG.Interface
{
    public interface IExcelRepository
    {
        Task<List<ExcelModel>> GetExcelRow(IFormFile file, string fileName);
      
    }
}
