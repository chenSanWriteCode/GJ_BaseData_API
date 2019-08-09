using System;
using log4net;

namespace GJ_BaseData_API.Infrastructure
{
    public class LogTimesFactory
    {
        private LogTimes moduleLog;
        public void createLogTimes(LogTimeType moduleType)
        {
            switch (moduleType)
            {
                case LogTimeType.Driver:
                    moduleLog = new LogTimes_Driver();
                    break;
                case LogTimeType.Bus:
                    moduleLog = new LogTimes_Bus();
                    break;
                case LogTimeType.Line:
                    moduleLog = new LogTimes_Line();
                    break;
                case LogTimeType.LineUD:
                    moduleLog = new LogTimes_LineUD();
                    break;
                case LogTimeType.LineStation:
                    moduleLog = new LogTimes_LineStation();
                    break;
                case LogTimeType.Depart:
                    moduleLog = new LogTimes_Depart();
                    break;
                case LogTimeType.ChangeData:
                    moduleLog = new LogTimes_ChangeData();
                    break;
            }
        }
        public void logTimes(string sql)
        {
            if (ConstInfo.LogFlag=="1")
            {
                moduleLog.logTimes(sql);
            }
        }
    }
    public abstract class LogTimes
    {
        protected ILog log = LogManager.GetLogger("访问次数监控");
        private static DateTime current = DateTime.Now;
        protected void getCurrentTimes(ref int times)
        {
            if (current.Date == DateTime.Now.Date)
            {
                times++;
            }
            else
            {
                current = DateTime.Now;
                times = 1;
            }
        }
        public abstract void logTimes(string sql);

    }
    public class LogTimes_Driver : LogTimes
    {
        private static int times = 0;
        public override void logTimes(string sql)
        {
            getCurrentTimes(ref times);
            log.Info($"第{times}次访问{LogTimeType.Driver.ToString()},sql:{sql}");
        }
    }

    public class LogTimes_Bus : LogTimes
    {
        private static int times = 0;
        public override void logTimes(string sql)
        {
            getCurrentTimes(ref times);
            log.Info($"第{times}次访问{LogTimeType.Bus.ToString()},sql:{sql}");
        }
    }

    public class LogTimes_Line : LogTimes
    {
        private static int times = 0;
        public override void logTimes(string sql)
        {
            getCurrentTimes(ref times);
            log.Info($"第{times}次访问{LogTimeType.Line.ToString()},sql:{sql}");
        }
    }

    public class LogTimes_LineUD : LogTimes
    {
        private static int times = 0;
        public override void logTimes(string sql)
        {
            getCurrentTimes(ref times);
            log.Info($"第{times}次访问{LogTimeType.LineUD.ToString()},sql:{sql}");
        }
    }

    public class LogTimes_Depart : LogTimes
    {
        private static int times = 0;
        public override void logTimes(string sql)
        {
            getCurrentTimes(ref times);
            log.Info($"第{times}次访问{LogTimeType.Depart.ToString()},sql:{sql}");
        }
    }
    public class LogTimes_LineStation : LogTimes
    {
        private static int times = 0;
        public override void logTimes(string sql)
        {
            getCurrentTimes(ref times);
            log.Info($"第{times}次访问{LogTimeType.LineStation.ToString()},sql:{sql}");
        }
    }
    public class LogTimes_ChangeData : LogTimes
    {
        private static int times = 0;
        public override void logTimes(string sql)
        {
            getCurrentTimes(ref times);
            log.Info($"第{times}次访问{LogTimeType.ChangeData.ToString()},sql:{sql}");
        }
    }

    public enum LogTimeType
    {
        Driver = 1,
        Bus,
        Line,
        LineUD,
        LineStation,
        Depart,
        ChangeData
    }
}