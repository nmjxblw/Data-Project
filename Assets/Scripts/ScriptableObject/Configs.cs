using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Configs")]
public class Configs : ScriptableObject
{
    public List<DataConfig> dataConfigs;
    [ContextMenu("Sort")]
    public void Sort() => dataConfigs?.Sort();
}
