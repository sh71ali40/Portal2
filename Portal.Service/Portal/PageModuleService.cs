using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Portal.DataLayer.Model;
using Portal.DataLayer.Model.Entities;
using Portal.Statics;


namespace Portal.Service.Portal
{
    public interface IPageModuleService
    {
        PageModule GetById(int id);
        List<PageModule> GetPageModule(int pageId);
        IQueryable<PageModule> GetByPageId(int pageId);
        IQueryable<PageModule> GetLayoutModule();
    }

    public class PageModuleService : IPageModuleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DbSet<PageModule> _pageModuleDbSet;
        private IMemoryCache _cache;

        public PageModuleService(IUnitOfWork uow, IMemoryCache memoryCache)
        {
            _unitOfWork = uow;
            _pageModuleDbSet = uow.Set<PageModule>();
            _cache = memoryCache;
        }


        public PageModule GetById(int id)
        {
            return _pageModuleDbSet
                .Include(m => m.Page)
                .Include(m => m.ModuleDef).Include(m => m.ModuleSettings)
                .Include(m => m.ModuleRoles)
                .ThenInclude(m => m.Role.UserRoles)
                .FirstOrDefault(m => m.ModuleId == id);
        }

        public List<PageModule> GetPageModule(int pageId)
        {
            //var inCaheModules = _cache.Get<List<PageModule>>(Variable.CacheKeys.InPageModules);
            //var pageModules = inCaheModules.Where(m => m.PageId == pageId).ToList();
            //if (pageModules.Count !=0)
            //{
            //    return pageModules;
            //}
            var modules = _pageModuleDbSet.Include(pm => pm.ModuleDef).Include(pm => pm.Page).Where(pm =>
                pm.PageId == pageId
                && pm.ModuleRoles.Any() && pm.IsVisible).ToList();
            //inCaheModules.AddRange(modules);
            //_cache.Set(Variable.CacheKeys.InPageModules,inCaheModules);
            return modules;
        }

        public IQueryable<PageModule> GetLayoutModule()
        {
            return _pageModuleDbSet.Include(p => p.ModuleDef).Where(pm => (
                                                                              pm.PaneName == Variable.PageSectionName
                                                                                  .Header.ToString()
                                                                              || pm.PaneName == Variable.PageSectionName
                                                                                  .Footer.ToString())
                                                                          && pm.ModuleRoles.Any() && pm.IsVisible);
        }

        public IQueryable<PageModule> GetByPageId(int pageId)
        {
            return _pageModuleDbSet.Where(pm => pm.PageId == pageId);
        }
    }
}