using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRM;

// 修正履歴 2019/06/07 v0.1.0 ひとまず公開
//          2019/06/09 v0.1.1 少しソース整理 

// VRMBlendShapeProxy経由で表情、口パクを行う
[RequireComponent(typeof(VRMBlendShapeProxy))]
public class VrmEmotionCtrl : MonoBehaviour
{
    // 反映させるShapeProxy
    [SerializeField]
    public VRMBlendShapeProxy m_shapeProxy;

    // 表情一覧
    [SerializeField]
    [Range(0, 1)] public float m_a = 0.0f;
    [SerializeField]
    [Range(0, 1)] public float m_i = 0.0f;
    [SerializeField]
    [Range(0, 1)] public float m_u = 0.0f;
    [SerializeField]
    [Range(0, 1)] public float m_e = 0.0f;
    [SerializeField]
    [Range(0, 1)] public float m_o = 0.0f;
    [SerializeField]
    [Range(0, 1)] public float m_blink = 0.0f;
    [SerializeField]
    [Range(0, 1)] public float m_joy = 0.0f;
    [SerializeField]
    [Range(0, 1)] public float m_angry = 0.0f;
    [SerializeField]
    [Range(0, 1)] public float m_sorrow = 0.0f;
    [SerializeField]
    [Range(0, 1)] public float m_fun = 0.0f;
    [SerializeField]
    [Range(0, 1)] public float m_lookUp = 0.0f;
    [SerializeField]
    [Range(0, 1)] public float m_lookDown = 0.0f;
    [SerializeField]
    [Range(0, 1)] public float m_lookLeft = 0.0f;
    [SerializeField]
    [Range(0, 1)] public float m_lookRight = 0.0f;
    [SerializeField]
    [Range(0, 1)] public float m_blinkL = 0.0f;
    [SerializeField]
    [Range(0, 1)] public float m_blinkR = 0.0f;

    [SerializeField]
    [Range(0, 1)] public float m_kuchipakuMax = 1.0f;     // 口パクの開き具合

    [SerializeField]
    public bool m_scriptKuchipaku = false; // 口パク中フラグ

    [SerializeField]
    public float m_kuchipakuDuration = 0.1f;//KuchipakuAnimationが左から右に到達するまでの時間

    [SerializeField]
    public AnimationCurve m_kuchipakuAnimation = new AnimationCurve( // AnimationCurve
        new Keyframe(0f, 0f, 0f, 2f), //time, value, inTangent, outTangent
        new Keyframe(1f, 1f, 0f, 0f)  //time, value, inTangent, outTangent
    );

    private float m_kuchipakuRate = 0.0f; // AnimationCurveの横軸 位置(割合) 0～1
    private float m_kuchipakuRateDir = 1.0f; //口を開こうとしている場合は1.0f,閉じようとしている場合は-1.0f

    private bool m_prevScriptKuchipaku = false; // 直前の口パクフラグ

    private void Reset()
    {
        m_shapeProxy = GetComponent<VRMBlendShapeProxy>();
    }

    private void Update()
    {
        if (m_shapeProxy == null)
            return;
        if (!m_scriptKuchipaku)
        {
            // 口パク中以外のみ口のシェイプは反映させる
            m_shapeProxy.ImmediatelySetValue(BlendShapePreset.A, m_a);
            m_shapeProxy.ImmediatelySetValue(BlendShapePreset.I, m_i);
            m_shapeProxy.ImmediatelySetValue(BlendShapePreset.U, m_u);
            m_shapeProxy.ImmediatelySetValue(BlendShapePreset.E, m_e);
            m_shapeProxy.ImmediatelySetValue(BlendShapePreset.O, m_o);
        }
        m_shapeProxy.ImmediatelySetValue(BlendShapePreset.Blink, m_blink);
        m_shapeProxy.ImmediatelySetValue(BlendShapePreset.Joy, m_joy);
        m_shapeProxy.ImmediatelySetValue(BlendShapePreset.Angry, m_angry);
        m_shapeProxy.ImmediatelySetValue(BlendShapePreset.Sorrow, m_sorrow);
        m_shapeProxy.ImmediatelySetValue(BlendShapePreset.Fun, m_fun);
        m_shapeProxy.ImmediatelySetValue(BlendShapePreset.LookUp, m_lookUp);
        m_shapeProxy.ImmediatelySetValue(BlendShapePreset.LookRight, m_lookRight);
        m_shapeProxy.ImmediatelySetValue(BlendShapePreset.LookLeft, m_lookLeft);
        m_shapeProxy.ImmediatelySetValue(BlendShapePreset.LookDown, m_lookDown);
        m_shapeProxy.ImmediatelySetValue(BlendShapePreset.Blink_L, m_blinkL);
        m_shapeProxy.ImmediatelySetValue(BlendShapePreset.Blink_R, m_blinkR);

        // 口パク
        Kuchipaku();

    }

    // 口パク大まかな処理
    void Kuchipaku()
    {
        // 切り替えた瞬間のみ処理
        if (m_prevScriptKuchipaku != m_scriptKuchipaku)
        {
            // on off 状態切り替え
            if (m_scriptKuchipaku)
            {
                // on
                m_shapeProxy.ImmediatelySetValue(BlendShapePreset.A, 0.0f);
                m_shapeProxy.ImmediatelySetValue(BlendShapePreset.I, 0.0f);
                m_shapeProxy.ImmediatelySetValue(BlendShapePreset.U, 0.0f);
                m_shapeProxy.ImmediatelySetValue(BlendShapePreset.E, 0.0f);
                m_shapeProxy.ImmediatelySetValue(BlendShapePreset.O, 0.0f);
                m_kuchipakuRate = 0.0f;
                m_kuchipakuRateDir = 1.0f;
                m_scriptKuchipaku = m_prevScriptKuchipaku = true;
            }
            else
            {
                // off
                m_shapeProxy.ImmediatelySetValue(BlendShapePreset.A, 0.0f);
                m_shapeProxy.ImmediatelySetValue(BlendShapePreset.I, 0.0f);
                m_shapeProxy.ImmediatelySetValue(BlendShapePreset.U, 0.0f);
                m_shapeProxy.ImmediatelySetValue(BlendShapePreset.E, 0.0f);
                m_shapeProxy.ImmediatelySetValue(BlendShapePreset.O, 0.0f);
                m_scriptKuchipaku = m_prevScriptKuchipaku = false;
            }
        }

        if (m_scriptKuchipaku)
        {
            // 口パク処理
            KuchipakuNow(BlendShapePreset.A, m_kuchipakuMax, m_kuchipakuDuration);
        }
        m_prevScriptKuchipaku = m_scriptKuchipaku;
    }

    // 口パクの処理を行う
    // preset:口の開きのパターン(A推奨)
    // max: 口パクの開き具合
    // duration:KuchipakuAnimationが左から右に到達するまでの時間
    void KuchipakuNow(BlendShapePreset preset, float max, float duration)
    {
        m_kuchipakuRate += Time.deltaTime / duration * m_kuchipakuRateDir;
        if (m_kuchipakuRate >= 1.0f || m_kuchipakuRate <= 0.0f)
        {
            m_kuchipakuRateDir *= -1;
            if (m_kuchipakuRate >= 1.0f)
                m_kuchipakuRate = 1.0f;
            else if (m_kuchipakuRate <= 0.0f)
                m_kuchipakuRate = 0.0f;
        }
        float value = m_kuchipakuAnimation.Evaluate(m_kuchipakuRate) * max;

        m_shapeProxy.ImmediatelySetValue(preset, value);

    }
}
