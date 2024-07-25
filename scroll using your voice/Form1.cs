using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Speech.Recognition;
using System.Windows.Forms;
using System.Diagnostics;
using NAudio.CoreAudioApi;
using NAudio.CoreAudioApi;
using AudioSwitcher.AudioApi.CoreAudio;
namespace ScrollUsingYourVoice
{
    public partial class MainForm : Form
    {
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        private SpeechRecognitionEngine recognizer;
        private IntPtr selectedWindowHandle;
        private bool isListening = false;

        private const int KEYEVENTF_KEYUP = 0x0002;
        private const byte VK_UP = 0x26;
        private const byte VK_DOWN = 0x28;
        Process chromeProcess;
        public MainForm()
        {
            InitializeComponent();
            InitializeSpeechRecognition();
            RefreshWindowList();
        }

        private void InitializeSpeechRecognition()
        {
            recognizer = new SpeechRecognitionEngine();
            recognizer.SetInputToDefaultAudioDevice();

            var commands = new Choices(new[] { "Scroll up", "Scroll down",
                "close chrome", "up audio", "down audio", 
                "mute audio", "close the app" });
            var grammar = new Grammar(new GrammarBuilder(commands));
            recognizer.LoadGrammar(grammar);
            recognizer.SpeechRecognized += Recognizer_SpeechRecognized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (isListening)
                {
                    // Stop recognition
                    recognizer.RecognizeAsyncStop();
                    isListening = false;
                    button1.Text = "Start Listening";
                }
                else
                {
                    // Start recognition
                    recognizer.SetInputToDefaultAudioDevice();
                    recognizer.RecognizeAsync(RecognizeMode.Multiple);
                    isListening = true;
                    button1.Text = "Stop Listening";
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message);
            }
        }

        private void Recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            // Ensure UI updates are performed on the UI thread
            if (InvokeRequired)
            {
                Invoke(new Action(() => Recognizer_SpeechRecognized(sender, e)));
                return;
            }

            switch (e.Result.Text)
            {
                case "Scroll up":
                    ScrollWindow(Keys.Up);
                    break;
                case "Scroll down":
                    ScrollWindow(Keys.Down);
                    break;
                case "up audio":
                    SetVolume(10);
                    break;
                case "down audio":
                    SetVolume(-10);
                    break;
                case "mute audio":
                    muteVolume();
                    break;
                case "close the app":
                    Close();
                    break;
            }
        }

        public void SetVolume(int volume)
        {
            
            // Get the default audio device
            var device = new CoreAudioController().DefaultPlaybackDevice;

            // Set the volume
            device.Volume = GetVolume() + volume; // AudioSwitcher expects volume in percentage
        }

        public Double GetVolume()
        {
            // Get the default audio device
            var device = new CoreAudioController().DefaultPlaybackDevice;

            // Return the current volume as a range from 0.0 to 1.0
            return device.Volume;
        }

        public void muteVolume()
        {
            // Get the default audio device
            // Get the default audio device
            var device = new CoreAudioController().DefaultPlaybackDevice;

            // Set the volume
            device.Volume = 0;
        }

        private void ScrollWindow(Keys key)
        {
            if (selectedWindowHandle != IntPtr.Zero)
            {
                if (SetForegroundWindow(selectedWindowHandle))
                {
                    byte vk = key == Keys.Up ? VK_UP : VK_DOWN;
                    keybd_event(vk, 0, 0, 0); // Key down
                    keybd_event(vk, 0, KEYEVENTF_KEYUP, 0); // Key up
                }
                else
                {
                    ShowWindowNotRunningMessage();
                }
            }
            else
            {
                MessageBox.Show("Choose an app from the combo box.");
            }
        }

        private void ShowWindowNotRunningMessage()
        {
            var selectedItem = comboBoxWindows.SelectedItem as WindowItem;
            if (selectedItem != null)
            {
                MessageBox.Show($"{selectedItem.Title} is not running.");
            }
        }

        private void RefreshWindowList()
        {
            comboBoxWindows.Items.Clear();
            EnumWindows((hWnd, lParam) =>
            {
                if (IsWindowVisible(hWnd))
                {
                    var windowText = new StringBuilder(256);
                    GetWindowText(hWnd, windowText, windowText.Capacity);
                    if (windowText.Length > 0)
                    {
                        comboBoxWindows.Items.Add(new WindowItem { Handle = hWnd, Title = windowText.ToString() });
                    }
                }
                return true;
            }, IntPtr.Zero);
        }

        private void comboBoxWindows_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxWindows.SelectedItem is WindowItem selectedItem)
            {
                selectedWindowHandle = selectedItem.Handle;
            }
        }

        private void buttonScrollUp_Click(object sender, EventArgs e)
        {
            ScrollWindow(Keys.Up);
        }

        private void buttonScrollDown_Click(object sender, EventArgs e)
        {
            ScrollWindow(Keys.Down);
        }

        private void comboBoxWindows_Click(object sender, EventArgs e)
        {
            RefreshWindowList();
        }

        private class WindowItem
        {
            public IntPtr Handle { get; set; }
            public string Title { get; set; }

            public override string ToString() => Title;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (recognizer != null)
            {
                if (isListening)
                {
                    recognizer.RecognizeAsyncStop();
                }
                recognizer.Dispose();
            }
            base.OnFormClosing(e);
        }
    }


}
