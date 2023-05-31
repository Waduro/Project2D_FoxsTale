using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpeedrunLevelEnd : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            EndSpeedrunLevel();
        }
    }

    public void EndSpeedrunLevel() {
        SubmitPointsOnLeaderboard.Instance.SendSpeedrunScore();
        StartCoroutine(EndSpeedrunLevelCoroutine());
    }

    public IEnumerator EndSpeedrunLevelCoroutine() {
        AudioManager.instance.PlayLevelVictory();

        PlayerController.instance.stopInput = true;

        CameraController.Instance.stopFollow = true;

        UIController.Instance.levelCompletedText.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        UIController.Instance.FadeToBlack();

        yield return new WaitForSeconds((3.0f / UIController.Instance.fadeSpeed) + 1.25f);

        SceneManager.LoadScene("Main Menu");
    }
}
