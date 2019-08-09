using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Timers;
using GJ_BaseData_API.Entity;
using GJ_BaseData_API.Infrastructure;
using log4net;

namespace GJ_BaseData_API.Job
{
    public class GJ_Driver_Job : GJ_Job
    {
        private ILog log = LogManager.GetLogger("GJ_Driver_Job");
        //private IHubContext driverHub = GlobalHost.ConnectionManager.GetHubContext<GJ_DriverHub>();
        //private static DateTime currentTime = DateTime.Now;

        public GJ_Driver_Job()
        {
            timerJob.Interval = 10000;
            timerJob.AutoReset = false;
            timerJob.Elapsed += DriverJob_Elapsed;
        }
        private async void DriverJob_Elapsed(object sender, ElapsedEventArgs e)
        {
            ORACLEHelper context = new ORACLEHelper();
            DateTime current = DateTime.Now;
            string sql = $"select b.bus_card_no 车牌, t.DRIVER_ID, t.tRADE_DATE|| t.TRADE_TIME tradeTime, t.DUTY_FLAG from MANAGE_REC_DRIVER_TOLED@ytiic t, bus_info @ytiic b  where  t.bus_id = b.bus_id where t.TRADE_TIME<={current.ToString("HHmmss")}";
           
            DataTable dt = new DataTable();
            try
            {
                dt = context.QueryTable(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    List<DriverClock> data = TableToList_DriverClock(dt);
                    //TODO 调用java接口
                    HttpClient client = new HttpClient() { BaseAddress = new Uri(ConstInfo.URL_ZhongHangXun) };
                    var response = await client.GetAsync("/bmpm/Driver/reportGetDriverInfo?jsonStr=测试一下get请求");
                    var result = await response.Content.ReadAsAsync<HttpError>();
                    sql = $"delete from MANAGE_REC_DRIVER_TOLED@ytiic t where  t.TRADE_TIME<={current.ToString("HHmmss")}";
                    context.ExecuteSql(sql);
                }

            }
            catch (Exception err)
            {
                log.Error(err);
            }
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
                    driverNo = dr["DRIVER_ID"].ToString(),
                    busNum = dr["车牌"].ToString(),
                    clockTime = dr["tradeTime"].ToString(),
                    isWork = Convert.ToInt32(dr["DUTY_FLAG"])
                };
                result.Add(model);
            }
            return result;
        }
    }
}