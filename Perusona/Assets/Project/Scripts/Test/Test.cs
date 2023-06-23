using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    AddressableData<GameObject> data;
    bool testFlag_ = false;
    // Start is called before the first frame update
    void Start()
    {
        data = new AddressableData<GameObject>();
        data.OnAutoRelease();
        data.LoadStart("Assets/Project/Assets/Map/9001.prefab");
    }

    // Update is called once per frame
    void Update()
    {
        if (data != null)
        {
            if (data.GetFlagSetUpLoading())
            {
                if (testFlag_ == false)
                {
                    GameObject.Instantiate(data.GetAddressableData());
                    testFlag_ = true;
                }
            }
        }
    }
}
