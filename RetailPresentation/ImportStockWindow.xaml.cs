using Retail.Application.DTOs;
using Retail.Application.Services;
using System;
using System.Windows;

namespace RetailPresentation
{
    public partial class ImportStockWindow : Window
    {
        private readonly ProductDto _product;
        private readonly IStockService _stockService;

        public ImportStockWindow(ProductDto product, IStockService stockService)
        {
            InitializeComponent();
            _product = product;
            _stockService = stockService;

            // Hiển thị thông tin sản phẩm hiện tại
            lblProductName.Text = _product.Name;
            lblCurrentQuantity.Text = _product.Quantity.ToString();
        }

        private async void btnImport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!int.TryParse(txtImportQuantity.Text, out int qty) || qty <= 0)
                {
                    MessageBox.Show("Số lượng nhập kho phải là số nguyên lớn hơn 0.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Chặn người dùng thao tác nhiều lần trong lúc chờ
                btnImport.IsEnabled = false;

                // Gọi Unit of Work thông qua Service
                await _stockService.ImportStockAsync(_product.Id, qty, txtNote.Text);

                MessageBox.Show("Nhập kho thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi nhập kho: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                btnImport.IsEnabled = true;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}