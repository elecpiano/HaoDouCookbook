
namespace HaoDouCookBook.Common
{
    public class Utilities
    {
        public static bool IsMatchNetworkFail(int errorCode)
        {
            if (errorCode == Constants.ERRORCODE_JSON_PARSE_FAILED // parse json data failed
                || errorCode == Constants.ERRORCODE_METAJSON_PARSE_FAILED //parse the original json data from haodou failed
                || errorCode == Constants.ERRORCODE_REMOTE_SERVER_UNAVAILABLE) // remote server unavaiable
            {
                return true;
            }

            return false;
        }
    }
}
