using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadableComponent : MonoBehaviour
{
    public void Reload()
    {
        var reloadables = GetComponents<IReloadable>();
        foreach (var reloadable in reloadables)
        {
            reloadable.Reload();
        }
    }
}
