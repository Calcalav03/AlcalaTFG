﻿using Mopups.Services;

namespace AlcalaTFG
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Application.Current.UserAppTheme = AppTheme.Dark;
            MainPage = new AppShell();
        }
    }
}
