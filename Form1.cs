using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Rotation_changer
{
    public partial class RotationChanger : Form
    {
        public RotationChanger()
        {
            InitializeComponent();
        }

        public enum Orientations
        {
            DEGREES_CW_0 = 0,
            DEGREES_CW_90 = 3,
            DEGREES_CW_180 = 2,
            DEGREES_CW_270 = 1
        }

        [DllImport("User32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern Boolean EnumDisplaySettings(
            byte[] lpszDeviceName,
            [param: MarshalAs(UnmanagedType.U4)]
        int iModeNum,
             [In, Out]
        ref DEVMODE lpDevMode);

        [StructLayout(LayoutKind.Sequential,
        CharSet = CharSet.Ansi)]
        public struct DEVMODE
        {
            // You can define the following constant  
            // but OUTSIDE the structure because you know  
            // that size and layout of the structure  
            // is very important  
            // CCHDEVICENAME = 32 = 0x50  
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string dmDeviceName;
            // In addition you can define the last character array  
            // as following:  
            //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]  
            //public Char[] dmDeviceName;  
            // After the 32-bytes array  
            [MarshalAs(UnmanagedType.U2)]
            public UInt16 dmSpecVersion;
            [MarshalAs(UnmanagedType.U2)]
            public UInt16 dmDriverVersion;
            [MarshalAs(UnmanagedType.U2)]
            public UInt16 dmSize;
            [MarshalAs(UnmanagedType.U2)]
            public UInt16 dmDriverExtra;
            [MarshalAs(UnmanagedType.U4)]
            public UInt32 dmFields;
            public POINTL dmPosition;
            [MarshalAs(UnmanagedType.U4)]
            public UInt32 dmDisplayOrientation;
            [MarshalAs(UnmanagedType.U4)]
            public UInt32 dmDisplayFixedOutput;
            [MarshalAs(UnmanagedType.I2)]
            public Int16 dmColor;
            [MarshalAs(UnmanagedType.I2)]
            public Int16 dmDuplex;
            [MarshalAs(UnmanagedType.I2)]
            public Int16 dmYResolution;
            [MarshalAs(UnmanagedType.I2)]
            public Int16 dmTTOption;
            [MarshalAs(UnmanagedType.I2)]
            public Int16 dmCollate;
            // CCHDEVICENAME = 32 = 0x50  
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string dmFormName;
            // Also can be defined as  
            //[MarshalAs(UnmanagedType.ByValArray,  
            // SizeConst = 32, ArraySubType = UnmanagedType.U1)]  
            //public Byte[] dmFormName;  
            [MarshalAs(UnmanagedType.U2)]
            public UInt16 dmLogPixels;
            [MarshalAs(UnmanagedType.U4)]
            public UInt32 dmBitsPerPel;
            [MarshalAs(UnmanagedType.U4)]
            public UInt32 dmPelsWidth;
            [MarshalAs(UnmanagedType.U4)]
            public UInt32 dmPelsHeight;
            [MarshalAs(UnmanagedType.U4)]
            public UInt32 dmDisplayFlags;
            [MarshalAs(UnmanagedType.U4)]
            public UInt32 dmDisplayFrequency;
            [MarshalAs(UnmanagedType.U4)]
            public UInt32 dmICMMethod;
            [MarshalAs(UnmanagedType.U4)]
            public UInt32 dmICMIntent;
            [MarshalAs(UnmanagedType.U4)]
            public UInt32 dmMediaType;
            [MarshalAs(UnmanagedType.U4)]
            public UInt32 dmDitherType;
            [MarshalAs(UnmanagedType.U4)]
            public UInt32 dmReserved1;
            [MarshalAs(UnmanagedType.U4)]
            public UInt32 dmReserved2;
            [MarshalAs(UnmanagedType.U4)]
            public UInt32 dmPanningWidth;
            [MarshalAs(UnmanagedType.U4)]
            public UInt32 dmPanningHeight;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINTL
        {
            [MarshalAs(UnmanagedType.I4)]
            public int x;
            [MarshalAs(UnmanagedType.I4)]
            public int y;
        }

        public const int DMDO_DEFAULT = 0;
        public const int DMDO_90 = 1;
        public const int DMDO_180 = 2;
        public const int DMDO_270 = 3;

        public const int ENUM_CURRENT_SETTINGS = -1;
        [DllImport("User32.dll")]
        [return: MarshalAs(UnmanagedType.I4)]
        public static extern int ChangeDisplaySettings(
        [In, Out]
            ref DEVMODE lpDevMode,
        [param: MarshalAs(UnmanagedType.U4)]
            uint dwflags);
        [DllImport("user32.dll")]
        internal static extern bool EnumDisplayDevices(
            string lpDevice, uint iDevNum, ref DISPLAY_DEVICE lpDisplayDevice,
            uint dwFlags);

        [Flags()]
        public enum DisplayDeviceStateFlags : int
        {
            /// <summary>The device is part of the desktop.</summary>
            AttachedToDesktop = 0x1,
            MultiDriver = 0x2,
            /// <summary>The device is part of the desktop.</summary>
            PrimaryDevice = 0x4,
            /// <summary>Represents a pseudo device used to mirror application drawing for remoting or other purposes.</summary>
            MirroringDriver = 0x8,
            /// <summary>The device is VGA compatible.</summary>
            VGACompatible = 0x10,
            /// <summary>The device is removable; it cannot be the primary display.</summary>
            Removable = 0x20,
            /// <summary>The device has more display modes than its output devices support.</summary>
            ModesPruned = 0x8000000,
            Remote = 0x4000000,
            Disconnect = 0x2000000
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct DISPLAY_DEVICE
        {
            [MarshalAs(UnmanagedType.U4)]
            public int cb;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string DeviceName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string DeviceString;
            [MarshalAs(UnmanagedType.U4)]
            public DisplayDeviceStateFlags StateFlags;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string DeviceID;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string DeviceKey;
        }

        public static uint endresult;

        public static void GetCurrentSettings()
        {
            DEVMODE mode = new DEVMODE();
            DISPLAY_DEVICE dd = new DISPLAY_DEVICE();
            dd.cb = Marshal.SizeOf(dd);
            mode.dmSize = (ushort)Marshal.SizeOf(mode);
            string result = "";

            for (uint id = 0; EnumDisplayDevices(null, id, ref dd, 0); id++)
            {
                if (dd.StateFlags.HasFlag(DisplayDeviceStateFlags.AttachedToDesktop))
                {
                    if (id == 1)
                    {
                        result = dd.DeviceName;

                        if (EnumDisplayDevices(null, 1, ref dd, 0) == true)
                        {
                            if (EnumDisplaySettings(dd.DeviceName.ToLPTStr(), ENUM_CURRENT_SETTINGS, ref mode) == true) // Succeeded  
                            {
                                //MessageBox.Show(EnumDisplaySettings(dd.DeviceName.ToLPTStr(), ENUM_CURRENT_SETTINGS, ref mode).ToString());
                                //MessageBox.Show("Current rotation: " + mode.dmDisplayOrientation * 90);

                                endresult = mode.dmDisplayOrientation;

                            }
                            else
                            {
                                MessageBox.Show(EnumDisplaySettings(dd.DeviceName.ToLPTStr(), ENUM_CURRENT_SETTINGS, ref mode).ToString());
                                Application.Exit();

                                endresult = 404;
                            }
                        }
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                GetCurrentSettings();

                if(endresult != 404)
                {
                    if (endresult == 0)
                    {
                        MessageBox.Show("liggend --> staand");
                        Display.Rotate(1, Display.Orientations.DEGREES_CW_270);
                    }
                    else if (endresult == 1)  // staand --> liggend 
                    {
                        MessageBox.Show("staand --> liggend");
                        Display.Rotate(1, Display.Orientations.DEGREES_CW_0);
                    }
                    else if (endresult == 2)
                    {
                        MessageBox.Show("liggend (gespiegeld) --> liggend");
                        Display.Rotate(1, Display.Orientations.DEGREES_CW_0);
                    }
                    else if (endresult == 3)
                    {
                        MessageBox.Show("staand (gespiegeld) --> staand");
                        Display.Rotate(1, Display.Orientations.DEGREES_CW_270);
                    }
                }
                else
                {
                    MessageBox.Show("endresult val = "+ endresult.ToString(), "error " + endresult.ToString());
                }

                Application.Exit();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
    public static class StringExtensions
    {
        public static byte[] ToLPTStr(this string str)
        {
            var lptArray = new byte[str.Length + 1];

            var index = 0;
            foreach (char c in str.ToCharArray())
                lptArray[index++] = Convert.ToByte(c);

            lptArray[index] = Convert.ToByte('\0');

            return lptArray;
        }
    }
    public class Display
    {
        public enum Orientations
        {
            DEGREES_CW_0 = 0,
            DEGREES_CW_90 = 3,
            DEGREES_CW_180 = 2,
            DEGREES_CW_270 = 1
        }

        public static bool Rotate(uint DisplayNumber, Orientations Orientation)
        {
            if (DisplayNumber == 0)
                throw new ArgumentOutOfRangeException("DisplayNumber", DisplayNumber, "First display is 1.");

            bool result = false;
            DISPLAY_DEVICE d = new DISPLAY_DEVICE();
            RotationChanger.DEVMODE dm = new RotationChanger.DEVMODE();
            d.cb = Marshal.SizeOf(d);

            if (!NativeMethods.EnumDisplayDevices(null, DisplayNumber - 1, ref d, 0))
                throw new ArgumentOutOfRangeException("DisplayNumber", DisplayNumber, "Number is greater than connected displays.");

            if (0 != NativeMethods.EnumDisplaySettings(
                d.DeviceName, NativeMethods.ENUM_CURRENT_SETTINGS, ref dm))
            {
                if ((dm.dmDisplayOrientation + (int)Orientation) % 2 == 1) // Need to swap height and width?
                {
                    uint temp = dm.dmPelsHeight;
                    dm.dmPelsHeight = dm.dmPelsWidth;
                    dm.dmPelsWidth = temp;
                }

                switch (Orientation)
                {
                    case Orientations.DEGREES_CW_90:
                        dm.dmDisplayOrientation = NativeMethods.DMDO_270;
                        break;
                    case Orientations.DEGREES_CW_180:
                        dm.dmDisplayOrientation = NativeMethods.DMDO_180;
                        break;
                    case Orientations.DEGREES_CW_270:
                        dm.dmDisplayOrientation = NativeMethods.DMDO_90;
                        break;
                    case Orientations.DEGREES_CW_0:
                        dm.dmDisplayOrientation = NativeMethods.DMDO_DEFAULT;
                        break;
                    default:
                        break;
                }

                DISP_CHANGE ret = NativeMethods.ChangeDisplaySettingsEx(
                    d.DeviceName, ref dm, IntPtr.Zero,
                    DisplaySettingsFlags.CDS_UPDATEREGISTRY, IntPtr.Zero);

                result = ret == 0;
            }

            return result;
        }

        public static void ResetAllRotations()
        {
            try
            {
                uint i = 0;
                while (++i <= 64)
                {
                    Rotate(i, Orientations.DEGREES_CW_0);
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                // Everything is fine, just reached the last display
            }
        }
    }

    internal class NativeMethods
    {
        [DllImport("user32.dll")]
        internal static extern DISP_CHANGE ChangeDisplaySettingsEx(
            string lpszDeviceName, ref RotationChanger.DEVMODE lpDevMode, IntPtr hwnd,
            DisplaySettingsFlags dwflags, IntPtr lParam);

        [DllImport("user32.dll")]
        internal static extern bool EnumDisplayDevices(
            string lpDevice, uint iDevNum, ref DISPLAY_DEVICE lpDisplayDevice,
            uint dwFlags);

        [DllImport("user32.dll", CharSet = CharSet.Ansi)]
        internal static extern int EnumDisplaySettings(
            string lpszDeviceName, int iModeNum, ref RotationChanger.DEVMODE lpDevMode);

        public const int DMDO_DEFAULT = 0;
        public const int DMDO_90 = 1;
        public const int DMDO_180 = 2;
        public const int DMDO_270 = 3;

        public const int ENUM_CURRENT_SETTINGS = -1;

    }

    // See: https://msdn.microsoft.com/en-us/library/windows/desktop/dd183569(v=vs.85).aspx
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    internal struct DISPLAY_DEVICE
    {
        [MarshalAs(UnmanagedType.U4)]
        public int cb;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string DeviceName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string DeviceString;
        [MarshalAs(UnmanagedType.U4)]
        public DisplayDeviceStateFlags StateFlags;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string DeviceID;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string DeviceKey;
    }

    internal enum DISP_CHANGE : int
    {
        Successful = 0,
        Restart = 1,
        Failed = -1,
        BadMode = -2,
        NotUpdated = -3,
        BadFlags = -4,
        BadParam = -5,
        BadDualView = -6
    }

    // http://www.pinvoke.net/default.aspx/Enums/DisplayDeviceStateFlags.html
    [Flags()]
    internal enum DisplayDeviceStateFlags : int
    {
        /// <summary>The device is part of the desktop.</summary>
        AttachedToDesktop = 0x1,
        MultiDriver = 0x2,
        /// <summary>The device is part of the desktop.</summary>
        PrimaryDevice = 0x4,
        /// <summary>Represents a pseudo device used to mirror application drawing for remoting or other purposes.</summary>
        MirroringDriver = 0x8,
        /// <summary>The device is VGA compatible.</summary>
        VGACompatible = 0x10,
        /// <summary>The device is removable; it cannot be the primary display.</summary>
        Removable = 0x20,
        /// <summary>The device has more display modes than its output devices support.</summary>
        ModesPruned = 0x8000000,
        Remote = 0x4000000,
        Disconnect = 0x2000000
    }

    // http://www.pinvoke.net/default.aspx/user32/ChangeDisplaySettingsFlags.html
    [Flags()]
    internal enum DisplaySettingsFlags : int
    {
        CDS_NONE = 0,
        CDS_UPDATEREGISTRY = 0x00000001,
        CDS_TEST = 0x00000002,
        CDS_FULLSCREEN = 0x00000004,
        CDS_GLOBAL = 0x00000008,
        CDS_SET_PRIMARY = 0x00000010,
        CDS_VIDEOPARAMETERS = 0x00000020,
        CDS_ENABLE_UNSAFE_MODES = 0x00000100,
        CDS_DISABLE_UNSAFE_MODES = 0x00000200,
        CDS_RESET = 0x40000000,
        CDS_RESET_EX = 0x20000000,
        CDS_NORESET = 0x10000000
    }

    [Flags()]
    internal enum DM : int
    {
        Orientation = 0x00000001,
        PaperSize = 0x00000002,
        PaperLength = 0x00000004,
        PaperWidth = 0x00000008,
        Scale = 0x00000010,
        Position = 0x00000020,
        NUP = 0x00000040,
        DisplayOrientation = 0x00000080,
        Copies = 0x00000100,
        DefaultSource = 0x00000200,
        PrintQuality = 0x00000400,
        Color = 0x00000800,
        Duplex = 0x00001000,
        YResolution = 0x00002000,
        TTOption = 0x00004000,
        Collate = 0x00008000,
        FormName = 0x00010000,
        LogPixels = 0x00020000,
        BitsPerPixel = 0x00040000,
        PelsWidth = 0x00080000,
        PelsHeight = 0x00100000,
        DisplayFlags = 0x00200000,
        DisplayFrequency = 0x00400000,
        ICMMethod = 0x00800000,
        ICMIntent = 0x01000000,
        MediaType = 0x02000000,
        DitherType = 0x04000000,
        PanningWidth = 0x08000000,
        PanningHeight = 0x10000000,
        DisplayFixedOutput = 0x20000000
    }

    public class EnumDisplayDevicesTest
    {
        [DllImport("user32.dll")]
        static extern bool EnumDisplayDevices(string lpDevice, uint iDevNum, ref DISPLAY_DEVICE lpDisplayDevice, uint dwFlags);

        [Flags()]
        public enum DisplayDeviceStateFlags : int
        {
            /// <summary>The device is part of the desktop.</summary>
            AttachedToDesktop = 0x1,
            MultiDriver = 0x2,
            /// <summary>The device is part of the desktop.</summary>
            PrimaryDevice = 0x4,
            /// <summary>Represents a pseudo device used to mirror application drawing for remoting or other purposes.</summary>
            MirroringDriver = 0x8,
            /// <summary>The device is VGA compatible.</summary>
            VGACompatible = 0x10,
            /// <summary>The device is removable; it cannot be the primary display.</summary>
            Removable = 0x20,
            /// <summary>The device has more display modes than its output devices support.</summary>
            ModesPruned = 0x8000000,
            Remote = 0x4000000,
            Disconnect = 0x2000000
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct DISPLAY_DEVICE
        {
            [MarshalAs(UnmanagedType.U4)]
            public int cb;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string DeviceName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string DeviceString;
            [MarshalAs(UnmanagedType.U4)]
            public DisplayDeviceStateFlags StateFlags;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string DeviceID;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string DeviceKey;
        }

        public void Display123()
        {
            DISPLAY_DEVICE d = new DISPLAY_DEVICE();
            d.cb = Marshal.SizeOf(d);
            try
            {
                for (uint id = 0; EnumDisplayDevices(null, id, ref d, 0); id++)
                {
                    if (d.StateFlags.HasFlag(DisplayDeviceStateFlags.AttachedToDesktop))
                    {
                        MessageBox.Show("Id = " + id + "\r\n"
                            + "DeviceName = " + d.DeviceName + "\r\n"
                            + "DeviceString = " + d.DeviceString + "\r\n"
                            + "StateFlags = " + d.StateFlags + "\r\n"
                            + "DeviceID = " + d.DeviceID + "\r\n"
                            + "DeviceKey = " + d.DeviceKey + "\r\n");

                        d.cb = Marshal.SizeOf(d);
                        EnumDisplayDevices(d.DeviceName, 0, ref d, 0);
                        MessageBox.Show("Id = " + id + "\r\n"
                            + "DeviceName = " + d.DeviceName + "\r\n"
                            + "DeviceString = " + d.DeviceString + "\r\n");
                    }
                    d.cb = Marshal.SizeOf(d);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Format("{0}", ex.ToString()));
            }
        }
    }
}
