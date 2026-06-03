using Retail.Application.DTOs;
using Retail.Application.Services;
using System;
using System.Windows;

namespace RetailPresentation
{
    public partial class AddProductWindow : Window
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public AddProductWindow(ICategoryService categoryService, IProductService productService)
        {
            InitializeComponent();
            _categoryService = categoryService;
            _productService = productService;
            this.Loaded += AddProductWindow_Loaded;
        }

        private async void AddProductWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var categories = await _categoryService.GetAllCategory();
                cboCategory.ItemsSource = categories;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh mục: {ex.Message}");
            }
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validate cơ bản trên UI
                if (string.IsNullOrWhiteSpace(txtName.Text)) throw new Exception("Tên sản phẩm không được rỗng.");
                if (cboCategory.SelectedItem == null) throw new Exception("Vui lòng chọn danh mục.");
                if (!decimal.TryParse(txtPrice.Text, out decimal price) || price < 0) throw new Exception("Giá phải là số hợp lệ >= 0.");
                if (!int.TryParse(txtQuantity.Text, out int qty) || qty < 0) throw new Exception("Số lượng phải là số nguyên >= 0.");

                var selectedCategoryId = ((CategoryDTO)cboCategory.SelectedItem).Id;

                // Gọi Service để lưu (Service sẽ tạo Entity và gọi UnitOfWork)
                await _productService.AddProductAsync(new ProductDto
                {
                    Name = txtName.Text,
                    Price = price,
                    Quantity = qty,
                    CategoryId = selectedCategoryId
                });

                MessageBox.Show("Thêm sản phẩm thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true; // Trả về kết quả thành công cho MainWindow
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi hợp lệ", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}