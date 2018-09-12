using System;
using System.Collections.Generic;
using ProtoBuf;
[ProtoContract]
public class HallRedPacketDto : SerializeMessage {
	[ProtoMember(1)]
	public long id;
	[ProtoMember(2)]
	public int count;
	[ProtoMember(3)]
	public int residueGold;
	[ProtoMember(4)]
	public int allGold;
	[ProtoMember(5)]
	public string userName;
	[ProtoMember(6)]
	public string headImgUrl;
	[ProtoMember(7)]
	public string desc;
	[ProtoMember(8)]
	public bool isFinish;
	[ProtoMember(9)]
	public short redPacketType;
	[ProtoMember(10)]
	public long createTime;
	[ProtoMember(11)]
	public long lastEditTime;
}
