using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.UI;

public class PlayerPushPower : MonoBehaviour
{
    public Button pushButton;
    public Image pushCDImage;
    public float CoolDownTime;
    public bool pushOnCD;
    public GameObject[] tubes;
    public float maxPushSize;
    public float pushGrowthSpeed;

    private void Awake()
    {
        pushButton.onClick.AddListener(CallPush);
    }

    void CallPush()
    {
        pushOnCD = true;
        StartCoroutine(ReloadPush());
        foreach (GameObject tube in tubes)
        {
            StartCoroutine(PushingSkill(tube));
        }
    }

    IEnumerator PushingSkill(GameObject tube)
    {
        var startScale = tube.transform.localScale;
        float scaleModifier = 1;
        float startValue = scaleModifier;
        float elapsedTime = 0;
        while (elapsedTime < pushGrowthSpeed)
        {
            scaleModifier = Mathf.Lerp(startValue, maxPushSize, elapsedTime / pushGrowthSpeed);
            tube.transform.localScale = startScale * scaleModifier;
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }
        tube.transform.localScale = startScale;
        tube.transform.localScale = new Vector3(1, 1, 1);
        StopCoroutine(PushingSkill(tube));
    }

    IEnumerator ReloadPush()
    {
        pushCDImage.gameObject.SetActive(true);
        pushCDImage.fillAmount = 1;
        float reloadingTime = CoolDownTime;
        while (reloadingTime > 0)
        {
            reloadingTime -= Time.deltaTime;
            pushCDImage.fillAmount = reloadingTime  / CoolDownTime; //fuck math
            yield return null;
        }
        pushCDImage.fillAmount = 1;
        pushCDImage.gameObject.SetActive(false);
        StopCoroutine(ReloadPush());
    }
}
