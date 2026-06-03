using Retail.Application.DTOs;
using Retail.Application.Services;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
// XÓA: using Retail_Application.Interfaces; (UI không được phép biết đến Repository)

namespace RetailPresentation
{
    public partial class MainWindow : Window
    {
        private readonly ICategoryService _categoryService;

        // SỬA: Chỉ tiêm Service vào UI, xóa bỏ hoàn toàn ICategoryRepository
        public MainWindow(ICategoryService categoryService)
        {
            InitializeComponent();
            _categoryService = categoryService;

            // Đăng ký sự kiện khi Window vừa load xong
            this.Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadCategoriesAsync();
        }

        private async Task LoadCategoriesAsync()
        {
            try
            {
                // SỬA LỚN NHẤT: Gọi hàm GetAllCategory() từ Service
                // Kết quả trả về lúc này là danh sách CategoryDTO sạch sẽ, an toàn
                var categories = await _categoryService.GetAllCategory();

                // Đổ dữ liệu vào ListBox
                lstCategories.ItemsSource = categories;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách danh mục: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void lstCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstCategories.SelectedItem == null) return;

            // Item được chọn lúc này chắc chắn là một CategoryDTO
            var selectedCategory = lstCategories.SelectedItem as CategoryDTO;

            if (selectedCategory != null)
            {
                try
                {
                    int categoryId = selectedCategory.Id;

                    // Lấy chi tiết category cùng danh sách product
                    var categoryWithProducts = await _categoryService.GetCategoryWithProductsAsync(categoryId);

                    if (categoryWithProducts != null && categoryWithProducts.Products != null)
                    {
                        // Đổ danh sách sản phẩm vào DataGrid bên phải
                        dgProducts.ItemsSource = categoryWithProducts.Products;
                    }
                    else
                    {
                        dgProducts.ItemsSource = null;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi tải danh sách sản phẩm: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}