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
    public class LineUDDao : ILineUDDao
    {
        private ILog log = LogManager.GetLogger("LineUDDao");

        public Result<LineUD> getLineUDById(int id)
        {
            Result<LineUD> result = new Result<LineUD>();
            string sql = $"select ud.线路id,ud.f_id,ud.公交线路,decode(ud.updown,1,2,1) 方向,ud.票价,ud.首班车时间,ud.末班车时间 from gj_公交线路上下行表 ud, gj_公交线路表 l, gj_公交线路组 g, gj_depart d where ud.线路id = l.f_id and l.所属线路组 = g.f_id  and g.单位id = d.f_id  and d.f_id in {ConstInfo.DepartRight}  and ud.f_id = { id}";
            var context = new ORACLEHelper();
            try
            {
                DataTable dt = context.QueryTable(sql);
                LogTimesFactory factory = new LogTimesFactory();
                factory.createLogTimes(LogTimeType.LineUD);
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

        public Result<List<LineUD>> getLIneUDByLineId(int? lineId)
        {
            Result<List<LineUD>> result = new Result<List<LineUD>>();
            string sql = $"select ud.线路id,ud.f_id,ud.公交线路,decode(ud.updown,1,2,1)方向,ud.票价,ud.首班车时间,ud.末班车时间 from gj_公交线路上下行表 ud, gj_公交线路表 l, gj_公交线路组 g, gj_depart d where ud.线路id = l.f_id and l.所属线路组 = g.f_id  and g.单位id = d.f_id  and d.f_id in {ConstInfo.DepartRight} ";
            sql += lineId == null ? "" : $" and l.f_id ={lineId} ";
            var context = new ORACLEHelper();
            try
            {
                DataTable dt = context.QueryTable(sql);
                LogTimesFactory factory = new LogTimesFactory();
                factory.createLogTimes(LogTimeType.LineUD);
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
        private List<LineUD> TableToList(DataTable dt)
        {
            List<LineUD> result = new List<LineUD>();
            if (dt == null || dt.Rows.Count == 0)
            {
                return result;
            }
            LineUD model;
            foreach (DataRow dr in dt.Rows)
            {
                model = new LineUD
                {
                    lineId = Convert.ToInt32(dr["线路id"]),
                    UDID = Convert.ToInt32(dr["f_id"]),
                    UDName = dr["公交线路"].ToString(),
                    UD = Convert.ToInt32(dr["方向"]),
                    ticketPrice = float.Parse(dr["票价"].ToString()),
                    firstTime = dr["首班车时间"].ToString(),
                    lastTime = dr["末班车时间"].ToString()
                };
                result.Add(model);
            }
            return result;
        }
    }
}