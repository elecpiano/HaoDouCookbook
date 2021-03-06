﻿using HaoDouCookBook.Common;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using HaoDouCookBook;

// The Blank Application template is documented at http://go.microsoft.com/fwlink/?LinkId=234227

namespace HaoDouCookBook
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App : Application
    {
        public static App CurrentInstance
        {
            get;
            private set;
        }

        private Frame rootFrame;

        public Frame RootFrame
        {
            get { return rootFrame; }
            set { rootFrame = value; }
        }

        public void RunAsync(Action action)
        {
            CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                if (action != null)
                {
                    action();
                }
            });
        }

#if WINDOWS_PHONE_APP
        private TransitionCollection transitions;
#endif

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += this.OnSuspending;
            this.UnhandledException += App_UnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;

            CurrentInstance = this;
            Shared.Utility.NetworkHelper.Current.StartListening();
        }

        void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            e.SetObserved();
        }

        void App_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            throw new NotImplementedException();
        }

#if WINDOWS_PHONE_APP

        // 用于FileOpenPicker等
        //
        ContinuationManager continuationManager;

        protected override void OnActivated(IActivatedEventArgs args)
        {
            base.OnActivated(args);
            continuationManager = new ContinuationManager();
            var rootFrame = Window.Current.Content as Frame;

            if (rootFrame == null)
            {
                rootFrame.Navigate(typeof(MainPage));
            }

            var continuationEventArgs = args as IContinuationActivatedEventArgs;
            if (continuationEventArgs != null)
            {
                // Call ContinuationManager to handle continuation activation
                continuationManager.Continue(continuationEventArgs);
            }
        }
#endif

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used when the application is launched to open a specific file, to display
        /// search results, and so forth.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif

            if (e.PreviousExecutionState != ApplicationExecutionState.Running)
            {
                bool loadState = (e.PreviousExecutionState == ApplicationExecutionState.Terminated);
                ExtendedSplash extendedSplash = new ExtendedSplash(e.SplashScreen, loadState);
                Window.Current.Content = extendedSplash;
            }

            Window.Current.Activate();
        }

#if WINDOWS_PHONE_APP
        /// <summary>
        /// Restores the content transitions after the app has launched.
        /// </summary>
        /// <param name="sender">The object where the handler is attached.</param>
        /// <param name="e">Details about the navigation event.</param>
        private void RootFrame_FirstNavigated(object sender, NavigationEventArgs e)
        {
            var rootFrame = sender as Frame;
            rootFrame.ContentTransitions = this.transitions ?? new TransitionCollection() { new NavigationThemeTransition() };
            rootFrame.Navigated -= this.RootFrame_FirstNavigated;
        }
#endif

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            // TODO: Save application state and stop any background activity
            deferral.Complete();

        }

    }
}