﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Yokoduna {
	///<summary>
	/// Data<T>(string, object)
	/// key value store (KVS) をモデルとしたDataプロパティです。
	/// <typeparam name="key">識別キーを任意の文字列に設定します。(英字Only)</typeparam>
	/// <typeparam name="instance">保存を対象とするクラスを引数にセットします。これらはクラスごと保存をするためです。</typeparam>
	///</summary>
	[System.Serializable] public class Data<T> {
		public string key {get; private set;}
		public string value {get; private set;}
	
		private Type type;
		private T instance;

		public Data(string key, T instance) {
			this.type = typeof(T);
			this.instance = instance;
			this.key = key;
			this.value = JsonUtility.ToJson(instance);
		}
	}

	/// <summary>
	/// APISetData
	/// </summary>
	[System.Serializable] public class APISetData {
		public string status{get; private set;}
		public string timestamp{get; private set;}
		public string error{get; private set;}
		public APISetData(string status, string timestamp, string error) {
			this.status = status;
			this.timestamp = timestamp;
			this.error = error;
		}
	}
}