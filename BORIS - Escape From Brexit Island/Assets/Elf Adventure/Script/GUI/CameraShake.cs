//Made for a cutscene in the Abandoned - scene, needs to be attatched to the camera that is supposed to shake. 


using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.PostProcessing;

public class CameraShake : MonoBehaviour
{

    public float amount;
    public AnimationCurve curve;
    public float time;

    private float localTime = 0;
    private Transform theCam;
    private ColorGrading pp_grading;

    void Start()
    {
        theCam = gameObject.transform;
        var ppp = this.GetComponent<PostProcessVolume>();
        if (ppp != null && ppp.profile != null) pp_grading = ppp.profile.GetSetting<ColorGrading>();        
    }

    public void ShakeMe()
    {
        StartCoroutine("Shake");
        
    }

    IEnumerator Shake()
    {
        Vector3 originalPos = theCam.position;
        if (pp_grading != null) pp_grading.colorFilter.Interp(Color.clear, Color.red, 1.0f);

        while (localTime < time)
        {
            localTime += Time.deltaTime;
            float moveAmount = amount * curve.Evaluate(localTime / time);
            theCam.position = originalPos + new Vector3(Random.Range(-moveAmount, moveAmount), 0.0f, 0.0f);
            //	yield return new WaitForSeconds (Time.deltaTime);	

            yield return new WaitForFixedUpdate();
        }
        localTime = 0.0f;
        StopAllCoroutines();
    }
}
