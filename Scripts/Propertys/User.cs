using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yokoduna {
	/// <summary>
	/// User Property Class
	/// "Impedance miss match" for resolution
	/// <typeparam name="name">ユーザー名</typeparam>
	/// <typeparam name="mail">登録されたメールアドレス</typeparam>
	/// <typeparam name="password">登録されたパスワード</typeparam>
	/// </summary>
	public class User {
		public string name {get; private set;}
		public string mail {get; private set;}
		public string password {get; private set;}
		public User (string name, string mail, string password) {
			this.name = name;
			this.mail = mail;
			this.password = password;
		}
	}

	/// <summary>
	/// Create User for API Cli Info
	/// </summary>
	public class APICreateUser {
		public string status {get; private set;}
		public string userID {get; private set;}
		public string timestamp {get; private set;}
		public string error {get; private set;}
		public APICreateUser (string status, string userID, string timestamp, string error) {
			this.status = status;
			this.userID = userID;
			this.timestamp = timestamp;
			this.error = error;
		}
		public bool isError {
			get {
				if (error != "") {
					return true;
				}
				return false;
			}
		}
	}

	/// <summary>
	/// Details for API Cli Info
	/// </summary>
	public class APIUserDetail {
		public string status {get; private set;}
		public string userID {get; private set;}
		public string userName {get; private set;}
		public string mailAddress {get; private set;}
		public string timestamp {get; private set;}
		public string error {get; private set;}
		public APIUserDetail (string status, string userID, string userName, string mailAddress, string timestamp, string error) {
			this.status = status;
			this.userID = userID;
			this.userName = userName;
			this.mailAddress = mailAddress;
			this.timestamp = timestamp;
			this.error = error;
		}
	}

	/// <summary>
	/// Login for API Cli Info
	/// </summary>
	public class APILoginUser {
		public string status {get; private set;}
		public string userID {get; private set;}
		public string timestamp {get; private set;}
		public string error {get; private set;}
		public APILoginUser (string status, string userID, string timestamp, string error) {
			this.status = status;
			this.userID = userID;
			this.timestamp = timestamp;
			this.error = error;
		}
	}
}
