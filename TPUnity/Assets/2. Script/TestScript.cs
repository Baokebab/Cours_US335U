using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Script pour faire des tests spécifiques
public class TestScript : MonoBehaviour
{
    public float timer;
    [SerializeField] Material[] _listmaterial;
    [SerializeField] SkinnedMeshRenderer skin;
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        skin = transform.GetChild(6).GetComponent<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            timer = 2;
            skin.material = _listmaterial[i++];
        }
    }


}
