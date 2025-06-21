using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level1Manager : MonoBehaviour
{
    public static Level1Manager instance;
    public int TaskIndex
    {
        get
        {
            return taskIndex;
        }
    }
    int taskIndex;
    public Text tips;
    public TriggerCounter step1Counter;
    public RotationTracker rotationTracker;
    public AngleTracker angleTracker;
    bool grab4;
    public TriggerCounter step5Counter;
    public WaterInteractor chahu;
    public WaterInteractable chabei;
    public TriggerCounter step7Counter;
    public Transform suichabang;
    public Transform lid;
    public Transform spoon;
    public Transform brush;
    public Transform saucer;
    bool step8;

    public UnityEngine.UI.Text timeText;
    public UnityEngine.UI.Text scoreText;
    public Button restart;
    public Button next;
    [HideInInspector] public float timer;
    public bool isStart;
    public void Grab4()
    {
        if(taskIndex==4)
            grab4 = true;
    }
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        StartCoroutine(Task());
        restart.onClick.AddListener(delegate { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); });
        restart.gameObject.SetActive(false);

        next.onClick.AddListener(delegate { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1); });
        next.gameObject.SetActive(false);
    }
    void Update()
    {
        if (isStart)
        {
            timer += Time.deltaTime;
            timeText.text = "��ǰ��ʱ��" + timer.ToString("F2") + "��";
            if (timer >= 300)
            {
                isStart = false;
                StopAllCoroutines();
                tips.text = "ʱ��ľ���";
                restart.gameObject.SetActive(true);
            }
        }

    }
    IEnumerator Task()
    {
        isStart = true;
        taskIndex = 1;
        tips.text = "��һ����ʹ�����������Ҷ";
        step1Counter.GetComponent<MySpace.Outline>().enabled = true;
        suichabang.GetComponent<MySpace.Outline>().enabled = true;
        while (!step1Counter.isFinish)
        {
            yield return null;
        }
        step1Counter.GetComponent<MySpace.Outline>().enabled = false;
        suichabang.GetComponent<MySpace.Outline>().enabled = false;

        taskIndex = 2;
        tips.text = "�ڶ�������ת�����";
        rotationTracker.GetComponent<MySpace.Outline>().enabled = true;
        while (!rotationTracker.isFinish)
        {
            yield return null;
        }
        rotationTracker.GetComponent<MySpace.Outline>().enabled = false;

        taskIndex = 3;
        tips.text = "���������ζ�����";
        angleTracker.GetComponent<MySpace.Outline>().enabled = true;
        while (!angleTracker.IsCountMatched())
        {
            yield return null;
        }
        angleTracker.GetComponent<MySpace.Outline>().enabled = false;

        taskIndex = 4;
        tips.text = "���Ĳ���ȡ�¸��ӷ����ĩ";
        lid.GetComponent<MySpace.Outline>().enabled = true;
        while (!grab4)
        {
            yield return null;
        }
        lid.GetComponent<MySpace.Outline>().enabled = false;

        taskIndex = 5;
        tips.text = "���岽����ȡ���ӷ��뱭��";
        step5Counter.GetComponent<MySpace.Outline>().enabled = true;
        spoon.GetComponent<MySpace.Outline>().enabled = true;
        while (!step5Counter.isFinish)
        {
            yield return null;
        }
        step5Counter.GetComponent<MySpace.Outline>().enabled = false;
        spoon.GetComponent<MySpace.Outline>().enabled = false;

        taskIndex = 6;
        tips.text = "��������������е�ˮ���뱭��";
        chabei.GetComponent<MySpace.Outline>().enabled = true;
        chahu.GetComponent<MySpace.Outline>().enabled = true;
        yield return Water(chahu,chabei);
        chabei.GetComponent<MySpace.Outline>().enabled = false;
        chahu.GetComponent<MySpace.Outline>().enabled = false;

        taskIndex = 7;
        tips.text = "���߲�����ȡëˢ�����ĩ";
        chabei.GetComponent<MySpace.Outline>().enabled = true;
        brush.GetComponent<MySpace.Outline>().enabled = true;
        while (!step7Counter.isFinish)
        {
            yield return null;
        }
        chabei.GetComponent<MySpace.Outline>().enabled = false;
        brush.GetComponent<MySpace.Outline>().enabled = false;

        taskIndex = 8;
        tips.text = "�ڰ˲�������������ڲ�����";
        chahu.GetComponent<MySpace.Outline>().enabled = true;
        saucer.GetComponent<MySpace.Outline>().enabled = true;
        while (!step8)
        {
            yield return null;
        }
        chahu.GetComponent<MySpace.Outline>().enabled = false;
        saucer.GetComponent<MySpace.Outline>().enabled = false;

        isStart = false;
        taskIndex = 9;
        tips.text = "��ϲ���Ѿ�������в��裡";
        next.gameObject.SetActive(true);
        scoreText.text = "��ǰ�÷֣�" + CalculateScore();
    }
    IEnumerator Water(WaterInteractor interactor, WaterInteractable interactable)
    {
        interactor.canWater = true;

            interactable.targetName = interactor.ItemName;
        interactable.IsWatered = false;
        

        while (!DetectIsWatered(interactable))
        {

            yield return null;
        }


            interactable.targetName = string.Empty;
            interactable.IsWatered = false;
        
        yield return new WaitForSeconds(2f);
        interactor.canWater = false;
    }


    public bool DetectIsWatered(WaterInteractable interactable)
    {
            if (!interactable.IsWatered)
                return false;
        return true;
    }

    public void Step8()
    {
        if(taskIndex==8)
        step8 = true;
    }
    private float CalculateScore()
    {
        float s = 180 - timer;
        if (s >= 60)
        {
            return 100;
        }
        else
        {
            return 40 / 60 * s + 60;
        }
    }
}
