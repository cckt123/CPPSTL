using System;
using System.Collections.Generic;

namespace MySTL
{
    class MyHashTable<T1>
    {
        /*
        > 手撕过程可以分为以下步骤
        > 1. 把插入的对象转为int类型，制作并调用哈希函数
        > 2. 遍历index位置处的链表，确定key在不在元素中
        > 3. 把key装进结点中，并插入到对应的链表中
        > 4. 维护元素的个数
        > 5. …通过维护负载因子，进而维护较低的负载因子
        */

        private int size = 0;

        public int Size 
        {
            get => size;
        }

        private List<T1>[] array = new List<T1>[11];

        public bool insert(T1 key) 
        {
            int HashValue = key.GetHashCode();
            //获取哈希码

            int index = HashValue % array.Length;
            //转换为合法的下标

            List<T1> CurrentList = array[index];
            if (CurrentList != null)
            {
                if (CurrentList.Contains(key)) return false;
                CurrentList.Add(key);
            }
            else 
            {
                CurrentList = new List<T1>();
                CurrentList.Add(key);
            }

            size++;

            if (size / array.Length * 100 > 75) 
            {
                //处理维护负载因子
                dilatation();
            }

            return true;
        }

        public bool remove(T1 key) 
        {
            int HashValue = key.GetHashCode();
            int index = HashValue % array.Length;
            List<T1> Current = array[index];
            if (Current != null) 
            {
                Current.Remove(key);
                size--;
                return true;
            }
            return false;
        }

        public bool contains(T1 key) 
        {
            int HashValue = key.GetHashCode();
            int index = HashValue % array.Length;
            List<T1> Current = array[index];
            if (Current != null)
            {
                if(Current.Contains(key))
                    return true;
            }
            return false;
        }

        public void dilatation() 
        {
            List<T1>[] newArray = new List<T1>[array.Length*2];
            for (int i = 0; i < array.Length; i++) 
            {
                List<T1> CurrentList = array[i];
                for(int z = 0;z<CurrentList.Count;z++) 
                {
                    T1 key = CurrentList[z];
                    int HashValue = key.GetHashCode();
                    int index = HashValue % newArray.Length;
                    if (newArray[index] == null) newArray[index] = new List<T1>();
                    newArray[index].Add(key);
                }
            }
            array = newArray;
        }
    }
}
