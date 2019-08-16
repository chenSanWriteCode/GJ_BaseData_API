using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Timers;
using System.Web;
using GJ_BaseData_API.Entity;
using GJ_BaseData_API.Infrastructure;
using log4net;
using Newtonsoft.Json;

namespace GJ_BaseData_API.Job
{
    public class GJ_Driver_Job:GJ_Job
    {
        private ILog log = LogManager.GetLogger("GJ_Driver_Job");

        public GJ_Driver_Job()
        {
            timerJob.Interval = 10000;
            timerJob.AutoReset = true;
            timerJob.Elapsed += DriverJob_Elapsed;
        }
        private async void DriverJob_Elapsed(object sender, ElapsedEventArgs e)
        {
            ORACLEHelper context = new ORACLEHelper();
            DateTime current = DateTime.Now;
            string sql = $"select lpad(OPER_ID,5,'0') 员工号,trim(OPER_NAME) 姓名,-1 线路id,flag isWork from DRIVER_INFO_CHANGE_TOLED@ytiic where UPDATE_TIME<=to_date('{current.ToString("yyyy-MM-dd HH:mm:ss")}','yyyy-MM-dd HH24:mi:ss')";
            DataTable dt = new DataTable();
            try
            {
                dt = context.QueryTable(sql);
                if (dt != null && dt.Rows.Count > 0)
                {

                    List<Driver> dataList = TableToList(dt);
                    HttpClient client = new HttpClient() { BaseAddress = new Uri(ConstInfo.URL_ZhongHangXun) };
                    var jsonStr = JsonConvert.SerializeObject(dataList);
                    Dictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("jsonStr", jsonStr);//bmpm/Driver/reportDriverInfo
                    var response = await client.PostAsync("/mediaplayer/Driver/reportDriverInfo", new FormUrlEncodedContent(dict));
                    var result = await response.Content.ReadAsAsync<HttpError>();
                    if (response.StatusCode == HttpStatusCode.OK && result.code == "200")
                    {
                        sql = $"delete from DRIVER_INFO_CHANGE_TOLED@ytiic t where  t.UPDATE_TIME<to_date('{current.ToString("yyyy-MM-dd HH:mm:ss")}','yyyy-MM-dd HH24:mi:ss')";
                        context.ExecuteSql(sql);
                    }
                    else
                    {
                        log.Error("调用接口失败：" + result.code + result.message);
                    }
                }
            }
            catch (Exception err)
            {
                log.Error(err);
            }
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
    }
}