<?php

class VSMS
{
    private static $version = '1.0';
    // 你的用户名 
    private static $username = '37481';
    // 验证单元 vid
    public static $vid = '5b3b1eb0a485e5041018a416';
    // 短信smskey
    private static $smskey = '410480b2311644cd8eeb376d77cc0269';
    private static $url = 'https://smsapi.vaptcha.com/sms/verifycode';

    private static function createPost($url, $data)
    {
        $data = http_build_query($data);
        if (function_exists('curl_exec')) {
            $ch = curl_init();
            curl_setopt($ch, CURLOPT_URL, $url);  
            curl_setopt($ch, CURLOPT_POST, 1);
            curl_setopt($ch, CURLOPT_POSTFIELDS, $data);
            curl_setopt($ch, CURLOPT_HEADER, false);  
            curl_setopt($ch, CURLOPT_HTTPHEADER, array('ContentType:application/json'));  
            curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);  
            curl_setopt($ch, CURLOPT_BINARYTRANSFER, true);  
            curl_setopt($ch, CURLOPT_CONNECTTIMEOUT, 5*1000);  
            $errno = curl_errno($ch);
            $response = curl_exec($ch);
            curl_close($ch);
            return $errno > 0 ? 'error' : $response;
        } else {
            $opts = array(
                'http' => array(
                    'method' => 'POST',
                    'header'=> "Content-type: application/json\r\n" . "Content-Length: " . strlen($data) . "\r\n",
                    'content' => $data,
                    'timeout' => 5*1000
                ),
                'content' => $data
            );
            $context = stream_context_create($opts);
            $response = @file_get_contents($url, false, $context);
            return $response ? $response : 'error';
        }
    }

    /**
     * @method send
     * @param array $data
     */
    public static function send($data) {
        $data = array_merge($data, array(
            "vid" => self::$vid,
            "smskey" => self::$smskey,
            "version" => self::$version,
            "time" => time() * 1000
        ));
        return self::createPost(self::$url, $data);
    }
}
