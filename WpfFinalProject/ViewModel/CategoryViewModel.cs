using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WpfFinalProject.ViewModel
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public ObservableCollection<ProductViewModel> Products { get; set; } = new ObservableCollection<ProductViewModel>();
        
    }
}
