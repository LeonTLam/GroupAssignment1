using Microsoft.AspNetCore.Mvc;
using GroupAssignment1.Models;
using GroupAssignment1.ViewModels;
using GroupAssignment1.DAL;
using Microsoft.AspNetCore.Authorization;
using GroupAssignment1.Controllers;
using Microsoft.EntityFrameworkCore;

namespace GroupAssignment1.DAL
{
    public class OrderRepository : IOrderRepository
    {
        private readonly HousingDbContext _db;
        private readonly ILogger<OrderRepository> _logger;

        public OrderRepository(HousingDbContext db, ILogger<OrderRepository> logger)
        {
            _logger = logger;
            _db = db;
        }

        public async Task<IEnumerable<Order>?> GetAll()
        {
            try
            {
                return await _db.Orders.ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError("[OrderRepository] Orders ToListAsync() failed when GetAll(), error message: {e}", e.Message);
                return null;
            }

        }

        public async Task<Housing?> GetHousingById(int id)
        {
            try
            {
                return await _db.Housings.FindAsync(id);
            }
            catch (Exception e)
            {
                _logger.LogError("[OrderRepository] Orders FindAsync(id) failed when GetHousingById for HousingId {HousingId:0000}, error messasge: {e}", id, e.Message);
                return null;
            }
        }

        public async Task<Order?> GetOrderById(int id)
        {
            try
            {
                return await _db.Orders.FindAsync(id);
            }
            catch (Exception e)
            {
                _logger.LogError("[OrderRepository] Orders FindAsync(id) failed when GetHousingById for HousingId {HousingId:0000}, error messasge: {e}", id, e.Message);
                return null;
            }
        }

        public async Task<bool> CreateOrder(Order order)
        {
            try
            {
                _db.Orders.Add(order);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("[OrderRepository] Order creation failed for item {@order}, error message: {e}", order, e.Message);
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var order = await _db.Orders.FindAsync(id);
                if (order == null)
                {
                    return false;
                }

                _db.Orders.Remove(order);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("[OrderRepository] Order deletion failed for the OrderId {OrderId:0000}, error message: {e}", id, e.Message);
                return false;
            }
        }
       
    }
}
