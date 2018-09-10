<?php
error_reporting(E_ALL);
include 'sms.class.php';
$msg = '';
$code = array(
    "200" => "成功",
    "201" => "发送失败",
    "202" => "用户验证失败/smskey不正确",
    "203" => "剩余短信条数不足",
    "204" => "发送频率过快，换号码或稍后再试",
    "205" => "token验证失败",
    "206" => "手机号码格式错误",
    "207" => "验证码格式错误",
    "208" => "验证次数上限",
    "209" => "验证码过期",
    "210" => "验证码不匹配",
    "211" => "模板数据错误",
    "212" => "模板编号错误",
    "230" => "系统维护"
);
if ($_SERVER['REQUEST_METHOD'] == 'POST') {
    $data = array(
        "countrycode" => "86",
        "phone" => $_POST['phone'],
        "label" => $_POST['label'],
        "token" => $_POST['vaptcha_token']
    );
    $result =  VSMS::send($data);
    if ($result == 200) {
        $msg = "发送成功";
    } else {
        $msg = "error code: ".$result." ".$code[$result];
    }
}
?>
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>vsms php sample</title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://bootswatch.com/4/lux/bootstrap.min.css">
    <style>
        .vaptcha-container {
            width: 100%;
            height: 36px;
            line-height: 36px;
            text-align: center;
        }
        .vaptcha-init-main {
            display: table;
            width: 100%;
            height: 100%;
            background-color: #EEEEEE;
        }
        ​
        .vaptcha-init-loading {
            display: table-cell;
            vertical-align: middle;
            text-align: center
        }
        ​
        .vaptcha-init-loading>a {
            display: inline-block;
            width: 18px;
            height: 18px;
            border: none;
        }
        ​
        .vaptcha-init-loading>a img {
            vertical-align: middle
        }
        ​
        .vaptcha-init-loading .vaptcha-text {
            font-family: sans-serif;
            font-size: 12px;
            color: #CCCCCC;
            vertical-align: middle
        }
    </style>
</head>
<body>
    
    <form class="row justify-content-md-center" action="" method="post">
        <div class="col-lg-4 card mt-5">
            <div class="card-body">
                <?php if($msg) { ?>
                    <div class="alert alert-dismissible alert-success"><?php echo $msg; ?></div>
                <?php } ?>
                <div class="form-group">
                    <input type="text" class="form-control" name="phone" placeholder="手机号">
                </div>
                <div class="form-group">
                    <input type="text" class="form-control" name="label" placeholder="签名(eg: 网站名称)">
                </div>
                <div class="form-group">
                    <div data-vid="<?php echo VSMS::$vid ?>" class="vaptcha-container">
                        <div class="vaptcha-init-main">
                            <div class="vaptcha-init-loading">
                                <a href="/" target="_blank">
                                    <img src="https://cdn.vaptcha.com/vaptcha-loading.gif" />
                                </a>
                                <span class="vaptcha-text">Vaptcha启动中...</span>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <button class="btn btn-sm btn-success">发送</button>
                </div>
            </div>
        </div>
    </form>
    <script src="https://cdn.vaptcha.com/v2.js"></script>
</body>
</html>

<!-- 200 成功
201 发送失败
202 用户验证失败/smskey不正确
203 剩余短信条数不足
204 发送频率过快，换号码或稍后再试
205 token验证失败
206 手机号码格式错误
207 验证码格式错误
208 验证次数上限
209 验证码过期
210 验证码不匹配
211 模板数据错误
212 模板编号错误
230 系统维护 -->
