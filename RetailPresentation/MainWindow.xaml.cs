using Retail.Application.DTOs;
using Retail.Application.Services;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RetailPresentation
{
    public partial class MainWindow : Window
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IStockService _stockService;

        // Tiêm cả 3 service thông qua DI
        public MainWindow(ICategoryService categoryService, IProductService productService, IStockService stockService)
        {
            InitializeComponent();
            _categoryService = categoryService;
            _productService = productService;
            _stockService = stockService;
            this.Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            try
            {
                // Tải danh sách Categories vào ComboBox
                var categories = await _categoryService.GetAllCategory();
                cboCategories.ItemsSource = categories;

                // Tải toàn bộ Products vào DataGrid (mặc định)
                var products = await _productService.GetAllProductsAsync();
                dgProducts.ItemsSource = products;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void cboCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboCategories.SelectedItem is CategoryDTO selectedCategory)
            {
                try
                {
                    // Lọc sản phẩm theo Category
                    var categoryWithProducts = await _categoryService.GetCategoryWithProductsAsync(selectedCategory.Id);
                    dgProducts.ItemsSource = categoryWithProducts?.Products;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi lọc sản phẩm: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private async void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            cboCategories.SelectedItem = null;
            await LoadDataAsync();
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            // Truyền các Service cần thiết sang màn hình Add
            var addWindow = new AddProductWindow(_categoryService, _productService);
            if (addWindow.ShowDialog() == true)
            {
                // Refresh lại dữ liệu nếu thêm thành công
                btnRefresh_Click(null, null);
            }
        }

        private void btnImportStock_Click(object sender, RoutedEventArgs e)
        {
            if (dgProducts.SelectedItem is ProductDto selectedProduct)
            {
                var importWindow = new ImportStockWindow(selectedProduct, _stockService);
                if (importWindow.ShowDialog() == true)
                {
                    // Refresh lại dữ liệu sau khi nhập kho thành công
                    btnRefresh_Click(null, null);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một sản phẩm từ danh sách để nhập kho.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}