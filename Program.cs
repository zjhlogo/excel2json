﻿using System;
using System.IO;
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

            //-- Load Excel
            ExcelLoader excel = new ExcelLoader(excelPath);
            var tableInfos = ExcelParser.ReadSheetData(excel.Sheets[0]);

            //-- export json
            if (options.jsonDir != null && options.jsonDir.Length > 0)
            {
                JsonExporter jsonExporter = new JsonExporter(tableInfos, excelName);

                var finalPath = Path.Combine(options.jsonDir, NameFormater.FormatFileName(excelName) + ".json");
                jsonExporter.SaveToFile(finalPath, cd);
            }

            //-- export xml
            if (options.xmlDir != null && options.xmlDir.Length > 0)
            {
                XmlExporter xmlExporter = new XmlExporter(tableInfos, excelName);
                var finalPath = Path.Combine(options.xmlDir, NameFormater.FormatFileName(excelName) + ".xml");
                xmlExporter.SaveToFile(finalPath, cd);
            }

            //-- export c#
            if (options.csharpDir != null && options.csharpDir.Length > 0)
            {
                CSharpExporter csharpExporter = new CSharpExporter(tableInfos, excelName);
                var finalPath = Path.Combine(options.csharpDir, NameFormater.FormatCamelName(excelName, false) + ".cs");
                csharpExporter.SaveToFile(finalPath, cd);
            }
        }
    }
}
