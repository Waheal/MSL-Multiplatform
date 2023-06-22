using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Documents;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Styling;
using Avalonia.Threading;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;
using MessageBox.Avalonia.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reactive;
using System.Text;
using System.Threading;

namespace MSL_Multiplatform
{
    public partial class MainWindow : Window
    {
        Process ServerProsess = new Process();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void ControlServerBtn_Click(object sender,RoutedEventArgs e)
        {
            //MessageBoxHelper.ShowMessageBox(this, "你好");
            if (ControlServerBtn.Content.ToString() == "开服")
            {
                ControlServerBtn.Content = "关服";
                ServeroutputBox.Clear();
                if (ServerPathBox.Text != null)
                {
                    Directory.CreateDirectory(ServerPathBox.Text);
                    Directory.SetCurrentDirectory(ServerPathBox.Text);
                }
                
                ServerProsess.StartInfo.FileName = JavaPathBox.Text;
                ServerProsess.StartInfo.Arguments = "-jar " + ServerPathBox.Text + Path.DirectorySeparatorChar + ServercorePathBox.Text + " nogui";

                ServerProsess.StartInfo.CreateNoWindow = true;
                ServerProsess.StartInfo.UseShellExecute = false;
                ServerProsess.StartInfo.RedirectStandardInput = true;
                ServerProsess.StartInfo.RedirectStandardOutput = true;
                ServerProsess.StartInfo.RedirectStandardError = true;
                ServerProsess.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);
                ServerProsess.ErrorDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);
                ServerProsess.Start();
                ServerProsess.BeginOutputReadLine();
                ServerProsess.BeginErrorReadLine();
                //timer1.Interval = TimeSpan.FromSeconds(1);
                //timer1.Start();
                Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            }
            else
            {
                ServerProsess.StandardInput.WriteLine("stop");
            }
        }
        private void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                //Dispatcher.Invoke(ReadStdOutput, new object[] { e.Data });
                Dispatcher.UIThread.Post(delegate { ServeroutputBox.Text += e.Data+"\n"; });
                
            }
        }

        private void SelectJavaPath_Click(object sender, RoutedEventArgs e)
        {
            SelectFileAndShowPath(JavaPathBox);
        }

        private void SelectServerPath_Click(object sender,RoutedEventArgs e)
        {
            SelectFileAndShowPath(ServerPathBox);
        }

        private void SelectServercorePath_Click(object sender,RoutedEventArgs e)
        {
            SelectFileAndShowPath(ServercorePathBox);
        }

        public async void SelectFileAndShowPath(TextBox textBox)
        {
            var dialog = new OpenFileDialog();
            if (dialog.Filters != null)
            {
                dialog.Filters.Add(new FileDialogFilter() { Name = "All", Extensions = { "*" } });
                var result = await dialog.ShowAsync(this);
                if (result != null && result.Length > 0)
                {
                    textBox.Text = result[0];
                }
            }
        }
    }
}