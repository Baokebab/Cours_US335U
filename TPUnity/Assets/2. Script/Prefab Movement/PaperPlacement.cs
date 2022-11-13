using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperPlacement : MonoBehaviour
{

    [SerializeField] PaperPlaced[] _placementList;
    public static HashSet<PaperMovement> PaperPlacedSet = new HashSet<PaperMovement>();
    public static bool _paperFullyPlaced = false;
    [SerializeField] Player _player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!_paperFullyPlaced) CheckPlacement();
    }

    void CheckPlacement()
    {
        for(int i = 0; i < _placementList.Length; i++)
        {
            if (!_placementList[i].paperPlaced) return;
        }
        _paperFullyPlaced = true;
        Game_Manager.PartGame++;
        foreach(PaperMovement paper in PaperPlacedSet)
        {
            if(paper != null)
            {
                paper.rb.constraints = RigidbodyConstraints.FreezeAll;
                paper.rb.useGravity = false;
                paper.BoxCollider.enabled = false;
            }
        }
        _player.YouCanStartVoice();
    }
}
