using System;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

enum ABNORMAL
{
    NONE=0,
    INETENSITY,
    GHOSTLIGHT,

}


public class PlayerStress : MonoBehaviour
{
    [SerializeField]private IntReactiveProperty nowStressLevel = new IntReactiveProperty();

    [SerializeField]private FloatReactiveProperty stress;

    [SerializeField]private Volume volume;
    private Vignette vignette;

    [SerializeField] Image bar;

    float downStress=0.1f;

    // Start is called before the first frame update
    void Start()
    {
        bar = GameObject.Find("StressBar").GetComponent<Image>();
        volume = GameObject.Find("Global Volume").GetComponent<Volume>();
        stress.Value = 0;
        nowStressLevel.Value = 0;

        GameManager.Instance.SetGhostLightList();


        // 스트레스 값이 변하면 다음 문장 호출
        stress.DistinctUntilChanged()
            // 스트레스 값 0~100 사이에서 게이지 값 변경해주기
            .Do(val => { val = Mathf.Clamp(val, 0, 100); bar.fillAmount = val / 100; })
            // 스트레스 값이 30 넘으면 스트레스레벨 1로 변경
            .Where(x => x > 30f).Do(_ => nowStressLevel.Value = 1)
            // 스트레스 값이 70 넘으면 스트레스레벨 2로 변경
            .Where(x => x > 70f).Do(_ => nowStressLevel.Value = 2).Subscribe();

        // 스트레스 레벨이 바뀌면 다음 문장 호출(중복값 인정x)
        nowStressLevel.Distinct().Subscribe(val => SetStressLevel(val));


        // 스트레스 수치 서서히 감소
        Observable.Timer(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(0.1f))
            .Select(_ =>  stress.Value=Mathf.Clamp(stress.Value - downStress,0,100)).Subscribe();
    }

    public void GetStress(float amount)
    {
        stress.Value = Mathf.Clamp(stress.Value + amount, 0, 100);
    }

    private void SetStressLevel(int level)
    {
        AddAbnormal((ABNORMAL)level);
    }

    private void AddAbnormal(ABNORMAL situation)
    {
        switch (situation)
        {
            case ABNORMAL.INETENSITY:
                if(volume.profile.TryGet<Vignette>(out vignette))
                {
                    IntensityChange(vignette);

                }
                break;
            case ABNORMAL.GHOSTLIGHT:
                // 불끄기 -> 라이트 목록 다른곳에 두고 호출만 하기
                GameManager.Instance.OnGhostLight(true);
                break;

        }
    }

    /// <summary>
    /// 서서히 시야 감소
    /// </summary>
    /// <param name="vignette"></param>
    private void IntensityChange(Vignette vignette)
    {
        Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(0.1f))
            .Do(_ =>
            {
                vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, 0.7f, Time.deltaTime * 5f);

            }).Subscribe().AddTo(this);
    }

    public float GetStressValue()
    {
        return stress.Value;
    }
}
