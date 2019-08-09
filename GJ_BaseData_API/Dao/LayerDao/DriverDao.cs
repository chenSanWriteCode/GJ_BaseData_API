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
    public class DriverDao : IDriverDao
    {
        private ILog log = LogManager.GetLogger("DriverDao");
        public Result<DriverClock> getBusById(int id)
        {
            Result<DriverClock> result = new Result<DriverClock>();
            string sql = $"select c.车牌, lpad(t.driver_id, 5, '0') driverNo,  t.duty_time tradeTime,   t.duty_flag  from v_driver_ban_last@ytiic t, bus_info@ytiic b, gj_公交车 c where c.车牌 =b.bus_card_no  and t.bus_id = b.bus_id and c.f_id ={id}";
            var context = new ORACLEHelper();
            try
            {
                DataTable dt = context.QueryTable(sql);
                LogTimesFactory factory = new LogTimesFactory();
                factory.createLogTimes(LogTimeType.Driver);
                factory.logTimes(sql);
                result.data = TableToList_DriverClock(dt).FirstOrDefault();
            }
            catch (Exception err)
            {
                result.addError(err.Message);
                log.Error(MethodBase.GetCurrentMethod() + err.Message);
            }
            return result;
        }

        public Result<Driver> getDriverById(string id)
        {
            Result<Driver> result = new Result<Driver>();
            //select lpad(OPER_ID,5,'0') driverNo,trim(OPER_NAME)driverName, nvl(d.线路id,-1) lineid,nvl(d.using,1)isWork from EMPLOYEE_INFO@ytiic e,gj_驾驶员表 d,gj_公交线路表 l,gj_公交线路组 g, gj_depart t where e.driver_flag='Y' and trim(e.oper_name)=d.姓名(+) and d.线路id=l.f_id and l.所属线路组=g.f_id and g.单位id=t.f_id and t.f_id in(2,3,4,5,10)
            string sql = $"select lpad(OPER_ID,5,'0') 员工号,trim(OPER_NAME) 姓名, nvl(d.线路id,-1) 线路id,nvl(d.using,1)isWork from EMPLOYEE_INFO@ytiic e,(select * from (select d.线路id,d.姓名,d.using,row_number() over(partition by d.姓名 order by d.modify_time desc)sn from gj_驾驶员表 d)d where d.sn=1)  d,gj_公交线路表 l,gj_公交线路组 g, gj_depart t where e.driver_flag='Y' and trim(e.oper_name)=d.姓名(+) and d.线路id=l.f_id(+) and l.所属线路组=g.f_id(+) and g.单位id=t.f_id(+) and (t.f_id in {ConstInfo.DepartRight} or t.f_id is null) and lpad(OPER_ID,5,'0')='{id}'";
            var context = new ORACLEHelper();
            try
            {
                DataTable dt = context.QueryTable(sql);
                LogTimesFactory factory = new LogTimesFactory();
                factory.createLogTimes(LogTimeType.Driver);
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

        public Result<List<Driver>> getDriversByLineId(int? lineId)
        {
            Result<List<Driver>> result = new Result<List<Driver>>();
            string sql = $"select lpad(OPER_ID,5,'0') 员工号,trim(OPER_NAME) 姓名, nvl(d.线路id,-1) 线路id,nvl(d.using,1)isWork from EMPLOYEE_INFO@ytiic e,(select * from (select d.线路id,d.姓名,d.using,row_number() over(partition by d.姓名 order by d.modify_time desc)sn from gj_驾驶员表 d)d where d.sn=1)  d,gj_公交线路表 l,gj_公交线路组 g, gj_depart t where e.driver_flag='Y' and trim(e.oper_name)=d.姓名(+) and d.线路id=l.f_id(+) and l.所属线路组=g.f_id(+) and g.单位id=t.f_id(+) and (t.f_id in {ConstInfo.DepartRight} or t.f_id is null)";
            sql += lineId == null ? "" : $" and d.线路id ={lineId} ";
            var context = new ORACLEHelper();
            try
            {
                DataTable dt = context.QueryTable(sql);
                LogTimesFactory factory = new LogTimesFactory();
                factory.createLogTimes(LogTimeType.Driver);
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
        private List<Driver> TableToList(DataTable dt)
        {
            List<Driver> result = new List<Driver>();
            if (dt == null || dt.Rows.Count == 0)
            {
                return result;
            }
            Driver model;
            foreach (DataRow dr in dt.Rows)
            {
                model = new Driver
                {
                    driverNo = dr["员工号"]?.ToString(),
                    driverName = dr["姓名"]?.ToString(),
                    lineId = Convert.ToInt32(dr["线路id"]),
                    isWork = Convert.ToInt32(dr["isWork"])
                };
                result.Add(model);
            }
            return result;
        }
        private List<DriverClock> TableToList_DriverClock(DataTable dt)
        {
            List<DriverClock> result = new List<DriverClock>();
            if (dt == null || dt.Rows.Count == 0)
            {
                return result;
            }
            DriverClock model;
            foreach (DataRow dr in dt.Rows)
            {
                model = new DriverClock
                {
                    driverNo = dr["driverNo"].ToString(),
                    busNum = dr["车牌"].ToString(),
                    clockTime = dr["tradeTime"].ToString(),
                    isWork = Convert.ToInt32(dr["duty_flag"])
                };
                result.Add(model);
            }
            return result;
        }

        
       
    }
}