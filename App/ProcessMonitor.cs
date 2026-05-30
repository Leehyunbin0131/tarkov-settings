using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace tarkov_settings
{
    static class NativeMethods
    {
        private const uint WINEVENT_OUTOFCONTEXT = 0;
        private const uint EVENT_SYSTEM_FOREGROUND = 3;

        public delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType, IntPtr hWnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);

        public static WinEventDelegate dele = null;

        private static IntPtr m_hhook;

        public static void SetHook()
        {
            if (dele == null)
                return;

            m_hhook = SetWinEventHook(EVENT_SYSTEM_FOREGROUND,
                EVENT_SYSTEM_FOREGROUND,
                IntPtr.Zero,
                dele,
                0, 0, WINEVENT_OUTOFCONTEXT | 2);
        }

        public static void UnHook()
        {
            if (m_hhook != IntPtr.Zero)
            {
                UnhookWinEvent(m_hhook);
                m_hhook = IntPtr.Zero;
            }
        }

        #region Win32 API Calls
        [DllImport("user32.dll")]
        public static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr hmodWinEventProc, WinEventDelegate lpfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);

        [DllImport("user32.dll")]
        public static extern IntPtr UnhookWinEvent(IntPtr hWinEventHook);

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
        #endregion
        public static string GetActiveWindowTitle()
        {
            try
            {
                IntPtr handle = GetForegroundWindow();
                uint threadID = GetWindowThreadProcessId(handle, out uint processID);
                return Process.GetProcessById(Convert.ToInt32(processID)).ProcessName;
            }
            catch
            {
                return null;
            }
        }
    }
    class ProcessMonitor
    {
        private NativeMethods.WinEventDelegate processHook;

        private readonly ColorController cController = ColorController.Instance;

        private HashSet<string> pTargets = new HashSet<string>();

        #region Singleton Pattern implement
        private static readonly Lazy<ProcessMonitor> instance =
            new Lazy<ProcessMonitor>(() => new ProcessMonitor());

        public static ProcessMonitor Instance
        {
            get
            {
                return instance.Value;
            }
        }
        #endregion

        public MainForm Parent { get; set; }

        private ProcessMonitor() { }

        public void Add(string process)
        {
            if (!string.IsNullOrWhiteSpace(process))
                this.pTargets.Add(process.ToLowerInvariant());
        }

        public void Init()
        {
            processHook = new NativeMethods.WinEventDelegate(WinEventProc);
            NativeMethods.dele += processHook;
            NativeMethods.SetHook();

            // Init ColorController
            cController.Init();
        }

        /**
         * Window Focus changed Event Handler
         */
        public void WinEventProc(IntPtr hWinEventHook, uint eventType, IntPtr hWnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
        {
            Console.WriteLine("Running Tasks : {0}", GetWorkingThreads());
            RefreshCurrentFocus();
        }

        public void RefreshCurrentFocus()
        {
            var activeProcess = NativeMethods.GetActiveWindowTitle();
            Console.WriteLine("Focused Process : {0}", activeProcess);

            var isTargetFocused = !string.IsNullOrWhiteSpace(activeProcess)
                && this.pTargets.Contains(activeProcess.ToLowerInvariant());

            if (isTargetFocused && Parent.IsEnabled)
            {
                Console.WriteLine("[pMonitor] Target Process is focused");

                var (b, c, g, dvl) = Parent.GetColorValue();
                cController.ChangeColorRamp(brightness: b,
                                            contrast: c,
                                            gamma: g,
                                            reset: false);
                cController.DVL = dvl;
                Parent.UpdateRuntimeStatus("Tarkov Active", activeProcess);
            }
            else
            {
                Console.WriteLine("[pMonitor] Target Process is not focused");
                ResetColors();
                Parent.UpdateRuntimeStatus(Parent.IsEnabled ? "Desktop" : "Disabled", activeProcess);
            }
        }

        public void ResetColors()
        {
            cController.ResetAll();
        }

        public string GetActiveProcessName()
        {
            return NativeMethods.GetActiveWindowTitle();
        }

        /**
         * Reset to original color settings before exit
         */
        public void Close()
        {
            Console.WriteLine("[pMonitor] Remove Delegates");
            NativeMethods.dele -= processHook;
            NativeMethods.UnHook();

            Console.WriteLine("[pMonitor] Resetting Color");
            cController.Close();
        }

        private static int GetWorkingThreads()
        {
            System.Threading.ThreadPool.GetMaxThreads(out int maxThreads, out int _);
            System.Threading.ThreadPool.GetAvailableThreads(out int availableThreads, out _);
            return maxThreads - availableThreads;
        }
    }
}
