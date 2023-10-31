using GroupAssignment1.Models;

namespace GroupAssignment1.DAL
{
    public interface IHousingRepository
    {
        Task<IEnumerable<Housing>?> GetAll();
        Task<Housing?> GetHousingById(int id);
        Task<bool> Create(Housing housing);
        
        Task<bool> Update(Housing housing);
        Task<bool> Delete(int id);
        
    }
}
