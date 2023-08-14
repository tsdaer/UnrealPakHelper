using System;
using System.Windows;
using System.Windows.Controls;
using FolderBrowserDialog = System.Windows.Forms.FolderBrowserDialog;
using DialogResult = System.Windows.Forms.DialogResult;
using Button = System.Windows.Controls.Button;
using Control = System.Windows.Controls.Control;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;

namespace UnrealPakHelper.ControlLibrary
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:UnrealPakHelper.ControlLibrary"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:UnrealPakHelper.ControlLibrary;assembly=UnrealPakHelper.ControlLibrary"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误:
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[浏览查找并选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:SelectPathControl/>
    ///
    /// </summary>

    public enum SelectModeType
    {
        /// <summary> 选择文件
        /// </summary>
        SelectFile,

        /// <summary> 选择文件夹
        /// </summary>
        SelectFolder,

        /// <summary> 保存文件
        /// </summary>
        SaveFile
    }

    [TemplatePart(Name = "SelectBtn", Type = typeof(Button))]
    [TemplatePart(Name = "PathTitle", Type = typeof(Label))]
    public class SelectPathControl : Control
    {
        public static readonly RoutedEvent Event_SelectButtonClicked = 
            EventManager.RegisterRoutedEvent("SelectButtonClicked", RoutingStrategy.Direct, typeof (RoutedEventHandler), typeof (Control));

        public event RoutedEventHandler SelectButtonClicked
        {
            add
            {
                AddHandler(Event_SelectButtonClicked,value);
            }

            remove
            {
                RemoveHandler(Event_SelectButtonClicked, value);
            }
        }
        
        #region 属性

        public static readonly DependencyProperty PathProperty =
            DependencyProperty.Register("Path", typeof(string), typeof(SelectPathControl),
                new PropertyMetadata(string.Empty));

        /// <summary> 选择的路径
        /// </summary>
        public string Path
        {
            get { return (string)GetValue(PathProperty); }
            set { SetValue(PathProperty, value); }
        }


        public static readonly DependencyProperty FilterProperty =
            DependencyProperty.Register("Filter", typeof(string), typeof(SelectPathControl),
                new PropertyMetadata("All|*.*"));
        
        public static readonly DependencyProperty TitleContentProperty =
            DependencyProperty.Register("TitleContent", typeof(string), typeof(SelectPathControl),
                new PropertyMetadata("输入标题"));

        /// <summary> 文件格式过滤器。
        /// </summary>
        public string Filter
        {
            get { return (string)GetValue(FilterProperty); }
            set { SetValue(FilterProperty, value); }
        }
        
        public string TitleContent
        {
            get { return (string)GetValue(TitleContentProperty); }
            set { SetValue(TitleContentProperty, value); }
        }

        public static readonly DependencyProperty SelectModeProperty =
            DependencyProperty.Register("SelectMode", typeof(SelectModeType), typeof(SelectPathControl),
                new PropertyMetadata(SelectModeType.SelectFile));

        /// <summary> 选择格式
        /// </summary>
        public SelectModeType SelectMode
        {
            get { return (SelectModeType)GetValue(SelectModeProperty); }
            set { SetValue(SelectModeProperty, value); }
        }
        

        #endregion

        static SelectPathControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SelectPathControl),
                new FrameworkPropertyMetadata(typeof(SelectPathControl)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var btn = GetTemplateChild("SelectBtn") as Button;
            if (btn != null)
            {
                btn.Click += (s, e) =>
                {
                    switch (SelectMode)
                    {
                        case SelectModeType.SelectFile:
                            OpenSelectFileDialog();
                            break;
                        case SelectModeType.SelectFolder:
                            OpenSelectFolderDialog();
                            break;
                        case SelectModeType.SaveFile:
                            OpenSaveFileDialog();
                            break;
                    }
                    RoutedEventArgs args = new RoutedEventArgs(Event_SelectButtonClicked, this);
                    RaiseEvent(args);
                };
            }
            var title = GetTemplateChild("PathTitle") as Label;
            title.Content = TitleContent;
        }

        #region 按钮相应

        /// <summary> 设置保存的文件名称
        /// </summary>
        private void OpenSaveFileDialog()
        {
            var dlg = new SaveFileDialog { Filter = Filter, FileName = Path };
            var res = dlg.ShowDialog();
            if (res != true) return;
            Path = dlg.FileName;
        }

        /// <summary> 选择文件
        /// </summary>
        private void OpenSelectFileDialog()
        {
            var dlg = new OpenFileDialog { Filter = Filter, FileName = Path };
            var res = dlg.ShowDialog();
            if (res != true) return;
            Path = dlg.FileName;
        }

        /// <summary> 选择文件夹
        /// </summary>
        private void OpenSelectFolderDialog()
        {
            var dlg = new FolderBrowserDialog { SelectedPath = Path };
            var res = dlg.ShowDialog() == DialogResult.OK;
            if (!res) return;
            Path = dlg.SelectedPath;
        }

        #endregion
    }
}

