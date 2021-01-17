using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDeActive : MonoBehaviour
{
    public void DeActive() { transform.parent.gameObject.SetActive(false); }
}
