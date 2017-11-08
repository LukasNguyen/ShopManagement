﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaleShop.Data.Infrastructure;
using SaleShop.Model.Models;

namespace SaleShop.Data.Repositories
{
    public interface IApplicationUserGroupRepository:IRepository<ApplicationUserGroup>
    {
    }
    public class ApplicationUserGroupRepository:RepositoryBase<ApplicationUserGroup>,IApplicationUserGroupRepository
    {
        public ApplicationUserGroupRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
