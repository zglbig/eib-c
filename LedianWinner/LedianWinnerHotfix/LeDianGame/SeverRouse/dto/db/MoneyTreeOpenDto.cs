using System;
using System.Collections.Generic;
using ProtoBuf;
[ProtoContract]
public class MoneyTreeOpenDto : SerializeMessage {
	[ProtoMember(1)]
	public int lv;
	[ProtoMember(2)]
	public int timer;
	[ProtoMember(3)]
	public int goldNum;
	[ProtoMember(4)]
	public long holdGold;
}
