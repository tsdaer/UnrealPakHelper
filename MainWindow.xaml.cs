using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using UnrealPakHelper.Core;

namespace UnrealPakHelper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
public partial class MainWindow
    {
        public CommandExecutor _executor;
        public bool AppRunning = true;

        public MainWindow()
        {
            InitializeComponent();
            SelectPathControl_UnrealPakExe.Path = Properties.Settings.Default.DefaultUnrealPakExePath;
            SelectPathControl_WorkDir.Path = Properties.Settings.Default.WorkDir;
            _executor = new CommandExecutor(this);
            Thread UpdateUIThread = new Thread(UpdateUI);
            UpdateUIThread.Start();
        }
        
        
        //method
        public void UpdateUI()
        {
            Thread.Sleep(1000);
            while (AppRunning)
            {
                Thread.Sleep(100);
                string logNow = _executor.PowerShell.StandardOutput.ReadLine();
                this.Dispatcher.Invoke(new Action(delegate
                {
                    if (logNow != null)
                    {
                        AddNewLog(logNow);
                    }
                }));
            }
        }
        
        public void ButtonChangeArea(Button btn, StackPanel pan)
        {

            if (pan.Visibility == Visibility.Collapsed)
            {
                pan.Visibility = Visibility.Visible;
                btn.Content = "⋘";
            }
            else
            {
                pan.Visibility = Visibility.Collapsed;
                btn.Content = "⋙";
            }
        }
        public void AddNewLog(string logContent)
        {
            TextBox_Log.AppendText("\n" + logContent);
            ScrollViewer_Log.ScrollToEnd();
        }
        
        
        
        private void SelectPathControl_UnrealPakExe_OnSelectButtonClicked(object sender, RoutedEventArgs e)
        {
            //throw new System.NotImplementedException();

            if (System.IO.File.Exists(SelectPathControl_UnrealPakExe.Path))
            {
                Properties.Settings.Default.DefaultUnrealPakExePath = SelectPathControl_UnrealPakExe.Path;
                Properties.Settings.Default.Save();
            }
        }

        private void Button_PakList_OnClick(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(SelectPathControl_ViewTargetPath.Path))
            {
                return;
            }
            unrealPakCommand Command = new unrealPakCommand(CommondType.List, SelectPathControl_UnrealPakExe.Path);
            Command.CommandData_String.Add("PakFilename", SelectPathControl_ViewTargetPath.Path);
            if ((bool)CheckBox_PakList_ExcludeDeleted.IsChecked)
            {
                Command.CommandData_bool.Add("ExcludeDeleted" , true);
            }
            _executor.AddNewCommand(Command);
        }

        private void Window_OnClosed(object sender, EventArgs e)
        {
            _executor.Stop = true;
            AppRunning = false;
            _executor.PowerShell.Kill();
        }

        private void Button_Pak_OnClick(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(SelectPathControl_ResponseFile.Path) && 
                Directory.Exists(Path.GetDirectoryName(SelectPathControl_PakFilename.Path)))
            {
                unrealPakCommand Command =
                    new unrealPakCommand(CommondType.Create, SelectPathControl_UnrealPakExe.Path);
                Command.CommandData_String.Add("PakFilename", SelectPathControl_PakFilename.Path);
                Command.CommandData_String.Add("ResponseFile", SelectPathControl_ResponseFile.Path);
                Command.Options.Enqueue("-sign");
                _executor.AddNewCommand(Command);
            }

        }

        private void Button_PakTest_OnClick(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(SelectPathControl_ViewTargetPath.Path))
            {
                return;
            }
            unrealPakCommand Command = new unrealPakCommand(CommondType.Test, SelectPathControl_UnrealPakExe.Path);
            Command.CommandData_String.Add("PakFilename", SelectPathControl_ViewTargetPath.Path);
            _executor.AddNewCommand(Command);
        }

        private void Button_PakInfo_OnClick(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(SelectPathControl_ViewTargetPath.Path))
            {
                return;
            }
            unrealPakCommand Command = new unrealPakCommand(CommondType.Info, SelectPathControl_UnrealPakExe.Path);
            Command.CommandData_String.Add("PakFilename", SelectPathControl_ViewTargetPath.Path);
            _executor.AddNewCommand(Command);
        }

        private void Button_PakVerify_OnClick(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(SelectPathControl_ViewTargetPath.Path))
            {
                return;
            }
            unrealPakCommand Command = new unrealPakCommand(CommondType.Verify, SelectPathControl_UnrealPakExe.Path);
            Command.CommandData_String.Add("PakFilename", SelectPathControl_ViewTargetPath.Path);
            _executor.AddNewCommand(Command);
        }

        private void SelectPathControl_WorkDir_OnSelectButtonClicked(object sender, RoutedEventArgs e)
        {
            if (System.IO.Directory.Exists(SelectPathControl_WorkDir.Path))
            {
                Properties.Settings.Default.WorkDir = SelectPathControl_WorkDir.Path;
                Properties.Settings.Default.Save();
            }
        }

        private void Button_RePak_OnClick(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(SelectPathControl_ResponseFile.Path))
            {
                return;
            }
            unrealPakCommand Command = new unrealPakCommand(CommondType.Repack, SelectPathControl_UnrealPakExe.Path);
            Command.CommandData_String.Add("PakFilename", SelectPathControl_ViewTargetPath.Path);
            if ((bool)CheckBox_Repak_Output.IsChecked)
            {
                Command.CommandData_bool.Add("OutputPath" , true);
            }

            if ((bool)CheckBox_RePak_ExcludeDeleted.IsChecked)
            {
                Command.CommandData_bool.Add("ExcludeDeleted" , true);
            }
            _executor.AddNewCommand(Command);
        }

        private void Button_CloseWindow_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Maximized_OnClick(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                Button_Maximized.Content = "最大化";
                this.WindowState = WindowState.Normal;
            }
            else
            {
                Button_Maximized.Content = "恢复";
                this.WindowState = WindowState.Maximized;
            }
        }

        private void Button_Minimized_OnClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Button_ClearLog_OnClick(object sender, RoutedEventArgs e)
        {
            TextBox_Log.Clear();
        }

        private void Button_SaveLog_OnClick(object sender, RoutedEventArgs e)
        {
            string savePath =  SelectPathControl_WorkDir.Path;
            if (Directory.Exists( SelectPathControl_WorkDir.Path))
            {
                var fs = File.Create(savePath + "/" + "Log.log");
                fs.Close();
                var sw = new StreamWriter(savePath + "/" + "Log.log");
                sw.Write(TextBox_Log.Text);
            }
        }
        
        private void Button_DefaultSet_Fold_OnClick(object sender, RoutedEventArgs e)
        {
            ButtonChangeArea(Button_DefaultSet_Fold,StackPanel_DefaultSetArea);
        }
        
        private void Button_View_Fold_OnClick(object sender, RoutedEventArgs e)
        {
            ButtonChangeArea(Button_View_Fold, StackPanel_ViewArea);
        }

        private void Button_Pak_Fold_OnClick(object sender, RoutedEventArgs e)
        {
            ButtonChangeArea(Button_Pak_Fold, StackPanel_PakArea);
        }

        private void Button_Extract_Fold_OnClick(object sender, RoutedEventArgs e)
        {
            ButtonChangeArea(Button_Extract_Fold,StackPanel_ExtractArea);
        }

        private void Button_MountPoint_OnClick(object sender, RoutedEventArgs e)
        {
            if (TextBox_MountPoint.Text == "" || !File.Exists(SelectPathControl_ExtractTargetFile.Path))
            {
                return;
            }
            unrealPakCommand Command = new unrealPakCommand(CommondType.Dest, SelectPathControl_UnrealPakExe.Path);
            Command.CommandData_String.Add("PakFilename", SelectPathControl_ExtractTargetFile.Path);
            Command.CommandData_String.Add("MountPoint",TextBox_MountPoint.Text);
            _executor.AddNewCommand(Command);
        }

        private void Button_Extract_OnClick(object sender, RoutedEventArgs e)
        {
            if (File.Exists(SelectPathControl_ExtractTargetFile.Path) &&
                Directory.Exists(SelectPathControl_ExtractDir.Path))
            {
                unrealPakCommand Command =
                    new unrealPakCommand(CommondType.Extract, SelectPathControl_UnrealPakExe.Path);
                Command.CommandData_String.Add("PakFilename", SelectPathControl_ExtractTargetFile.Path);
                Command.CommandData_String.Add("ExtractDir",SelectPathControl_ExtractDir.Path);
                _executor.AddNewCommand(Command);
            }

        }
    }
}