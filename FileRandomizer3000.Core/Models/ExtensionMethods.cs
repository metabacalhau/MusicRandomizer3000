using System;
using System.ComponentModel;

namespace FileRandomizer3000.Core.Models
{
    public static class Extension
    {
        public static void InvokeAction<T>(this T type, Action<T> action) where T : ISynchronizeInvoke
        {
            if (action != null)
            {
                if (type.InvokeRequired)
                {
                    type.Invoke(action, new object[] { type });
                }
                else
                {
                    action(type);
                }
            }
        }
    }
}