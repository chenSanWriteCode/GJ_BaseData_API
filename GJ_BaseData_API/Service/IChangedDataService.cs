using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GJ_BaseData_API.Infrastructure;
using GJ_BaseData_API.Models;

namespace GJ_BaseData_API.Service
{
    public interface IChangedDataService
    {
        Task<Result<List<string>>> getBusChangedData(TimeQueryModel condition);
        Task<Result<List<string>>> getLineChangedData(TimeQueryModel condition);
        Task<Result<List<string>>> getLineUDChangedData(TimeQueryModel condition);
        Task<Result<List<string>>> getLineStationChangedData(TimeQueryModel condition);
    }
}
