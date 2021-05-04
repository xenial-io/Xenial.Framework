using System;
using System.Configuration;
using System.Windows.Forms;

using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Xpo.DB;
using DevExpress.XtraEditors;

namespace Xenial.FeatureCenter.Win
{
    internal static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            System.Windows.Forms.Application.ThreadException += Application_ThreadException;
            try
            {
                //XenialLicense.Register();

                DevExpress.ExpressApp.FrameworkSettings.DefaultSettingsCompatibilityMode = DevExpress.ExpressApp.FrameworkSettingsCompatibilityMode.Latest;
#if EASYTEST
            DevExpress.ExpressApp.Win.EasyTest.EasyTestRemotingRegistration.Register();
#endif
                WindowsFormsSettings.LoadApplicationSettings();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                DevExpress.Utils.ToolTipController.DefaultController.ToolTipType = DevExpress.Utils.ToolTipType.SuperTip;
                if (Tracing.GetFileLocationFromSettings() == DevExpress.Persistent.Base.FileLocation.CurrentUserApplicationDataFolder)
                {
                    Tracing.LocalUserAppDataPath = Application.LocalUserAppDataPath;
                }
                Tracing.Initialize();

                var winApplication = new FeatureCenterWindowsFormsApplication();

#if EASYTEST
            if(ConfigurationManager.ConnectionStrings["EasyTestConnectionString"] != null)
            {
                winApplication.ConnectionString = ConfigurationManager.ConnectionStrings["EasyTestConnectionString"].ConnectionString;
            }
#endif
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached && winApplication.CheckCompatibilityType == CheckCompatibilityType.DatabaseSchema)
            {
                winApplication.DatabaseUpdateMode = DatabaseUpdateMode.UpdateDatabaseAlways;
            }
#endif
                try
                {
                    winApplication.Setup();
                    winApplication.Start();
                }
                catch (Exception e)
                {
                    winApplication.StopSplash();
                    winApplication.HandleException(e);
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
            }
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            System.Windows.Forms.MessageBox.Show(e.Exception.ToString());
        }
    }
}
