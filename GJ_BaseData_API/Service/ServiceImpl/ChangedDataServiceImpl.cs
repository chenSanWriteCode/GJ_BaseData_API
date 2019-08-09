using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using GJ_BaseData_API.Dao;
using GJ_BaseData_API.Infrastructure;
using GJ_BaseData_API.Models;
using Unity;

namespace GJ_BaseData_API.Service.ServiceImpl
{
    public class ChangedDataServiceImpl : IChangedDataService
    {
        [Dependency]
        public IChangedDataDao dao { get; set; }
        public async Task<Result<List<string>>> getBusChangedData(TimeQueryModel condition)
        {
            return checkTimeFormat(condition) ? await Task.Factory.StartNew(() => dao.getBusChangedData(condition.startTime, condition.endTime)) : new Result<List<string>> { success = false, message = "时间格式不正确" }; 
        }

        public async Task<Result<List<string>>> getLineChangedData(TimeQueryModel condition)
        {
            return checkTimeFormat(condition) ? await Task.Factory.StartNew(() => dao.getLineChangedData(condition.startTime, condition.endTime)) : new Result<List<string>> { success = false, message = "时间格式不正确" };
        }

        public async Task<Result<List<string>>> getLineStationChangedData(TimeQueryModel condition)
        {
            return checkTimeFormat(condition) ? await Task.Factory.StartNew(() => dao.getLineStationChangedData(condition.startTime, condition.endTime)) : new Result<List<string>> { success = false, message = "时间格式不正确" };
        }

        public async Task<Result<List<string>>> getLineUDChangedData(TimeQueryModel condition)
        {
            return checkTimeFormat(condition) ? await Task.Factory.StartNew(() => dao.getLineUDChangedData(condition.startTime, condition.endTime)) : new Result<List<string>> { success = false, message = "时间格式不正确" };
        }
        private bool checkTimeFormat(TimeQueryModel condition)
        {
            DateTime date;
            try
            {
                date = DateTime.ParseExact(condition.startTime, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
                date = DateTime.ParseExact(condition.endTime, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}