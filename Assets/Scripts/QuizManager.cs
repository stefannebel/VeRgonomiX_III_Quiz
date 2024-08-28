using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.Content.Animation;
using UnityEngine.XR.Content.Interaction;

public class QuizManager : MonoBehaviour
{
    public List<GameObject> Questions = new List<GameObject>();

    [HideInInspector]
    public int questionID = 0;
    GameObject lastQuestion;

    public Slider slider;
    public TextMeshProUGUI questionIndicator;
    public TextMeshProUGUI timeIndicator;

    public Color correctAnswer;
    public Color wrongAnswer;

    [Header("SoundOptions")]
    public AudioClip correctAnswerSound;
    public AudioClip wrongAnswerSound;

    public float score = 0;
    public TextMeshProUGUI[] Scores;
    public TextMeshProUGUI rememberText;

    public UnityEvent onEnd;

    public Transform Quiz; //SpawnLocation der Fragen
    public Transform Merke;

    public UICameraFacing cameraFollower;

    private void Start()
    {
        GetComponent<DissolveAnimation>().Dissolving(false);

        slider.maxValue = Questions.Count;
        updateProgress();
        //LoadQuestion();
    }

    private void Update()
    {
        timeIndicator.text = FormatTime(Time.time);
    }

    public void getTime(TextMeshProUGUI myTimeGUI)
    {
        string timeText = FormatTime(Time.time);
        myTimeGUI.text = "Benötigte Zeit: \n" + getEndTime(Time.time);
    }

    public void checkAnswers(Transform panel)
    {
        //----> Hier für Animationfragen überschreiben
        Toggle[] answers = panel.GetComponentsInChildren<Toggle>();

        bool isCorrectAnswer = true;

        foreach (var answer in answers)
        {
            ToggleColorChanger2 answerVisuals = answer.transform.parent.GetComponent<ToggleColorChanger2>();

            if (string.Equals(answer.name.ToLower(), answer.isOn.ToString().ToLower()))
            {
                Debug.Log("String und Boolean sind gleich.");
                // Hier kannst du weitere Aktionen ausführen, wenn der Vergleich wahr ist

                //Hier werden die Antworten farblich markiert
                answer.enabled = false;
                ShowAnswers(answerVisuals, true);

            }
            else
            {
                Debug.Log("String und Boolean sind nicht gleich.");
                // Hier kannst du weitere Aktionen ausführen, wenn der Vergleich falsch ist

                answer.enabled = false;
                ShowAnswers(answerVisuals, false);
                isCorrectAnswer = false;
            }
        }

        PlaySound(isCorrectAnswer ? correctAnswerSound : wrongAnswerSound);

        if (isCorrectAnswer) score++;
        
        foreach (var Score in Scores)
        {
            Score.text = score + "/" + Questions.Count;
        }


    }

    void ShowAnswers(ToggleColorChanger2 answerVisuals, bool isTrue)
    {

        answerVisuals.ImgBackground.color = isTrue ? correctAnswer : wrongAnswer;
        answerVisuals.Label.color = Color.black;
        Debug.Log("Color" + correctAnswer);
        Destroy(answerVisuals);
    }

    void updateProgress()
    {
        slider.value = questionID + 1;
        questionIndicator.text = "Question " + (questionID + 1) + "/" + Questions.Count;

        //Achtung, wird auch nach dem Antwortcheck aufgerufen
        foreach (var Score in Scores)
        {
            Score.text = score + "/" + Questions.Count;
        }
    }

    public void LoadQuestion()
    {
        updateProgress();
        Destroy(lastQuestion);

        if (questionID < Questions.Count)
        {

            //Falls es sich um eine Wort oder Bildfrage handelt
            if (Questions[questionID].CompareTag("AnimationQuestion"))
            {
                //ANIMATIONQUESTION
                GameObject newQuestion = Instantiate(Questions[questionID]);
                rememberText.text = newQuestion.GetComponent<Question>().RememberText;
                Quiz.parent.gameObject.SetActive(false);
                lastQuestion = newQuestion;
                Quiz.parent.gameObject.SetActive(true);
                GetComponent<DissolveAnimation>().Dissolving(true);
                
                cameraFollower.enabled = false;
                cameraFollower.gameObject.transform.Rotate(new Vector3(30, 0, 0),3);
                
            }
            else
            {
                GameObject newQuestion = Instantiate(Questions[questionID], Quiz.GetChild(1));
                newQuestion.transform.SetSiblingIndex(newQuestion.transform.GetSiblingIndex() - 2);
                rememberText.text = newQuestion.GetComponent<Question>().RememberText;
                lastQuestion = newQuestion;
            }
        }

        questionID++;


        if (questionID > Questions.Count)
        {
            endOfTheQuiz();
        }
    }

    public void confirmAnimationQuestion(Transform panel)
    {
        checkAnswers(panel);
        Quiz.parent.gameObject.SetActive(true);
        Merke.GetChild(2).gameObject.SetActive(true);
        Merke.GetComponent<Animator>().SetTrigger("IN");
        Destroy(lastQuestion, 1f);
    }

    public void AutomaticNextQuestion()
    {
        StartCoroutine(AutomaticNextQuestionCoRo());
    }

    IEnumerator AutomaticNextQuestionCoRo()
    {
        yield return new WaitForSeconds(1.5f);
        Debug.LogWarning("Hallo");
        Scores[0].transform.parent.GetComponent<Animator>().SetTrigger("OUT");
        LoadQuestion();
    }


    public void animationMerke()
    {

    }



    void endOfTheQuiz()
    {
        Debug.Log("End of the Quiz");
        onEnd.Invoke();
    }

    

    void PlaySound(AudioClip ac)
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = ac;
        audioSource.Play();
    }





    string FormatTime(float timeInSeconds)
    {
        // Berechne Minuten und Sekunden
        int minutes = Mathf.FloorToInt(timeInSeconds / 60f);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60f);

        // Erzeuge den formatierten String
        string formattedTime = string.Format("{0:00}:{1:00}", minutes, seconds);

        return formattedTime;
    }

    string getEndTime(float timeInSeconds)
    {

        // Berechne Minuten und Sekunden
        int minutes = Mathf.FloorToInt(timeInSeconds / 60f);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60f);

        // Erzeuge den formatierten String
        //string formattedTime = string.Format("{0:00}:{1:00}", minutes, seconds);

        return minutes + " Minute(n) " + seconds + " Sekunde(n)";
    }

    public void RestartQuiz()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void setEndText(TextMeshProUGUI endText)
    {
        Debug.Log("Q" + Questions.Count + " S" + score);

        if (score < (Questions.Count / 2)) { endText.text = "Sie sind an Ergonomie interessiert, bei einigen Fragen gibt es jedoch noch Unklarheiten. Informieren Sie sich gerne bei der AUVA über weitere Schulungsmöglichkeiten."; }
        else if (score < (Questions.Count * 0.75f)) { endText.text = "Sie sind an Ergonomie interessiert und kennen sich schon ziemlich gut aus. Ergänzende Informationen erhalten Sie bei weiteren Schulungsmöglichkeiten der AUVA"; }
        else endText.text = "Das ist ein sehr gutes Resultat. Bitte auch künftig gut informieren, um Sicherheit und Ergonomie am Arbeitsplatz zu gewährleisten.";
    }

}
