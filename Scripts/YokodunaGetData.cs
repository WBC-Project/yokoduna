using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Yokoduna {

    public class YokodunaGetData {
        public YokodunaGetData(string user_id, string key, string groupName, Config conf, Subject<APIData[]> unit, bool throwHandle = false) {
            string uri = String.Format("{0}api/getdata?_api_token={1}&product_id={2}&user_id={3}&pdk={4}&pdt={5}",
                conf.baseURL,
                conf.apikey,
                conf.productkey,
                user_id,
                key,
                groupName
            );
            Subject<string> sj = new Subject<string>();
            Client cli = new Client(uri, sj);
            sj.Subscribe(_jsn => {
                Debug.Log(_jsn);
                APIGetData info = JsonUtility.FromJson<APIGetData>(_jsn);
                if ( info.error != "" ) {
                    if (!throwHandle) Debug.LogError(String.Format("[Yokoduna Error] Get Data: {0}",info.error));
                    else Debug.LogWarning(String.Format("[Yokoduna Error] Get Data: {0}",info.error));
                    unit.OnNext(null);
                    unit.OnCompleted();
                    return;
                }
                unit.OnNext(info.datas);
            });
        }
    }
}