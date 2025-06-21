using MySpace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SystemManager : MonoBehaviour
{
    public int currentStep=0;
    public Transform Tilianghu;
    public Transform ZhuPaoQi;
    public Transform PinBei1;
    public Transform PinBei2;
    public Transform PinBei3;
    public Transform JianShui;
    public Transform GongDaoBei;
    public Transform chabo;
    public Transform chayeguan;
    public Transform chaze;

    public bool step1Finished;
    public bool step6Finished;
    public List<WaterInteractable> interactables = new List<WaterInteractable>();

    public static SystemManager instance;
    public Text text;
    public Text timeText;
    public Text scoreText;
    public Button restart;
    [HideInInspector]public float timer;
    public bool isStart;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        StartCoroutine(Step1());
        restart.onClick.AddListener(delegate { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); });
        restart.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(isStart)
        {
            timer += Time.deltaTime;
            timeText.text = "当前用时：" + timer.ToString("F2")+"秒";
            if(timer>=300)
            {
                isStart = false;
                StopAllCoroutines();
                text.text = "时间耗尽！";
                restart.gameObject.SetActive(true);
            }    
        }

    }
    IEnumerator Step1()
    {
        isStart = true;
        text.text = "第一步，主客行礼，备茶:请抓起茶拨，将茶叶罐中的茶叶拨出";
        ////currentStep++;
        chabo.GetComponent<MySpace.Outline>().enabled = true;
        chayeguan.GetComponent<MySpace.Outline>().enabled = true;
        while (!step1Finished)
        {
            yield return null;
        }
        chabo.GetComponent<MySpace.Outline>().enabled = false;
        chayeguan.GetComponent<MySpace.Outline>().enabled = false;


        text.text = "第二步，温器:请拿起梁壶，将水倒在主泡器中";
        interactables.Add(ZhuPaoQi.GetComponent<WaterInteractable>());
        ZhuPaoQi.GetComponent<MySpace.Outline>().enabled = true;
        Tilianghu.GetComponent<MySpace.Outline>().enabled = true;
        yield return Water(Tilianghu.GetComponent<WaterInteractor>(), interactables);
        ZhuPaoQi.GetComponent<MySpace.Outline>().enabled = false;
        Tilianghu.GetComponent<MySpace.Outline>().enabled = false;

        text.text = "第三步，请拿起主泡器，将水倒入公道杯中";
        GongDaoBei.GetComponent<MySpace.Outline>().enabled = true;
        ZhuPaoQi.GetComponent<MySpace.Outline>().enabled = true;
        interactables.Add(GongDaoBei.GetComponent<WaterInteractable>());
        yield return Water(ZhuPaoQi.GetComponent<WaterInteractor>(), interactables);
        ZhuPaoQi.GetComponent<MySpace.Outline>().enabled = false;
        GongDaoBei.GetComponent<MySpace.Outline>().enabled = false;

        text.text = "第四步，温杯:拿起公道杯，将公道杯中的水倒入品杯";
        GongDaoBei.GetComponent<MySpace.Outline>().enabled = true;
        PinBei1.GetComponent<MySpace.Outline>().enabled = true;
        PinBei2.GetComponent<MySpace.Outline>().enabled = true;
        PinBei3.GetComponent<MySpace.Outline>().enabled = true;
        interactables.Add(PinBei1.GetComponent<WaterInteractable>());
        interactables.Add(PinBei2.GetComponent<WaterInteractable>());
        interactables.Add(PinBei3.GetComponent<WaterInteractable>());
        yield return Water(GongDaoBei.GetComponent<WaterInteractor>(), interactables);
        GongDaoBei.GetComponent<MySpace.Outline>().enabled = false;
        PinBei1.GetComponent<MySpace.Outline>().enabled = false;
        PinBei2.GetComponent<MySpace.Outline>().enabled = false;
        PinBei3.GetComponent<MySpace.Outline>().enabled = false;

        text.text = "第五步，请拿起品杯，将品杯中的水倒入建水再放回原处";
        PinBei1.GetComponent<MySpace.Outline>().enabled = true;
        JianShui.GetComponent<MySpace.Outline>().enabled = true;
        interactables.Add(JianShui.GetComponent<WaterInteractable>());
        yield return Water(PinBei1.GetComponent<WaterInteractor>(), interactables);
        PinBei1.GetComponent<MySpace.Outline>().enabled = false;
        JianShui.GetComponent<MySpace.Outline>().enabled = false;

        text.text = "第六步，泡茶:请拿起茶拨，将茶叶拨进主泡器";
        chabo.GetComponent<MySpace.Outline>().enabled = true;
        chaze.GetComponent<MySpace.Outline>().enabled = true;
        currentStep = 6;
        while (!step6Finished)
            yield return null;
        chabo.GetComponent<MySpace.Outline>().enabled = false;
        chaze.GetComponent<MySpace.Outline>().enabled = false;

        text.text = "第七步，请拿起提梁壶，把水倒入主泡器，再放回原处";
        Tilianghu.GetComponent<MySpace.Outline>().enabled = true;
        ZhuPaoQi.GetComponent<MySpace.Outline>().enabled = true;
        interactables.Add(ZhuPaoQi.GetComponent<WaterInteractable>());
        yield return Water(Tilianghu.GetComponent<WaterInteractor>(), interactables);
        Tilianghu.GetComponent<MySpace.Outline>().enabled = false;
        ZhuPaoQi.GetComponent<MySpace.Outline>().enabled = false;

        text.text = "第八步，请拿起主泡器，将茶水倒入公道杯中，再放回原处";
        GongDaoBei.GetComponent<MySpace.Outline>().enabled = true;
        ZhuPaoQi.GetComponent<MySpace.Outline>().enabled = true;
        interactables.Add(GongDaoBei.GetComponent<WaterInteractable>());
        yield return Water(ZhuPaoQi.GetComponent<WaterInteractor>(), interactables);
        GongDaoBei.GetComponent<MySpace.Outline>().enabled = false;
        ZhuPaoQi.GetComponent<MySpace.Outline>().enabled = false;

        text.text = "第九步，分茶:请拿起公道杯，将茶水均分倒人品茗杯中";
        GongDaoBei.GetComponent<MySpace.Outline>().enabled = true;
        PinBei1.GetComponent<MySpace.Outline>().enabled = true;
        PinBei2.GetComponent<MySpace.Outline>().enabled = true;
        PinBei3.GetComponent<MySpace.Outline>().enabled = true;
        interactables.Add(PinBei1.GetComponent<WaterInteractable>());
        interactables.Add(PinBei2.GetComponent<WaterInteractable>());
        interactables.Add(PinBei3.GetComponent<WaterInteractable>());
        yield return Water(GongDaoBei.GetComponent<WaterInteractor>(), interactables);
        GongDaoBei.GetComponent<MySpace.Outline>().enabled = false;
        PinBei1.GetComponent<MySpace.Outline>().enabled = false;
        PinBei2.GetComponent<MySpace.Outline>().enabled = false;
        PinBei3.GetComponent<MySpace.Outline>().enabled = false;

        text.text = "恭喜完成行茶十式泡茶流程!";
        isStart = false;
        scoreText.text = "当前得分：" + CalculateScore(); 
        restart.gameObject.SetActive(true);
    }
    private float CalculateScore()
    {
        float s = 300 - timer;
        if(s>=60)
        {
            return 100;
        }
        else
        {
            return 40 / 60 * s+60;
        }
    }
     IEnumerator Water(WaterInteractor interactor,List<WaterInteractable>  interactables)
    {
        interactor.canWater = true;
        foreach (var i in interactables)
        {
            i.targetName = interactor.ItemName;
            i.IsWatered = false;
        }

        while(!DetectIsWatered(interactables))
        {

            yield return null;
        }

        foreach (var i in interactables)
        {
            i.targetName = string.Empty;
            i.IsWatered = false;
        }
        interactables.Clear();
        yield return new WaitForSeconds(2f);
        interactor.canWater = false;
    }


    public bool DetectIsWatered(List<WaterInteractable> interactables)
    {
        foreach(WaterInteractable i in interactables)
            if(!i.IsWatered)
                return false;
        return true;
    }
}
