using UnityEngine;
using System.Collections;
public class ShowerController : MonoBehaviour
{
    [SerializeField] private ParticleSystem flowParticle; // Make it serialized field for Unity Editor
    private bool showerEnabled = false;
    private GameObject childCube;
    private bool canCollide = true;
    void Start()
    {
        childCube = transform.GetChild(0).GetChild(2).gameObject;
    }

    void Update()
    {

        if (showerEnabled)
        {
            StartShower();
        }
        if (!showerEnabled)
        {
            StopShower();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (canCollide && (other.gameObject.tag == "LeftHand" || other.gameObject.tag == "RightHand"))
        {
            canCollide = false;
            showerEnabled = !showerEnabled;
            StartCoroutine(RotateOverTime(childCube.transform, new Vector3(0f, 90f, 0f), 1.5f));
            StartCoroutine(EnableCollisionAfterDelay(2.0f));
        }
    }

    public void StartShower()
    {
        if (!flowParticle.isPlaying)
        {
            flowParticle.Play();
        }
    }

    public void StopShower()
    {
        if (flowParticle.isPlaying)
        {
            flowParticle.Stop();
        }
    }
    IEnumerator RotateOverTime(Transform target, Vector3 angles, float duration)
    {
        Quaternion startRotation = target.rotation;
        Quaternion endRotation = Quaternion.Euler(target.eulerAngles + angles);
        float startTime = Time.time;

        while (Time.time < startTime + duration)
        {
            float t = (Time.time - startTime) / duration;
            target.rotation = Quaternion.Slerp(startRotation, endRotation, t);
            yield return null;
        }
        target.rotation = endRotation;
    }
    IEnumerator EnableCollisionAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canCollide = true;
    }
}
