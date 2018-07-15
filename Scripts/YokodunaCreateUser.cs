using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Yokoduna {
    // "CreateUser" of rest client's invoke system
    // false => error , true => success
    public class YokodunaCreateUser {
        public YokodunaCreateUser(User user, Config conf, Subject<bool> unit, bool throwHandle = false) {
            if (_checkCreatedUser()) {
                Debug.LogError("[Yokoduna Error] Creating User Registed! Can not duplication user infomations!");
                return;
            }
            string uri = String.Format("{0}api/newuser?_api_token={1}&product_id={2}&name={3}&mail={4}&pass={5}",
                conf.baseURL,
                conf.apikey,
                conf.productkey,
                user.name,
                user.mail,
                user.password
            );
            Subject<string> sj = new Subject<string>();
            Client cli = new Client(uri, sj);
            sj.Subscribe(_jsn => {
                APICreateUser info = JsonUtility.FromJson<APICreateUser>(_jsn);
                if ( info.isError ) {
                    if (!throwHandle) Debug.LogError(String.Format("[Yokoduna Error] Create User: {0}",info.error));
                    else Debug.LogWarning(String.Format("[Yokoduna Error] Create User: {0}",info.error));
                    unit.OnNext(false);
                    return;
                }
                unit.OnNext(true);
            });
        }

        // Check 2 Registed cd
        private bool _checkCreatedUser () {
            string userID = PlayerPrefs.GetString("YokodunaPlayerRegistedID");
            if (userID != "") {
                return true;
            }
            return false;
        }
    }
}