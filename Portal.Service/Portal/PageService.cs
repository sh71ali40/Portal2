using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Portal.DataLayer.Model;
using Portal.DataLayer.Model.Entities;


namespace Portal.Service.Portal
{
    
    public interface IPageService
    {
        Task<Page> GetPageById(int pageId);
        IQueryable<Page> GetAll();
    }
    public class PageService : IPageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DbSet<Page> _pageDbSet;

        public PageService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
            _pageDbSet = uow.Set<Page>();

        }
        public virtual async Task<Page> GetPageById(int pageId)
        {
            return await _pageDbSet.Include(p=>p.PageModules)
                .ThenInclude(p=>p.ModuleRoles)
                .Include(q=>q.PageModules)
                .ThenInclude(q=>q.ModuleDef)
                .FirstOrDefaultAsync(p => p.Id == pageId);
        }

        public virtual IQueryable<Page> GetAll()
        {
            return _pageDbSet.OrderByDescending(p=>p.Id).AsNoTracking();
        }
    }
}
