using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BaseTree;

namespace AssemblyProc
{
    /// <summary>
    /// Фасад для дерева сборок
    /// </summary>
    public class AssemblyTreeFacade
    {
        /// <summary>
        /// Загрузка сборки в дерево
        /// </summary>
        /// <param name="assemblyPath">Полный путь к сборке</param>
        /// <param name="methodsMap">JSON содержащий карту загружаемых типов</param>
        public void LoadAssembly(string assemblyPath, string methodsMap)
        {
            Type objType = typeof(Object);
            IEnumerable<string> baseMethods = from item in objType.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                                              select item.Name;
            
            Assembly assembly = Assembly.LoadFile(assemblyPath);

            IEnumerable<Type> typesList = null;

            if (string.IsNullOrEmpty(methodsMap))
            {
                typesList = from item in assembly.GetTypes()
                            where item.IsVisible
                            select item;
            }
            else
            {
                //TODO: сделать загрузку типов по карте
            }


            foreach (Type typeItem in typesList)
            {
                IList<string> partsName = typeItem.FullName.Split('.').ToList();
                
                IEnumerable<MethodInfo> methodInfos = from item in typeItem.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                                                      where !baseMethods.Contains(item.Name)
                                                      select item;

                object obj = Activator.CreateInstance(typeItem);

                foreach (MethodInfo miItem in methodInfos)
                {
                    partsName.Add(miItem.Name);

                    AssemblyTreeItem item = null;
                    if (tree.Get(partsName, out item))
                    {
                        bool rez = item.Set(obj, miItem);
                    }
                    else
                    {
                        item = new AssemblyTreeItem();
                        item.Set(obj, miItem);
                        bool rez = tree.Set(partsName, item);

                        if (!rez) throw new Exception("Метод класса не добавлен");
                    }

                    partsName.Remove(partsName.Last());
                }
            }
        }

        /// <summary>
        /// Выполнить метод содержащийся в дереве
        /// Будет выполненн первый метод подходящий по имени и параметрам
        /// </summary>
        /// <param name="command">
        /// Метод и значения его параметров
        /// Пример: GenerateRealty("http://beta.api.gorod55.ru/realty", "C:\\yandex.realty.gz")
        /// </param>
        /// <returns>Результат выполнения метода</returns>
        public object Execute(string command)
        {
            object ret = null;
            string methodName = string.Empty;
            object[] par = null;

            GetMethAndPar(command, ref methodName, out par);

            if (!string.IsNullOrEmpty(methodName))
            {
                AssemblyTreeItem item = null;
                if (tree.FindFirst(methodName, out item))
                {
                    ret = item.Execute(par);
                }                    
            }

            return ret;
        }

        /// <summary>
        /// Получить список методов в дереве
        /// </summary>
        /// <returns>Список с названиями методов</returns>
        public IEnumerable<string> GetListTree()
        {
            ICollection<string> ret = new List<string>();
            ICollection<AssemblyTreeItem> items = new List<AssemblyTreeItem>();

            tree.GetAllEndValues(items);

            foreach (AssemblyTreeItem item in items)
            {
                item.GetDescriptiontList(ret);
            }

            return ret;
        }


        public IEnumerable<string> GetListTree(string path)
        {
            ICollection<string> ret = new List<string>();

            IList<string> pathList = path.Split('/').Where(x => !string.IsNullOrEmpty(x)).ToList();

            AssemblyTreeItem item = null;
            Dictionary<string, InvertedSearcher<string, AssemblyTreeItem>> treeItem = null;
            if (tree.Get(pathList, out item, out treeItem))
            {
                if (null != treeItem)
                {
                    foreach (string name in treeItem.Keys)
                    {
                        ret.Add(name);
                    }
                }
                else if (null != item)
                {
                    foreach (MethodInfo miItem in item.methodList)
                    {
                        ret.Add(miItem.ToString());
                    }                    
                }
            }

            return ret;
        }



        /// <summary>
        /// Из строки получить имя метода и его параметры
        /// </summary>
        /// <param name="parStr">Входная строка для разбора</param>
        /// <param name="methodName">Выходной параметр с именем метода</param>
        /// <param name="par">Выходной параметр с параметрами метода</param>
        private void GetMethAndPar(string parStr, ref string methodName, out object[] par)
        {
            int p1 = parStr.IndexOf('(');
            int p2 = parStr.LastIndexOf(')');

            if (p2 > p1)
            {
                int len = p2 - p1;

                methodName = parStr.Substring(0, p1);
                string methodPar = parStr.Substring(p1 + 1, len - 1);

                string[] parameters = methodPar.Split(',');

                for (int i = 0; i < parameters.Count(); i++)
                {
                    parameters[i] = parameters[i].Trim();
                }

                par = parameters;
            }
            else
            {
                par = null;
            }
        }

        /// <summary>
        /// Древо сборок собственно
        /// </summary>
        private InvertedSearcher<string, AssemblyTreeItem> tree = new InvertedSearcher<string, AssemblyTreeItem>();
    }
}
