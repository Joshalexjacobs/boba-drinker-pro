using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private TeaTimer _teaTimer;

    [SerializeField]
    private TextMeshProUGUI _starterText;

    [SerializeField]
    private TextMeshProUGUI _gameOvertext;

    [SerializeField]
    private TextMeshProUGUI _youWinText;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(3f);

        _starterText.enabled = false;

        _teaTimer.StartTeaTimer();
    }



}
