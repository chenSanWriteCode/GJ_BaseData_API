using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using GJ_BaseData_API.Entity;
using GJ_BaseData_API.Infrastructure;
using log4net;

namespace GJ_BaseData_API.Dao.LayerDao
{
    public class BusDao : IBusDao
    {
        private ILog log = LogManager.GetLogger("BusDao");
        public Result<Bus> getBusById(int id)
        {
            Result<Bus> result = new Result<Bus>();
            string sql = $"select c.f_id,c.线路id,c.车牌,c.是否报废 from gj_公交车  c ,gj_公交线路表 l,gj_公交线路组 g ,gj_depart d where c.线路id = l.f_id and l.所属线路组 = g.f_id and g.单位id = d.f_id and d.f_id in {ConstInfo.DepartRight} and c.f_id={id}";
            var context = new ORACLEHelper();
            try
            {
                DataTable dt = context.QueryTable(sql);
                LogTimesFactory factory = new LogTimesFactory();
                factory.createLogTimes(LogTimeType.Bus);
                factory.logTimes(sql);
                result.data = TableToList(dt).FirstOrDefault();
            }
            catch (Exception err)
            {
                result.addError(err.Message);
                log.Error(MethodBase.GetCurrentMethod() + err.Message);
            }
            return result;
        }

        public Result<List<Bus>> getBusByLineId(int? lineId)
        {
            Result < List < Bus >>result = new Result<List<Bus>>();
            string sql = $"select c.f_id,c.线路id,c.车牌,c.是否报废 from gj_公交车  c ,gj_公交线路表 l,gj_公交线路组 g ,gj_depart d where c.线路id = l.f_id and l.所属线路组 = g.f_id and g.单位id = d.f_id and d.f_id in {ConstInfo.DepartRight} ";
            sql += lineId == null ? "" : $" and c.线路id ={lineId} ";
            var context = new ORACLEHelper();
            try
            {
                DataTable dt = context.QueryTable(sql);
                LogTimesFactory factory = new LogTimesFactory();
                factory.createLogTimes(LogTimeType.Bus);
                factory.logTimes(sql);
                result.data = TableToList(dt);
            }
            catch (Exception err)
            {
                result.addError(err.Message);
                log.Error(MethodBase.GetCurrentMethod() + err.Message);
            }
            return result;

        }

        private List<Bus> TableToList(DataTable dt)
        {
            List<Bus> result = new List<Bus>();
            if (dt==null ||dt.Rows.Count==0)
            {
                return result;
            }
            Bus model;
            foreach (DataRow dr in dt.Rows)
            {
                model = new Bus
                {
                    busId = Convert.ToInt32(dr["f_id"]),
                    busNum = dr["车牌"].ToString(),
                    lineId = Convert.ToInt32(dr["线路id"]),
                    isAbandoned= Convert.ToInt32(dr["是否报废"])
                };
                result.Add(model);
            }
            return result;
        }
    }
}