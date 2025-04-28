using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif

public static class ReplaceEventSystems
{
    const float InitialHeight = 1.6f;

    // シーンロード直後に必ず呼ばれる（ビルド後も有効）
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void Init()
    {
        // OVRCameraRig がなければ VR シーンじゃない → 処理しない
        if (Object.FindObjectOfType<OVRCameraRig>() == null)
            return;

        DoReplace();
        SceneManager.sceneLoaded += (scene, mode) => DoReplace();
    }

    static void DoReplace()
    {
        // (1) FloorLevel トラッキング時のカメラ高さ補正
        var ovrMgr = Object.FindObjectOfType<OVRManager>();
        if (ovrMgr != null
            && ovrMgr.trackingOriginType == OVRManager.TrackingOrigin.FloorLevel
            && Camera.main != null)
        {
            Camera.main.transform.position += Vector3.up * InitialHeight;
        }

        // (2) EditorCamera はエディター時のみ追加したい場合
#if UNITY_EDITOR
        if (Camera.main != null
            && Camera.main.gameObject.GetComponent<EditorCamera>() == null)
        {
            Camera.main.gameObject.AddComponent<EditorCamera>();
        }
#endif

        // (3) EventSystem と Canvas の差し替え・追加処理
        ReplaceEventSystem();
    }

    static void ReplaceEventSystem()
    {
        // A) EventSystem の入力モジュール差し替え
        var es = Object.FindObjectOfType<EventSystem>();
        if (es != null)
        {
            var ovrInput = es.GetComponent<OVRInputModule>();
            if (ovrInput != null)
                ovrInput.enabled = false;

            if (es.GetComponent<StandaloneInputModule>() == null)
            {
                es.gameObject.AddComponent<StandaloneInputModule>();
                Debug.Log("Replaced OVRInputModule → StandaloneInputModule");
            }
        }

        // B) Canvas の GraphicRaycaster を有効化 or 追加
        if (SceneManager.GetActiveScene().name == "StartScene")
        {
            var parent = GameObject.Find("CanvasFollowManager");
            var canvas = parent.GetComponentsInChildren<Canvas>(includeInactive: true);
            foreach (var cv in canvas)
            {
                cv.gameObject.AddComponent<GraphicRaycaster>();
                Debug.Log($"[ReplaceEventSystems] Added GraphicRaycaster to Canvas '{cv.name}'");

                // if (gr == null)
                // {
                //     cv.gameObject.AddComponent<GraphicRaycaster>();
                //     Debug.Log($"[ReplaceEventSystems] Added GraphicRaycaster to Canvas '{cv.name}'");
                // }
                // else if (!gr.enabled)
                // {
                //     gr.enabled = true;
                //     Debug.Log($"[ReplaceEventSystems] Enabled existing GraphicRaycaster on Canvas '{cv.name}'");
                // }
            }
        }
    }
}