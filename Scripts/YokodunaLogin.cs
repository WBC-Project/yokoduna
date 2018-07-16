using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace WBCProject {

    public class YokodunaLogin {
        public User user = null;
        public YokodunaLogin(User user, Config conf, Subject<string> unit, bool throwHandle = false) {
            string uri = String.Format("{0}api/login?_api_token={1}&product_id={2}&mail={3}&pass={4}",
                conf.baseURL,
                conf.apikey,
                conf.productkey,
                user.mail,
                user.password
            );
            Subject<string> sj = new Subject<string>();
            Client cli = new Client(uri, sj);
            sj.Subscribe(_jsn => {
                APILoginUser info = JsonUtility.FromJson<APILoginUser>(_jsn);
                if ( info.error != "" ) {
                    if (!throwHandle) Debug.LogError(String.Format("[Yokoduna Error] Login: {0}",info.error));
                    else Debug.LogWarning(String.Format("[Yokoduna Error] Login: {0}",info.error));
                    unit.OnNext("");
                    unit.OnCompleted();
                    return;
                }
                unit.OnNext(info.userID);
                unit.OnCompleted();
                return;
            });
        }
    }
}