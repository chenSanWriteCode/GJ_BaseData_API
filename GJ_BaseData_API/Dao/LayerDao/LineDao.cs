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
    public class LineDao : ILineDao
    {
        private ILog log = LogManager.GetLogger("LineDao");

        public Result<List<Line>> getLineByDepartId(int? id)
        {
            Result<List<Line>> result = new Result<List<Line>>();
            string sql = $"select l.f_id,l.线路名称,d.f_id departId from gj_公交线路表 l,gj_公交线路组 g ,gj_depart d where l.所属线路组 = g.f_id and g.单位id = d.f_id and d.f_id in {ConstInfo.DepartRight} ";
            sql += id == null ? "" : $"  and d.f_id ={id} ";
            var context = new ORACLEHelper();
            try
            {
                DataTable dt = context.QueryTable(sql);
                LogTimesFactory factory = new LogTimesFactory();
                factory.createLogTimes(LogTimeType.Line);
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

        public Result<Line> getLineById(int lineId)
        {
            Result<Line> result = new Result<Line>();
            string sql = $"select l.f_id,l.线路名称,d.f_id departId from gj_公交线路表 l,gj_公交线路组 g ,gj_depart d where l.所属线路组 = g.f_id and g.单位id = d.f_id and d.f_id in {ConstInfo.DepartRight}  and l.f_id={lineId}";
            var context = new ORACLEHelper();
            try
            {
                DataTable dt = context.QueryTable(sql);
                LogTimesFactory factory = new LogTimesFactory();
                factory.createLogTimes(LogTimeType.Line);
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

        private List<Line> TableToList(DataTable dt)
        {
            List<Line> result = new List<Line>();
            if (dt == null || dt.Rows.Count == 0)
            {
                return result;
            }
            Line model;
            foreach (DataRow dr in dt.Rows)
            {
                model = new Line
                {
                    lineName = dr["线路名称"]?.ToString(),
                    lineId = Convert.ToInt32(dr["f_id"]),
                    departId = Convert.ToInt32(dr["departId"])
                };
                result.Add(model);
            }
            return result;
        }
    }
}