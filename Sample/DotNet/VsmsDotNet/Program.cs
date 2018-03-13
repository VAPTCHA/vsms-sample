using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Vsms
{
    class Program
    {
        static void Main(string[] args)
        {
        }
        /// <summary>
        /// 后端发起，需要生成和保存验证码
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> SendSmsAsync()
        {
            var hc = new HttpClient();
            var url = "https://smsapi.vaptcha.com/sms/send";
            var secretKey = "123456789abcdef12345678abcdef12"; //SecretKey
            var vid = "123456789abcdef123456789"; //验证单元Id
            var token = "432156789abcdef123456789";//可以为空
            var label = "注册";
            var code = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);  //code保存到自己的服务器做验证
            var expiretime = 10; //时间自定义，单位为分钟
            //短信格式：
            //var smstext = $"你的{label}验证码：{code},{expiretime}分钟内有效。";
            //你的注册验证码：123456,10分钟内有效。
            var countrycode = "86";
            var phone = "1888888888";
            var time = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            var version = "1.0";
            var query =
                $"vid={vid}&token={token}&label={label}&code={code}&expiretime={expiretime}&countrycode={countrycode}&phone={phone}&time={time}&version={version}";
            //query = "vid=123456789abcdef123456789&token=432156789abcdef123456789&label=注册&code=ee4313&expiretime=10&countrycode=86&phone=1888888888&time=1520927710279&version=1.0"
            var sign = Hmacsha1(secretKey, query).Result;

            var body = $"vid={vid}&token={token}&label={label}&code={code}&expiretime={expiretime}&countrycode={countrycode}&phone={phone}&time={time}&version={version}&signature={sign}";
            //body = "vid=123456789abcdef123456789&token=432156789abcdef123456789&label=注册&code=ee4313&expiretime=10&countrycode=86&phone=1888888888&time=1520927710279&version=1.0&signature=Y3IizKORL2jlitge1JhVsgUe4";
            Debug.WriteLine(body);
            var content = new StringContent(body, Encoding.UTF8, "application/x-www-form-urlencoded");
            var response = await hc.PostAsync(url, content);
            var responseCode = await response.Content.ReadAsStringAsync();
            switch (responseCode)
            {
                case "2001":
                    return true;
                case "2003":
                    //do something when false
                    return false;
                // other
                default:
                    return false;
            }

        }

        /// <summary>
        /// 可以由前端或后端发起
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> SendSmsCodeAsync()
        {
            var hc = new HttpClient();
            var url = "https://smsapi.vaptcha.com/sms/sendsmscode";
            var secretKey = "123456789abcdef12345678abcdef12";
            var vid = "123456789abcdef123456789";
            var token = "432156789abcdef123456789"; //可以为空
            var label = "登陆";
            var countrycode = "86";
            var phone = "18888888888";
            var time = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            var version = "1.0";
            var query =
                $"vid={vid}&token={token}&label={label}&countrycode={countrycode}&phone={phone}&time={time}&version={version}";
            var sign = await Hmacsha1(secretKey, query);

            var body =
                $"vid={vid}&token={token}&label={label}&countrycode={countrycode}&phone={phone}&time={time}&version={version}&signture={sign}";

            var content = new StringContent(body, Encoding.UTF8, "application/x-www-form-urlencoded");
            var response = await hc.PostAsync(url, content);
            var responseCode = await response.Content.ReadAsStringAsync();
            switch (responseCode)
            {
                case "2101":
                    return true;
                case "2103":
                    //do something when false
                    return false;
                // other
                default:
                    return false;
            }
        }

        /// <summary>
        /// 可以由前端或后端发起
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> VerifySmsCodeAsync()
        {
            var hc = new HttpClient();
            var url = "https://smsapi.vaptcha.com/sms/sendsmscode";
            var secretKey = "123456789abcdef12345678abcdef12";
            var vid = "123456789abcdef123456789";
            var code = "123456"; //用户收到的验证码
            var countrycode = "86";
            var phone = "18888888888";
            var time = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            var version = "1.0";
            var query = $"vid={vid}&code={code}&countrycode={countrycode}&phone={phone}&time={time}&version={version}";
            var sign = await Hmacsha1(secretKey, query);

            var body = $"vid={vid}&code={code}&countrycode={countrycode}&phone={phone}&time={time}&version={version}&signture={sign}";

            var content = new StringContent(body, Encoding.UTF8, "application/x-www-form-urlencoded");
            var response = await hc.PostAsync(url, content);
            var responseCode = await response.Content.ReadAsStringAsync();
            switch (responseCode)
            {
                case "2201":
                    return true;
                case "2203":
                    //do something when false
                    return false;
                // other
                default:
                    return false;
            }
        }

        public static Task<string> Hmacsha1(string key, string text)
        {
            using (HMACSHA1 hmac = new HMACSHA1(Encoding.GetEncoding("utf-8").GetBytes(key)))
            {
                byte[] hashValue = hmac.ComputeHash(Encoding.GetEncoding("utf-8").GetBytes(text));

                var result = Convert.ToBase64String(hashValue).Replace("/", string.Empty).Replace("+", string.Empty).Replace("=", string.Empty);
                return Task.FromResult(result);
            }
        }
    }
}
