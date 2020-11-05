﻿using System.Diagnostics;

namespace Liftmanagement.Common
{
    public class Tracer
    {
        public static string Topic;
        
        public static void LogValidation(string msg) 
        {
            Debug.WriteLine(msg);
        }

        public static void LogUserDefinedValidation(string msg) 
        {
            Debug.WriteLine(msg);
        }
        
        public static void LogApplication(string msg) 
        {
            Debug.WriteLine(msg);
        }
    }
}
