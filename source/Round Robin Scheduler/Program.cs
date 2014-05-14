using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SomeTechie.RoundRobinScheduler
{
    static class Program
    {
        static private string _logPath = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Log.txt");
        public static string LogPath
        {
            get
            {
                return _logPath;
            }
        }
        private static System.IO.StreamWriter logWriter;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
#if !DEBUG
            try
            {

                // Subscribe to unhandled exception events
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
#endif
            writeToLog("Application Started");
            if (Environment.OSVersion.Version.Major >= 6) SetProcessDPIAware();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
            writeToLog("Application Closed\r\n");
            closeLogWriter();
#if !DEBUG
            }
            catch (Exception ex)
            {
                string errorText = ex.Message + "\n\n" + ex.ToString();
                logException(errorText, "Unhandled Exception. Application is exiting:");
                MessageBox.Show(string.Format(Properties.Resources.UnhandledExceptionMessage, errorText), Properties.Resources.UnhandledExceptionTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(-1);
            }
#endif
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();

#if !DEBUG
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string errorText;
            try
            {
                Exception ex = ((Exception)e.ExceptionObject);
                errorText = ex.Message + "\n\n" + ex.ToString();
            }
            catch
            {
                errorText = e.ExceptionObject.ToString();
            }
            logException(errorText, "Unhandled Exception. Application is exiting:");
            MessageBox.Show(string.Format(Properties.Resources.UnhandledExceptionMessage, errorText), Properties.Resources.UnhandledExceptionTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            Environment.Exit(-1);
        }
#endif
        public static void closeLogWriter()
        {
            if (logWriter != null)
            {
                logWriter.Close();
                logWriter.Dispose();
                logWriter = null;
            }
        }

        static private void createLogWriterIfNeeded()
        {
            if (logWriter == null)
            {
                logWriter = new System.IO.StreamWriter(_logPath, true);
                logWriter.AutoFlush = true;
            }
        }

        static public void logException(Exception ex, string message = null)
        {
            logException(ex.Message + "\n\n" + ex.ToString(), message);
        }
        static public void logException(string error, string message = null)
        {
            try
            {
                createLogWriterIfNeeded();

                logWriter.WriteLine();
                if (message != null)
                {
                    logWriter.WriteLine(DateTime.Now.ToString("G") + " - " + message + ":");
                    logWriter.WriteLine();
                }
                logWriter.Write(error);
                logWriter.WriteLine();
                logWriter.WriteLine();

                closeLogWriter();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(Properties.Resources.ErrorWritingToLogMessage, ex), Properties.Resources.ErrorWritingToLogTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        static public void writeToLog(string str)
        {
            try
            {
                createLogWriterIfNeeded();

                logWriter.Write(DateTime.Now.ToString("G") + " - " + str);
                logWriter.WriteLine();

                closeLogWriter();
            }
            catch{}
        }

        static public bool getLogExists()
        {
            return System.IO.File.Exists(LogPath);
        }

        static public void clearLog()
        {
            closeLogWriter();
            System.IO.File.Delete(LogPath);
        }

        static public void copyLogTo(string path, bool overwrite = false)
        {
            System.IO.File.Copy(LogPath, path, overwrite);
        }
    }
}