namespace ConfigMaker
{
    public static class SplashManager
    {
        // ReSharper disable once MethodOverloadWithOptionalParameter
        public static void ShowSplashScreen(string caption = null, string description = null)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.SkinName = "DevExpress Style";
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm(caption, description);
        }

        public static void ShowSplashScreen(string description = null)
        {
            ShowSplashScreen(null, description);
        }

        public static void CloseSplashScreen()
        {
            if (DevExpress.XtraSplashScreen.SplashScreenManager.Default != null)
                if (DevExpress.XtraSplashScreen.SplashScreenManager.Default.IsSplashFormVisible)
                    DevExpress.XtraSplashScreen.SplashScreenManager.CloseDefaultWaitForm();
        }
    }
}