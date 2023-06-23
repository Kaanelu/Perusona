using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
#if UNITY_EDITOR

/// <summary>
/// AddressableDataCore‚ÌŠg’£
/// </summary>
[CustomEditor(typeof(AddressableDataCore))]
public class AddressableCoreInformation : Editor
{
    public override void OnInspectorGUI()
    {
        OnGUI();
    }
    public static void OnGUI()
    {
        var addressable_data_container =  AddressableDataCore.Instance.GetAddressableDataContainer();
        if (addressable_data_container == null) return;

        for(uint count = 0; count < addressable_data_container.GetListAddressableDataCount(); count++)
        {
            var addressable_data = addressable_data_container.Find(count);
            OnAddressableDataGUI(addressable_data); 
        }
    }
    public static void OnAddressableDataGUI(BaseAddressableData data)
    {
        if (data == null) return;

        GUILayout.BeginHorizontal();

        if (data.GetBaseAddressableData() != null)
        {
            GUILayout.Label(data.GetBaseAddressableData().name);
        }
        else if (data.GetBaseArrayAddressableData() != null)
        {
            for (uint count = 0; count < data.GetArrayAddressableDataCount(); count++)
            {
                var data_array = data.GetBaseArrayAddressableData((int)count);
                if (data_array == null) continue;
                GUILayout.BeginHorizontal();
                GUILayout.Label(data_array.name);
                GUILayout.EndHorizontal();
            }
        }

        GUILayout.EndHorizontal();
    }
}
#endif
