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
    public class LineStationDao : ILineStationDao
    {
        private ILog log = LogManager.GetLogger("LineStationDao");

        public Result<List<LineStation>> getLineStationByLineId(int lineId)
        {
            Result<List<LineStation>> result = new Result<List<LineStation>>();
            string sql = $" select ls.线路上下行id,ls.顺序, ls.站点id,s.名称,s.gpsx2,s.gpsy2,decode(substr(s.名称,0,1),'.',1,0) realStation from gj_线路站点表 ls,gj_站点 s,gj_公交线路上下行表 ud, gj_公交线路表 l, gj_公交线路组 g, gj_depart d where ls.站点id = s.f_id and ls.线路上下行id = ud.f_id and ud.线路id = l.f_id and l.所属线路组 = g.f_id  and g.单位id = d.f_id  and d.f_id in {ConstInfo.DepartRight}   and l.f_id = {lineId}   order by ls.线路上下行id,ls.顺序 ";
            var context = new ORACLEHelper();
            try
            {
                DataTable dt = context.QueryTable(sql);
                LogTimesFactory factory = new LogTimesFactory();
                factory.createLogTimes(LogTimeType.LineStation);
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

        public Result<List<LineStation>> getLineStationByUDID(int UDID)
        {
            Result<List<LineStation>> result = new Result<List<LineStation>>();
            string sql = $" select ls.线路上下行id,ls.顺序, ls.站点id,s.名称,s.gpsx2,s.gpsy2,decode(substr(s.名称,0,1),'.',1,0) realStation from gj_线路站点表 ls,gj_站点 s,gj_公交线路上下行表 ud, gj_公交线路表 l, gj_公交线路组 g, gj_depart d where ls.站点id = s.f_id and ls.线路上下行id = ud.f_id and ud.线路id = l.f_id and l.所属线路组 = g.f_id  and g.单位id = d.f_id  and d.f_id in {ConstInfo.DepartRight}  and ls.线路上下行id  ={UDID} order by ls.顺序 ";
            var context = new ORACLEHelper();
            try
            {
                DataTable dt = context.QueryTable(sql);
                LogTimesFactory factory = new LogTimesFactory();
                factory.createLogTimes(LogTimeType.LineStation);
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
        private List<LineStation> TableToList(DataTable dt)
        {
            List<LineStation> result = new List<LineStation>();
            if (dt == null || dt.Rows.Count == 0)
            {
                return result;
            }
            LineStation model;
            foreach (DataRow dr in dt.Rows)
            {
                model = new LineStation
                {
                    UDID = Convert.ToInt32(dr["线路上下行id"]),
                    stationIndex = Convert.ToInt32(dr["顺序"]),
                    stationId = Convert.ToInt32(dr["站点id"]),
                    stationName = ConstInfo.filterStr(dr["名称"].ToString()),
                    gpsX= dr["gpsx2"].ToString(),
                    gpsY = dr["gpsy2"].ToString(),
                    isVirtual= Convert.ToInt32(dr["realStation"])
                };
                result.Add(model);
            }
            return result;
        }

        
    }
}