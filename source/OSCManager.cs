using UnityEngine;

public class OSCManager : MonoBehaviour
{
    OSC _osc;

    [SerializeField]
    PerObjectColor[] groups;


    public struct GroupState
    {
       public float fades;
       public float bpms;
       public int rmodes;
       public int cmodes;
    }

    public GroupState[] elements;

    private void Start()
    {
        _osc = GetComponent<OSC>();

        for (int i = 0; i < groups.Length; i++)
        {
            int index = i;
            _osc.SetAddressHandler($"/bpm/{index+1}", msg => groups[index].SetBpm(msg.GetFloat(0)));
            _osc.SetAddressHandler($"/fade/{index+1}", msg => groups[index].SetFade(msg.GetFloat(0)));
            _osc.SetAddressHandler($"/rmode/{index+1}", msg => groups[index].SetRMode(msg.GetInt(0)));
            _osc.SetAddressHandler($"/cmode/{index+1}", msg => groups[index].SetCMode(msg.GetInt(0)));
        
        }
    }
    
}
