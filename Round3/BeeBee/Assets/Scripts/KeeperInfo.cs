using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KeeperInfo
{

    [SerializeField] float x;
    [SerializeField] float y;
    [SerializeField] float z;
    public float Move;
    public bool Attack;
    public bool Hurt;
    public bool Die;
    public float health;

    public bool Ready;
    public bool Roll;

    public float moveInputX;
    public Vector3 getPosition(){
        return new Vector3(this.x,this.y,this.z);
    }

    public static KeeperInfo CreateFromJSON(string jsonString){
        return JsonUtility.FromJson<KeeperInfo>(jsonString);
    }
}
