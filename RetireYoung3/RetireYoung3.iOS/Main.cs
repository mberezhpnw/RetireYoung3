using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using System.Diagnostics;

namespace RetireYoung3.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            try
            {
                UIApplication.Main(args, null, "AppDelegate");

            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex.InnerException);
                //Debug.WriteLine(ex.StackTrace);
                //Debug.WriteLine(ex.Source);
                //Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.ToString());
            }
            //UIApplication.Main(args, null, "AppDelegate");


            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
        }
    }
}
