using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

partial class PlayerData
{
    #region Equip
    public ItemInfo EquipWeapon()
    {

        if (myEquipment[1] >= 0 && currentWeapon != myEquipment[1])
        {
            currentWeapon = myEquipment[1];
            myWeapon = CSVData.Instance.find(myEquipment[1]);
        }
        return myWeapon;
    }

    public ItemInfo EquipArmor()
    {

        if (myEquipment[2] >= 0 && currentArmor != myEquipment[2])
        {
            currentArmor = myEquipment[2];
            myArmor = CSVData.Instance.find(myEquipment[2]);
        }
        return myArmor;
    }

    public ItemInfo EquipAccessory()
    {

        if (myEquipment[3] >= 0 && currentAccessory != myEquipment[3])
        {
            currentAccessory = myEquipment[3];
            myAccessory = CSVData.Instance.find(myEquipment[3]);
        }
        return myAccessory;
    }
    public ItemInfo EquipBossItem()
    {

        if (myEquipment[4] >= 0 && currentBossItem != myEquipment[4])
        {
            currentBossItem = myEquipment[4];
            myBossItem = CSVData.Instance.find(myEquipment[4]);
        }
        return myBossItem;
    }
    #endregion
}