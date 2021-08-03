using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterviewDemo
{
    public class LogHelper
    {
        public static void LogException(Exception exception)
        {
            try
            {
                string filePath = AppDomain.CurrentDomain.BaseDirectory + string.Format("Logs\\error_{0}.txt", DateTime.Now.ToString("yyyyMMdd"));
                using (System.IO.StreamWriter writer = new System.IO.StreamWriter(filePath, true))
                {
                    writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                    writer.WriteLine("Date :" + DateTime.Now.ToString()
                                    + Environment.NewLine + "Message :" + exception.Message
                                    + Environment.NewLine + "StackTrace :" + exception.StackTrace
                                    + Environment.NewLine + "Source :" + exception.Source
                         );
                    writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                }


            }
            catch (Exception ex)
            {
              
            }
        }

        public static void LogException(string exception)
        {
            try
            {
                string filePath = AppDomain.CurrentDomain.BaseDirectory + string.Format("Logs\\error_{0}.txt", DateTime.Now.ToString("yyyyMMdd"));
                using (System.IO.StreamWriter writer = new System.IO.StreamWriter(filePath, true))
                {
                    writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                    writer.WriteLine("Date :" + DateTime.Now.ToString()
                                    + Environment.NewLine + "Message :" + exception

                         );
                    writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                }


            }
            catch (Exception ex)
            {

            }
        }

    }
}