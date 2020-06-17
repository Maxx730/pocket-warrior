using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    public static string IntToSixString(int amount) {
        int length = amount.ToString().Length;
        string finalString = "";
        for(int i = 0;i < 6 - amount.ToString().Length;i++) {
            finalString += "0";
        }
        return finalString + amount.ToString();
    }
}
