
using UniRx;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

enum ABNORMAL
{
    NONE,
    INETENSITY,
    GHOSTLIGHT,

}


public class PlayerStress : MonoBehaviour
{
    //[SerializeField] private float stress;


    [SerializeField]private IntReactiveProperty nowStressLevel = new IntReactiveProperty();


    // 위치 이동 필요
    [SerializeField] private Light[] stressLight;

    // UI 표현 방식 확정 필요
    public Image slider;

    [SerializeField]private FloatReactiveProperty stress;


    [SerializeField]private Volume volume;
    private Vignette vignette;

    // Start is called before the first frame update
    void Start()
    {
        nowStressLevel.Value = 0;
        stress.DistinctUntilChanged().Select(val => slider.fillAmount = val/100)
            .Where(x => x > 0.3f).Select(_ => nowStressLevel.Value = 1)
            .Where(x => x > 0.7f).Select(_ => nowStressLevel.Value = 2).Subscribe();
        nowStressLevel.Distinct().Subscribe(val => SetStressLevel(val));

    }

    public void GetStress(float amount)
    {
        stress.Value += amount;
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
                    vignette.intensity.value = 0.6f;
                }
                break;
            case ABNORMAL.GHOSTLIGHT:
                // 불끄기 -> 라이트 목록 다른곳에 두고 호출만 하기
                foreach (Light light in stressLight)
                {
                    light.GetComponent<StressLight>().enabled = true;
                }
                break;

        }
    }

}
