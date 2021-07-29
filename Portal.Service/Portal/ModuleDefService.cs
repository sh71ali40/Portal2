using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Portal.DataLayer.Model;
using Portal.DataLayer.Model.Entities;

namespace Portal.Service.Portal
{
    public interface IModuleDefService
    {
        IQueryable<ModuleDef> GetAll();
    }
    public class ModuleDefService: IModuleDefService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DbSet<ModuleDef> _moduleDefDbSet;

        public ModuleDefService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
            _moduleDefDbSet = uow.Set<ModuleDef>();
        }
        public IQueryable<ModuleDef> GetAll()
        {
            return _moduleDefDbSet.Where(m => m.Enabled).OrderByDescending(m => m.ModuleDefId);
        }
    }
}
