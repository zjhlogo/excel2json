using CommandLine;

namespace excel2json
{
    partial class Program
    {
        /// <summary>
        /// 命令行参数定义
        /// </summary>
        internal sealed class Options
        {
            public Options()
            {
            }

            [Option('i', "input", Required = true, HelpText = "input excel file path.")]
            public string excelPath
            {
                get;
                set;
            }

            [Option('j', "json", Required = false, HelpText = "export json file dir.")]
            public string jsonDir
            {
                get;
                set;
            }

            [Option('x', "xml", Required = false, HelpText = "export xml file dir.")]
            public string xmlDir
            {
                get;
                set;
            }

            [Option('p', "csharp", Required = false, HelpText = "export C# data struct code file dir.")]
            public string csharpDir
            {
                get;
                set;
            }

            [Option('m', "meta", Required = false, HelpText = "meta files dir")]
            public string metaDir
            {
                get;
                set;
            }
        }
    }
}
