// //Copyright honkai-rts. All Rights Reserved.

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace UnrealPakHelper.Core
{
    /*
     UnrealPak <PakFilename> -Test :测试Pak文件能否打开（是否加密）
     UnrealPak <PakFilename> -Verify :
     UnrealPak <PakFilename> -Info
     UnrealPak <PakFilename> -List [-ExcludeDeleted]
     UnrealPak <PakFilename> <GameUProjectName> <GameFolderName> -ExportDependencies=<OutputFileBase> -NoAssetRegistryCache -ForceDependsGathering
     UnrealPak <PakFilename> -Extract <ExtractDir> [-Filter=<filename>]
     UnrealPak <PakFilename> -Create=<ResponseFile> [Options]
     UnrealPak <PakFilename> -Dest=<MountPoint>
     UnrealPak <PakFilename> -Repack [-Output=Path] [-ExcludeDeleted] [Options]
     UnrealPak <PakFilename1> <PakFilename2> -diff
     UnrealPak <PakFolder> -AuditFiles [-OnlyDeleted] [-CSV=<filename>] [-order=<OrderingFile>] [-SortByOrdering]
     UnrealPak <PakFilename> -WhatsAtOffset [offset1] [offset2] [offset3] [...]
     UnrealPak <PakFolder> -GeneratePIXMappingFile -OutputPath=<Path>
Options:
     -blocksize=<BlockSize> :块大小
     -bitwindow=<BitWindow> :
     -compress  :压缩
     -encrypt :加密
     -order=<OrderingFile> :
     -diff :首先需要两个文件名
     -enginedir :使用ini加密配置时指定的引擎目录
     -projectdir :使用ini加密配置时指定的项目目录
     -cryptokeys=<cryptofile> :使用ini加密配置时指定的项目目录
     -encryptionini :指定从中收集加密设置的ini基础名称
     -extracttomountpoint :提取到pak文件的装载点路径
     -encryptindex :加密pak文件索引，使其在未提供密钥的情况下无法在unrealpak中使用
     -compressionformat[s]=<Format[,format2,...]> :设置要压缩的格式，在出现故障时回退
     -encryptionkeyoverrideguid :重写用于加密此pak文件中数据的加密密钥guid
     -sign :在pak旁边生成一个签名（.sig）文件
     -fallbackOrderForNonUassetFiles :如果没有为ubulk/uexp文件指定顺序，请根据uaset顺序计算出隐式顺序。一般仅适用于cooker order
     */

    public enum CommondType
    {
        //测试
        Debug,
        
        //测试Pak文件能否打开（是否加密）
        Test,
        
        //验证
        Verify,
        
        //包体信息
        Info,
        
        //查看包体结构
        List,
        
        //导出依赖
        ExportDependencies,
        
        //解包
        Extract,
        
        //打包
        Create,
        
        //修改挂载点
        Dest,
        
        //重新打包
        Repack,
        
        //对比两个包
        Diff,
        
        //
        AuditFiles,
        //
        WhatsAtOffset,
        //
        GeneratePIXMappingFile
    }
    
    
    public struct unrealPakCommand
    {
        public Dictionary<string, string> CommandData_String;
        public Dictionary<string, int> CommandData_Int;
        public Dictionary<string, bool> CommandData_bool;
        public ConcurrentQueue<string> Options; 
        public CommondType Type;
        public string PakExePath;

        public unrealPakCommand(CommondType _type,string _pakExePath)
        {
            Type = _type;
            CommandData_String = new Dictionary<string, string>();
            CommandData_Int = new Dictionary<string, int>();
            CommandData_bool = new Dictionary<string, bool>();
            Options = new ConcurrentQueue<string>();
            PakExePath = _pakExePath;
        }

        public string GenerateCommandLine()
        {
            string res = "";
            
            string PakFilename;
            string GameUProjectName;
            string GameFolderName;
            string OutputFileBase;
            string ExtractDir;
            string filename;
            string MountPoint;
            string ResponseFile;
            string PakFilename1;
            string PakFilename2;
            string OrderingFile;
            string Path;
            
            
            switch (Type)
            {
                case CommondType.Test:
                    //UnrealPak <PakFilename> -Test
                    if (CommandData_String.TryGetValue("PakFilename", out PakFilename))
                    {
                        res = PakExePath + string.Format(" {0} -Test",PakFilename);
                    }
                    break;
                case CommondType.Verify:
                    //UnrealPak <PakFilename> -Verify
                    if (CommandData_String.TryGetValue("PakFilename", out PakFilename))
                    {
                        res = PakExePath + string.Format(" {0} -Verify",PakFilename);
                    }
                    break;
                case CommondType.Info:
                    //UnrealPak <PakFilename> -Info
                    if (CommandData_String.TryGetValue("PakFilename", out PakFilename))
                    {
                        res = PakExePath + string.Format(" {0} -Info",PakFilename);
                    }
                    break;
                case CommondType.List:
                    //UnrealPak <PakFilename> -List [-ExcludeDeleted]
                    bool ExcludeDeleted = false;
                    if (CommandData_String.TryGetValue("PakFilename", out PakFilename))
                    {
                        res = PakExePath + string.Format(" {0} -List {1}",PakFilename,CommandData_bool.TryGetValue("ExcludeDeleted",out ExcludeDeleted)?"-ExcludeDeleted":"");
                    }
                    break;
                case CommondType.ExportDependencies:
                    //UnrealPak <PakFilename> <GameUProjectName> <GameFolderName> -ExportDependencies=<OutputFileBase> -NoAssetRegistryCache -ForceDependsGathering
                    if (CommandData_String.TryGetValue("PakFilename", out PakFilename)&&
                        CommandData_String.TryGetValue("GameUProjectName", out GameUProjectName)&&
                        CommandData_String.TryGetValue("GameFolderName", out GameFolderName)&&
                        CommandData_String.TryGetValue("OutputFileBase", out OutputFileBase) )
                    {
                        res = PakExePath + string.Format(" {0} {1} {2} -ExportDependencies={3} -NoAssetRegistryCache -ForceDependsGathering",
                            PakFilename,GameUProjectName,GameFolderName,OutputFileBase);
                    }
                    break;
                case CommondType.Extract:
                    //UnrealPak <PakFilename> -Extract <ExtractDir> [-Filter=<filename>]
                    if (CommandData_String.TryGetValue("PakFilename", out PakFilename) &&
                        CommandData_String.TryGetValue("ExtractDir", out ExtractDir)
                       )
                    {
                        if (CommandData_String.TryGetValue("filename", out filename))
                        {
                            res = PakExePath + string.Format(" {0} -Extract {1} ", PakFilename, ExtractDir)
                                             + string.Format("-Filter={0}", filename);
                        }
                        else
                        {
                            res = PakExePath + string.Format(" {0} -Extract {1} ", PakFilename, ExtractDir);
                        }

                    }
                    break;
                case CommondType.Create:
                    //UnrealPak <PakFilename> -Create=<ResponseFile> [Options]
                    if (CommandData_String.TryGetValue("PakFilename", out PakFilename) && 
                        CommandData_String.TryGetValue("ResponseFile",out ResponseFile))
                    {
                        res = PakExePath + string.Format(" {0} -Create= {1}",PakFilename,ResponseFile);
                        while (!Options.IsEmpty)
                        {
                            string OptionNow;
                            if (Options.TryDequeue(out OptionNow))
                            {
                                res = res +" "+ OptionNow;
                            }
                            
                        }
                    }
                    break;
                case CommondType.Dest:
                    //UnrealPak <PakFilename> -Dest=<MountPoint>
                    if (CommandData_String.TryGetValue("PakFilename", out PakFilename) &&
                        CommandData_String.TryGetValue("MountPoint", out MountPoint))
                    {
                        res = PakExePath + string.Format(" {0} -Dest={1}" , PakFilename,MountPoint);
                    }
                    break;
                case CommondType.Repack:
                    //UnrealPak <PakFilename> -Repack [-Output=Path] [-ExcludeDeleted] [Options]
                    if (CommandData_String.TryGetValue("PakFilename", out PakFilename))
                    {
                        bool OutputPath = false;
                        res = PakExePath + string.Format("UnrealPak {0} -Repack {1} {2}",
                            PakFilename,CommandData_bool.TryGetValue("OutputPath",out OutputPath) ? "-Output=Path":"",
                            CommandData_bool.TryGetValue("ExcludeDeleted",out OutputPath) ? "-ExcludeDeleted":"");
                    }
                    while (!Options.IsEmpty)
                    {
                        string OptionNow;
                        if (Options.TryDequeue(out OptionNow))
                        {
                            res = res +" "+ OptionNow;
                        }
                            
                    }
                    
                    break;
                case CommondType.Diff:
                    //UnrealPak <PakFilename1> <PakFilename2> -diff
                    if (CommandData_String.TryGetValue("PakFilename1", out PakFilename1) &&
                        CommandData_String.TryGetValue("PakFilename2", out PakFilename2))
                    {
                        res = PakExePath + string.Format("{0} {1} -diff",PakFilename1,PakFilename2);
                    }
                    break;
                case CommondType.AuditFiles:
                    break;
                case CommondType.WhatsAtOffset:
                    break;
                case CommondType.GeneratePIXMappingFile:
                    break;
                case CommondType.Debug:
                    res = "ping www.bing.com";
                    break;
                default:
                    break;
            }
            return res;
        }
    }

    public class CommandExecutor
    {
        public ConcurrentQueue<unrealPakCommand> CommandList;
        public Process PowerShell;
        public bool Stop = false;
        public Thread TickThread;
        private MainWindow mainWindow;
        public CommandExecutor(MainWindow _mainWindow)
        {
            CommandList = new ConcurrentQueue<unrealPakCommand>();
            mainWindow = _mainWindow;
            CreatePowershellProcess();
            ActiveThread();
        }

        public void CreatePowershellProcess()
        {
            PowerShell = new Process();
            PowerShell.StartInfo.FileName = "PowerShell.exe";
            //PowerShell.StartInfo.CreateNoWindow = true;
            PowerShell.StartInfo.UseShellExecute = false;
            PowerShell.StartInfo.RedirectStandardInput = true;
            PowerShell.StartInfo.RedirectStandardOutput = true;
            PowerShell.StartInfo.Verb = "runas";
            PowerShell.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            PowerShell.StartInfo.CreateNoWindow = false;
            PowerShell.Start();
        }
        
        public void UpdateProcess()
        {
            if (PowerShell.HasExited && !Stop)
            {
                CreatePowershellProcess();
            }
        }

        public void RunCommand()
        {
            if (CommandList.IsEmpty)
            {
                return;
            }
            unrealPakCommand CommandNow;
            var dequeueSuccess = CommandList.TryDequeue(out CommandNow);
            if (dequeueSuccess)
            {
                PowerShell.StandardInput.WriteLine(CommandNow.GenerateCommandLine());
                //PowerShell.StandardInput.AutoFlush = true;
            }
        }

        public void AddNewCommand(unrealPakCommand _unrealPakCommand)
        {
            CommandList.Enqueue(_unrealPakCommand);
        }
        private void Active()
        {
            while (!Stop)
            {
                Thread.Sleep(500);
                UpdateProcess();
                RunCommand();
                
            }
        }

        public void ActiveThread()
        {
            Thread TickThread = new Thread(()=>{
                //下面写一些在线程中处理的方法

                Active();
            });
            TickThread.Start();
        }
    }
}