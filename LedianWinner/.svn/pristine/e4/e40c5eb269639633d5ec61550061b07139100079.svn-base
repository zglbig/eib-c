using System;
using System.Collections.Generic;
namespace org.zgl.error{
	public class AppErrorDataTable : DataTableMessage {
		public int Id(){
			return id;
		}
		public static AppErrorDataTable get(int id){
			return StaticConfigMessage.Instance.get<AppErrorDataTable>(typeof(AppErrorDataTable),id);
		}
		public int id;
		public string name;
		public string value;
		public void AfterInit(){}
	}
}
