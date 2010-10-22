using System;
using System.Diagnostics;

namespace WhitePaperBible.Phone.Infrastructure {
    public class Logger {

        public static void Log(String notes = "") {
            StackTrace stackTrace = new StackTrace();

            String typeName = stackTrace.GetFrame(1).GetMethod().DeclaringType.Name;
            String methodName = stackTrace.GetFrame(1).GetMethod().Name;

            if (!String.IsNullOrEmpty(notes)) {
                notes = String.Concat(" - ", notes);
            }
            Debug.WriteLine(String.Format("WP7: {0} - {1}{2}", typeName, methodName, notes));
        }

        Logger() {
        }
    }
}
