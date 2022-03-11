using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBack : MonoBehaviour
{
    [SerializeField] private KnifeMovement knifeMovement;

    [SerializeField] private GameObject colorCube;

    private float flashTime = 0.25f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Constants.Cuttable_Tag) == true)
        {
            knifeMovement.PreparePushBack();
            StartCoroutine(FlashColorCube());
            SoundManager.Instance.PlayPushBackSound();
        }
    }

    private IEnumerator FlashColorCube()
    {
        colorCube.SetActive(true);

        yield return new WaitForSeconds(flashTime);

        colorCube.SetActive(false);
    }
}
