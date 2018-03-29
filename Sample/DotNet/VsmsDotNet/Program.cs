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
            var url = "https://smsapi.vaptcha.com/sms/sendcode";
            
            var body = new SendSmsRequest()
            {
                vid = "your vaptcha cell id",
                code = "verify code",
                countrycode = "86",
                label = "your site name", //
                expiretime = "10",//minute
                phone = "13xxxxxxxxx",
                smskey = "your smskey",
                time = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                token = "your token",
                version = "1.0",
            }.ToJson();
            var content = new StringContent(body, Encoding.UTF8, "application/json");
            var response = await hc.PostAsync(url, content);
            var responseCode = await response.Content.ReadAsStringAsync();
            switch (responseCode)
            {
                case "200":
                    return true;
                case "201":
                    //do something when false
                    return false;
                // other
                default:
                    return false;
            }

        }

        /// <summary>
        /// 后端发起
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> SendSmsCodeAsync()
        {
            var hc = new HttpClient();
            var url = "https://smsapi.vaptcha.com/sms/verifycode";
            var body = new SendSmsCodeRequest()
            {
                vid = "your vaptcha cell id", 
                countrycode = "86",
                label = "your site name", 
                phone = "13xxxxxxxxx",
                smskey = "123456",
                time = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                token = "your token",
                version = "1.0" 
            }.ToJson();
            var content = new StringContent(body, Encoding.UTF8, "application/json");
            var response = await hc.PostAsync(url, content);
            var responseCode = await response.Content.ReadAsStringAsync();
            switch (responseCode)
            {
                case "200":
                    return true;
                case "201":
                    //do something when false
                    return false;
                // other
                default:
                    return false;
            }
        }

        /// <summary>
        /// 后端发起
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> VerifySmsCodeAsync()
        {
            var hc = new HttpClient();
            var url = "https://smsapi.vaptcha.com/sms/verifysms";
          var body = new SmsVerifyRequest()
            {
                vid = "your vid", 
                code = "recived code",
                smskey = "123456",
                countrycode = "86", 
                phone = "13xxxxxxxx", 
                time = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString(), 
                version = "1.0" 
            }.ToJson();
            var content = new StringContent(body, Encoding.UTF8, "application/json");
            var response = await hc.PostAsync(url, content);
            var responseCode = await response.Content.ReadAsStringAsync();
            switch (responseCode)
            {
                case "200":
                    return true;
                case "201":
                    //do something when false
                    return false;
                // other
                default:
                    return false;
            }
        }
    }
}
