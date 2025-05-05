using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class BeeInfo
{
    [SerializeField] float x;
    [SerializeField] float y;
    [SerializeField] float z;
    public bool Attack;
    public float health;
    public bool Ready;    

    public Vector3 getPosition(){
        return new Vector3(this.x,this.y,this.z);
    }

    public static BeeInfo CreateFromJSON(string jsonString){
        return JsonUtility.FromJson<BeeInfo>(jsonString);
    }
}
