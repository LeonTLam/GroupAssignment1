using Microsoft.EntityFrameworkCore;
using GroupAssignment1.Models;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace GroupAssignment1.DAL
{
    public class HousingRepository : IHousingRepository
    {
        private readonly HousingDbContext _db;
        private readonly ILogger<HousingRepository> _logger;

        public HousingRepository(HousingDbContext db, ILogger<HousingRepository> logger) 
        {
            _db = db;
            _logger = logger;
        }

        public async Task<IEnumerable<Housing>?> GetAll()
        {
            try
            {
                return await _db.Housings.ToListAsync();
            } 
            catch (Exception e) 
            {
                _logger.LogError("[HousingRepository] housings ToListAsync() failed when GetAll(), error message: {e}", e.Message);
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
                _logger.LogError("[HousingRepository] housing FindAsync(id) failed when GetHousingById for HousingId {HousingId:0000}, error messasge: {e}", id, e.Message);
                return null;
            }
        }

        public async Task<bool> Create(Housing housing)
        {
            try
            {
                _db.Housings.Add(housing);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("[HousingRepository] housing creation failed for item {@housing}, error message: {e}", housing, e.Message);
                return false;
            }
        }

        public async Task<bool> Update(Housing housing)
        {
            try
            {
                _db.Housings.Update(housing);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("[HousingRepository] housing FindAsync(id) failed when updating the HousingId {HousingId:0000}, error message: {e}", housing, e.Message);
                return false;
            }

        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var housing = await _db.Housings.FindAsync(id);
                if (housing == null)
                {
                    return false;
                }

                _db.Housings.Remove(housing);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("[HousingRepository] housing deletion failed for the HousingId {HousingId:0000}, error message: {e}", id, e.Message);
                return false;
            }
        }
        
        

    }
}
