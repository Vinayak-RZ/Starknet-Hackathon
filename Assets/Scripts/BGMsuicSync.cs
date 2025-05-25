using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMsuicSync : MonoBehaviour
{
    private void Awake()
    {
        GameObject[] musicObj = GameObject.FindGameObjectsWithTag("BGMusic");
        if(musicObj.Length > 1)
        {
            for(int i = 0; i < musicObj.Length; i++)
            {
                if (musicObj[i] != this.gameObject)
                {
                    Destroy(musicObj[i]);
                }
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
