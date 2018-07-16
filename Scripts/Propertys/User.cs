using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WBCProject {
	/// <summary>
	/// User Property Class
	/// "Impedance miss match" for resolution
	/// <typeparam name="name">ユーザー名</typeparam>
	/// <typeparam name="mail">登録されたメールアドレス</typeparam>
	/// <typeparam name="password">登録されたパスワード</typeparam>
	/// </summary>
	public class User {
		public string name ;
		public string mail ;
		public string password ;
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
		public string status;
		public string userID;
		public string timestamp;
		public string error;
	}

	/// <summary>
	/// Details for API Cli Info
	/// </summary>
	public class APIUserDetail {
		public string status;
		public string userID;
		public string userName;
		public string mailAddress;
		public string timestamp;
		public string error;
	}

	/// <summary>
	/// Login for API Cli Info
	/// </summary>
	public class APILoginUser {
		public string status;
		public string userID;
		public string timestamp;
		public string error;
	}
}
