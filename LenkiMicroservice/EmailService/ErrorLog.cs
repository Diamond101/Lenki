﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LenkiMicroservice.EmailService
{

    public static class ErrorLog
    {
        
        public static void LogException(Exception exception)
        {
            string errorMessage = "Date/Time: " + DateTime.Now + Environment.NewLine +
                                  "Error message: " + exception.Message + Environment.NewLine +
                                  "Inner exception: " + exception.InnerException + Environment.NewLine +
                                  "Stack trace: " + exception.StackTrace + Environment.NewLine;

           
            string path = Path.Combine("~/ErrorLog.txt");
            // path = HttpContext.Current.Server.MapPath("~/ErrorLog.txt");
            if (!File.Exists(path))
            {
                using (var fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    using (var tw = new StreamWriter(fs))
                    {
                        tw.WriteLine(errorMessage);
                        tw.Close();
                    }
                }
            }
            else if (File.Exists(path))
            {
                using (var tw = new StreamWriter(path, true))
                {
                    tw.WriteLine(errorMessage);
                    tw.Close();
                }
            }
        }

        public static void LogString(string errorMessage)
        {
            // errorMessage = DateTime.Now + ": " + errorMessage + Environment.NewLine;
            string path = Path.Combine("~/ErrorLog.txt");
            //string path = HttpContext.Current.Server.MapPath("~/ErrorLog.txt");
            if (!File.Exists(path))
            {
                using (var fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    using (var tw = new StreamWriter(fs))
                    {
                        tw.WriteLine(errorMessage);
                        tw.Close();
                    }
                }
            }
            else if (File.Exists(path))
            {
                using (var tw = new StreamWriter(path, true))
                {
                    tw.WriteLine(errorMessage);
                    tw.Close();
                }
            }
        }
    }
}
