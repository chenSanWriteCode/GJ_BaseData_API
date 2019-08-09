using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using GJ_BaseData_API.Infrastructure;
using log4net;

namespace GJ_BaseData_API.Dao.LayerDao
{
    public class ChangedDataDao : IChangedDataDao
    {
        private ILog log = LogManager.GetLogger("ChangedDataDao");
        public Result<List<string>> getLineChangedData(string startTime, string endTime)
        {
            Result<List<string>> result = new Result<List<string>>();
            string sql = $"select s.ybid from syn_log s , gj_公交线路表 l, gj_公交线路组 g, gj_depart d where s.ybid=l.f_id and l.所属线路组 = g.f_id  and g.单位id = d.f_id  and d.f_id in {ConstInfo.DepartRight}  and s.adddatetime >= to_date('{startTime}', 'yyyy-MM-dd HH:mi:ss') and s.adddatetime < to_date('{endTime}', 'yyyy-MM-dd HH:mi:ss') and s.sjly = 'GJ_公交线路表'";
            var context = new ORACLEHelper();
            try
            {
                DataTable dt = context.QueryTable(sql);
                LogTimesFactory factory = new LogTimesFactory();
                factory.createLogTimes(LogTimeType.ChangeData);
                factory.logTimes(sql);
                List<string> list = new List<string>();
                foreach (DataRow item in dt.Rows)
                {
                    list.Add(item[0].ToString());
                }
                result.data = list;
            }
            catch (Exception err)
            {
                result.addError(err.Message);
                log.Error(MethodBase.GetCurrentMethod() + err.Message);
            }
            return result;
        }

        public Result<List<string>> getBusChangedData(string startTime, string endTime)
        {
            Result<List<string>> result = new Result<List<string>>();
            string sql = $"select distinct(s.ybid) from syn_log s,gj_公交车 c, gj_公交线路表 l, gj_公交线路组 g, gj_depart d where s.ybid=c.f_id and c.线路id = l.f_id and l.所属线路组 = g.f_id  and g.单位id = d.f_id  and d.f_id in {ConstInfo.DepartRight} and s.adddatetime >= to_date('{startTime}', 'yyyy-MM-dd HH:mi:ss') and s.adddatetime < to_date('{endTime}', 'yyyy-MM-dd HH:mi:ss') and s.sjly = 'GJ_公交车'";
            var context = new ORACLEHelper();
            try
            {
                DataTable dt = context.QueryTable(sql);
                LogTimesFactory factory = new LogTimesFactory();
                factory.createLogTimes(LogTimeType.ChangeData);
                factory.logTimes(sql);
                List<string> list = new List<string>();
                foreach (DataRow item in dt.Rows)
                {
                    list.Add(item[0].ToString());
                }
                result.data = list;
            }
            catch (Exception err)
            {
                result.addError(err.Message);
                log.Error(MethodBase.GetCurrentMethod() + err.Message);
            }
            return result;
        }

        public Result<List<string>> getLineStationChangedData(string startTime, string endTime)
        {
            Result<List<string>> result = new Result<List<string>>();
            string sql = $"select distinct(ls.线路id) from syn_log s ,gj_线路站点表 ls, gj_公交线路表 l, gj_公交线路组 g, gj_depart d where s.ybid=ls.f_id and ls.线路id = l.f_id and l.所属线路组 = g.f_id  and g.单位id = d.f_id  and d.f_id in {ConstInfo.DepartRight} and s.adddatetime >= to_date('{startTime}', 'yyyy-MM-dd HH:mi:ss') and s.adddatetime < to_date('{endTime}', 'yyyy-MM-dd HH:mi:ss') and s.sjly = 'GJ_线路站点表'";
            var context = new ORACLEHelper();
            try
            {
                DataTable dt = context.QueryTable(sql);
                LogTimesFactory factory = new LogTimesFactory();
                factory.createLogTimes(LogTimeType.ChangeData);
                factory.logTimes(sql);
                List<string> list = new List<string>();
                foreach (DataRow item in dt.Rows)
                {
                    list.Add(item[0].ToString());
                }
                result.data = list;
            }
            catch (Exception err)
            {
                result.addError(err.Message);
                log.Error(MethodBase.GetCurrentMethod() + err.Message);
            }
            return result;
        }

        public Result<List<string>> getLineUDChangedData(string startTime, string endTime)
        {
            Result<List<string>> result = new Result<List<string>>();
            string sql = $"select distinct(ud.线路id) from syn_log s ,gj_公交线路上下行表 ud, gj_公交线路表 l, gj_公交线路组 g, gj_depart d where s.ybid=ud.f_id and ud.线路id = l.f_id and l.所属线路组 = g.f_id  and g.单位id = d.f_id  and d.f_id in {ConstInfo.DepartRight} and s.adddatetime >= to_date('{startTime}', 'yyyy-MM-dd HH:mi:ss') and s.adddatetime < to_date('{endTime}', 'yyyy-MM-dd HH:mi:ss') and s.sjly = 'GJ_公交线路上下行表'";
            var context = new ORACLEHelper();
            try
            {
                DataTable dt = context.QueryTable(sql);
                LogTimesFactory factory = new LogTimesFactory();
                factory.createLogTimes(LogTimeType.ChangeData);
                factory.logTimes(sql);
                List<string> list = new List<string>();
                foreach (DataRow item in dt.Rows)
                {
                    list.Add(item[0].ToString());
                }
                result.data = list;
            }
            catch (Exception err)
            {
                result.addError(err.Message);
                log.Error(MethodBase.GetCurrentMethod() + err.Message);
            }
            return result;
        }
    }
}