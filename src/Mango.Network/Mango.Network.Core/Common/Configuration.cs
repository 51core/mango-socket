using System.Collections.Generic;

namespace Mango.Network.Core.Common
{
    public class Configuration
    {
        /// <summary>
        /// 存储配置项信息的容器
        /// </summary>
        private static Dictionary<string, string> Items = new Dictionary<string, string>();
        /// <summary>
        /// 添加配置项
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        public static void AddItem(string Key, string Value)
        {
            Items.Add(Key, Value);
        }
        /// <summary>
        /// 获取配置项
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static string GetItem(string Key)
        {
            if (Items.ContainsKey(Key))
            {
                return Items[Key];
            }
            else
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// 删除配置项
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static bool DeleteItem(string Key)
        {
            if (Items.ContainsKey(Key))
            {
                Items.Remove(Key);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
