using System.Collections.Generic;

namespace Tools
{
    public static class ListTools
    {
        public static List<T> RandomizeList<T>(List<T> list)
        {
            var tempList = list;

            List<T> randomizedList = new List<T>();

            while(tempList.Count > 0)
            {
                int randomIndex = UnityEngine.Random.Range(0, tempList.Count);

                T sortedItem = tempList[randomIndex];

                tempList.Remove(sortedItem);

                randomizedList.Add(sortedItem);
            }

            return randomizedList;
        }
        public static T GetRandomEntryFromList<T>(List<T> list)
        {
            int randomIndex = UnityEngine.Random.Range(0, list.Count);

            return list[randomIndex];
        }
    }

}

