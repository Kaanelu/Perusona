using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// map詳細クラス
/// </summary>
public class MapDetails
{


    /// <summary>
    /// バトルフィールド
    /// </summary>
    private Vector3 battleFieldPosition_ = Vector3.zero;
    public Vector3 GetBattleFieldPosition()
    {
        return battleFieldPosition_;
    }

    /// <summary>
    /// バトルフィールド前伊
    /// </summary>
    private static readonly string battleObjName_ = "BattleField";

    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="mapOnj"></param>
    public bool Initialize(GameObject mapOnj)
    {
        bool res;

        if(mapOnj == null)
        {
            return false;
        }


        //子要素全取得
        var chiled = mapOnj.GetComponentsInChildren<Transform>();

        //バトルフィールドオブジェクト
        GameObject battleField = null;
        foreach(var ch in chiled)
        {
            if(ch.gameObject.name == battleObjName_)
            {
                battleField = ch.gameObject;
                break;
            }
        }

        if(battleField == null)
        {
            return false;
        }

        //バトルフィールドオブジェから座標だけ取得
        battleFieldPosition_ = battleField.transform.position;

        res = true;
        return res;
    }
}
