using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace excel2json
{
    public class MetaCenter : IDisposable
    {
        public enum MetaType
        {
            Json,
            Xml,
            CSharp
        }
        
        private string metaFile;
        private Dictionary<string, string> body;
        private string md5;
        
        public MetaCenter(string metaDir, string excelName, string excelPath)
        {
            if (!string.IsNullOrEmpty(metaDir))
                metaFile = Path.Combine(metaDir, excelName + ".meta");
            else
                metaFile = string.Empty;
            
            if (!string.IsNullOrEmpty(metaFile) && File.Exists(metaFile))
            {
                body = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(metaFile));
            }
            else
            {
                body = new Dictionary<string, string>();
            }
            
            md5 = MD5Utility.GetMD5HashFromFile(excelPath);
        }

        /// <summary>
        /// 指定类型数据是否有变更
        /// </summary>
        /// <param name="metaType"></param>
        /// <returns></returns>
        public bool IsModify(MetaType metaType)
        {
            if (!body.TryGetValue(metaType.ToString(), out var lastMD5))
                return true;

            return lastMD5 != md5;
        }

        /// <summary>
        /// 标记指定类型数据已经发生变更
        /// </summary>
        /// <param name="metaType"></param>
        public void Modify(MetaType metaType)
        {
            body[metaType.ToString()] = md5;
        }

        /// <summary>
        /// 是否存在变更的数据
        /// </summary>
        /// <returns></returns>
        public bool ContainsModified(List<MetaType> values)
        {
            foreach (var value in values)
            {
                if (IsModify((MetaType) value))
                    return true;
            }
            
            return false;
        }

        public void Save()
        {
            if (string.IsNullOrEmpty(metaFile))
                return;
            
            var directory = Path.GetDirectoryName(metaFile);
            
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            
            File.WriteAllText(metaFile, JsonConvert.SerializeObject(body));
        }

        public void Dispose()
        {
            Save();
        }
    }
}