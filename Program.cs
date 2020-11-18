using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace excel2json
{
    /// <summary>
    /// 应用程序
    /// </summary>
    sealed partial class Program
    {
        /// <summary>
        /// 应用程序入口
        /// </summary>
        /// <param name="args">命令行参数</param>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length <= 0)
            {
                //-- GUI MODE ----------------------------------------------------------
                Console.WriteLine("Launch excel2json GUI Mode...");
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new GUI.MainForm());
            }
            else
            {
                //-- COMMAND LINE MODE -------------------------------------------------

                //-- 分析命令行参数
                var options = new Options();
                var parser = new CommandLine.Parser(with => with.HelpWriter = Console.Error);

                if (parser.ParseArgumentsStrict(args, options, () => Environment.Exit(-1)))
                {
                    //-- 执行导出操作
                    try
                    {
                        DateTime startTime = DateTime.Now;
                        Run(options);
                        //-- 程序计时
                        DateTime endTime = DateTime.Now;
                        TimeSpan dur = endTime - startTime;
                        Console.WriteLine(
                            string.Format("[{0}]：\tConversion complete in [{1}ms].",
                            Path.GetFileName(options.excelPath),
                            dur.TotalMilliseconds)
                            );
                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine("Error: " + exp.Message);
                    }
                }
            }// end of else
        }

        /// <summary>
        /// 根据命令行参数，执行Excel数据导出工作
        /// </summary>
        /// <param name="options">命令行参数</param>
        private static void Run(Options options)
        {
            //-- Excel File
            string excelPath = options.excelPath;
            string excelName = Path.GetFileNameWithoutExtension(options.excelPath);

            //-- Encoding
            Encoding cd = new UTF8Encoding(false);
            
            //-- Exporters
            List<ExportTask> exportTasks = new List<ExportTask>();
            
            //-- export json
            if (!string.IsNullOrEmpty(options.jsonDir))
                exportTasks.Add(new ExportTask(ExportJsonFile, MetaCenter.MetaType.Json, options.jsonDir));
            
            //-- export xml
            if (!string.IsNullOrEmpty(options.xmlDir))
                exportTasks.Add(new ExportTask(ExportXmlFile, MetaCenter.MetaType.Xml, options.xmlDir));
            
            //-- export csharp
            if (!string.IsNullOrEmpty(options.csharpDir))
                exportTasks.Add(new ExportTask(ExportCsharpFile, MetaCenter.MetaType.CSharp, options.csharpDir));
            
            //-- Load Meta
            using (var metaCenter = new MetaCenter(options.metaDir, excelName, excelPath))
            {
                if (!metaCenter.ContainsModified(exportTasks.Select(task => task.metaType).ToList()))
                {
                    Console.WriteLine("{0} is no change, skip generation", excelName);
                }
                else
                {
                    //-- Load Excel
                    ExcelLoader excel = new ExcelLoader(excelPath);
                    var tableInfos = ExcelParser.ReadSheetData(excel.Sheets[0]);

                    foreach (var exportTask in exportTasks)
                    {
                        if (!metaCenter.IsModify(exportTask.metaType))
                        {
                            Console.WriteLine("{0} {1} data is no change, skip generation", excelName, exportTask.metaType);
                            continue;
                        }
                        exportTask.function(tableInfos, excelName, exportTask.outputDir, cd);
                        metaCenter.Modify(exportTask.metaType);
                    }
                }
            }
        }

        private static void ExportCsharpFile(List<ExcelParser.TableInfo> tableInfos, string excelName, string outputDir, Encoding outputEncoding)
        {
            CSharpExporter csharpExporter = new CSharpExporter(tableInfos, excelName);
            var finalPath = Path.Combine(outputDir, NameFormater.FormatCamelName(excelName, false) + ".cs");
            csharpExporter.SaveToFile(finalPath, outputEncoding);
        }

        private static void ExportXmlFile(List<ExcelParser.TableInfo> tableInfos, string excelName, string outputDir, Encoding outputEncoding)
        {
            XmlExporter xmlExporter = new XmlExporter(tableInfos, excelName);
            var finalPath = Path.Combine(outputDir, xmlExporter.m_strFileName + ".xml");
            xmlExporter.SaveToFile(finalPath, outputEncoding);
        }

        private static void ExportJsonFile(List<ExcelParser.TableInfo> tableInfos, string excelName, string outputDir, Encoding outputEncoding)
        {
            JsonExporter jsonExporter = new JsonExporter(tableInfos, excelName);

            var finalPath = Path.Combine(outputDir, NameFormater.FormatFileName(excelName) + ".json");
            jsonExporter.SaveToFile(finalPath, outputEncoding);
        }

        private delegate void ExportFunction(List<ExcelParser.TableInfo> tableInfos, string excelName, string outputDir, Encoding outputEncoding);

        private class ExportTask
        {
            public ExportFunction function;
            public MetaCenter.MetaType metaType;
            public string outputDir;

            public ExportTask(ExportFunction function, MetaCenter.MetaType metaType, string outputDir)
            {
                this.function = function;
                this.metaType = metaType;
                this.outputDir = outputDir;
            }
        }
    }
}
