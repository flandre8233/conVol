using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using myFunction;

namespace ConVol
{
    public partial class VolControl : Form
    {
        const int Mute = 0x80000; // APPCOMMAND_VOLUME_MUTE
        const int Up = 0xA0000;
        const int Down = 0x90000;
        const int WM_APPCOMMAND = 0x319;
        
        Function myfunction= new Function();

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessageW(IntPtr hwnd, int Msg, IntPtr wParam, IntPtr lparam);

        //global hotkey
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hwnd, int id, int fsModifiers, int vlc);

        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hwnd, int id);

        const int MYACTION_HOTKEY_UP_VolUP = 1;
        const int MYACTION_HOTKEY_Down_VolDown = 2;
        const int MYACTION_HOTKEY_Left_VolDown = 3;
        const int MYACTION_HOTKEY_Right_VolUp = 4;
        const int MYACTION_HOTKEY_ShowAndHide = 5;

        const int MYACTION_HOTKEY_Snake = 6;
    
        bool IsHide;
        bool OpenDoubleSpeed;
        public VolControl()
        {
            // Modifier Keys codes:Alt = 1,Ctrl = 2,Shift = 4,Win = 8
            //Compute the addition of each combination of the keys you want to be pressed
            //ALT+CTRL = 1+2=3,CTRL+SHIFT = 2+4=6...
            //RegisterHotKey(this.Handle, MYACTION_HOTKEY_UP_VolUP, 1, (int)Keys.Up);
            //RegisterHotKey(this.Handle, MYACTION_HOTKEY_Down_VolDown, 1, (int)Keys.Down);
            
            if (Properties.Settings.Default.VolDown == 0 && Properties.Settings.Default.VolUp == 0) {
                RegisterHotKey(this.Handle, MYACTION_HOTKEY_Left_VolDown, 1, (int)Keys.OemOpenBrackets);
                RegisterHotKey(this.Handle, MYACTION_HOTKEY_Right_VolUp, 1, (int)Keys.OemCloseBrackets);
            }
            else {
                RegisterHotKey(this.Handle, MYACTION_HOTKEY_Left_VolDown, 1, Properties.Settings.Default.VolDown);
                RegisterHotKey(this.Handle, MYACTION_HOTKEY_Right_VolUp, 1, Properties.Settings.Default.VolUp);
            }
            
            RegisterHotKey(this.Handle, MYACTION_HOTKEY_ShowAndHide, 1, (int)Keys.X);

            //RegisterHotKey(this.Handle, MYACTION_HOTKEY_Snake, 1, (int)Keys.Z);

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            label1.Text = "ALT + X to Show And Hide ";

            if (Properties.Settings.Default.VolDown == 0 && Properties.Settings.Default.VolUp == 0) {
                label2.Text = "ALT + [ VOL-          ALT + ] VOL+";
            }
            else {
                label2.Text = "ALT + "+  myfunction.keysStringConvert(((Keys)Properties.Settings.Default.VolDown).ToString()) + " VOL-          ALT +  " + myfunction.keysStringConvert(((Keys)Properties.Settings.Default.VolUp).ToString()) + "  VOL+";
            }
                

            

        }
            //SendMessageW(this.Handle, WM_APPCOMMAND, this.Handle, (IntPtr)Mute);

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            
            switch (m.Msg)
            {
                case 0x0200:
                    break;
                case 0x312:
                    switch (m.WParam.ToInt32())
                    {
                        case MYACTION_HOTKEY_Snake:
                            break;

                        case MYACTION_HOTKEY_UP_VolUP:

                            break;
                        case MYACTION_HOTKEY_Down_VolDown:
                            
                            break;
                        case MYACTION_HOTKEY_Left_VolDown:
                            if (OpenDoubleSpeed == false)
                            {
                                SendMessageW(this.Handle, WM_APPCOMMAND, this.Handle, (IntPtr)Down);
                            }
                            else
                            {
                                SendMessageW(this.Handle, WM_APPCOMMAND, this.Handle, (IntPtr)Down);
                                SendMessageW(this.Handle, WM_APPCOMMAND, this.Handle, (IntPtr)Down);
                            }
                            
                            break;
                        case MYACTION_HOTKEY_Right_VolUp:
                            if (OpenDoubleSpeed == false)
                            {
                                SendMessageW(this.Handle, WM_APPCOMMAND, this.Handle, (IntPtr)Up);
                            }
                            else
                            {
                                SendMessageW(this.Handle, WM_APPCOMMAND, this.Handle, (IntPtr)Up);
                                SendMessageW(this.Handle, WM_APPCOMMAND, this.Handle, (IntPtr)Up);
                            }
                            
                            break;
                        case MYACTION_HOTKEY_ShowAndHide:
                            if (IsHide == true)
                            {
                                Show();
                                IsHide = false;
                            }
                            else
                            {
                                Hide();
                                IsHide = true;
                            }
                            break;
                    }
                    break;
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnregisterHotKey(this.Handle, MYACTION_HOTKEY_UP_VolUP);
            UnregisterHotKey(this.Handle, MYACTION_HOTKEY_Down_VolDown);
            UnregisterHotKey(this.Handle, MYACTION_HOTKEY_Left_VolDown);
            UnregisterHotKey(this.Handle, MYACTION_HOTKEY_Right_VolUp);
            UnregisterHotKey(this.Handle, MYACTION_HOTKEY_ShowAndHide);
            Properties.Settings.Default.Save();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false)
            {
                OpenDoubleSpeed = false;
            }
            else
            {
                OpenDoubleSpeed = true;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e) {
            if (checkBox2.Checked) {
                radioButton1.Enabled = true;
                radioButton2.Enabled = true;
            }
            else {
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
            }
        }

        private void VolControl_KeyUp(object sender, KeyEventArgs e) {

            if (checkBox2.Checked) {
                if (radioButton1.Checked) {
                    Properties.Settings.Default.VolDown = e.KeyValue;
                    MessageBox.Show("VolDown 已更改為" + myfunction.keysStringConvert(((Keys)Properties.Settings.Default.VolDown).ToString()) + "下次開啟就有效");
                }
                else {
                    Properties.Settings.Default.VolUp = e.KeyValue;
                    MessageBox.Show("VolUp 已更改為" + myfunction.keysStringConvert(((Keys)Properties.Settings.Default.VolUp).ToString()) + "下次開啟就有效");
                }
            }

        }
    }
}
