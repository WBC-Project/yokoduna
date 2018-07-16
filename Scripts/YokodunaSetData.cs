using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Yokoduna {

    public class YokodunaSetData {
        public YokodunaSetData(string user_id, string key, string value, Config conf, Subject<bool> unit, bool throwHandle = false) {
            string uri = String.Format("{0}api/setdata?_api_token={1}&product_id={2}&user_id={3}&pdk={4}&pdv={5}&pdt={6}",
                conf.baseURL,
                conf.apikey,
                conf.productkey,
                user_id,
                key,
                value,
                CreateGUID()
            );
            Debug.Log(uri);
            Subject<string> sj = new Subject<string>();
            Client cli = new Client(uri, sj);
            sj.Subscribe(_jsn => {
                APISetData info = JsonUtility.FromJson<APISetData>(_jsn);
                if ( info.error != "" ) {
                    if (!throwHandle) Debug.LogError(String.Format("[Yokoduna Error] Set Data: {0}",info.error));
                    else Debug.LogWarning(String.Format("[Yokoduna Error] Set Data: {0}",info.error));
                    unit.OnNext(false);
                    unit.OnCompleted();
                    return;
                }
                unit.OnNext(true);
            });
        }

        public string CreateGUID() {
            return Guid.NewGuid().ToString();
        }
    }
}