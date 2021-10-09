using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRM;

// 修正履歴 2019/06/07 v0.1.0 ひとまず公開
//          2019/06/09 v0.1.1 少しソース整理 
//          2019/06/09 v0.1.2 表情に考慮したまばたきを実装

// VRMBlendShapeProxy経由で表情、口パクを行う
[RequireComponent(typeof(VRMBlendShapeProxy))]
public class VrmEmotionCtrl2 : MonoBehaviour
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

    // 口パク関連
    [SerializeField]
    public bool m_scriptKuchipaku = false; // 口パク中フラグ

    [SerializeField]
    [Range(0, 1)] public float m_kuchipakuMax = 1.0f;     // 口パクの開き具合

    [SerializeField]
    public float m_kuchipakuDuration = 0.1f;//KuchipakuAnimationが左から右に到達するまでの時間

    [SerializeField]
    public AnimationCurve m_kuchipakuAnimation = new AnimationCurve( // 口パクAnimationCurve
        new Keyframe(0f, 0f, 0f, 2f), //time, value, inTangent, outTangent
        new Keyframe(1f, 1f, 0f, 0f)  //time, value, inTangent, outTangent
    );

    private float m_kuchipakuRate = 0.0f; // AnimationCurveの横軸 位置(割合) 0～1
    private float m_kuchipakuRateDir = 1.0f; //口を開こうとしている場合は1.0f,閉じようとしている場合は-1.0f

    private bool m_prevScriptKuchipaku = false; // 直前の口パクフラグ


    // 瞬き関連
    [SerializeField]
    public bool m_useBlink = false; // まばたきを使用する
    private bool m_prevUseBlink = false; // まばたき直前
    [SerializeField]
    public AnimationCurve m_blinkerAnimation = new AnimationCurve( // 瞬き用AnimationCurve(眼を開けた状態->眼を閉じた状態->眼を開けた状態)
        new Keyframe(0f, 0f),
        new Keyframe(0.1f, 1f, 1f, 0f),
        new Keyframe(0.16f, 1f, 0f, 2f),
        new Keyframe(0.19f, 0f, 1f, 0f)
    );
    [SerializeField]
    public float m_blinkerSpeedmultiplier = 1.0f; // blinkAnimationCurveのスピード倍率
    [SerializeField]
    public float m_blinkerMaxInterval = 5.0f; // 瞬きの最大インターバル(単位:秒)
    [SerializeField]
    public bool m_blinkerOneTrigger = false; // 一回だけ即時まばたきを行うトリガー
    [SerializeField]
    public bool m_blinkerRandom = false; // まばたきをランダムで行う
    private bool m_blinkerNow = false; // blink中かどうか

    // 表情によって目が閉じる場合があるので瞬き時にそれを考慮するためのウェイト
    [SerializeField]
    [Range(0, 1)] private float m_joyBlinkWeight = 0.0f;
    [SerializeField]
    [Range(0, 1)] private float m_angryBlinkWeight = 0.0f;
    [SerializeField]
    [Range(0, 1)] private float m_sorrowBlinkWeight = 0.0f;
    [SerializeField]
    [Range(0, 1)] private float m_funBlinkWeight = 0.0f;


    private Coroutine m_blinkerCoroutine; // 瞬き用コルーチン

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
        if (!m_blinkerNow)
        {
            // 瞬き中以外のみ反映させる
            m_shapeProxy.ImmediatelySetValue(BlendShapePreset.Blink_L, m_blinkL);
            m_shapeProxy.ImmediatelySetValue(BlendShapePreset.Blink_R, m_blinkR);
        }

        // 口パク
        Kuchipaku();

        // まばたき
        Blink();
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

    void Blink()
    {
        // 切り替えた瞬間のみ処理
        if (m_prevUseBlink != m_useBlink)
        {
            if (m_useBlink)
            {
                //on
                BlinkStart();
            }
            else
            {
                //off
                BlinkStop(true);
            }
        }
        m_prevUseBlink = m_useBlink;
    }

    // まばたき処理を行う
    // univrmのBlinkerを参考に作成
    protected IEnumerator BlinkRoutine()
    {
        while (true)
        {
            var waitTime = Time.time + Random.value * m_blinkerMaxInterval;
            float playTime = 0.0f; // 現在の再生時間
            float maxTime = m_blinkerAnimation.keys[m_blinkerAnimation.length - 1].time;
            bool prevBlinkRandom = m_blinkerRandom; // 直前のランダムフラグ
            while (true)
            {
                if (m_blinkerRandom != prevBlinkRandom && m_blinkerRandom)
                {
                    // まばたき使用中でかつ、ランダムまばたきがオンになった瞬間のときは待ち時間を再計算する
                    waitTime = Time.time + Random.value * m_blinkerMaxInterval;
                }
                if (m_blinkerOneTrigger)
                {
                    // 特定のタイミングで1回
                    m_blinkerOneTrigger = false;
                    break;
                }
                if (m_blinkerRandom && waitTime <= Time.time)
                {
                    // ランダムでの瞬き有効時
                    break;
                }
                prevBlinkRandom = m_blinkerRandom;
                yield return null;
            }
            m_blinkerNow = true;

            while (true)
            {
                // blinkAnimation 開始
                // 表情分の目の開きを考慮する
                if (m_blinkerSpeedmultiplier <= 0.0f)
                {
                    // マイナス方向のスピードの場合は強制的に1で処理する
                    m_blinkerSpeedmultiplier = 1.0f;
                }
                playTime += Time.deltaTime * m_blinkerSpeedmultiplier;
                float blinkVal = m_blinkerAnimation.Evaluate(playTime);
                float blinkMax = 1.0f;
                blinkMax -= m_joy * m_joyBlinkWeight;
                blinkMax -= m_angry * m_angryBlinkWeight;
                blinkMax -= m_sorrow * m_sorrowBlinkWeight;
                blinkMax -= m_fun * m_funBlinkWeight;
                blinkMax -= m_blink;
                float blinkValL = (1 - m_blinkL) * blinkVal + m_blinkL;
                float blinkValR = (1 - m_blinkR) * blinkVal + m_blinkR;
                if (blinkMax <= 0.0f)
                    blinkMax = 0.0f;
                if (blinkValL >= blinkMax)
                    blinkValL = blinkMax;
                if (blinkValR >= blinkMax)
                    blinkValR = blinkMax;

                m_shapeProxy.ImmediatelySetValue(BlendShapePreset.Blink_L, blinkValL);
                m_shapeProxy.ImmediatelySetValue(BlendShapePreset.Blink_R, blinkValR);
                if (maxTime <= playTime)
                {
                    // 再生終了後抜ける
                    break;
                }
                yield return null;
            }
            m_blinkerNow = false;
        }
    }

    private void BlinkStart()
    {
        if (m_blinkerCoroutine != null)
        {
            StopCoroutine(m_blinkerCoroutine);
            m_blinkerCoroutine = null;
        }
        m_blinkerCoroutine = StartCoroutine(BlinkRoutine());
        m_useBlink = m_prevUseBlink = true;
    }

    //temp:一時的な停止かどうか
    private void BlinkStop(bool tempStop)
    {
        if (m_blinkerCoroutine != null)
        {
            StopCoroutine(m_blinkerCoroutine);
            m_blinkerCoroutine = null;
        }
        m_blinkerNow = false;
        if (!tempStop)
            m_useBlink = m_prevUseBlink = false;
    }

    private void OnEnable()
    {
        if (m_useBlink)
            BlinkStart();
    }

    private void OnDisable()
    {
        if (m_useBlink)
            BlinkStop(true);
    }
}
