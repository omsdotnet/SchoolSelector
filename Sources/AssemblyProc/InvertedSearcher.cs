using System.Collections.Generic;
using System.Linq;

namespace BaseTree
{
    /// <summary>
    /// Древо обобщенного инвертированного поиска
    /// © ПК
    /// </summary>
    /// <typeparam name="TKey">тип на которые разбиваеться ключ</typeparam>
    /// <typeparam name="TValue">тип хранимых значений</typeparam>
    public class InvertedSearcher<TKey, TValue>
    {
        public InvertedSearcher()
        {
            Init(0, null);
        }

        public void Clear()
        {
            Init(0, null);
        }


        /// <summary>
        /// Поместить элемент в дерево
        /// </summary>
        /// <param name="key">ключ - должен обеспечивать получение элементов по индексу</param>
        /// <param name="val">значение</param>
        public bool Set(IList<TKey> key, TValue val)
        {
            bool ret = false;
            
            if (level == (key.Count))
            {
                if (null == valueItem)
                {
                    valueItem = val;
                    ret = true;
                }
            }
            else
            {
                TKey keyVal = key[level];

                if (null == keyDict)
                {
                    keyDict = new Dictionary<TKey, InvertedSearcher<TKey, TValue>>();
                }

                InvertedSearcher<TKey, TValue> dictionaryVal = null;
                if (!keyDict.TryGetValue(keyVal, out dictionaryVal))
                {
                    dictionaryVal = new InvertedSearcher<TKey, TValue>(level + 1, this);
                    keyDict.Add(keyVal, dictionaryVal);
                }
                
                ret = dictionaryVal.Set(key, val);
            }

            return ret;
        }

        public bool Get(IList<TKey> key, out TValue val)
        {
            Dictionary<TKey, InvertedSearcher<TKey, TValue>> tmp = null;
            return Get(key, out val, out tmp);
        }

        public bool Get(IList<TKey> key, out TValue val, out Dictionary<TKey, InvertedSearcher<TKey, TValue>> dictionaryPaths)
        {
            bool ret = false;
            val = default(TValue);
            dictionaryPaths = null;

            if (level == (key.Count))
            {
                val = valueItem;
                dictionaryPaths = keyDict;
                ret = true;
            }
            else
            {
                TKey keyVal = key[level];

                if (null != keyDict)
                {
                    InvertedSearcher<TKey, TValue> dictionaryVal = null;
                    if (keyDict.TryGetValue(keyVal, out dictionaryVal))
                    {
                        ret = dictionaryVal.Get(key, out val, out dictionaryPaths);
                    }
                }
            }

            return ret;
        }


        public bool FindFirst(TKey keyVal, out TValue val)
        {
            bool ret = false;
            val = default(TValue);

            if (null != keyDict)
            {
                InvertedSearcher<TKey, TValue> dictionaryVal = null;
                if (keyDict.TryGetValue(keyVal, out dictionaryVal))
                {
                    val = dictionaryVal.valueItem;
                    ret = true;
                }
                else
                {
                    foreach (KeyValuePair<TKey, InvertedSearcher<TKey, TValue>> item in keyDict)
                    {
                        ret = item.Value.FindFirst(keyVal, out val);
                        if (ret) break;
                    }
                }

            }

            return ret;
        }


        public void GetAllEndValues(ICollection<TValue> valList)
        {
            if (null != valList)
            {
                if (null == keyDict)
                {
                    if (!valList.Contains(valueItem))
                    {
                        valList.Add(valueItem);
                    }
                }
                else
                {
                    foreach (KeyValuePair<TKey, InvertedSearcher<TKey, TValue>> item in keyDict)
                    {
                        item.Value.GetAllEndValues(valList);
                    }
                }
            }
        }


        /// <summary>
        /// Закрытый конструктор класса - для внутренней рекурсии
        /// </summary>
        /// <param name="level">уровень вложенности элемента в дереве поиска</param>
        /// <param name="par">ссылка на родительский элемент</param>
        private InvertedSearcher(int level, InvertedSearcher<TKey, TValue> par)
        {
            Init(level, par);
        }

        /// <summary>
        /// Инициализатор полей класса
        /// </summary>
        private void Init(int level, InvertedSearcher<TKey, TValue> par)
        {
            this.level = level;
            keyDict = null;
            valueItem = default(TValue);
            Parent = par;
        }

        private TValue valueItem;
        private Dictionary<TKey, InvertedSearcher<TKey, TValue>> keyDict;
        private int level;
        private InvertedSearcher<TKey, TValue> Parent;
    }
}
