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
    public class DepartDao : IDepartDao
    {
        private ILog log = LogManager.GetLogger("DepartDao");
        public Result<List<Depart>> getDepart()
        {
            Result<List<Depart>> result = new Result<List<Depart>>();
            string sql = $"select d.f_id,d.f_name,d.f_pid,decode(d.f_pid,0,1,2) departLevel from gj_depart d where  d.f_id in {ConstInfo.DepartAll}";
            var context = new ORACLEHelper();
            try
            {
                DataTable dt = context.QueryTable(sql);
                LogTimesFactory factory = new LogTimesFactory();
                factory.createLogTimes(LogTimeType.Depart);
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

        public Result<Depart> getDepartById(int id)
        {
            Result<Depart> result = new Result<Depart>();
            string sql = $"select d.f_id,d.f_name,d.f_pid,decode(d.f_pid,0,1,2) departLevel from gj_depart d where  d.f_id in {ConstInfo.DepartAll} and d.f_id={id}";
            var context = new ORACLEHelper();
            try
            {
                DataTable dt = context.QueryTable(sql);
                LogTimesFactory factory = new LogTimesFactory();
                factory.createLogTimes(LogTimeType.Depart);
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
        private List<Depart> TableToList(DataTable dt)
        {
            List<Depart> result = new List<Depart>();
            if (dt == null || dt.Rows.Count == 0)
            {
                return result;
            }
            Depart model;
            foreach (DataRow dr in dt.Rows)
            {
                model = new Depart
                {
                    departName = dr["f_name"].ToString(),
                    departId = Convert.ToInt32(dr["f_id"]),
                    highLevel= Convert.ToInt32(dr["f_pid"]),
                    departLevel = Convert.ToInt32(dr["departLevel"])
                };
                result.Add(model);
            }
            return result;
        }

    }
}