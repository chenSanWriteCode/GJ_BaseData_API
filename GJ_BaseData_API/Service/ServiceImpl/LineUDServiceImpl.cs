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
    public class LineUDServiceImpl : ILineUDService
    {
        [Dependency]
        public ILineUDDao dao { get; set; }
        public async Task<Result<LineUD>> getLineUDById(int id)
        {
            return await Task.Factory.StartNew(() => dao.getLineUDById(id));
        }

        public async Task<Result<List<LineUD>>> getLIneUDByLineId(int? lineId)
        {
            return await Task.Factory.StartNew(() => dao.getLIneUDByLineId(lineId));
        }
    }
}