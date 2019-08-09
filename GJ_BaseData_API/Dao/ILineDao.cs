using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GJ_BaseData_API.Entity;
using GJ_BaseData_API.Infrastructure;

namespace GJ_BaseData_API.Dao
{
    public interface ILineDao
    {
        Result<Line> getLineById(int lineId);
        Result<List<Line>> getLineByDepartId(int? id);
    }
}
