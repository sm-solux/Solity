using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPoint3 : MonoBehaviour
{
    public static EventPoint3 instance;

    #region Singleton
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
    }
    #endregion Singleton

    [SerializeField]
    public Dialogue dialogue1;
    public Dialogue dialogue2;
    public Dialogue dialogue3;
    public Dialogue dialogue4;

    public GameObject TCarea; //Ÿ��ĸ�� ��ġ

    private DialogueManager theDM;
    private OrderManager theOrder;
    private TimeCapsuleManager theTCM;

    private WaitForSeconds waitTime = new WaitForSeconds(0.5f);

    private bool eventOn = true; //true�� �� �̺�Ʈ ���� ����

    void Start()
    {
        theOrder = FindObjectOfType<OrderManager>();
        theTCM = FindObjectOfType<TimeCapsuleManager>();
        theDM = FindObjectOfType<DialogueManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(eventOn && Input.GetKey(KeyCode.LeftArrow))
        {
            StartCoroutine(StartEvent3());
        }
    }

    IEnumerator StartEvent3()
    {
        theOrder.NotMove();
        yield return waitTime;

        theOrder.TurnLeft();
        theDM.ShowDialogue(dialogue1);
        yield return new WaitUntil(() => !theDM.talking);

        theOrder.Left(1.0f);
        yield return new WaitUntil(() => !theOrder.doEvent);

        theOrder.Up(0.5f);
        yield return new WaitUntil(() => !theOrder.doEvent);

        theDM.ShowDialogue(dialogue2);
        yield return new WaitUntil(() => !theDM.talking);

        theOrder.TurnLeft();
        yield return waitTime;
        theOrder.TurnRight();
        yield return waitTime;
        theOrder.TurnUp();

        yield return new WaitForSeconds(2f);
        TCarea.SetActive(true);
        yield return waitTime;

        theDM.ShowDialogue(dialogue3);
        yield return new WaitUntil(() => !theDM.talking);

        yield return waitTime;
        theTCM.ShowTimeCapsule();
        yield return new WaitUntil(() => !theTCM.letter);

        yield return waitTime;
        theDM.ShowDialogue(dialogue4);
        yield return new WaitUntil(() => !theDM.talking);

        theOrder.Left(2f);
        yield return new WaitUntil(() => !theOrder.doEvent);

        eventOn = false;
        theOrder.Move();
    }
}