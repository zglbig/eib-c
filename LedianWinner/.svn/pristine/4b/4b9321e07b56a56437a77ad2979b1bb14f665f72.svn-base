using System;
using System.Collections.Generic;
namespace org.zgl.datable{
	public class ActivityDataTable : DataTableMessage {
		public int Id(){
			return id;
		}
		public static ActivityDataTable get(int id){
			return StaticConfigMessage.Instance.get<ActivityDataTable>(typeof(ActivityDataTable),id);
		}
		public int id;
		public int selling;
		public List<ActivityModel> model;
		public void AfterInit(){}
	}
}
