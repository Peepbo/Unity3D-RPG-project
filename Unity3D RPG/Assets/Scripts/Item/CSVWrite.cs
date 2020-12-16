using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CSVWrite
{
    public static class Write
    {
       public static void Save(int money, string itemName, string ability, string filePath)
        {
            string _money = money.ToString();
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@filePath,false))
            {
                file.Write(_money + "," + itemName + "," + ability);
            }
        }
        
    }
}
