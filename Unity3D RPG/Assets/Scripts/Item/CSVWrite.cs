using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CSVWrite
{
    public static class Write
    {
       public static void PlayerSave(int money, string weaponEquip, string armourEquip, string acceEquip, List<ItemInfo> storage, List<string> ability, string filePath)
        {
            string _money = money.ToString();
            string storageStr="";
            string abilityStr="";
            for(int i=0; i<storage.Count; i++)
            {
                storageStr += storage[i].itemName + ','+ storage[i].count;
                if (i != storage.Count - 1)
                    storageStr += ',';
            }
               
            for(int i=0; i<ability.Count;i++)
            {
                if (i != (ability.Count - 1))
                    abilityStr += ability[i] + ',';
                else
                    abilityStr += ability[i];
            }
            
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@filePath,false))
            {
                
                file.WriteLine(_money + ',' + weaponEquip + ',' + armourEquip + ','+ acceEquip);
                file.WriteLine(storageStr);
                file.Write(abilityStr);
            }
        }
        
    }
}
