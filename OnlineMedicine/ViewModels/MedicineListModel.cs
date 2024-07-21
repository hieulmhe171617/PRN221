using OnlineMedicine.Models;

namespace OnlineMedicine.ViewModels
{
    public class MedicineListModel
    {
        public string? SearchKeyword { get; set; }
        public List<int>? CategoryIds { get; set; }
        
        public int SortType { get; set; }

        public int? CountryId { get; set; }
        public List<Medicine> Medicines { get; set; }
        public List<Category> Categories { get; set; }

        public List<Country> Countries { get; set; }
    }
}
