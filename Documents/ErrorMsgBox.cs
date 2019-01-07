using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Documents
{
    public partial class ErrorMsgBox : Form
    {
        bool showInfo = false;
        int height = 0;
        public static void Show(string caption, string message, string info)
        {
            ErrorMsgBox box = new ErrorMsgBox(caption, message, info);
            box.ShowDialog();
        }
        ErrorMsgBox()
        {
            InitializeComponent();
        }
        ErrorMsgBox(string caption, string message, string info)
        {
            InitializeComponent();
            txtInfo.Text = info;
            txtMsg.Text = message;
            this.Text = caption;
        }
        private void ErrorMsgBox_Load(object sender, EventArgs e)
        {
            txtInfo.Visible = showInfo; //должно быть ложным
            height = txtInfo.Height;
            Height += -txtInfo.Height; ;
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            showInfo = !showInfo;
            txtInfo.Visible = showInfo;
            if (!showInfo) height = txtInfo.Height;
            Height += showInfo ? height : -height;
            btnInfo.Text = "Побробности " + (showInfo ? "<" : ">");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
