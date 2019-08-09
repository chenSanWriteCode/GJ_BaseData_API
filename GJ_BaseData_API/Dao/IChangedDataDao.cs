using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GJ_BaseData_API.Infrastructure;

namespace GJ_BaseData_API.Dao
{
    public interface IChangedDataDao
    {
        Result<List<string>> getBusChangedData(string startTime,string endTime);
        Result<List<string>> getLineChangedData(string startTime, string endTime);
        Result<List<string>> getLineUDChangedData(string startTime, string endTime);
        Result<List<string>> getLineStationChangedData(string startTime, string endTime);
    }
}
