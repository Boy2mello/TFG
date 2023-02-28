using TFG.CodeExtensions;
namespace TFG.Models
{
    public class ExcelModel
    {
        public List<int>? ColumnItems { get; set; } = new List<int> ();  
        public bool? HasErrors { get; set; } = false;   
        public string ? Error { get; set; }    
    }
}
