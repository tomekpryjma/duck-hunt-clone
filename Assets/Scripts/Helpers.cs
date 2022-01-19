using System;
using UnityEngine;

class Helpers : MonoBehaviour
{
    public static bool AnimatorIsPlaying(Animator animator)
    {
        return animator.GetCurrentAnimatorStateInfo(0).length > animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    public static void ShowScore(Vector3 pos, GameObject prefab, int score, bool isSpecial)
    {
        Progress.AddToStat("total_score", score);

        GameObject scorePopup = Instantiate(
            prefab,
            pos,
            Quaternion.identity
        );
        scorePopup.GetComponent<ScorePopup>().Setup(score, isSpecial);
    }
}