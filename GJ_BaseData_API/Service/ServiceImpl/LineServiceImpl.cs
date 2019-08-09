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
    public class LineServiceImpl : ILineService
    {
        [Dependency]
        public ILineDao dao { get; set; }
        public async Task<Result<List<Line>>> getLineByDepartId(int? id)
        {
            return await Task.Factory.StartNew(() => dao.getLineByDepartId(id));
        }

        public async Task<Result<Line>> getLineById(int lineId)
        {
            return await Task.Factory.StartNew(() => dao.getLineById(lineId));
        }
    }
}