using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class TimeManager : MonoBehaviour
{
    [Button]
   public void SetTimeScale(float scale)
   {
        Time.timeScale = scale;
   }
}
