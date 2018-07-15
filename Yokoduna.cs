using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Yokoduna {
    public class Yokoduna : MonoBehaviour {
        [SerializeField] private Config setting;

        /// <summary>
        /// Create User Sample
        /// </summary>
        private void Start() {
            if (setting == null ){
                Debug.LogError("[Yokoduna]Settingファイルが指定されていません。");
                return;
            }
            var sequence = CreateUser("testUser", "user@axluse.test", "password");
            sequence.Subscribe( isSuccess =>{
                if (isSuccess == true) {
                    Debug.Log("ユーザーの作成が終了しました");
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
            Subject<bool> cliPass = new Subject<bool>();
            User user = new User(userName, mail, password);
            var getter = new YokodunaCreateUser(user, setting, cliPass);
            cliPass.Subscribe(_flg => {
                if ( _flg ) {
                    retPass.OnNext(true);
                }
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