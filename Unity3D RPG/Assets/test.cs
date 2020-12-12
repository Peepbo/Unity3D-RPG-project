using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : Singleton<test>
{
    protected test () {}

    public string myGlobalVar = "whatever";
}