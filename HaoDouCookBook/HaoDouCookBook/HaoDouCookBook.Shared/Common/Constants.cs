using System.Collections.Generic;

namespace HaoDouCookBook.Common
{
    public class Constants
    {
        public const string DEFAULT_USER_PHOTO = "../Assets/Images/noavatar_300.jpg";
        public const string DEFAULT_IMAGE_BIG = "../Assets/Images/DefaultImageBig.png";
        public const string DEFAULT_IMAGE_SMALL = "../Assets/Images/DefaultImageSmall.png";

        public const string LOCAL_USERDATA_FOLDER = "LocalUserData";
        public const string PUBLISH_RECIPES_TEMP_FOLDER = "RepublishRecipesTemp";

        public const string BOUGHT_STUFFCATEGORY_TITLE = "已购买食材";
        public const int BOUGHT_STUFFCATEGORY_ID = int.MaxValue;

        public const string HAODOU_WEBSITE = "www.haodou.com";
        public const string HAODOU_SERVICE_PHONE_NUMBER_STRING = "4006-700-760";
        public const string HAODOU_QQ = "80007728";
        public const string HAODOU_SINA_WEIBO = "weibo.cn/haodoucom";
        public const string HAODOU_QQ_WEIBO = "t.qq.com/haodoucom";
        public const string HAODOU_INTRODUCTION = "好豆菜谱是好豆网开发的一款菜谱分享与交流的社交型应用。好斗最先倡导互联网菜谱正版化、标准化、社交化、实用化，现已成为互联网菜谱的行业风向标，是全球最受欢迎、最为活跃、成长性最好的中文菜谱应用。";
        public const string HAODOU_FIND_LOSTPASSWORD = "http://login.haodou.com/find.php?do=lostPassword";
        public const string DEFAULT_USER_INTRO = "这个豆子很懒，\n还没有给自己加个人价绍~";
        public const string NOONE_FOLLOW_OTHERUSER = "没有人关注ta,\n 好可怜~~你去关注下呗";
        public const string ONONE_FOLLOW_ME = "没有人关注你\n要加油咯~";
        public const string I_FOLLOW_NOONE = "没有关注的做菜高手~\n你怎么成长嘞？";
        public const string OTHERUSER_FOLLOW_NOONE = "TA还没来得及关注别人哦";
        public const string I_DONT_HAVE_PRODUCTS = "学习很久了吧\n也来秀秀你的作品呗~~";
        public const string OTHERUSER_DONT_HAVE_PRODUCTS = "TA还没有作品嘞~\n去别人家蹭饭吧！";
        public const string I_DONT_HAVE_RECIPES = "你的碗里没有菜啦~\n快去厨房炒菜吧！";
        public const string OTHERUSER_DONT_HAVE_RECIPES = "TA的碗里没有菜哟~\n去吃别人的吧！";
        public const string I_DONT_HAVE_ACTIVITIES = "来了这么久\n怎么还没有动静嘞？";
        public const string OTHERUSER_DONT_HAVE_ACTIVITIES = "TA还没有最新动态哦\n去别人家里逛逛吧~";
        public const string I_DONT_HAVE_DRAFT = "暂时没有发布的草稿哦~";
        public const string I_DONT_HAVE_FRIENDS = "找啊找啊找朋友\n快去找个好朋友~";
        public const string NO_FAVORITE_ALUMBS = "去收藏专辑吧~\n寻找属于你的对味美食";
        public const string NO_FAVORITE_TOPICS = "去收藏话题吧~\n一秒钟变成生活小达人";
        public const string NO_FAVORITE_RECIPES = "去收藏菜谱吧~\n出得了厅堂，下得了厨房";
        public const string NO_LIKE_RECIPES = "赞美他们快乐自己\n为你喜欢的菜谱送红心~";

        public const string MODIFY_NAME_DESCRIPTION = "可以免费修改一次，以后每次修改将会扣除100豆币";
        public const string MOIDFIED_DESCRIPTION = "非首次修改昵称，此次修改会\n扣除100豆币！是否继续？";

        public const int ERRORCODE_JSON_PARSE_FAILED = int.MaxValue;
        public const int ERRORCODE_METAJSON_PARSE_FAILED = int.MinValue;
        public const int ERRORCODE_REMOTE_SERVER_UNAVAILABLE = -2146233079;
        public const string ERROR_MESSAGE_NETWORK_UNSTABLE = "网络不稳定，请稍后再试";

        public readonly static Dictionary<int, string> ACTIVITY_TYPE_MAP = new Dictionary<int, string>() 
        {
            {10, "菜谱"},
            {30, "作品"}
        };
    }
}
