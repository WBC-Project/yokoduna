using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace WBCProject {
    public class Yokoduna : MonoBehaviour {
        [SerializeField] private Config setting;
        public string user_id;

        /// <summary>
        /// Settingファイルの参照が正しいかチェックをします
        /// </summary>
        private void Awake() {
            if (setting == null ){
                Debug.LogError("[Yokoduna]Settingファイルが指定されていません。");
                return;
            }
        }

        /// <summary>
        /// ユーザー新規登録コントローラー
        /// </summary>
        /// <param name="userName">ユーザー名</param>
        /// <param name="mail">メールアドレス</param>
        /// <param name="password">ログインパスワード</param>
        /// <returns>非同期boolean </returns>
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
        /// <returns>非同期boolean</returns>
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

        /// <summary>
        /// keyとvalueで対になったデータをサーバーにプッシュします
        /// これらのデータには、グループを設定できます
        /// データの保有はサーバー上でユーザーごとに保有することになります
        /// </summary>
        /// <param name="key">データにアクセスする変数名のようなもの</param>
        /// <param name="value">変数の中身のデータのようなもの</param>
        /// <param name="groupName">データグループ</param>
        /// <returns>非同期boolean</returns>
        public Subject<bool> SetData(string user_id, string key, string value, string groupName) {
            Subject<bool> retPass = new Subject<bool>();
            Subject<bool> cliPass = new Subject<bool>();
            var getter = new YokodunaSetData(user_id, key, value, groupName, setting, cliPass, true);
            cliPass.Subscribe(_flg => {
                if ( _flg ) {
                    retPass.OnNext(true);
                } else {
                    retPass.OnNext(false);
                }
                retPass.OnCompleted();
            });
            return retPass;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="key"></param>
        /// <param name="groupName"></param>
        /// <returns>非同期データプロパティクラス</returns>
        public Subject<APIData[]> GetData (string user_id, string key, string groupName) {
            Subject<APIData[]> pass = new Subject<APIData[]>();
            Subject<APIData[]> ret = new Subject<APIData[]>();
            var getter = new YokodunaGetData(user_id, key, groupName, setting, pass, true);
            pass.Subscribe(_unit => {
                if (_unit == null) {
                    ret.OnNext(null);
                } else {
                    ret.OnNext(_unit);
                }
                ret.OnCompleted();
            });
            return ret;
        }
    }
}