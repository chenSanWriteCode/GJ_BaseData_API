using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using GJ_BaseData_API.Dao;
using GJ_BaseData_API.Entity;
using GJ_BaseData_API.Infrastructure;
using Unity;

namespace GJ_BaseData_API.Service.ServiceImpl
{
    public class DepartServiceImpl : IDepartService
    {
        [Dependency]
        public IDepartDao dao { get; set; }

        public async Task<Result<List<Depart>>> getDepart()
        {
            return await Task.Factory.StartNew(() => dao.getDepart());
        }

        public async Task<Result<Depart>> getDepartById(int id)
        {
            return await Task.Factory.StartNew(() => dao.getDepartById(id));
        }


    }
}