using ShopApp.Business.Abstract;
using ShopApp.DataAccess.Abstract;
using ShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp.Business.Concrete
{
    public class OrderManager : IOrderService
    {
        private IOrderDal _orderdal;
        public OrderManager(IOrderDal orderdal)
        {
            _orderdal = orderdal;
        }
        public void Create(Order entity)
        {
            _orderdal.Create(entity);
        }

        public List<Order> GetAll()
        {
            return _orderdal.GetAll();
        }

        public List<Order> GetOrders(string userId)
        {
           return  _orderdal.GetOrders(userId);
        }
    }
}
