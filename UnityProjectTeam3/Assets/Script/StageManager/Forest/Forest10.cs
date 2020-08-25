using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest10 : MonoBehaviour
{
    public PlayerCtrl mPlayerCtrl;
    public ButtonEvent mButton;
    [SerializeField]
    private GameObject MovePlatform1;
    [SerializeField]
    private GameObject MovePlatform2;
    bool isButtonDown;
    [SerializeField]
    float speed1 = 1.0f;
    [SerializeField]
    float speed2 = 1.0f;

    //platform 1 이동위치
    private Vector3 pos1 = new Vector3(-2f, -2f, 0f);
    private Vector3 pos2 = new Vector3(-2f, 2f, 0f);

    //platform 2 이동위치
    private Vector3 pos3 = new Vector3(0f, 2f, 0f);
    private Vector3 pos4 = new Vector3(4f, 2f, 0f);

    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        mPlayerCtrl.forestTen(true);
    }

    // Update is called once per frame
    void Update()
    {
        //Button이 눌렸다면
        if (mButton.buttonTriggerd)
        {
            MovePlatform1.transform.position = Vector3.Lerp(pos2, pos1, (Mathf.Sin(speed1 * Time.time) + 1.0f) / 2.0f);
            MovePlatform2.transform.position = Vector3.Lerp(pos3, pos4, (Mathf.Sin(speed2 * Time.time) + 1.0f) / 2.0f);
        }
    }
    
}
