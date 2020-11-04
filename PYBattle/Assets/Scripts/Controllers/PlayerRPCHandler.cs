using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ExitGames.Client.Photon;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Controlelrs
{
    public class PlayerRPCHandler : MonoBehaviour, IOnEventCallback
    {
        int updatedScore = 0;
        static int updatedScorePlayer1 = 0;
        static int updatedScorePlayer2 = 0;
        static string playerName1 = null;
        static string playerName2 = null;

        public float StartDelay = 1.5f;
        [SerializeField]
        private PhotonView _photonView;

        private GPlayer _player;
        private bool _canAnswer = false;
        private bool _answered = false;
        private static int count = 0;

        private readonly byte RequestAnswer = 0;
        private readonly byte RightAnswer = 1;
        private readonly byte WrongAnswer = 2;

        private bool masterNext = false;



        public bool CanAnswer
        {
            get
            {
                return _canAnswer;
            }

            set
            {
                _canAnswer = value;
            }
        }

        private void Start()
        {
            _player = GetComponent<GPlayer>();

            if (_photonView.IsMine)
                _photonView.RPC("SetPlayer", RpcTarget.All, PlayerPrefs.GetString("PlayerName"));

            if (PhotonNetwork.IsMasterClient)
            {
                PreparePlayers();
                _photonView.RPC("PreparePlayers", RpcTarget.Others);
            }

        }

        public void OnEnable()
        {
            PhotonNetwork.AddCallbackTarget(this);
        }

        public void OnDisable()
        {
            PhotonNetwork.RemoveCallbackTarget(this);
        }

        [PunRPC]
        public void SetPlayer(string name)
        {
            if (_player == null)
                _player = GetComponent<GPlayer>();

            _player.PlayerName = name;
        }



        [PunRPC]
        public void PreparePlayers()
        {
            GameUIController.instance.PreparePlayers(PhotonNetwork.PlayerList);
            StartCoroutine(QuestionController.instance.LoadQuestions());



            StartCoroutine(WaitStart(StartDelay));
        }



        private IEnumerator WaitStart(float delay)
        {
            yield return new WaitForSeconds(delay);

            QuestionController.instance.Next();

            if (PhotonNetwork.IsMasterClient)
                QuestionController.instance.Next(0);


            CanAnswer = true;
        }

        public void OnEvent(EventData photonEvent)
        {
            if (!_photonView.IsMine)
                return;

            if (photonEvent.Code > 3)
                return;


            string playerName = ((object[])photonEvent.CustomData)[0] as string;

            if (String.IsNullOrEmpty(playerName1))
            {
                playerName1 = String.Copy(playerName);
                print("COPIED P1" + playerName1);
            }
            else if (String.IsNullOrEmpty(playerName2) && !playerName.Equals(playerName1))
            {
                playerName2 = String.Copy(playerName);
                print("COPIED P2" + playerName2);
            }

            switch (photonEvent.Code)
            {
                case 0:

                    Player[] players = PhotonNetwork.PlayerList;

                    for (int i = 0; i < players.Length; i++)
                    {
                        if (playerName == PhotonNetwork.NickName)
                        {
                            if (CanAnswer)
                            {

                                GameUIController.instance.EnableAnswers(true);

                                GameUIController.instance.StartAnswerTimer();
                                _answered = true;
                                return;

                            }
                        }
                    }
                    CanAnswer = false;
                    GameUIController.instance.EnableBuzzer(false);


                    break;
                case 1:

                    if (count < 4)
                    {

                        updatedScore = PhotonRoom.instance.AddScore(playerName, GameSettings.instance.AnswerPoints);
                        QuestionController.instance.QuestionIndex += 1;
                        int index = PhotonNetwork.IsMasterClient ? QuestionController.instance.QuestionIndex - 1 : QuestionController.instance.QuestionIndex;
                        QuestionController.instance.Next(index);
                        if (playerName.Equals(playerName1))
                        {
                            updatedScorePlayer1 = updatedScore;
                        }
                        else
                        {
                            updatedScorePlayer2 = updatedScore;
                        }


                        CanAnswer = true;

                        _answered = false;
                        count++;
                        print(playerName1 + updatedScorePlayer1 + playerName2 + updatedScorePlayer2);
                    }
                    else if (count == 4)
                    {

                        updatedScore = PhotonRoom.instance.AddScore(playerName, GameSettings.instance.AnswerPoints);
                        CanAnswer = true;

                        _answered = false;
                        GameUIController.instance.EnableBuzzer(true);

                        count++;

                        if (playerName.Equals(playerName1))
                        {
                            updatedScorePlayer1 = updatedScore;
                        }
                        else
                        {
                            updatedScorePlayer2 = updatedScore;
                        }

                        print("Last Qn! CORRECT");
                        print(playerName1 + updatedScorePlayer1 + playerName2 + updatedScorePlayer2);
                        PlayerPrefs.SetInt("player1_score", updatedScorePlayer1);
                        PlayerPrefs.SetString("player1_name", playerName1);
                        PlayerPrefs.SetInt("player2_score", updatedScorePlayer2);
                        PlayerPrefs.SetString("player2_name", playerName2);
                        SceneManager.LoadScene("EndPVP");
                    }


                    break;
                case 2:

                    /*
                    if (_answered)
                        return;
                    */

                    if (count < 4)
                    {
                        updatedScore = PhotonRoom.instance.MinusScore(playerName, GameSettings.instance.AnswerPoints);
                        QuestionController.instance.QuestionIndex += 1;
                        int index = PhotonNetwork.IsMasterClient ? QuestionController.instance.QuestionIndex - 1 : QuestionController.instance.QuestionIndex;
                        QuestionController.instance.Next(index);
                        CanAnswer = true;

                        _answered = false;
                        GameUIController.instance.EnableBuzzer(true);

                        count++;

                        if (playerName.Equals(playerName1))
                        {
                            updatedScorePlayer1 = updatedScore;
                        }
                        else
                        {
                            updatedScorePlayer2 = updatedScore;
                        }
                        print(playerName1 + updatedScorePlayer1 + playerName2 + updatedScorePlayer2);
                    }
                    else if (count == 4)
                    {
                        updatedScore = PhotonRoom.instance.MinusScore(playerName, GameSettings.instance.AnswerPoints);
                        CanAnswer = true;

                        _answered = false;
                        GameUIController.instance.EnableBuzzer(true);

                        count++;

                        if (playerName.Equals(playerName1))
                        {
                            updatedScorePlayer1 = updatedScore;
                        }
                        else
                        {
                            updatedScorePlayer2 = updatedScore;
                        }
                        print("Last Qn! WRONG");
                        print(playerName1 + updatedScorePlayer1 + playerName2 + updatedScorePlayer2);
                        PlayerPrefs.SetInt("player1_score", updatedScorePlayer1);
                        PlayerPrefs.SetString("player1_name", playerName1);
                        PlayerPrefs.SetInt("player2_score", updatedScorePlayer2);
                        PlayerPrefs.SetString("player2_name", playerName2);
                        SceneManager.LoadScene("EndPVP");
                    }

                    break;
            }
        }


    }
}