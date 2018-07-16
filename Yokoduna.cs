using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Yokoduna {
    public class Yokoduna : MonoBehaviour {
        [SerializeField] private Config setting;
        [SerializeField] private string userName;
        [SerializeField] private string mail;
        [SerializeField] private string pass;
        public string user_id;

        private void Start() {
            if (setting == null ){
                Debug.LogError("[Yokoduna]Settingファイルが指定されていません。");
                return;
            }
        }

        /// <summary>
        /// Create User Sample
        /// </summary>
        private void StartCreateUser() {
            
            var sequence = CreateUser(userName, mail, pass);
            sequence.Subscribe( isSuccess =>{
                if (isSuccess == true) {
                    Debug.Log("ユーザーの作成が終了しました");
                } else {
                    Debug.Log("既に登録されているユーザー");
                }
            });
        }

        /// <summary>
        /// Create User Sample
        /// </summary>
        private void StartLogin() {
            if (setting == null ){
                Debug.LogError("[Yokoduna]Settingファイルが指定されていません。");
                return;
            }
            var sequence = Login(mail, pass);
            sequence.Subscribe( isSuccess =>{
                if (isSuccess == true) {
                    Debug.Log("対象ユーザーのログインに成功しました");
                } else {
                    Debug.Log("対象ユーザーのログインに失敗しました");
                }
            });
        }

        /// <summary>
        /// ユーザー新規登録コントローラー
        /// </summary>
        /// <param name="userName">ユーザー名</param>
        /// <param name="mail">メールアドレス</param>
        /// <param name="password">ログインパスワード</param>
        /// <returns></returns>
        public Subject<bool> CreateUser(string userName, string mail, string password) {
            Subject<bool> retPass = new Subject<bool>();
            Subject<string> cliPass = new Subject<string>();
            User user = new User(userName, mail, password);
            var getter = new YokodunaCreateUser(user, setting, cliPass);
            cliPass.Subscribe(_flg => {
                if ( _flg != "" ) {
                    retPass.OnNext(true);
                } else {
                    retPass.OnNext(false);
                }
                retPass.OnCompleted();
                this.user_id = _flg;
            });
            return retPass;
        }

        /// <summary>
        /// ログインコントローラー
        /// </summary>
        /// <param name="mail">メールアドレス</param>
        /// <param name="password">パスワード</param>
        /// <returns></returns>
        public Subject<bool> Login(string mail, string password) {
            Subject<bool> retPass = new Subject<bool>();
            Subject<string> cliPass = new Subject<string>();
            User user = new User("", mail, password);
            var getter = new YokodunaLogin(user,setting, cliPass);
            cliPass.Subscribe(_flg => {
                if ( _flg != "" ) {
                    retPass.OnNext(true);
                } else {
                    retPass.OnNext(false);
                }
                retPass.OnCompleted();
                this.user_id = _flg;
            });
            return retPass;
        }

        public Subject<bool> SetData(string mail, string password) {
            Subject<bool> retPass = new Subject<bool>();
            Subject<string> cliPass = new Subject<string>();
            User user = new User("", mail, password);
            var getter = new YokodunaLogin(user,setting, cliPass);
            cliPass.Subscribe(_flg => {
                if ( _flg != "" ) {
                    retPass.OnNext(true);
                } else {
                    retPass.OnNext(false);
                }
                retPass.OnCompleted();
            });
            return retPass;
        }
    }

    public enum InitSequence {
        Init,
        WaitCreateUser,
        SuccessCreateUser,
        AllSuccess,
        Error
    }
}