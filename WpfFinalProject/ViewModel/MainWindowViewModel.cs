using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml;
using Linq_examples;
using WpfApplication1.ViewModel;
using WpfFinalProject.Annotations;

namespace WpfFinalProject.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private CategoryViewModel _selectedCategory;
        private ProductViewModel _selectedProduct;

        public ObservableCollection<CategoryViewModel> Categories { get; set; }
            = new ObservableCollection<CategoryViewModel>();

        public CategoryViewModel SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                if (value == _selectedCategory) return;

                _selectedCategory = value;
                SelectedProduct = null;
                SelectedCategoryProducts.Clear();
                foreach (var product in _selectedCategory.Products)
                {
                    SelectedCategoryProducts.Add(product);
                }
            }
        }

        public ObservableCollection<ProductViewModel> SelectedCategoryProducts { get; set; }
            = new ObservableCollection<ProductViewModel>();

        public ProductViewModel SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));
            }
        }

        public RelayCommand AddProductCommand { get; set; }

        public MainWindowViewModel()
        {
            AddProductCommand = new RelayCommand(AddNewProduct, _ => SelectedCategory != null);
            Init();
        }

        private void Init()
        {
            List<ProductViewModel> allProducts = new List<ProductViewModel>();
            using (var xmlReader = XmlReader.Create("products.xml"))
            {
                DataContractSerializer dcs = new DataContractSerializer(typeof(List<Product>));
                var prods = (List<Product>)dcs.ReadObject(xmlReader);

                int i = 1;

                foreach (var product in prods)
                {
                    allProducts.Add(
                        new ProductViewModel()
                        {
                            CategoryId = product.CategoryID,
                            ProductName = product.Name,
                            UnitPrice = product.UnitPrice,
                            QuantityPerUnit = product.QuantityPerUnit,
                            ImagePath = $@"..\..\Images\{i}.bmp"
                        });
                    i++;
                }
            }

            using (var xmlReader = XmlReader.Create("categories.xml"))
            {
                DataContractSerializer dcs = new DataContractSerializer(typeof(List<Category>));
                var categories = (List<Category>)dcs.ReadObject(xmlReader);
                foreach (var category in categories)
                {
                    var c = new CategoryViewModel()
                    {
                        CategoryName = category.Name,
                        CategoryId = category.CategoryID
                    };
                    var relatedProducts = allProducts.Where(p => p.CategoryId == c.CategoryId);
                    foreach (var relatedProduct in relatedProducts)
                    {
                        c.Products.Add(relatedProduct);
                    }
                    Categories.Add(c);
                }
            }
        }

        private void AddNewProduct(object parameter)
        {
            var newProduct = new ProductViewModel() { ProductName = "Enter name" };
            SelectedCategoryProducts.Add(newProduct);
            SelectedCategory.Products.Add(newProduct);
            SelectedProduct = newProduct;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
