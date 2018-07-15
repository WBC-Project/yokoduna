using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Yokoduna {
    /// <summary>
    /// REST API Client System
    /// </summary>
    public class Client {
        public Client (string url, Subject<string> subject) {
            var getter = ObservableWWW.Get(url).Subscribe(response => {
                subject.OnNext(response);
            });
        }
    }
}