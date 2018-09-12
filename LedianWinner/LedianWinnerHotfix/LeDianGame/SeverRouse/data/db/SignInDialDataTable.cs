using System;
using System.Collections.Generic;
namespace org.zgl.datable{
	public class SignInDialDataTable : DataTableMessage {
		public int Id(){
			return id;
		}
		public static SignInDialDataTable get(int id){
			return StaticConfigMessage.Instance.get<SignInDialDataTable>(typeof(SignInDialDataTable),id);
		}
		public int id;
		public int awardId;
		public int count;
		public int probability;
		public int awardType;
		public void AfterInit(){}
	}
}
