## VSMS PHP DEMO

> 此demo仅包含由V-SMS代为生成验证码的方式，其他调用方式类似。
>
> 详情: https://v-sms.vaptcha.com/#%E6%8E%A5%E5%8F%A3%E5%BC%95%E5%85%A5

对应修改`sms.class.php`中一下变量的值

```php
// 你的用户名 
private static $username = '';
// 验证单元 vid
public static $vid = '';
// 短信smskey
private static $smskey = '';
```

运行Demo

```bash
php -S localhost:4396
```

浏览器打开http://localhost:4396/