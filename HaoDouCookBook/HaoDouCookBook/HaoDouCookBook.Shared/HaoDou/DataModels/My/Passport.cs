using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HaoDouCookBook.HaoDou.DataModels.My
{
    /*
     * 	    'UserId'		=> '登录用户ID',
			'Name'			=> '用户账号',
			'Nick' 			=> '用户昵称',
			'Avatar' 		=> '用户头像',
			'Sign'			=> '身份验证码',
			'MsgCnt'		=> '消息计数',
			'NoticeCnt'		=> '消息计数[android需要]'
			'Vip'			=> '是否VIP用户，0：否， 1：是',
			'CheckIn'		=> '是否签到， false：未签到， true：已签到',
			'MessageCnt'	=> '私信计数，现在仅菜谱客户端会返回此数据',
			'Sina'			=> '是否绑定新浪微博',
			'QQWeibo'		=> '是否绑定腾讯微博',
     */
    [DataContract]
    public class PassportLoginResultData
    {
        [DataMember]
        public string UserId { get; set; }


        [DataMember]
        public string Name { get; set; }


        [DataMember]
        public string Nick { get; set; }


        [DataMember]
        public string Avatar { get; set; }


        [DataMember]
        public string Sign { get; set; }


        [DataMember]
        public int MsgCnt { get; set; }


        [DataMember]
        public int NoticCnt { get; set; }


        [DataMember]
        public int Vip { get; set; }


        [DataMember]
        public bool CheckIn { get; set; }


        [DataMember]
        public int MessageCnt { get; set; }


        [DataMember]
        public int Sina { get; set; }


        [DataMember]
        public int QQWeibo { get; set; }

        public PassportLoginResultData()
        {
            UserId = string.Empty;
            Name = string.Empty;
            Nick = string.Empty;
            Avatar = string.Empty;
            Sign = string.Empty;
            MsgCnt = 0;
            NoticCnt = 0;
            CheckIn = false;
            MessageCnt = 0;
            Sina = 0;
            QQWeibo = 0;
            
        }
    }

}
