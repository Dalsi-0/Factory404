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


        // ��Ʈ���� ���� ���ϸ� ���� ���� ȣ��
        stress.DistinctUntilChanged()
            // ��Ʈ���� �� 0~100 ���̿��� ������ �� �������ֱ�
            .Do(val => { val = Mathf.Clamp(val, 0, 100); bar.fillAmount = val / 100; })
            // ��Ʈ���� ���� 30 ������ ��Ʈ�������� 1�� ����
            .Where(x => x > 30f).Do(_ => nowStressLevel.Value = 1)
            // ��Ʈ���� ���� 70 ������ ��Ʈ�������� 2�� ����
            .Where(x => x > 70f).Do(_ => nowStressLevel.Value = 2).Subscribe();

        // ��Ʈ���� ������ �ٲ�� ���� ���� ȣ��(�ߺ��� ����x)
        nowStressLevel.Distinct().Subscribe(val => SetStressLevel(val));


        // ��Ʈ���� ��ġ ������ ����
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
                // �Ҳ��� -> ����Ʈ ��� �ٸ����� �ΰ� ȣ�⸸ �ϱ�
                GameManager.Instance.OnGhostLight(true);
                break;

        }
    }

    /// <summary>
    /// ������ �þ� ����
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
}
