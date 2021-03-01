using System;
using System.Runtime.InteropServices;
using System.Timers;
using System.Windows.Forms;

namespace MouseMover
{
    public partial class Form1 : Form
    {
        public bool GoOn { get; set; }

        private static System.Timers.Timer aTimer;

        private const int MOUSEEVENTF_MOVE = 0x0001;

        public Form1()
        {
            InitializeComponent();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            this.GoOn = !this.GoOn;
            startButton.Text = this.GoOn ? "Arrêter" : "Démarrer";

            aTimer = new System.Timers.Timer()
            {
                Interval = 5000, 
                Enabled = true
            };

            if(this.GoOn) 
                aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);

            return;
        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if (GetLastInputTime() >= 240)
            {
                mouse_event(MOUSEEVENTF_MOVE, new Random().Next(-10, 10), new Random().Next(-10, 10), 0, 0);
            }
        }

        private static uint GetLastInputTime()
        {
            uint idleTime = 0;
            LASTINPUTINFO lastInputInfo = new LASTINPUTINFO();
            lastInputInfo.cbSize = (uint)Marshal.SizeOf(lastInputInfo);
            lastInputInfo.dwTime = 0;
            uint envTicks = (uint)Environment.TickCount;

            if (GetLastInputInfo(ref lastInputInfo))
            {
                uint lastInputTick = lastInputInfo.dwTime;
                idleTime = envTicks - lastInputTick;
            }

            return ((idleTime > 0) ? (idleTime / 1000) : 0);
        }

        [DllImport("user32")]
        public static extern int SetCursorPos(int x, int y);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        [DllImport("user32.dll")]
        static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        [StructLayout(LayoutKind.Sequential)]
        struct LASTINPUTINFO
        {
            public static readonly int SizeOf = Marshal.SizeOf(typeof(LASTINPUTINFO));

            [MarshalAs(UnmanagedType.U4)]
            public UInt32 cbSize;
            [MarshalAs(UnmanagedType.U4)]
            public UInt32 dwTime;
        }



    }
}
