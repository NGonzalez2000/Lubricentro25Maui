
using Lubricentro25.ViewModels.Configurations;

namespace Lubricentro25.Pages.Configuration;

public partial class ConfigurationPage : BasePage
{
    public ConfigurationPage(ConfigurationsViewModel vm) : base(vm) 
    {
        InitializeComponent();
        ul_ListView.ItemsSource = new object[] { "CONTABLE" };
    }
}

// P/Invoke declarations
//   [DllImport("user32.dll", SetLastError = true)]
//   private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

//   [DllImport("user32.dll")]
//   private static extern bool SetForegroundWindow(IntPtr hWnd);

//   [DllImport("user32.dll", SetLastError = true)]
//   private static extern uint SendInput(uint nInputs, [In] INPUT[] pInputs, int cbSize);

//   [DllImport("user32.dll", CharSet = CharSet.Auto)]
//   private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

//   [DllImport("user32.dll")]
//   public static extern short VkKeyScanEx(char ch, IntPtr dwhkl);

//   [DllImport("user32.dll")]
//   public static extern IntPtr GetKeyboardLayout(uint idThread);

//   private const int INPUT_KEYBOARD = 1;
//   private const int KEYEVENTF_KEYUP = 0x0002;
//   private const uint WM_KEYDOWN = 0x0100;
//   private const uint WM_KEYUP = 0x0101;
//   private const int VK_DOWN = 0x28;

//   [StructLayout(LayoutKind.Sequential)]
//   private struct INPUT
//   {
//       public int type;
//       public MOUSEKEYBDHARDWAREINPUT mkhi;
//   }

//   [StructLayout(LayoutKind.Explicit)]
//   private struct MOUSEKEYBDHARDWAREINPUT
//   {
//       [FieldOffset(0)]
//       public MOUSEINPUT mi;
//       [FieldOffset(0)]
//       public KEYBDINPUT ki;
//       [FieldOffset(0)]
//       public HARDWAREINPUT hi;
//   }

//   [StructLayout(LayoutKind.Sequential)]
//   private struct MOUSEINPUT
//   {
//       public int dx;
//       public int dy;
//       public int mouseData;
//       public int dwFlags;
//       public int time;
//       public IntPtr dwExtraInfo;
//   }

//   [StructLayout(LayoutKind.Sequential)]
//   private struct KEYBDINPUT
//   {
//       public ushort wVk;
//       public ushort wScan;
//       public int dwFlags;
//       public int time;
//       public IntPtr dwExtraInfo;
//   }

//   [StructLayout(LayoutKind.Sequential)]
//   private struct HARDWAREINPUT
//   {
//       public int uMsg;
//       public short wParamL;
//       public short wParamH;
//   }
//   public ConfigurationPage()
//{
//	InitializeComponent();
//}


//   private void ExecuteWhatsAppSequence()
//   {
//       // Press Ctrl + N
//       SendKeyCombo(0x11, 0x4E); // Ctrl (0x11) + N (0x4E)
//       System.Threading.Thread.Sleep(1000);

//       // Type a string with numbers
//       string message = PhoneEntry.Text;
//       TypeString(message);
//       System.Threading.Thread.Sleep(1000);

//       // Press Tab
//       SendKey(0x09); // Tab (0x09)
//       System.Threading.Thread.Sleep(1000);

//       // Press Enter
//       SendKey(0x0D); // Enter (0x0D)
//       System.Threading.Thread.Sleep(1000);

//       // Press Shift + Tab
//       SendKeyCombo(0x10, 0x09); // Shift (0x10) + Tab (0x09)
//       System.Threading.Thread.Sleep(1000);

//       // Press Enter
//       SendKey(0x0D); // Enter (0x0D)
//       System.Threading.Thread.Sleep(1000);

//       //// Press two Down Arrow keys
//       //SendKey(0x28); // Down Arrow (0x28)
//       //System.Threading.Thread.Sleep(2000);
//       //SendKey(0x28); // Down Arrow (0x28)
//       //System.Threading.Thread.Sleep(2000);

//       //// Press Enter
//       //SendKey(0x0D); // Enter (0x0D)
//       //System.Threading.Thread.Sleep(1000);

//       // Press Enter
//       SendKey(0x0D); // Enter (0x0D)
//       System.Threading.Thread.Sleep(3000);

//       string path = FileEntry.Text;
//       TypeString(path.ToLower());
//       System.Threading.Thread.Sleep(1000);

//       // Press Enter
//       SendKey(0x0D); // Enter (0x0D)
//       System.Threading.Thread.Sleep(3000);

//       // Press Enter
//       SendKey(0x0D); // Enter (0x0D)
//       System.Threading.Thread.Sleep(3000);
//   }

//   private static void SendKey(ushort keyCode)
//   {
//       INPUT[] inputs = new INPUT[2];

//       // Key down
//       inputs[0].type = INPUT_KEYBOARD;
//       inputs[0].mkhi.ki.wVk = keyCode;
//       inputs[0].mkhi.ki.dwFlags = 0; // Key down

//       // Key up
//       inputs[1].type = INPUT_KEYBOARD;
//       inputs[1].mkhi.ki.wVk = keyCode;
//       inputs[1].mkhi.ki.dwFlags = KEYEVENTF_KEYUP; // Key up

//       SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));
//       System.Threading.Thread.Sleep(50); // Delay between keystrokes
//   }

//   private static void SendKeyCombo(ushort keyCode1, ushort keyCode2)
//   {
//       INPUT[] inputs = new INPUT[4];

//       // Key down first key
//       inputs[0].type = INPUT_KEYBOARD;
//       inputs[0].mkhi.ki.wVk = keyCode1;
//       inputs[0].mkhi.ki.dwFlags = 0; // Key down

//       // Key down second key
//       inputs[1].type = INPUT_KEYBOARD;
//       inputs[1].mkhi.ki.wVk = keyCode2;
//       inputs[1].mkhi.ki.dwFlags = 0; // Key down

//       // Key up second key
//       inputs[2].type = INPUT_KEYBOARD;
//       inputs[2].mkhi.ki.wVk = keyCode2;
//       inputs[2].mkhi.ki.dwFlags = KEYEVENTF_KEYUP; // Key up

//       // Key up first key
//       inputs[3].type = INPUT_KEYBOARD;
//       inputs[3].mkhi.ki.wVk = keyCode1;
//       inputs[3].mkhi.ki.dwFlags = KEYEVENTF_KEYUP; // Key up

//       SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));
//       System.Threading.Thread.Sleep(50); // Delay between keystrokes
//   }

//   private static void TypeString(string text)
//   {
//       foreach (char c in text)
//       {
//           ushort keyCode = CharToVirtualKey(c);
//           if (c == ':')
//           {
//               SendKeyCombo(0xA0, keyCode);
//               continue;
//           }
//           if (keyCode != 0)
//           {
//               SendKey(keyCode);
//           }
//       }
//   }
//   private static ushort CharToVirtualKey(char ch)
//   {
//       IntPtr hkl = GetKeyboardLayout(0);
//       return (ushort)VkKeyScanEx(ch, hkl);
//   }

//   IntPtr GetWppPtr()
//   {
//       // Find WhatsApp window
//       IntPtr hwndWhatsApp = FindWindow(null, "WhatsApp");
//       if (hwndWhatsApp == IntPtr.Zero)
//       {
//           ErrorEntry.Text = "WhatsApp no se encontró!";
//       }

//       return hwndWhatsApp;
//   }

//   private void Button_Clicked(object sender, EventArgs e)
//   {
//       IntPtr hwndWhatsApp = GetWppPtr();
//       if (hwndWhatsApp == IntPtr.Zero) return;

//       string phoneNumber = PhoneEntry.Text;
//       string message = MessageEntry.Text;
//       string url = $"https://wa.me/{phoneNumber}?text={Uri.EscapeDataString(message)}";

//       // Open the URL in the default browser
//       Launcher.OpenAsync(new Uri(url));
//   }

//   private void Button_Clicked_1(object sender, EventArgs e)
//   {
//       // Find WhatsApp window
//       IntPtr hwndWhatsApp = GetWppPtr();
//       if (hwndWhatsApp == IntPtr.Zero) return;

//       // Bring WhatsApp window to the foreground
//       SetForegroundWindow(hwndWhatsApp);

//       // Wait for the window to be focused
//       System.Threading.Thread.Sleep(1000);

//       // Execute the WhatsApp sequence
//       ExecuteWhatsAppSequence();
//   }