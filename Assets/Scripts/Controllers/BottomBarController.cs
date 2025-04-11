using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class BottomBarController : MonoBehaviour
    {
        public TextMeshProUGUI barText;
        public TextMeshProUGUI personNameText;
        public Image charaterSprite;

        private int sentenceIndex = -1;
        public StoryScene currentScene;
        private State state = State.COMPLETED;

        private enum State
        {
            PLAYING, COMPLETED
        }

    
        public void PlayScene(StoryScene scene)
        {
            currentScene = scene;
            sentenceIndex = -1;
            PlayNextSentence();
        }

        public void PlayNextSentence()
        {
            StartCoroutine(TypeText(currentScene.sentences[++sentenceIndex].text));
            personNameText.text = currentScene.sentences[sentenceIndex].speaker.speakerName;
            personNameText.color = currentScene.sentences[sentenceIndex].speaker.textColor;
            charaterSprite.sprite = currentScene.sentences[sentenceIndex].speaker.characterSprite;
        }

        public bool IsCompleted()
        {
            return state == State.COMPLETED;
        }

        public bool IsLastSentence()
        {
            return sentenceIndex + 1 == currentScene.sentences.Count;
        }

        public void FinishSentence()
        {
            StopAllCoroutines();
            barText.text=currentScene.sentences[sentenceIndex].text;
            state = State.COMPLETED;
        }

        private IEnumerator TypeText(string text)
        {
            barText.text = "";
            state = State.PLAYING;
            int wordIndex = 0;

            while (state != State.COMPLETED)
            {
                barText.text += text[wordIndex];
                yield return new WaitForSeconds(0.05f);
                if(++wordIndex == text.Length)
                {
                    state = State.COMPLETED;
                    break;
                }
            }
        }
    }
}
