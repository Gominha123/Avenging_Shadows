using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipInvCntrl : MonoBehaviour
{
    public EquipedInv equipedInv;

    public void DiscardEquiped()
    {
        equipedInv.RemoveEquiped();
    }
}
