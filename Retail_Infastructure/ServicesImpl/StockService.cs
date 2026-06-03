using Retail.Application.Factories;
using Retail.Application.Interfaces;
using Retail.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail.Infastructure.ServicesImpl
{
    public class StockService : IStockService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IStockTransactionFactory _factory;

        // Tiêm Unit of Work và Factory vào Service
        public StockService(IUnitOfWork unitOfWork, IStockTransactionFactory factory)
        {
            _unitOfWork = unitOfWork;
            _factory = factory;
        }

        public async Task ImportStockAsync(int productId, int quantity, string? note)
        {

            if (quantity <= 0)
                throw new ArgumentException("Số lượng nhập phải lớn hơn 0.");


            var product = await _unitOfWork.productRepository.GetByIdAsync(productId);
            if (product == null)
                throw new Exception("Không tìm thấy sản phẩm.");

            // 3. Cập nhật số lượng
            product.Quantity += quantity;
            await _unitOfWork.productRepository.UpdateAsync(product);

            // 4. Dùng Factory tạo giao dịch nhập kho
            var transaction = _factory.CreateImportTransaction(productId, quantity, note);
            await _unitOfWork.stockTransactionRepository.AddAsync(transaction);

            // 5. LƯU THAY ĐỔI: Chỉ gọi SaveChangesAsync đúng 1 lần ở cuối nghiệp vụ
            var check = await _unitOfWork.SaveChangesAsync();


            string s = "s";
        }
    }
}
