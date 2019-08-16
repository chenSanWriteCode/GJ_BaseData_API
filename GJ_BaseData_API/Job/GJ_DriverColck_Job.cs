using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Timers;
using GJ_BaseData_API.Entity;
using GJ_BaseData_API.Infrastructure;
using log4net;
using Newtonsoft.Json;

namespace GJ_BaseData_API.Job
{
    public class GJ_DriverColck_Job : GJ_Job
    {
        private ILog log = LogManager.GetLogger("GJ_DriverColck_Job");
        //打卡：http://117.34.118.23:9101/mediaplayer/Driver/reportDriverClockInfo?jsonStr=


        //司机：http://117.34.118.23:9101/mediaplayer/Driver/reportDriverInfo?jsonStr=
        public GJ_DriverColck_Job()
        {
            timerJob.Interval = 10000;
            timerJob.AutoReset = true;
            timerJob.Elapsed += DriverJob_Elapsed;
        }
        private async void DriverJob_Elapsed(object sender, ElapsedEventArgs e)
        {
            ORACLEHelper context = new ORACLEHelper();
            DateTime current = DateTime.Now;
            string sql = $"select t.bus_card_no 车牌, t.DRIVER_ID,t.driver_name, t.tRADE_DATE|| t.TRADE_TIME tradeTime, t.DUTY_FLAG from MANAGE_REC_DRIVER_TOLED@ytiic t where  t.tRADE_DATE='{current.ToString("yyyyMMdd")}' and t.TRADE_TIME<='{current.ToString("HHmmss")}'";
            DataTable dt = new DataTable();
            try
            {
                dt = context.QueryTable(sql);
                if (dt != null && dt.Rows.Count > 0)
                {

                    List<DriverClock> dataList = TableToList_DriverClock(dt);
                    HttpClient client = new HttpClient() { BaseAddress = new Uri(ConstInfo.URL_ZhongHangXun)};
                    var jsonStr = JsonConvert.SerializeObject(dataList);
                    Dictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("jsonStr", jsonStr);//bmpm/Driver/reportDriverClockInfo
                    var response = await client.PostAsync("mediaplayer/Driver/reportDriverClockInfo", new FormUrlEncodedContent(dict));
                    var result = await response.Content.ReadAsAsync<HttpError>();
                    if (response.StatusCode == HttpStatusCode.OK && result.code == "200")
                    {
                        sql = $"delete from MANAGE_REC_DRIVER_TOLED@ytiic t where  t.TRADE_TIME<='{current.ToString("HHmmss")}'";
                        context.ExecuteSql(sql);
                    }
                    else
                    {
                        log.Error("调用接口失败："+result.code + result.message);
                    }
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
                    isWork = Convert.ToInt32(dr["DUTY_FLAG"]),
                    driverName = dr["driver_name"].ToString()
                };
                result.Add(model);
            }
            return result;
        }
    }
}