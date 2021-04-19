using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labixa.ViewModels;
using Outsourcing.Data.Models;

namespace Outsourcing.Core.Common
{
    public static class OrderCalculator
    {
        /// <summary>
        /// Tính tổng tiền của 1 đơn hàng
        /// </summary>
        /// <param name="order">đơn hàng cần tính tổng</param>
        /// <param name="Percent">Phần trăm giảm giá nếu có</param>
        /// <returns>Đơn hàng đã tính tổng</returns>
        public static Order CalcTotalPayment(Order order, int Percent = 0)
        {
            double total = 0;
            foreach (var item in order.OrderItems)
            {
                if (item.Product.IsHomePage)
                {
                    item.Price = item.Product.Price;
                }
                else
                {
                    item.Price = item.Product.OldPrice;
                }
                total = total + (item.Price * item.Quantity);
            }
            order.Discount = Percent;
            Percent = (100 - Percent) / 100;
            //tổng trước khi khuyễn mãi
            order.OrderTotal = total;

            total = total * Percent;

            //tổng sau khi khuyến mãi
            order.TotalPayment = total;
            return order;
        }
        public static ListProductCartModel CalculatorCart(ListProductCartModel model)
        {
            if (model!=null)
            {
                double totalPayment = 0;
                foreach (var item in model.listProduct)
                {
                    totalPayment += item.Price * item.Quantity;
                }
                if (model.GiftCodePercent >0)
                {
                    var remainPercent = (100 - model.GiftCodePercent);
                    model.TotalAfterDiscount = (totalPayment * remainPercent)/100;
                }
                model.TotalPayment = totalPayment;
            }
            return model;
        }
    }
}
