using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace AssemblyProc
{
    internal class AssemblyTreeItem
    {
        public object classObj = null;
        public List<MethodInfo> methodList = new List<MethodInfo>();

        internal bool Set(object obj, MethodInfo mi)
        {
            bool ret = false;

            classObj = obj;

            if (!methodList.Contains(mi))
            {
                methodList.Add(mi);
                ret = true;
            }

            return ret;
        }

        internal object Execute(object[] par)
        {
            object ret = null;

            MethodInfo itemMethod = null;

            if (1 == methodList.Count)
            {
                itemMethod = methodList[0];
            }
            else
            {
                itemMethod = (from item in methodList
                              where item.GetParameters().Count() == par.Count()
                              select item).FirstOrDefault();
            }

            if (null != itemMethod)
            {
                ret = itemMethod.Invoke(classObj, par);
            }

            return ret;
        }

        internal void GetDescriptiontList(ICollection<string> items)
        {
            foreach (MethodInfo miItem in methodList)
            {
                items.Add(miItem.ToString());
            }
        }


    }
}
