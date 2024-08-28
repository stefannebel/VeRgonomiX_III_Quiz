using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationConfirmButton : MonoBehaviour
{
    Button btn;

    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        QuizManager qm = (QuizManager)FindAnyObjectByType(typeof(QuizManager));
        qm.confirmAnimationQuestion(transform.parent.transform);
    }
}
